using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.CustomAttributes;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    [CustomAuthenticationFilter]
    public class DashBoardController : Controller
    {
        #region Constructor_Injection

        IMasterBusiness _masterBusiness;
        IOrderBusiness _orderBusiness;
        IProductBusiness _productBusiness;
        public DashBoardController(IMasterBusiness masterBusiness,IOrderBusiness orderBusiness,IProductBusiness productBusiness)
        {
            _masterBusiness = masterBusiness;
            _orderBusiness = orderBusiness;
            _productBusiness = productBusiness;
        }

        #endregion Constructor_Injection
        // GET: DashBoard
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult Index()
        {
            return View();
        }
        #region GetWeeklySalesSummeryForChart
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetWeeklySalesDetails()
        {
            try
            {
                List<GraphViewModel> salesSummeryList = Mapper.Map<List<Graph>, List<GraphViewModel>>(_masterBusiness.GetWeeklySalesDetails());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = salesSummeryList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion  GetWeeklySalesSummeryForChart
        #region GetRootCategorySalesSummeryForChart
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetRootCategoryWiseSalesDetail()
        {
            try
            {
                List<GraphViewModel> GraphList = Mapper.Map<List<Graph>, List<GraphViewModel>>(_masterBusiness.GetRootCategoryWiseSalesDetail());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = GraphList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion  GetRootCategorySalesSummeryForChart
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetTop10Products()
        {
            try
            {
                List<ProductViewModel> productList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetTop10Products());

                return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }
    }
}