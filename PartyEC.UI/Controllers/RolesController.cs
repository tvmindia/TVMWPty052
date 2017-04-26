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

namespace PartyEC.UI.Controllers
{
    public class RolesController : Controller
    {
        private IAuthenticationBusiness _authenticationBusiness;
        public RolesController(IAuthenticationBusiness authenticationBusiness)
        {
            _authenticationBusiness = authenticationBusiness;
        }
        // GET: Roles
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetAllRolesOfSystem()
        {
            try
            {
                List<RoleViewModel> rolesList = Mapper.Map<List<Role>, List<RoleViewModel>>(_authenticationBusiness.GetAllRoles());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = rolesList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }

    }
}