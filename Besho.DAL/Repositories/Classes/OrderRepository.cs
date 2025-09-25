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
    }
}
