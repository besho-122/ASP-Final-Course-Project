using Besho.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.DAL.Repositories.Interfaces
{
   public  interface IReviewRepository
    {
        Task<bool> HasUserReviewProduct(string userId, int productId);
        Task AddReviewAsync(Review request, string userId);
    }
}
