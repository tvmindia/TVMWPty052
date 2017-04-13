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
    public class QuotationsController : Controller
    {
        #region Constructor_Injection
        IQuotationsBusiness _quatationsBusiness;
        ICommonBusiness _commonBusiness;
        IMasterBusiness _masterBusiness;
        public QuotationsController(IQuotationsBusiness quatationsBusiness, ICommonBusiness commonBusiness, IMasterBusiness masterBusiness)
        {
            _quatationsBusiness = quatationsBusiness;
            _commonBusiness = commonBusiness;
            _masterBusiness = masterBusiness;
        }
        #endregion Constructor_Injection
        // GET: Quatations
        public ActionResult Index()
        {
            QuotationsViewModel Quotationsat_obj = new QuotationsViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();

            List<QuotationStatusViewModel> orderstatusListVM = Mapper.Map<List<QuotationStatusMaster>, List<QuotationStatusViewModel>>(_masterBusiness.GetAllQuotationStatus());
            foreach (QuotationStatusViewModel ovm in orderstatusListVM)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = ovm.Description,
                    Value = ovm.Code.ToString(),
                    Selected = false
                });
            }
            Quotationsat_obj.QuotationstatusList = selectListItem;
            return View(Quotationsat_obj);
        }

        #region GetAllQuotations
        [HttpGet]
        public string GetAllQuotations()
        {
            try
            {
                List<QuotationsViewModel> productList = Mapper.Map<List<Quotations>, List<QuotationsViewModel>>(_quatationsBusiness.GetAllQuotations());

                return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }
        #endregion GetAllQuotations

        #region GetQuotations
        [HttpGet]
        public string GetQuotations(string id)
        {
            try
            {
               
                QuotationsViewModel quotation = Mapper.Map<Quotations, QuotationsViewModel>(_quatationsBusiness.GetQuotations(Int32.Parse(id)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = quotation });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            } 
        }
        #endregion GetQuotations

        #region GetQuotationsDetails
        [HttpGet]
        public string GetQuotationsDetails(string id)
        {
            try
            {

                List<QuotationsViewModel> quotationdetailslist = null;
                QuotationsViewModel quotation = Mapper.Map<Quotations, QuotationsViewModel>(_quatationsBusiness.GetQuotations(Int32.Parse(id)));
                quotationdetailslist = new List<QuotationsViewModel>();
                quotationdetailslist.Add(quotation);
                return JsonConvert.SerializeObject(new { Result = "OK", Records = quotationdetailslist });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion GetQuotations

        #region  UpdateQuotationStatus

        [HttpPost]
        public string UpdateQuotationStatus(QuotationsViewModel quotationObj)
        {
            if (ModelState.IsValid)
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;                
                try
                {
                    quotationObj.commonObj = new LogDetailsViewModel();
                    quotationObj.commonObj.UpdatedBy = _commonBusiness.GetUA().UserName;
                    quotationObj.commonObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_quatationsBusiness.UpdateQuotations(Mapper.Map<QuotationsViewModel, Quotations>(quotationObj)));
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

        #endregion UpdateQuotationStatus
    }
}