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
          
            try
            {
                UA uaObj = null;
                if (HttpContext.Current.Session == null)
                {
                    //For Mobile
                    uaObj = new UA();
                    uaObj.UserName = "Tvm-Mobile";
                    uaObj.DateTime = GetCurrentDateTime();
                }
                else
                {
                  if(HttpContext.Current.Session["TvmValid"] != null)
                  {
                            uaObj = (UA)HttpContext.Current.Session["TvmValid"];
                            uaObj.DateTime = GetCurrentDateTime();
                  }
                }
                    //this code is temporary arrangement
                    //must replace...!
                 _uaObj = uaObj;
            }
            catch(Exception ex)
            {
                throw ex;
            }
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



        public void SendOTP(string OTP, string MobileNo) {
            if (OTP.Length != 4)
                throw new Exception("OTP should be 4 digits");

            SendMessage(OTP, MobileNo, "2factor", "OTP");
        }


        #region messageSending


        private void SendMessage(string Msg, string MobileNos, string provider = "txtlocal", string type = "Promotional")
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
                            #region textlocal
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
                            #endregion

                            #region smshorizon
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

                            #endregion

                            #region 2factor
                            //-----------------------------------------------------------------------------------------------------------
                            else if (provider == "2factor" && type == "OTP")
                            {

                                string Confgs = System.Web.Configuration.WebConfigurationManager.AppSettings["2factorOTP"];

                                if (!String.IsNullOrEmpty(Confgs))
                                {


                                    string[] ConfgValues = Confgs.Split('|');

                                    using (var wb = new WebClient())
                                    {
                                        // byte[] response = wb.UploadValues("http://205.147.96.66/API/R1/", "POST", new NameValueCollection()
                                        //{
                                        //{ "module","SMS_OTP"},
                                        //{"apikey" , "bddc3759-107a-11e7-9462-00163ef91450"},                                
                                        //{"to" , MobileNos},
                                        //{"otpvalue" , msg}

                                        byte[] response = wb.UploadValues(ConfgValues[0], "POST", new NameValueCollection()
                                    {
                                    { "module",ConfgValues[1]},
                                    {"apikey" , ConfgValues[2]},
                                    {"template_name" , ConfgValues[3]},
                                    {"to" , MobileNos},
                                    {"otpvalue" , msg}

                                    });
                                        string result = System.Text.Encoding.UTF8.GetString(response);

                                    }
                                }
                            }
                            #endregion
                            //-------------------------------------------------------------------------------------------------------------



                        }



                    }
                }

            }
        }
        #endregion





    }

}
