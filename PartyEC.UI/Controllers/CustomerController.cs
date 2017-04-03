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
    public class CustomerController : Controller
    {
        #region Constructor_Injection 
       
        ICustomerBusiness _customerBusiness;
        ICommonBusiness _commonBusiness;
        IOrderBusiness _orderBusiness;

        public CustomerController(ICustomerBusiness customerBusiness,ICommonBusiness commonBusiness, IOrderBusiness orderBusiness)
        { 
            _commonBusiness = commonBusiness;
            _customerBusiness = customerBusiness;
            _orderBusiness = orderBusiness;
        }
        #endregion Constructor_Injection 
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }


        #region GetAllCustomers
        [HttpGet]
        public string GetAllCustomers()
        {
            try
            {
                List<CustomerViewModel> customersList = Mapper.Map<List<Customer>, List<CustomerViewModel>>(_customerBusiness.GetAllCustomers());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = customersList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion  GetAllCustomers

        #region GetCustomerById

        [HttpGet]
        public string GetCustomer(string ID)
        {
            try
            {
                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                CustomerViewModel customerObj = Mapper.Map<Customer, CustomerViewModel>(_customerBusiness.GetCustomer(Int32.Parse(ID), Mapper.Map<OperationsStatusViewModel, OperationsStatus>(operationsStatus)));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = customerObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }


        #endregion GetCustomerById

        #region GetOrderSummaryForCustomer
        [HttpGet]
        public string GetSalesStatistics(string customerID)
        {
            try
            {
                OrderViewModel OrderObj = null;
                if (!string.IsNullOrEmpty(customerID))
                {
                    OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                    OrderObj = Mapper.Map<Order, OrderViewModel>(_orderBusiness.GetSalesStatistics(Int32.Parse(customerID), _commonBusiness.GetCurrentDateTime()));
                }
              
                return JsonConvert.SerializeObject(new { Result = "OK", Record = OrderObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion GetOrderSummaryForCustomer


        #region OrderSummary
        [HttpGet]
        public string GetOrderSummary(string customerID)
        {
            try
            {
                List<OrderViewModel> orderList = null;
                if (!string.IsNullOrEmpty(customerID))
                {
                    OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                    orderList = Mapper.Map<List<Order>, List<OrderViewModel>>(_orderBusiness.GetOrderSummary(int.Parse(customerID)));
                }
               
                return JsonConvert.SerializeObject(new { Result = "OK", Records = orderList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion OrderSummary
    }
}