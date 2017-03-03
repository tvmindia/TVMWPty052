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
        public AttributesController(IAttributesBusiness attributeBusiness)
        {
            _attributeBusiness = attributeBusiness;
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

        #region InsertAttributes

        [HttpPost]
        public string InsertAttributes(AttributesViewModel attributesObj)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    attributesObj.commonObj = new CommonViewModel();
                    attributesObj.commonObj.CreatedBy = "Albert Thomson";
                    OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_attributeBusiness.InsertAttributes(Mapper.Map<AttributesViewModel,Attributes>(attributesObj)));
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }

        #endregion InsertAttributes

        #region UpdateAttributes


        #endregion UpdateAttributes
    }
}