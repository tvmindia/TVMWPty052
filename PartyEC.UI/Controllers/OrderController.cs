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
    public class OrderController : Controller
    {
        #region Constructor_Injection
        IOrderBusiness _orderBusiness;
        ICommonBusiness _commonBusiness;
        public OrderController(IOrderBusiness orderBusiness, ICommonBusiness commonBusiness)
        {
            _orderBusiness = orderBusiness;
            _commonBusiness = commonBusiness;
        }
        #endregion Constructor_Injection
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public string GetAllOrderHeader()
        {
            try
            {
                List<OrderViewModel> OrderHeaderList = Mapper.Map<List<Order>, List<OrderViewModel>>(_orderBusiness.GetAllOrderHeader());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = OrderHeaderList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            // return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
        }
    }
}