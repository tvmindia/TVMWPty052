using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;

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



        public static void SendMessage(string Msg, string MobileNos)
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
                            string strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=shamilatps5@gmail.com:123456&senderID=TEST SMS&receipientno=" + Num + "&msgtxt=" + msg + "&state=4";
                            WebRequest request = HttpWebRequest.Create(strUrl);
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            Stream s = (Stream)response.GetResponseStream();
                            StreamReader readStream = new StreamReader(s);
                            string dataString = readStream.ReadToEnd();
                            response.Close();
                            s.Close();
                            readStream.Close();

                        }
                    }
                }

            }
        }

    }
}
