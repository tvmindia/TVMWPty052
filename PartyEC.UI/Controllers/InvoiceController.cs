using PartyEC.BusinessServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    public class InvoiceController : Controller
    {
        #region Constructor_Injection

        IInvoiceBusiness _invoiceBusiness;
        ICommonBusiness _commonBusiness;
        IMasterBusiness _masterBusiness;

        public InvoiceController(IInvoiceBusiness invoiceBusiness, ICommonBusiness commonBusiness, IMasterBusiness masterBusiness)
        {
            _commonBusiness = commonBusiness;
            _masterBusiness = masterBusiness;
            _invoiceBusiness = invoiceBusiness;
        }
        #endregion Constructor_Injection
        // GET: Invoices
        public ActionResult Index()
        {
            return View("../UnderConstruction/UnderConstruction");
        }
    }
}