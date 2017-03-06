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
    public class AttributesController : Controller
    {
        #region Constructor_Injection
        IAttributesBusiness _attributeBusiness;
        ICommonBusiness _commonBusiness;
        public AttributesController(IAttributesBusiness attributeBusiness, ICommonBusiness commonBusiness)
        {
            _attributeBusiness = attributeBusiness;
            _commonBusiness = commonBusiness;
        } 
        #endregion Constructor_Injection

        // GET: Attributes
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        #endregion Index

        #region GetAllAttributes


        #endregion  GetAllAttributes

        #region InsertUpdateAttributes

        [HttpPost]
        public string InsertUpdateAttributes(AttributesViewModel attributesObj)
        {
            if (ModelState.IsValid)
            {                
                if (attributesObj.ID == 0)
                {
                    //Create Attribute
                    try
                    {

                        attributesObj.commonObj = new CommonViewModel();
                        attributesObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                        attributesObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime().ToString();
                        OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_attributeBusiness.InsertAttributes(Mapper.Map<AttributesViewModel, Attributes>(attributesObj)));
                        return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                    }
                }
                else
                {
                    //Update Attribute
                    try
                    {
                        attributesObj.commonObj = new CommonViewModel();
                        attributesObj.commonObj.CreatedBy = "Albert";
                        OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_attributeBusiness.UpdateAttributes(Mapper.Map<AttributesViewModel, Attributes>(attributesObj)));
                        return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                    }
                }               
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }

        #endregion InsertUpdateAttributes
    }
}