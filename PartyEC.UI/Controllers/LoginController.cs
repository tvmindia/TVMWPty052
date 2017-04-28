using Newtonsoft.Json;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        #region Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public string LookupUser(LoginViewModel loginvm)
        {
           
               try
                {

                if ((loginvm.LoginName == "tvm") && (loginvm.Password == "tvm"))
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = "true" });
                }
                
                    
                }
                catch (Exception ex)
                {
                  //  return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }

            return JsonConvert.SerializeObject(new { Result = "OK", Record = "false" });
        }
        #endregion UserInsertUpdate

    }
}