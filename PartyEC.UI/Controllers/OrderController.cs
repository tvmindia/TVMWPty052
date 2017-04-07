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
        IMasterBusiness _masterBusiness;
        public OrderController(IOrderBusiness orderBusiness, ICommonBusiness commonBusiness,IMasterBusiness masterBusiness)
        {
            _orderBusiness = orderBusiness;
            _commonBusiness = commonBusiness;
            _masterBusiness = masterBusiness;
        }
        #endregion Constructor_Injection
        // GET: Order
        public ActionResult Index()
        {
            OrderViewModel order = null;
            try
            {
                order = new OrderViewModel();
                List<SelectListItem> selectListItem = new List<SelectListItem>();
                //Country Drop down bind
                List<CountryViewModel> CounrtyList = Mapper.Map<List<Country>, List<CountryViewModel>>(_masterBusiness.GetAllCountries());
                foreach (CountryViewModel ccl in CounrtyList)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = ccl.Name,
                        Value = ccl.Code,
                        Selected = false
                    });
                }
                order.Countries = selectListItem;
            }
            catch(Exception ex)
            {
                
            }
            
            return View(order);
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
        [HttpGet]
        public string GetAllOrdersList(string ID)
        {
            try
            {
                List<OrderViewModel> OrderHeaderList = Mapper.Map<List<Order>, List<OrderViewModel>>(_orderBusiness.GetAllOrdersList(ID));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = OrderHeaderList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            // return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
        }
        [HttpGet]
        public string GetOrderDetails(string ID)
        {
            try
            {
                OrderViewModel OrderList = null;
                if (ID != "")
                {
                    OrderList = Mapper.Map < Order,  OrderViewModel >( _orderBusiness.GetOrderDetails(ID));
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = OrderList });
                }
                return JsonConvert.SerializeObject(new { Result = "OK", Records = OrderList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            //return JsonConvert.SerializeObject(new { Result = "OK", Records = NodeList });
        }

        [HttpGet]
        public string GetEventsLog(string ID)
        {
            try
            {
                List<EventsLogViewModel> eventsLogList = Mapper.Map<List<EventsLog>, List<EventsLogViewModel>>(_masterBusiness.GetEventsLog(int.Parse(ID), "Order"));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = eventsLogList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpGet]
        public string GetOrderSummery(string ID)
        {
            try
            {
                OrderViewModel OrderViewModelObj = Mapper.Map<Order, OrderViewModel>(_orderBusiness.GetOrderSummery(int.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = OrderViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        
        [HttpPost]
        public string UpdateEventsLog(OrderViewModel orderObj)
        {

            MailViewModel modelObj = new MailViewModel();
            modelObj.UserName = "Thomson K Varkey";
            modelObj.OrderID = "O_1002";
            modelObj.comment = "Thank you for purchasing with us PartyEc.com";
            var MailBod= PartialView("_OrderCommentView", modelObj);
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
                try
                {
                    orderObj.EventsLogViewObj.commonObj = new LogDetailsViewModel();
                    orderObj.EventsLogViewObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                    orderObj.EventsLogViewObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_masterBusiness.InsertEventsLog(Mapper.Map<EventsLogViewModel, EventsLog>(orderObj.EventsLogViewObj)));
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }

                if (OperationsStatusViewModelObj.StatusCode == 1)
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "Error", Record = OperationsStatusViewModelObj });
                }
        }
        [ValidateAntiForgeryToken]
        public string UpdateBillingDetails(OrderViewModel orderViewModelObj)
        {
            try
            {
                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                OperationsStatusViewModel OperationsStatusViewModelObj =Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_orderBusiness.UpdateBillingDetails(Mapper.Map < OrderViewModel, Order >(orderViewModelObj)));
                if (OperationsStatusViewModelObj.StatusCode == 0)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Records = OperationsStatusViewModelObj });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = OperationsStatusViewModelObj });
                }

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string UpdateShipingDetails(OrderViewModel orderViewModelObj)
        {
            try
            {
                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_orderBusiness.UpdateShipingDetails(Mapper.Map<OrderViewModel, Order>(orderViewModelObj)));
                if (OperationsStatusViewModelObj.StatusCode == 0)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Records = OperationsStatusViewModelObj });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = OperationsStatusViewModelObj });
                }

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}