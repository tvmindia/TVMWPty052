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
    public class UsersController : Controller
    {
        private IAuthenticationBusiness _authenticationBusiness;
        public UsersController(IAuthenticationBusiness authenticationBusiness)
        {
            _authenticationBusiness = authenticationBusiness;
        }
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetAllUsersOfSystem()
        {
            try
            {
                List<UserViewModel> userList = Mapper.Map<List<User>, List<UserViewModel>>(_authenticationBusiness.GetAllUsers());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = userList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }
    }
}