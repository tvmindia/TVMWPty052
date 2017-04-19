using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace PartyEC.BusinessServices.Services
{
    public class NotificationBusiness: INotificationBusiness
    {
        private INotificationRepository  _notificationRepository;
        private IMailBusiness _mailBusiness;
        private ICustomerBusiness _customerBusiness;
        public NotificationBusiness(INotificationRepository notificationRepository, IMailBusiness mailBusiness, ICustomerBusiness customerBusiness)
        {
            _notificationRepository = notificationRepository;
            _mailBusiness = mailBusiness;
            _customerBusiness = customerBusiness;
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
                if(operationsStatus.StatusCode==1)
                {
                    //call customer detail
                    //call notificatoin cloud system
                }
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
        public OperationsStatus InsertNotification(Notification notification)
        {
            OperationsStatus operationsStatus = null;
            try
            {
              operationsStatus = _notificationRepository.NotificationPush(notification);
            

            }
            catch (Exception ex)
            {

            }
            return operationsStatus;
        }
        #endregion mailnotification


        //public async Task<bool> albertMail(Notification notification)
        //{
        //    try
        //    {
        //        string EmailFromAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailFromAddress"];
        //        string smtpUserName = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-UserName"];
        //        string smtpPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-Password"];
        //        string myport = System.Web.Configuration.WebConfigurationManager.AppSettings["Port"];
        //        var from = new MailAddress(EmailFromAddress, "Admin_@_PartyEC");
        //        var to = new MailAddress("albertthomson2008@gmail.com");
        //        var useDefaultCredentials = true;
        //        var enableSsl = false;
        //      //  var replyto = "albertthomson2008@gmail.com"; // set here your email; 
        //        var userName = smtpUserName;
        //        var password = smtpPassword;
        //        var port = int.Parse(myport);
        //        var host = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-host"];
        //        userName = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-UserName"];  // setup here the username; 
        //        password = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-Password"]; // setup here the password; 
        //        bool.TryParse("true", out useDefaultCredentials); //setup here if it uses defaault credentials 
        //        bool.TryParse("true", out enableSsl); //setup here if it uses ssl 
        //       // int.TryParse(myport, out port); //setup here the port 
        //        host = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-host"]; //setup here the host 

        //        using (var mail = new MailMessage(from, to))
        //        {
        //            mail.Subject = "testing mail by albert";
        //            mail.Body = "<table><tr>Thrithavam from table</tr></table>";
        //            mail.IsBodyHtml = true;

        //           // mail.ReplyToList.Add(new MailAddress(replyto, "albertthomson2008@gmail.com"));
        //            mail.ReplyToList.Add(from);
        //            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.Delay |
        //                                               DeliveryNotificationOptions.OnFailure |
        //                                               DeliveryNotificationOptions.OnSuccess;
        //            using (var client = new SmtpClient())
        //            {
        //                client.Host = host;
        //                client.Port = int.Parse(myport);
        //                client.EnableSsl = enableSsl;
        //                client.UseDefaultCredentials = useDefaultCredentials;
        //                client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //               // client.Credentials=
        //                if (!client.UseDefaultCredentials && !string.IsNullOrEmpty(userName) &&
        //                    !string.IsNullOrEmpty(password))
        //                {
        //                    client.Credentials = new NetworkCredential(userName, password);
        //                }
        //                client.Credentials = new NetworkCredential(userName, password);
        //                await client.SendMailAsync(mail);
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //    return true;

        //}


        //public async Task<bool> albertMail(Notification notification)
        //{
        //    try
        //    {
        //        string EmailFromAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailFromAddress"];
        //        string host = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-host"];
        //        string smtpUserName = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-UserName"];
        //        string smtpPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-Password"];
        //        string port = System.Web.Configuration.WebConfigurationManager.AppSettings["Port"];
        //        var from = new MailAddress(EmailFromAddress, "Admin_@_PartyEC");
        //        var to = new MailAddress("albertthomson2008@gmail.com");
        //        // var useDefaultCredentials = true;
        //        var enableSsl = false;
        //        //  var replyto = "albertthomson2008@gmail.com"; // set here your email; 
        //        //  bool.TryParse("true", out useDefaultCredentials); //setup here if it uses defaault credentials 
        //        bool.TryParse("true", out enableSsl); //setup here if it uses ssl 
        //        // int.TryParse(myport, out port); //setup here the port 
        //        // host = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-host"]; //setup here the host 
        //        using (var mail = new MailMessage(from, to))
        //        {
        //            mail.Subject = "ATTN";
        //            mail.Body = "wonderfull after ge";
        //            mail.IsBodyHtml = true;

        //            // mail.ReplyToList.Add(new MailAddress(replyto, "albertthomson2008@gmail.com"));
        //          //  mail.ReplyToList.Add(from);
        //            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.Delay |
        //                                               DeliveryNotificationOptions.OnFailure |
        //                                               DeliveryNotificationOptions.OnSuccess;
        //            using (var client = new SmtpClient())
        //            {
        //                client.Host = host;
        //                client.Port = int.Parse(port);
        //                client.EnableSsl = enableSsl;
        //                //client.UseDefaultCredentials = useDefaultCredentials;
        //                client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //                // client.Credentials=
        //                //if (!client.UseDefaultCredentials && !string.IsNullOrEmpty(smtpUserName) &&
        //                //    !string.IsNullOrEmpty(password))
        //                //{
        //                //    client.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
        //                //}
        //                client.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
        //                await client.SendMailAsync(mail);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    return true;

        // }

         public async Task<bool> NotificationEmailPush(Notification notification)
         {
            bool sendsuccess = false;
            try
            {
               
                //Get customer information
                OperationsStatus opstatus = new OperationsStatus();
                Customer customer = null;
                customer = _customerBusiness.GetCustomer(notification.customer.ID, opstatus);
                
                    Mail _mail = new Mail();
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/PartyEcTemplates/Notifications.html")))
                    {
                        _mail.Body = reader.ReadToEnd();
                    }
                    _mail.Body = _mail.Body.Replace("{CustomerName}", customer.Name);
                    _mail.Body = _mail.Body.Replace("{Message}", notification.Message);
                    _mail.Subject = notification.Title;
                    _mail.To = customer.Email;
                    sendsuccess=  await _mailBusiness.MailSendAsync(_mail);
             }
            catch (Exception ex)
            {
                return sendsuccess;
            }
            return sendsuccess;
        }
    }
}