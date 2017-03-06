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
        [HttpGet]
        public string GetAllAttributes(AttributesViewModel attributesObj)
        {
            try
            {
                List<AttributesViewModel> attributeList = Mapper.Map<List<Attributes>, List<AttributesViewModel>>(_attributeBusiness.GetAllAttributes(Mapper.Map<AttributesViewModel, Attributes>(attributesObj)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = attributeList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }            
        }

        #endregion  GetAllAttributes

        #region GetAttributesByID

        [HttpGet]
        public string GetAttributes(string id)
        {
            try
            {
                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                AttributesViewModel attribute = Mapper.Map<Attributes,AttributesViewModel>(_attributeBusiness.GetAttributes(Int32.Parse(id), Mapper.Map<OperationsStatusViewModel, OperationsStatus>(operationsStatus)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = attribute });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }


        #endregion GetAttributesByID

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