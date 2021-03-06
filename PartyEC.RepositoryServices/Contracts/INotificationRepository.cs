﻿using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.RepositoryServices.Contracts
{
  public interface INotificationRepository
  {
        List<Notification> GetAllNotifications();
        Notification GetNotification(int ID);
        OperationsStatus NotificationPush(Notification notification);
    }
}
