using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace PartyEC.BusinessServices.Services
{
    public class MailBusiness: IMailBusiness
    {
        string EmailFromAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailFromAddress"];
        string host = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-host"];
        string smtpUserName = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-UserName"];
        string smtpPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-Password"];
        string port = System.Web.Configuration.WebConfigurationManager.AppSettings["Port"];


        public bool Send(Mail mailObj)
        {
            string body = string.Empty;
            //Mail mailObj = new Mail();
            SmtpClient mailServer = new SmtpClient();
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(EmailFromAddress, "Admin_@_PartyEC");
            mail.To.Add(mailObj.CustomerEmail);
            mail.Subject = "Shipping Tracking";
            mail.IsBodyHtml = true;
            
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/PartyEcTemplates/OrderComment.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{CustomerName}", mailObj.CustomerName);
            body = body.Replace("{OrderNo}", mailObj.OrderNo.ToString());
            body = body.Replace("{OrderComment}",mailObj.OrderComment);
            mail.Body = body;

            try
            {
                mailServer.Host = host;
                mailServer.Port = int.Parse(port);
                mailServer.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                mailServer.EnableSsl = true;
                mail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                mailServer.Send(mail);
            }
            catch (SmtpException e)
            {
                //LogHelper.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "Error sending welcome email.", e);
                return false;
            }

            return true;
        }

        public bool SendMail(Mail mailObj)
        {
            string body = string.Empty;
            //Mail mailObj = new Mail();
            SmtpClient mailServer = new SmtpClient();
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(EmailFromAddress, "Admin_@_PartyEC");
            mail.To.Add(mailObj.CustomerEmail);
            mail.Subject = "Shipping Tracking";
            mail.IsBodyHtml = true;

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/PartyEcTemplates/OrderComment.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{CustomerName}", mailObj.CustomerName);
            body = body.Replace("{OrderNo}", mailObj.OrderNo.ToString());
            body = body.Replace("{OrderComment}", mailObj.OrderComment);
            mail.Body = body;

            try
            {
                mailServer.Host = host;
                mailServer.Port = int.Parse(port);
                mailServer.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                mailServer.EnableSsl = true;
                mail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                mailServer.Send(mail);
            }
            catch (SmtpException e)
            {
                //LogHelper.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "Error sending welcome email.", e);
                return false;
            }

            return true;
        }

    }
}