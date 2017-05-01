using Newtonsoft.Json;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Security;

namespace PartyEC.UI.CustomAttributes
{
 
    public class CustomAuthenticationFilter : FilterAttribute, IAuthenticationFilter
    {
        public CustomAuthenticationFilter()
        {

        }
        //string superAdminRole = "Manager"; // can be taken from resource file or config file
        //string adminRole = "Admin"; // can be taken from resource file or config file

        public void OnAuthentication(AuthenticationContext context)
        {
            ////
            var authCookie = context.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                // Unauthorized
                context.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary() { { "controller", "Account" }, { "action", "Login" } });
                return;
            }

            // Get the forms authentication ticket.
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
          //  object usercookie = JsonConvert.DeserializeObject(authTicket.UserData); // Up to you to write this Deserialize method -> it should be the reverse of what you did in your Login action
            if(authTicket == null)
            {
                context.Result = new HttpUnauthorizedResult(); // mark unauthorized
            }
            else
            {
                
                  context.HttpContext.User = new System.Security.Principal.GenericPrincipal(
                  new System.Security.Principal.GenericIdentity(authTicket.Name, "Forms"), authTicket.UserData.Split(',').Select(t => t.Trim()).ToArray());
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext context)
        {
            if (context.Result == null || context.Result is HttpUnauthorizedResult)
            {
               
                context.Result = new RedirectToRouteResult("Default",
                    new System.Web.Routing.RouteValueDictionary{
                        {"controller", "Account"},
                        {"action", "NotAuthorized"}
                        //{"returnUrl", context.HttpContext.Request.RawUrl}
                    });
            }
        }

    }

    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}