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
    public class NotificationsController : Controller
    {
        private INotificationBusiness _notificationBusiness;
        public NotificationsController(INotificationBusiness notificationBusiness)
        {
            _notificationBusiness = notificationBusiness;
        }
        // GET: Notifications
        public ActionResult Index()
        {
            return View();
        }

        #region GetAllNotifications
        [HttpGet]
        public string GetAllNotifications()
        {
            try
            {
                List<NotifiationViewModel> notifiationList = Mapper.Map<List<Notification>, List<NotifiationViewModel>>(_notificationBusiness.GetAllNotifications());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = notifiationList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion  GetAllNotifications
    }
}