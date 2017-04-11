using PartyEC.BusinessServices.Contracts;
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
        public QuotationsController(IQuotationsBusiness quatationsBusiness, ICommonBusiness commonBusiness)
        {
            _quatationsBusiness = quatationsBusiness;
            _commonBusiness = commonBusiness;
        }
        #endregion Constructor_Injection
        // GET: Quatations
        public ActionResult Index()
        {
            return View();
        }
    }
}