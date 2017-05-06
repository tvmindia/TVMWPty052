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
        IMasterBusiness _masterBusiness;

        public InvoiceController(IInvoiceBusiness invoiceBusiness, ICommonBusiness commonBusiness, IMasterBusiness masterBusiness)
        {
            _commonBusiness = commonBusiness;
           _invoiceBusiness = invoiceBusiness;
            _masterBusiness = masterBusiness;
        }
        #endregion Constructor_Injection
        // GET: Invoices
        public ActionResult Index()
        {
            OrderViewModel order = new OrderViewModel();
            List<SelectListItem> selectListPaymentStatus = new List<SelectListItem>();
            List<PaymentStatusViewModel> paymentstatusListVM = Mapper.Map<List<PaymentStatusMaster>, List<PaymentStatusViewModel>>(_masterBusiness.GetAllPaymentStatus());
            foreach (PaymentStatusViewModel pvm in paymentstatusListVM)
            {
                selectListPaymentStatus.Add(new SelectListItem
                {
                    Text = pvm.Description,
                    Value = pvm.Code.ToString(),
                    Selected = false
                });
            }
            order.PaymentStatusList = selectListPaymentStatus;
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


        #region ChangeButtonStyle
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "Detail":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    ToolboxViewModelObj.backbtn.Event= "goback()";
                    break;
                default:
                    return Content("Nochange");
            }
            return PartialView("_ToolboxView", ToolboxViewModelObj);
        }


        #endregion ChangeButtonStyle

    }
}