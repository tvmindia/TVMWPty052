using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace PartyEC.BusinessServices.Services
{
    public class NotificationBusiness: INotificationBusiness
    {
        private  INotificationRepository  _notificationRepository;
        private IMailBusiness _mailBusiness;
        public NotificationBusiness(INotificationRepository notificationRepository, IMailBusiness mailBusiness)
        {
            _notificationRepository = notificationRepository;
            _mailBusiness = mailBusiness;
        }

        public List<Notification> GetAllNotifications(string fromdate = null, string todate = null, bool IsMobile = true)
        {
            List<Notification> notificationList = null,filteredList=null;
            try
            {
                switch(IsMobile)
                {
                    case true:
                        notificationList = _notificationRepository.GetAllNotifications().Where(noti => noti.Type == "Mobile").ToList();
                        break;
                    case false:
                        notificationList = _notificationRepository.GetAllNotifications().Where(noti=>noti.Type == "Email").ToList();
                        break;
                }
                if ((!string.IsNullOrEmpty(fromdate)) && (!string.IsNullOrEmpty(todate)))
                {
                    var f = DateTime.Parse(fromdate);
                    var t = DateTime.Parse(todate);
                    notificationList.Where((noti => (noti.logDetailsObj.CreatedDate >= DateTime.Parse(f.Day + "-" + f.Month + "-" + f.Year)) && (noti.logDetailsObj.CreatedDate <= DateTime.Parse(t.Day + "-" + t.Month + "-" + t.Year))));
                }
                filteredList = new List<Notification>();
                foreach (Notification notif in notificationList)
                {
                    //takes only first few characters for the message
                    notif.Message = notif.Message!=null?new string(notif.Message.Take(50).ToArray()):null;
                    filteredList.Add(notif);
                }

            }
            catch (Exception ex)
            {
            }
            return filteredList;
        }


       

        public Notification GetNotification(int ID)
        {
           
                Notification notiObj = null;
                try
                {
                    notiObj = _notificationRepository.GetNotification(ID);

                }
                catch (Exception)
                {

                }
                return notiObj;
           
        }

        public OperationsStatus NotificationMobilePush(Notification notification)
        {
            OperationsStatus operationsStatus = null;
            try
            {
                //Call SendToFCM(string titleString, string descriptionString, Boolean isCommon, string CustomerID = "");
                //on success call NotificationPush
                operationsStatus = _notificationRepository.NotificationPush(notification);
            }
            catch(Exception ex)
            {

            }
            return operationsStatus;
        }

        #region Notification message to Cloud messaging system
        /// <summary>
        /// Function to communicate with Firebase Cloud Messaging system by Google, for sending app notifications
        /// </summary>
        /// <param name="titleString">Title of notification</param>
        /// <param name="descriptionString">Description of notification</param>
        /// <param name="isCommon">Specify whether the notification is common for all app users or a specific</param>
      
        public void SendToFCM(string titleString, string descriptionString, Boolean isCommon, string CustomerID = "")
        {
            //Validation
            if (!isCommon)//if not a message to all apps, CustomerID should be provided
            {
                if (CustomerID == "" || CustomerID == null)
                    throw new Exception("No CustomerID");
            }
            if (titleString == "" || titleString == null)
                throw new Exception("No title");
            if (descriptionString == "" || descriptionString == null)
                throw new Exception("No description");
            //Sending notification through Firebase Cluod Messaging
            try
            {
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                string to_String = "";
                if (isCommon)
                    to_String = "/topics/common";
            
                else
                    to_String = "/topics/" + CustomerID;
                var objNotification = new
                {
                    to = to_String,

                    data = new
                    {
                        title = titleString,
                        body = descriptionString,
                        sound = "default",
                       
                    }
                };
                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonNotificationFormat = js.Serialize(objNotification);
                Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);

                //Put here the Server key from Firebase
                string FCMServerKey = ConfigurationManager.AppSettings["FCMServerKey"].ToString();
                tRequest.Headers.Add(string.Format("Authorization: key={0}", FCMServerKey));
                //Put here the Sender ID from Firebase
                string FCMSenderID = ConfigurationManager.AppSettings["FCMSenderID"].ToString();
                tRequest.Headers.Add(string.Format("Sender: id={0}", FCMSenderID));

                tRequest.ContentLength = byteArray.Length;
                tRequest.ContentType = "application/json";
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String responseFromFirebaseServer = tReader.ReadToEnd();
                                tReader.Close();
                                dataStream.Close();
                                tResponse.Close();

                                if (!responseFromFirebaseServer.Contains("message_id"))//Doesn't contain message_id means some error occured
                                    throw new Exception(responseFromFirebaseServer);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }
        #endregion Notification message to Cloud messaging system
        #region mailnotification
        public OperationsStatus NotificationEmailPush(Notification notification)
        {
            OperationsStatus operationsStatus = null;
            try
            {
              operationsStatus = _notificationRepository.NotificationPush(notification);
              if(operationsStatus.StatusCode==1)
                {
                    Mail _mail = new Mail();
                   // _mail.
                   // _mailBusiness.Send();
                }

            }
            catch (Exception ex)
            {

            }
            return operationsStatus;
        }
        #endregion mailnotification
    }
}