using Azure.Core;
using Besho.BLL.Services.Interfaces;
using Besho.DAL.Dats.Migrations;
using Besho.DAL.DTO.Requests;
using Besho.DAL.DTO.Responses;
using Besho.DAL.Models;
using Besho.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe.BillingPortal;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.CommonModels;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;
using SessionService = Stripe.Checkout.SessionService;

namespace Besho.BLL.Services.Classes
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;
        private readonly IOrderItemRepository _orderItem;
        private readonly IProductRepository _productRepository;

        public CheckOutService(ICartRepository cartRepository,IOrderRepository orderRepository,IEmailSender emailSender,IOrderItemRepository orderItem,IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _emailSender = emailSender;
            _orderItem = orderItem;
            _productRepository = productRepository;
        }

        public  async Task<bool> HandlePaymentSuccessAsync(int orderId)
        {
            var order = await _orderRepository.GetUserByOrderAsync(orderId);
            var subject = "";
            var body = "";  

            if (order.PaymentMethod == PaymentMethodEnum.Visa) {
                 order.Status = OrderStatus.Approved;
                var orderItems=new List<OrderItem>();
                var carts = await _cartRepository.GetUserCartAsync(order.UserId);
                var productsUpdated =new List<(int productId ,int quantity)>();
                foreach (var cart in carts)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = cart.ProductId,
                        totalPrice = cart.Product.Price * cart.Count,
                        Count = cart.Count,
                        Price = cart.Product.Price
                    };
                    orderItems.Add(orderItem);
                    productsUpdated.Add((cart.ProductId, cart.Count));
                }
               
                await _orderItem.AddRangeAsync(orderItems);
                await _cartRepository.ClearCartAsync(order.UserId);
                await _productRepository.DecreaseQuantityAsync(productsUpdated);

                subject = "Payment Successful - besho store ";
                 body = $"<h1>thank you for your payment</h1> <p>this is your order id {orderId}</p>" +
                    $"<p>total amount : {order.TotalAmount}" +
                    $"<p>your order status :{order.Status}</p>" +
                    $"<p>orderd at : {order.OrderDate}</p>";
            }
            else if (order.PaymentMethod == PaymentMethodEnum.Cash)
            {
                subject = "order placed successfully";
                body = $"<h1>thank you for your order</h1> <p>this is your order id {orderId}</p>" +
                  $"<p>total amount : {order.TotalAmount}";


            }

            await _emailSender.SendEmailAsync(order.User.Email, subject, body);
            return true;
        }
          

        public async Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, string UserId, HttpRequest httpRequest)
        {
            var cartItems =await _cartRepository.GetUserCartAsync(UserId);
            if (!cartItems.Any())
            {
                return new CheckOutResponse
                {
                    Success = false,
                    Message = "Cart is empty"
                };
            }

            Order order = new Order
            {
                UserId = UserId,
                PaymentMethod = request.PaymentMethod,
                TotalAmount=cartItems.Sum(c1=>c1.Product.Price * c1.Count)

            };
            await _orderRepository.AddAsync(order);  

            if (request.PaymentMethod == PaymentMethodEnum.Cash)
            {
                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Cash"
                };
            }
            if (request.PaymentMethod == PaymentMethodEnum.Visa)
            {


                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/Customer/CheckOuts/Success/{order.Id}",
                    CancelUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/Customer/CheckOuts/Cancel",
                };
                foreach (var item in cartItems)
                {
                    
                    var unitAmount = (long)(item.Product.Price * 100);
                    if (unitAmount < 50) unitAmount = 50;

                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                                Description = item.Product.Description,
                            },
                            UnitAmount = unitAmount,
                        },
                        Quantity = item.Count,
                    });



                }
                var service = new SessionService();
                var session = await service.CreateAsync(options);

                order.PaymentId=session.Id;
                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Payment session created successfully",
                    PaymentId = session.Id,
                    Url = session.Url,
                };
            }
            return new CheckOutResponse
            {
                Success = false,
                Message = "Invalid method"
            };
        }
    }
}
