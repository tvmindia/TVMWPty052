using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PartyEC.UI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationBusiness _authenticationBusiness;
        private ICommonBusiness _commonBusiness;
        public AccountController(IAuthenticationBusiness authenticationBusiness, ICommonBusiness commonBusiness)
        {
            _commonBusiness = commonBusiness;
            _authenticationBusiness = authenticationBusiness;
        }
        // GET: Account
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }

        #region Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public string Login(LoginViewModel loginvm)
        {
            UserViewModel uservm = null;
            try
            {
                if ((!string.IsNullOrEmpty(loginvm.LoginName)) && (!string.IsNullOrEmpty(loginvm.Password)))
                {
                    uservm = Mapper.Map<User, UserViewModel>(_authenticationBusiness.CheckUserCredentials(Mapper.Map<LoginViewModel, User>(loginvm)));
                    if(uservm!=null)
                    {
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,uservm.UserName, DateTime.Now, DateTime.Now.AddHours(24), true, uservm.RoleList);
                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));
                        //session setting
                        UA ua = new UA();
                        ua.UserName = uservm.UserName;
                        Session.Add("TvmValid", ua);
                        return JsonConvert.SerializeObject(new { Result = "OK", Record = "true" });
                    }
                
               
                }


            }
            catch (Exception ex)
            {
                throw ex;
                //  return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

            return JsonConvert.SerializeObject(new { Result = "OK", Record = "false" });
        }
        #endregion UserInsertUpdate

        #region Logout
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
                Session.Remove("TvmValid");


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("Index");
        }

        #endregion Logout

        [HttpGet]
        public ActionResult NotAuthorized()
        {
            return View();
        }

        [HttpGet]
        public string AreyouAlive()
        {
            string result="";
            try
            {
                result = _commonBusiness.GetUA() == null ? "dead" : "alive";
               
            }
            catch(Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
           
            return JsonConvert.SerializeObject(new { Result = "OK", Record = result }); 
        }

    }
}