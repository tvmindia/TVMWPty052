using PartyEC.BusinessServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;

namespace PartyEC.BusinessServices.Services
{
    public class ReviewBusiness : IReviewBusiness
    {
        private IReviewRepository _reviewRepository;

        public ReviewBusiness(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }


        public List<ProductReview> GetAllReviews()
        {
            List<ProductReview> reviewlist = null;
            try
            {
                reviewlist = _reviewRepository.GetAllReviews();

            }
            catch (Exception)
            {

            }
            return reviewlist;
        }
    }
}