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
    public class NotificationsController : Controller
    {
        private INotificationBusiness _notificationBusiness;
        private ICommonBusiness _commonBusiness;
        public NotificationsController(INotificationBusiness notificationBusiness, ICommonBusiness commonBusiness)
        {
            _notificationBusiness = notificationBusiness;
            _commonBusiness = commonBusiness;
        }
        // GET: Notifications
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult Index()
        {
            return View();
        }

        #region GetAllNotifications
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetAllNotifications(string fromdate=null,string todate=null)
        {
            try
            {
                fromdate = (fromdate != "" ? fromdate : null);
                todate = (todate != "" ? todate : null);
                List <NotifiationViewModel> notifiationList = Mapper.Map<List<Notification>, List<NotifiationViewModel>>(_notificationBusiness.GetAllNotifications(fromdate,todate,true));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = notifiationList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion  GetAllNotifications

        #region GetNotification
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetNotification(string ID)
        {
            try
            {
                NotifiationViewModel NotifObj = null;
                if (!string.IsNullOrEmpty(ID))
                {
                    NotifObj = Mapper.Map<Notification, NotifiationViewModel>(_notificationBusiness.GetNotification(Int32.Parse(ID)));
                }

                return JsonConvert.SerializeObject(new { Result = "OK", Record = NotifObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion GetNotification

        #region InserUpdateNotification
        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        [ValidateAntiForgeryToken]
        public string NotificationPush(NotifiationViewModel notification)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    OperationsStatusViewModel OperationsStatusViewModelObj = null;
                    //INSERT
                    notification.logDetailsObj = new LogDetailsViewModel();
                    //Getting UA
                    notification.logDetailsObj.CreatedBy = _commonBusiness.GetUA().UserName;
                    notification.logDetailsObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                    string[] CustomerIDList= notification.CustomerIDList!=null?notification.CustomerIDList.Split(','):null;
                    foreach(string cid in CustomerIDList)
                    {
                        notification.customer.ID = int.Parse(cid);
                        OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_notificationBusiness.NotificationMobilePush(Mapper.Map<NotifiationViewModel, Notification>(notification)));
                    }
                          
                 
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                 }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }
            }
            //Model state errror
            else
            {
                List<string> modelErrors = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        modelErrors.Add(modelError.ErrorMessage);
                    }
                }
                return JsonConvert.SerializeObject(new { Result = "VALIDATION", Message = string.Join(",", modelErrors) });
            }
        }
        #endregion InserUpdateNotification

        #region ChangeButtonStyle
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                   case "Push":
                    ToolboxViewModelObj.sendbtn.Visible = true;
                    ToolboxViewModelObj.sendbtn.Event = "PushNotification()";
                    ToolboxViewModelObj.sendbtn.Title = "Save";

                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    break;

                case "Add":
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Title = "Add";
                    ToolboxViewModelObj.addbtn.Event = "AddNotification()";
                    break;

                case "Edit":
                    ToolboxViewModelObj.sendbtn.Visible = true;
                    ToolboxViewModelObj.sendbtn.Disable = true;
                    ToolboxViewModelObj.sendbtn.Event = "PushNotification()";
                    ToolboxViewModelObj.sendbtn.Title = "Save";

                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    break;

                default:
                    return Content("Nochange");
            }
            return PartialView("_ToolboxView", ToolboxViewModelObj);
        }


        #endregion ChangeButtonStyle
    }
}