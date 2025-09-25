using Besho.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.DAL.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order>? GetUserByOrderAsync(int orderid);
        Task <Order>AddAsync(Order order);  
    }
}
