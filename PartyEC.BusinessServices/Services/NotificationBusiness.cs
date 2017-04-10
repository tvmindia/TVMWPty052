using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Services
{
    public class NotificationBusiness: INotificationBusiness
    {
        private  INotificationRepository  _notificationRepository;
        public NotificationBusiness(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public List<Notification> GetAllNotifications()
        {
            List<Notification> notificationList = null;
            try
            {
                notificationList = new List<Notification>();
                foreach(Notification notif in _notificationRepository.GetAllNotifications())
                {
                    //takes only first few characters for the message
                    notif.Message = notif.Message!=null?new string(notif.Message.Take(50).ToArray()):null;
                    notificationList.Add(notif);
                   
                }

            }
            catch (Exception ex)
            {
            }
            return notificationList;
        }

    }
}