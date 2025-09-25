using Besho.BLL.Services.Interfaces;
using Besho.DAL.DTO.Requests;
using Besho.DAL.DTO.Responses;
using Besho.DAL.Models;
using Besho.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services.Classes
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<bool> AddToCartAsync(CartRequest request, string UserId)
        {
            var newItem = new Cart
            {
                ProductId = request.ProductId,
                UserId = UserId,
                Count = 1

            };
            return await _cartRepository.AddAsync(newItem)>0;

        }

        public async Task<CartSummaryResponse> CartSummaryResponseAsync(string UserId)
        {
           var cartItems=await _cartRepository.GetUserCartAsync(UserId);
            var response = new CartSummaryResponse()
            {
                Items = cartItems.Select(cartitem => new CartResponse
                {
                    ProductId = cartitem.ProductId,
                    ProductName = cartitem.Product.Name,
                    Price = cartitem.Product.Price,
                    Count = cartitem.Count
                }).ToList()
            };
            return response;    

        }

        public async Task<bool> ClearCartAsync(string UserId)
        {
           return await _cartRepository.ClearCartAsync(UserId);
        }
    }
}
