using Besho.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services.Interfaces
{
   public  interface IOrderService
    {
        Task<Order>? GetUserByOrderAsync(int orderid);
        Task<Order> AddAsync(Order order);
        Task<List<Order>> GetByStatusAsync(OrderStatus status);
        Task<List<Order>> GetOredrByUserAsync(string userId);
        Task<bool> ChangeStatusAsync(int orderId, OrderStatus status);
    }

}
