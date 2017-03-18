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

        public CustomerController(ICustomerBusiness customerBusiness,ICommonBusiness commonBusiness)
        { 
            _commonBusiness = commonBusiness;
            _customerBusiness = customerBusiness;
        }
        #endregion Constructor_Injection 
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }


        #region GetAllCustomers
        [HttpGet]
        public string GetAllCustomers(CustomerViewModel eventObj)
        {
            try
            {
                List<CustomerViewModel> eventList = Mapper.Map<List<Customer>, List<CustomerViewModel>>(_customerBusiness.GetAllCustomers());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = eventList });
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
                CustomerViewModel attribute = Mapper.Map<Customer, CustomerViewModel>(_customerBusiness.GetCustomer(Int32.Parse(ID), Mapper.Map<OperationsStatusViewModel, OperationsStatus>(operationsStatus)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = attribute });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }


        #endregion GetCustomerById
    }
}