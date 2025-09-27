using Besho.BLL.Services.Interfaces;
using Besho.DAL.DTO.Requests;
using Besho.DAL.Models;
using Besho.DAL.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services.Classes
{
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _review;

        public ReviewService(IOrderRepository orderRepository,IReviewRepository review)
        {
            _orderRepository = orderRepository;
            _review = review;
        }

      
        public  async Task<bool> AddReviewAsync(ReviewRequset reviewRequset, string userId)
        {
          var hasOrder=await _orderRepository.UserHasApproveOrderForProductAsync(userId, reviewRequset.ProductId);
            if (!hasOrder)
            {
                return false;
            }
            var alreadyReviews = await _review.HasUserReviewProduct(userId, reviewRequset.ProductId);
            if (alreadyReviews)
            {
                return false;
            }
            var review = reviewRequset.Adapt<Review>();
            await _review.AddReviewAsync(review,userId);

            return true;
        }
    }
}
