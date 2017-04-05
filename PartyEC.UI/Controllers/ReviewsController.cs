using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    public class ReviewsController : Controller
    {
        #region Constructor_Injection 

        IReviewBusiness _reviewBusiness;
        ICommonBusiness _commonBusiness;

        public ReviewsController(IReviewBusiness reviewBusiness, ICommonBusiness commonBusiness)
        {
            _commonBusiness = commonBusiness;
            _reviewBusiness = reviewBusiness;
        }
        #endregion Constructor_Injection 

        // GET: Reviews
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetAllReviews(ProductReviewViewModel productObj)
        {
            try
            {
                List<ProductReviewViewModel> productList = Mapper.Map<List<ProductReview>, List<ProductReviewViewModel>>(_reviewBusiness.GetAllReviews());

                return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }


    }
}