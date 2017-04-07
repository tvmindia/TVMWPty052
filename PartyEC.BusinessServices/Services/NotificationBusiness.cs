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
                notificationList = _notificationRepository.GetAllNotifications();
            }
            catch (Exception ex)
            {
            }
            return notificationList;
        }

    }
}