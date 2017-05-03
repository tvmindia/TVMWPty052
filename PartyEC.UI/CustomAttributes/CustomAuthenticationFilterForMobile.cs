using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Security;



namespace PartyEC.UI.CustomAttributes
{
    public class CustomAuthenticationFilterForMobile : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext context)
        {
            if(context.HttpContext.Request!=null)
            {

            }
            else
            {

            }

        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext context)
        {

        }

        }


    //public class AuthorizeRolesAttributeForMobile : AuthorizeAttribute
    //{
    //    public AuthorizeRolesAttributeForMobile(params string[] roles) : base()
    //    {
    //        Roles = string.Join(",", roles);
    //    }
    //}
}