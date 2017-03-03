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
            var uaObj = (UA)HttpContext.Current.Session["SessVal"];
            if (uaObj == null)
            {
                uaObj = new UA();
                uaObj.UserName = "Albert Thomson";
                HttpContext.Current.Session["SessVal"] = uaObj;
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
    }
}
