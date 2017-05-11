using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.BusinessServices.Services
{
    public class MailBusiness: IMailBusiness
    {
        string EmailFromAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailFromAddress"];
        string host = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-host"];
        string smtpUserName = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-UserName"];
        string smtpPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-Password"];
        string port = System.Web.Configuration.WebConfigurationManager.AppSettings["Port"];

        private IOrderRepository _orderRepository;
        private ICommonBusiness _commonBusiness;
        public MailBusiness(IOrderRepository orderRepository,ICommonBusiness commonBusiness)
        {
            _orderRepository = orderRepository;
            _commonBusiness = commonBusiness;
        }
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
            SmtpClient mailServer = new SmtpClient();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(EmailFromAddress, "Admin_@_PartyEC");
            mail.To.Add(mailObj.To);
            mail.Subject = mailObj.Subject;
            mail.IsBodyHtml = mailObj.IsBodyHtml;
            mail.Body = mailObj.Body;
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


        public string GetMailTemplate(int ID)
        {
            string body = string.Empty;
            string Detail = string.Empty;
            float Amount = 0;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/PartyEcTemplates/OrderSucess.html")))
            {
                body = reader.ReadToEnd();
            }
            Order OrderObj = _orderRepository.GetOrderDetails(ID.ToString());
            List <OrderDetail> OrderList = _orderRepository.GetAllOrdersList(ID.ToString());
            body = body.Replace("{@CustomerName}", OrderObj.CustomerName);
            body = body.Replace("{@OrdeNo}", OrderObj.OrderNo.ToString());
            body = body.Replace("{@Address}", OrderObj.ShipAddress);
            body = body.Replace("{@Place}", OrderObj.ShipCity);
            body = body.Replace("{@State}", "");
            body = body.Replace("{@PinCode}", "");
            body = body.Replace("{@Mobile}", OrderObj.ContactNo);
            foreach(var i in OrderList)
            {
                //_commonBusiness.GetAttributeValueFromXML(i.ProductSpecXML1);
                Amount = Amount+i.SubTotal;
                Detail = Detail+ @"<tr><td width='120' valign='top' align='left'>
                        <a style='color:#027cd8;text-decoration:none;outline:none;color:#ffffff;font-size:13px;display:block;width:100px' href='' target='_blank' > <img border='0' src='"+i.ImageUrl +"' alt='' style='border:none;width:100%' class='CToWUd'> </a></td><td width='8'></td><td valign='top' align='left'><p style='margin-bottom:13px'><a style='font-family:'Roboto',sans-serif;font-size:14px;font-weight:normal;font-style:normal;font-stretch:normal;line-height:1.25;color:#2175ff;text-decoration:none' href='' target='_blank' >"+i.ProductName + "</a><sup></sup> <br>  </p> <p style='font-family:'Roboto',sans-serif;font-size:12px;font-weight:normal;font-style:normal;line-height:1.5;font-stretch:normal;color:#878787;margin:0px 0px'>Seller: "+i.SupplierName+"</p> <p style='font-family:'Roboto-Medium',sans-serif;font-style:normal;line-height:1.5;font-stretch:normal;color:#212121;margin:5px 0px'><span style='padding-right:10px'>"+i.SubTotal+"</span> <span style='font-family:'Roboto-Medium',sans-serif;font-size:12px;font-weight:normal;font-style:normal;line-height:1.5;font-stretch:normal;color:#878787;margin:0px 0px;border:1px solid #dfdfdf;display:inline;border-radius:3px;padding:3px 10px'>Qty:"+i.Qty+"</span> </p></td></tr>";
            }
            body= body.Replace("{@Content}", Detail);
            body= body.Replace("{@Amount}", Amount.ToString());
            return body;
        }

        #region MailSendAsync
        /// <summary>
        /// send mail asynchronously one address at a time
        /// </summary>
        /// <param name="mailObj"></param>
        /// <returns>bool</returns>
        public async Task<bool> MailSendAsync(Mail mailObj)
        {
            try
            {
               using (var mail = new MailMessage(new MailAddress(EmailFromAddress, "Admin_@_PartyEC"), new MailAddress(mailObj.To)))
                {
                    mail.Subject = mailObj.Subject;
                    mail.Body = mailObj.Body;
                    mail.IsBodyHtml = true;
                   using (var client = new SmtpClient())
                    {
                        client.UseDefaultCredentials = false;
                        client.Host = host;
                        client.Port = int.Parse(port);
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                        await client.SendMailAsync(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;

        }
        #endregion MailSendAsync
    }
}