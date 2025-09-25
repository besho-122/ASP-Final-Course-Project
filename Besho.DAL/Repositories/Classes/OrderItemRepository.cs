using Besho.DAL.Data;
using Besho.DAL.Models;
using Besho.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.DAL.Repositories.Classes
{
    public class OrderItemRepository:IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(List<OrderItem> item)
        {
          await   _context.OrderItems.AddRangeAsync(item);
            await _context.SaveChangesAsync();
            
        }

       
    }
}
