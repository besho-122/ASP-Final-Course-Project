using Besho.DAL.Data;
using Besho.DAL.Models;
using Besho.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.DAL.Repositories.Classes
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddAsync(Order order)
        {
             await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;

        }

        public async Task<Order> GetUserByOrderAsync(int orderid)
        {
            return await _context.Orders.Include(o=>o.User).FirstOrDefaultAsync(o => o.Id == orderid);    
        }

        public async Task<List<Order>> GetByStatusAsync(OrderStatus status)
        {
            return await _context.Orders.Where(o => o.Status == status).OrderByDescending(o=>o.OrderDate).ToListAsync();

        }
        
        public async Task<List<Order>> GetOredrByUserAsync(string userId)
        {
            return await _context.Orders.Include(o=>o.User).Where(o => o.UserId == userId).OrderByDescending(o => o.OrderDate).ToListAsync();
        }
         
       
        public async Task<bool>ChangeStatusAsync (int orderId , OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order is null) return false;
            order.Status = status;
            _context.Orders.Update(order);
            var result =await _context.SaveChangesAsync();
            return result >0;
        }   

        
        public async Task<bool> UserHasApproveOrderForProductAsync(string userId, int productId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .AnyAsync(o => o.UserId == userId && o.Status == OrderStatus.Approved && o.OrderItems.Any(oi => oi.ProductId == productId));
        }   

    }
}
