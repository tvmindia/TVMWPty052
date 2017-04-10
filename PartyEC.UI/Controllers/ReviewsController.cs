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
            
            //ViewBag.CurrentDate= _commonBusiness.GetCurrentDateTime().ToString("dd-MMM-yyyy");
            //ViewBag.LastMonthDate = _commonBusiness.GetCurrentDateTime().AddMonths(-1).ToString("dd-MMM-yyyy");

            return View();
        }

        [HttpGet]
        public string GetAllReviews(string Condition, string FromDate, string ToDate)
        {
            try
            {
                List<ProductReviewViewModel> productList = Mapper.Map<List<ProductReview>, List<ProductReviewViewModel>>(_reviewBusiness.GetAllReviews(Condition,FromDate,ToDate));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }

        [HttpGet]
        public string GetProductRatingByCustomer(string productid, string customerid,string AttributesetID)
        {
            try
            {
                if (!string.IsNullOrEmpty(productid))
                {
                    List<ProductReviewViewModel> productReviewList = Mapper.Map<List<ProductReview>, List<ProductReviewViewModel>>(_reviewBusiness.GetProductRatingByCustomer(int.Parse(productid), int.Parse(customerid), int.Parse(AttributesetID)));

                    return JsonConvert.SerializeObject(new { Result = "OK", Records = productReviewList });
                }
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "id is empty" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }

        #region GetReviewByID

        [HttpGet]
        public string GetReview(string ID)
        {
            try
            {
                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                ProductReviewViewModel review = Mapper.Map<ProductReview, ProductReviewViewModel>(_reviewBusiness.GetReview(Int32.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = review });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }


        #endregion GetReviewByID

   

        #region UpdateReview

        [HttpPost]
        public string UpdateReview(ProductReviewViewModel reviewObj)
        {
            if (ModelState.IsValid)
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;               
                    try
                    {
                        reviewObj.commonObj = new LogDetailsViewModel();
                        reviewObj.commonObj.UpdatedBy = _commonBusiness.GetUA().UserName;
                        reviewObj.commonObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                        OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_reviewBusiness.UpdateReview(Mapper.Map<ProductReviewViewModel, ProductReview>(reviewObj)));
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                    } 
                if (OperationsStatusViewModelObj.StatusCode == 1)
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "Error", Record = OperationsStatusViewModelObj });
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }

        #endregion UpdateReview


        #region ChangeButtonStyle
        [HttpGet]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "Edit":


                    ToolboxViewModelObj.approve.Visible = true;
                    ToolboxViewModelObj.approve.Event = "clickapprove()";
                    ToolboxViewModelObj.approve.Title = "Approve";

                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";

                    break;

                default:
                    return Content("Nochange");
            }
            return PartialView("_ToolboxView", ToolboxViewModelObj);
        }
        #endregion ChangeButtonStyle

    }
}