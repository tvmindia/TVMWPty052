using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.CustomAttributes;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    [CustomAuthenticationFilter]
    public class MailNotificationsController : Controller
    {
        private INotificationBusiness _notificationBusiness;
        private ICommonBusiness _commonBusiness;
        public MailNotificationsController(INotificationBusiness notificationBusiness, ICommonBusiness commonBusiness)
        {
            _notificationBusiness = notificationBusiness;
            _commonBusiness = commonBusiness;
        }
        
        // GET: MailNotifications
   
        //[AuthorizeRoles(RoleContants.SuperAdminRole)]
        public ActionResult Index()
        {

            //ViewBag.Role = "Admin";
            return View();
        }

        #region GetAllMailNotifications
        [HttpGet]
        public string GetAllMailNotifications(string fromdate = null, string todate = null)
        {
            try
            {
                fromdate = (fromdate != "" ? fromdate : null);
                todate = (todate != "" ? todate : null);
                //get the notification of email
                List<MailNotificationViewModel> mailnotificationList = Mapper.Map<List<Notification>, List<MailNotificationViewModel>>(_notificationBusiness.GetAllNotifications(fromdate, todate,false));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = mailnotificationList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion  GetAllNotifications

        #region MailNotificationPush
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> MailNotificationPush(MailNotificationViewModel notification)
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
                    string[] CustomerIDList = notification.CustomerIDList != null ? notification.CustomerIDList.Split(',') : null;
                    foreach (string cid in CustomerIDList)
                    {
                        notification.customer.ID = int.Parse(cid);
                        bool sendsuccess = await _notificationBusiness.NotificationEmailPush(Mapper.Map<MailNotificationViewModel, Notification>(notification));
                        if (sendsuccess)
                        {
                            //1 is meant for mail sent successfully
                            notification.Status = 1;
                            OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_notificationBusiness.InsertNotification(Mapper.Map<MailNotificationViewModel, Notification>(notification)));
                        }
                        else
                        {
                            //0 is meant for failure
                            notification.Status = 0;
                            OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_notificationBusiness.InsertNotification(Mapper.Map<MailNotificationViewModel, Notification>(notification)));
                        }
                    }
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                catch(Exception ex)
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
        #endregion MailNotificationPush
    }
}