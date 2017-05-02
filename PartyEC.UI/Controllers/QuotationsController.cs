using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.CustomAttributes;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    [CustomAuthenticationFilter]
    public class QuotationsController : Controller
    {
        #region Constructor_Injection
        IQuotationsBusiness _quotationsBusiness;
        ICommonBusiness _commonBusiness;
        IMasterBusiness _masterBusiness;
        IMailBusiness _mailBusiness;
        public QuotationsController(IQuotationsBusiness quotationsBusiness, ICommonBusiness commonBusiness, IMasterBusiness masterBusiness, IMailBusiness mailBusiness)
        {
            _quotationsBusiness = quotationsBusiness;
            _commonBusiness = commonBusiness;
            _masterBusiness = masterBusiness;
            _mailBusiness = mailBusiness;
        }
        #endregion Constructor_Injection
        // GET: Quatations
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
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
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetAllQuotations()
        {
            try
            {
                List<QuotationsViewModel> productList = Mapper.Map<List<Quotations>, List<QuotationsViewModel>>(_quotationsBusiness.GetAllQuotations());

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
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetQuotations(string id)
        {
            try
            {
               
                QuotationsViewModel quotation = Mapper.Map<Quotations, QuotationsViewModel>(_quotationsBusiness.GetQuotations(Int32.Parse(id)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = quotation });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            } 
        }
        #endregion GetQuotations

        #region GetQuotationsDetails
        /// <summary>
        /// to display in table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetQuotationsDetails(string id)
        {
            try
            {

                List<QuotationsViewModel> quotationdetailslist = null;
                QuotationsViewModel quotation = Mapper.Map<Quotations, QuotationsViewModel>(_quotationsBusiness.GetQuotations(Int32.Parse(id)));
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

        #region  UpdateQuotations

        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string UpdateQuotations(QuotationsViewModel quotationObj)
        {
            if (ModelState.IsValid)
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;                
                try
                {
                    quotationObj.logDetails = new LogDetailsViewModel();
                    quotationObj.logDetails.UpdatedBy = _commonBusiness.GetUA().UserName;
                    quotationObj.logDetails.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_quotationsBusiness.UpdateQuotations(Mapper.Map<QuotationsViewModel, Quotations>(quotationObj)));
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

        #endregion UpdateQuotations

        #region InsertEventsLog
        /// <summary>
        /// Insert into event logs after notyfing the customer by mail if checked true
        /// </summary>
        /// <param name="quotationObj"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string InsertEventsLog(QuotationsViewModel quotationObj)
        {
            if (ModelState.IsValid)
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;
                bool Mailstatus = false;
                try
                {
                    quotationObj.EventsLogViewObj.commonObj = new LogDetailsViewModel();
                    quotationObj.EventsLogViewObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                    quotationObj.EventsLogViewObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                    if (quotationObj.mailViewModelObj.CustomerEmail != "" && quotationObj.EventsLogViewObj.CustomerNotifiedYN == true)
                    {
                        quotationObj.mailViewModelObj.OrderNo = quotationObj.EventsLogViewObj.ParentID;
                        quotationObj.mailViewModelObj.OrderComment = quotationObj.EventsLogViewObj.Comment; 

                            Mail _mail = new Mail();
                            using (StreamReader reader = new StreamReader(HttpContext.Server.MapPath("~/PartyEcTemplates/Notifications.html")))
                            {
                                _mail.Body = reader.ReadToEnd();
                            }
                        _mail.Body = _mail.Body.Replace("{CustomerName}", quotationObj.mailViewModelObj.CustomerName);
                        _mail.Body = _mail.Body.Replace("{Message}", quotationObj.EventsLogViewObj.Comment);
                        _mail.IsBodyHtml = true;
                        _mail.Subject = "Quotation No:"+ quotationObj.QuotationNo;
                        _mail.To = quotationObj.mailViewModelObj.CustomerEmail;
                        Mailstatus = _mailBusiness.SendMail(_mail);
                        quotationObj.EventsLogViewObj.CustomerNotifiedYN = Mailstatus;
                    }
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_masterBusiness.InsertEventsLog(Mapper.Map<EventsLogViewModel, EventsLog>(quotationObj.EventsLogViewObj)));
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


        #endregion InsertEventsLog


        #region SendQuotation

        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public async Task<string> SendQuotation(QuotationsViewModel quotationsObj)
        {
            if (ModelState.IsValid)
            {
                bool sendsuccess;


                try
                {
                    quotationsObj.logDetails = new LogDetailsViewModel();
                    quotationsObj.logDetails.CreatedBy = _commonBusiness.GetUA().UserName;
                    quotationsObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();

                      sendsuccess = await _quotationsBusiness.QuotationEmail(Mapper.Map<QuotationsViewModel, Quotations>(quotationsObj));
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }

                 if (sendsuccess)
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Message = "Quotation Send Sucessfully" });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "Error", Message = "Quotation Sending Failed" });
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }


        #endregion SendQuotation


        #region GetEventsLog
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetEventsLog(string ID)
        {
            try
            {
                List<EventsLogViewModel> eventsLogList = Mapper.Map<List<EventsLog>, List<EventsLogViewModel>>(_masterBusiness.GetEventsLog(int.Parse(ID), "Quotations"));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = eventsLogList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion GetEventsLog 

        #region ChangeButtonStyle
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "List":
                    ToolboxViewModelObj.backbtn.Visible = false;
                    ToolboxViewModelObj.sendbtn.Visible = false;
                    break;
                case "Edit_List":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    ToolboxViewModelObj.sendbtn.Visible = true;
                    ToolboxViewModelObj.sendbtn.Disable = true; 
                    break;
                case "Send":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    ToolboxViewModelObj.sendbtn.Visible = true;
                    ToolboxViewModelObj.sendbtn.Event = "SendMail()";
                    ToolboxViewModelObj.sendbtn.Title = "Send";
                    break;

                default:
                    return Content("Nochange");
            }
            return PartialView("_ToolboxView", ToolboxViewModelObj);
        }


        #endregion ChangeButtonStyle
    }
}