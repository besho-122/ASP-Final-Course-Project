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
    public class ProductRepository :GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DecreaseQuantityAsync (List<(int productId, int quantity)> items)
        {
            var productIds =items.Select(i => i.productId).ToList();
            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync(); 
            foreach (var product in products)
            {
                var item = items.First(i => i.productId == product.Id);
                if (product.Quantity < item.quantity)
                {
                    throw new Exception($"Product {product.Name} is out of stock"); 
                }

                product.Quantity -= item.quantity;
                


        }
            await _context.SaveChangesAsync();  
        }





        }
}
