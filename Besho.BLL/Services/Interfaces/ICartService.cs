using Besho.DAL.DTO.Requests;
using Besho.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddToCartAsync(CartRequest request, string UserId);
        Task<CartSummaryResponse> CartSummaryResponseAsync(string UserId);
        Task<bool> ClearCartAsync(string UserId);  

    }
}
