using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Specialized;
using System.Text;

namespace PartyEC.BusinessServices.Services
{
    public class CommonBusiness : ICommonBusiness
    {
        private UA _uaObj;
        public CommonBusiness()
        {
            UA uaObj = null;//need to find proper implementation , time being commenting for api calls-- (UA)HttpContext.Current.Session["SessVal"];
            if (uaObj == null)
            {
                uaObj = new UA();
                uaObj.UserName = "Albert Thomson";
                //need to find proper implementation , time being commenting for api calls--  HttpContext.Current.Session["SessVal"] = uaObj;
            }
            _uaObj = uaObj;
        }
        public UA GetUA()
        {
            return _uaObj;
        }

        public void UpdateUA(UA uaObj)
        {
            _uaObj.UserName = uaObj.UserName;
        }
        public DateTime GetCurrentDateTime()
        {
            string tz = System.Web.Configuration.WebConfigurationManager.AppSettings["TimeZone"];
            DateTime DateNow = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
            return (TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateNow, tz));
        }



        public  void SendMessage(string Msg, string MobileNos,string provider="txtlocal")
        {
            string[] IndividualMsgs = Msg.Split('|');
            string[] IndividualMobileNos = MobileNos.Split('|');
            foreach (var msg in IndividualMsgs) //msg is individual message
            {
                foreach (var Num in IndividualMobileNos) //Num is individual Number
                {
                    if (Num != string.Empty)
                    {
                        if (msg != string.Empty)
                        {
                            String message = HttpUtility.UrlEncode(msg);
                            //--------------------------------------------------------------------------------------------------
                            if (provider == "txtlocal")
                            {
                                using (var wb = new WebClient())
                                {
                                    byte[] response = wb.UploadValues("https://api.textlocal.in/send/", "POST", new NameValueCollection()
                                {
                                {"username" , "suvaneeth@gmail.com"},
                                {"hash" , "0f6f640793dfe7fd4c75ef55b57c2f841986f71e8c52fbdea5f6cb52cc723603"},
                                { "apiKey","dSGmbXNsOJU-OZI40tsiF6tEwF6fCgEVq3uZ9lpd56"},
                                {"sender" , "TXTLCL"},
                                {"numbers" , Num},
                                {"message" , message}
                                });
                                    string result = System.Text.Encoding.UTF8.GetString(response);

                                }
                            }
                            //-------------------------------------------------------------------------------------------------------
                            else if (provider == "smshorizon")
                            {

                                using (var wb = new WebClient())
                                {
                                    byte[] response = wb.UploadValues("http://smshorizon.co.in/api/sendsms.php", "POST", new NameValueCollection()
                                {
                                {"user" , "suvaneeth"},
                                {"apikey" , "Ge0hv03z2WvwlBOTK3B0"},                   
                                {"mobile" , Num},
                                {"message" , msg},
                                        { "senderid","MYTEXT"},
                                { "type","txt"}
                                });
                                    string result = System.Text.Encoding.UTF8.GetString(response);

                                }
                            }
                          




                          
                        }



                    }
                    }

                }
            }

        }
    
}
