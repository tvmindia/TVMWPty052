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
        ICart_WishlistBusiness _cart_WishlistBusiness;
        IMasterBusiness _masterBusiness;

        public CustomerController(ICustomerBusiness customerBusiness,ICommonBusiness commonBusiness, IOrderBusiness orderBusiness, ICart_WishlistBusiness cart_WishlistBusiness, IMasterBusiness masterBusiness)
        { 
            _commonBusiness = commonBusiness;
            _customerBusiness = customerBusiness;
            _orderBusiness = orderBusiness;
            _cart_WishlistBusiness = cart_WishlistBusiness;
            _masterBusiness = masterBusiness;
        }
        #endregion Constructor_Injection 
        // GET: Customer
        public ActionResult Index()
        {
            CustomerViewModel customer = null;
            try
            {
                customer = new CustomerViewModel();
                customer.customerAddress = new CustomerAddressViewModel();
                customer.customerAddress.country = new CountryViewModel();
                List<SelectListItem> selectListItem = new List<SelectListItem>();
                //Countries drop down bind
                List<CountryViewModel> countryListVM = Mapper.Map<List<Country>, List<CountryViewModel>>(_masterBusiness.GetAllCountries());
                foreach (CountryViewModel cvm in countryListVM)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = cvm.Name,
                        Value = cvm.Code.ToString(),
                        Selected = false
                    });
                }
                customer.customerAddress.Countries = selectListItem;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            


            return View(customer);
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



        #region GetCustomerCartDetails
        [HttpGet]
        public string GetCustomerCartDetails(string customerID)
        {
            try
            {
                List<Cart_WishlistViewModel> cartList = null;
                if (!string.IsNullOrEmpty(customerID))
                {
                    OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                    cartList = Mapper.Map<List<Cart_Wishlist>, List<Cart_WishlistViewModel>>(_cart_WishlistBusiness.GetCustomerShoppingCart(int.Parse(customerID)));
                }
                return JsonConvert.SerializeObject(new { Result = "OK", Records = cartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion GetCustomerCartDetails

        #region GetCustomerWishList
        [HttpGet]
        public string GetCustomerWishList(string customerID)
        {
            try
            {
                List<Cart_WishlistViewModel> wishList = null;
                if (!string.IsNullOrEmpty(customerID))
                {
                    OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                    wishList = Mapper.Map<List<Cart_Wishlist>, List<Cart_WishlistViewModel>>(_cart_WishlistBusiness.GetCustomerWishlist(int.Parse(customerID)));
                }
                return JsonConvert.SerializeObject(new { Result = "OK", Records = wishList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion GetCustomerWishList

        #region ActiateorDeactivateCustomer
   
        [HttpPost]
        public string ActiateorDeactivateCustomer(CustomerViewModel customer)
        {
            try
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = new OperationsStatusViewModel();
                customer.logDetailsObj = new LogDetailsViewModel();
                customer.logDetailsObj.UpdatedBy = _commonBusiness.GetUA().UserName;
                customer.logDetailsObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_customerBusiness.CustomerEnableORDisable(Mapper.Map<CustomerViewModel, Customer>(customer)));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion ActiateorDeactivateCustomer


        #region CustomerAddressInsertUpdate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string CustomerAddressInsertUpdate(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    OperationsStatusViewModel OperationsStatusViewModelObj = null;
                           //INSERT
                        
                            customer.customerAddress.logDetailsObj = new LogDetailsViewModel();
                            //Getting UA
                            customer.customerAddress.logDetailsObj.CreatedBy = _commonBusiness.GetUA().UserName;
                            customer.customerAddress.logDetailsObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                            OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_customerBusiness.InsertUpdateCustomerAddress(Mapper.Map<CustomerViewModel, Customer>(customer)));
                            return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                       
                
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }
            }
            //Model state errror
            else
            {
                List<string> modelErrors = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        modelErrors.Add(modelError.ErrorMessage);
                    }
                }
                return JsonConvert.SerializeObject(new { Result = "VALIDATION", Message = string.Join(",", modelErrors) });
            }
        }

        #endregion CustomerAddressInsertUpdate
        #region ChangeButtonStyle
        [HttpGet]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "Deactivate":
                    ToolboxViewModelObj.actDeactbtn.Visible = true;
                    ToolboxViewModelObj.actDeactbtn.Event = "ActivateORDeactivate()";
                    ToolboxViewModelObj.actDeactbtn.Title = "Deactivate";

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "AddressSave()";
                    ToolboxViewModelObj.savebtn.Title = "Save";

                    //ToolboxViewModelObj.resetbtn.Visible = true;
                    //ToolboxViewModelObj.resetbtn.Event = "btnreset()";
                    //ToolboxViewModelObj.resetbtn.Title = "Reset";

                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";

                    break;

                case "Activate":
                    ToolboxViewModelObj.actDeactbtn.Visible = true;
                    ToolboxViewModelObj.actDeactbtn.Event = "ActivateORDeactivate()";
                    ToolboxViewModelObj.actDeactbtn.Title = "Activate";

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "AddressSave()";
                    ToolboxViewModelObj.savebtn.Title = "Save";

                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    break;
                                            
                default:
                    return Content("Nochange");
            }
            return PartialView("_ToolboxView", ToolboxViewModelObj);
        }


        #endregion ChangeButtonStyle

    }
}