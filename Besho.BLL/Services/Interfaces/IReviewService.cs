using Besho.DAL.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services.Interfaces
{
    public interface IReviewService
    {
        public Task<bool>AddReviewAsync (ReviewRequset reviewRequset, string userId);


    }
}
