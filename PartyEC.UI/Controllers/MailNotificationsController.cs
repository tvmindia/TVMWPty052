using PartyEC.BusinessServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
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
        public ActionResult Index()
        {
            return View();
        }
    }
}