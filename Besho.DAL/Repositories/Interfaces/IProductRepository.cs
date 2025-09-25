using Besho.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.DAL.Repositories.Interfaces
{
   public interface IProductRepository:IGenericRepository<Product>
    {
        Task DecreaseQuantityAsync(List<(int productId,int quantity)>items);
    }
}
