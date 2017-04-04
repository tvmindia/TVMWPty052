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
    public class CountriesController : Controller
    {
        #region Constructor_Injection 

        IMasterBusiness _masterBusiness;
        ICommonBusiness _commonBusiness;

        public CountriesController(IMasterBusiness masterBusiness, ICommonBusiness commonBusiness)
        {
            _commonBusiness = commonBusiness;
            _masterBusiness = masterBusiness;
        }
        #endregion Constructor_Injection 
        // GET: Countries
        public ActionResult Index()
        {
            return View();
        }

        #region GetAllCountries
        [HttpGet]
        public string GetAllCountries()
        {
            try
            {
                List<CountryViewModel> CountryList = Mapper.Map<List<Country>, List<CountryViewModel>>(_masterBusiness.GetAllCountries());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = CountryList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }

        #endregion GetAllManufacturers
    }
}