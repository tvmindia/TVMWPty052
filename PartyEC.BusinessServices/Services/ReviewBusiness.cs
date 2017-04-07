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


        public List<ProductReview> GetAllReviews(string Condition,string FromDate,string ToDate)
        {
            List<ProductReview> reviewlist = null;
            try
            {
                reviewlist = _reviewRepository.GetAllReviews(Condition,FromDate,ToDate);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reviewlist;
        }

        public List<ProductReview> GetProductRatingByCustomer(int ProductID, int CustomerID, int AttributesetId)
        {
            List<ProductReview> reviewlist = null;
            try
            {
                reviewlist = _reviewRepository.GetProductRatingByCustomer(ProductID, CustomerID, AttributesetId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reviewlist;
        }

        public ProductReview GetReview(int ReviewID)
        {
            ProductReview  ReviewObj = null;
            try
            {
                ReviewObj = _reviewRepository.GetReview(ReviewID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReviewObj;
        }
    }
}