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
    public class DashBoardController : Controller
    {
        #region Constructor_Injection

        IMasterBusiness _masterBusiness;
        IOrderBusiness _orderBusiness;
        public DashBoardController(IMasterBusiness masterBusiness,IOrderBusiness orderBusiness)
        {
            _masterBusiness = masterBusiness;
            _orderBusiness = orderBusiness;
        }

        #endregion Constructor_Injection
        // GET: DashBoard
        public ActionResult Index()
        {
            return View();
        }
        #region GetWeeklySalesSummeryForChart
        [HttpGet]
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
        #endregion  GetRootCategorySalesSummeryForChart
        #region GetWeeklySalesSummeryForChart
        [HttpGet]
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
    }
}