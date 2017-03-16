using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
      
    }
}
