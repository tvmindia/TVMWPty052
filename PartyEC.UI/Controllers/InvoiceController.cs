using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.CustomAttributes;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    [CustomAuthenticationFilter]
    public class InvoiceController : Controller
    {
        #region Constructor_Injection

        IInvoiceBusiness _invoiceBusiness;
        ICommonBusiness _commonBusiness;
        

        public InvoiceController(IInvoiceBusiness invoiceBusiness, ICommonBusiness commonBusiness)
        {
            _commonBusiness = commonBusiness;
           _invoiceBusiness = invoiceBusiness;
        }
        #endregion Constructor_Injection
        // GET: Invoices
        public ActionResult Index()
        {
            OrderViewModel order = new OrderViewModel();
            return View(order);
        }

        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetAllInvoices()
        {
            try
            {
                List<InvoiceViewModel> invoiceList = Mapper.Map<List<Invoice>, List<InvoiceViewModel>>(_invoiceBusiness.GetAllInvoices());

                return JsonConvert.SerializeObject(new { Result = "OK", Records = invoiceList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }

    }
}