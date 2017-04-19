using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.BusinessServices.Contracts
{
    public  interface INotificationBusiness
    {
        List<Notification> GetAllNotifications(string fromdate=null,string todate=null,bool IsMobile=false);
        Notification GetNotification(int ID);
        OperationsStatus NotificationMobilePush(Notification notification);
        OperationsStatus InsertNotification(Notification notification);
        Task<bool> NotificationEmailPush(Notification notification);

    }
}
