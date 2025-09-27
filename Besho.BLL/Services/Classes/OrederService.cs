using Besho.BLL.Services.Interfaces;
using Besho.DAL.Models;
using Besho.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services.Classes
{
   public class OrederService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrederService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order> AddAsync(Order order)
        {
           return await _orderRepository.AddAsync(order);   
        }

        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatus status)
        {
            return await _orderRepository.ChangeStatusAsync(orderId, status);
        }

        public async Task<List<Order>> GetByStatusAsync(OrderStatus status)
        {
           return await _orderRepository.GetByStatusAsync(status);  
        }

        public async Task<List<Order>> GetOredrByUserAsync(string userId)
        {
            return await _orderRepository.GetOredrByUserAsync(userId);
        }

        public async Task<Order>? GetUserByOrderAsync(int orderid)
        {
           return await _orderRepository.GetUserByOrderAsync(orderid);  
        }
    }
}
