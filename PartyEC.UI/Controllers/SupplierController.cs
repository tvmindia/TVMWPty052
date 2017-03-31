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
    public class SupplierController : Controller
    {

        #region Constructor_Injection 

        IMasterBusiness _masterBusiness;
        ICommonBusiness _commonBusiness;

        public SupplierController(IMasterBusiness masterBusiness, ICommonBusiness commonBusiness)
        {
            _commonBusiness = commonBusiness;
            _masterBusiness = masterBusiness;
        }
        #endregion Constructor_Injection 
        // GET: Supplier
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public string GetAllSuppliers()
        {
            try
            {               
                List<SupplierViewModel> supplierList = Mapper.Map<List<Supplier>, List<SupplierViewModel>>(_masterBusiness.GetAllSuppliers());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = supplierList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }
    }
}