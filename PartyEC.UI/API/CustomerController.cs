﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PartyEC.UI.Models;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using AutoMapper;
using Newtonsoft.Json;

namespace PartyEC.UI.API
{
    public class CustomerController : ApiController
    {

        #region Constructor_Injection

        ICustomerBusiness _customerBusiness;
        ICommonBusiness _commonBusiness;
        IEventBusiness _eventBusiness;
        ICart_WishlistBusiness _cartwishlistBusiness;
        IOrderBusiness _orderBusiness;
        IQuotationsBusiness _quotationBusiness;
        IBookingsBusiness _bookingsBusiness;

        public CustomerController(ICustomerBusiness customerBusiness,ICommonBusiness commonBusiness, IEventBusiness eventBusiness,ICart_WishlistBusiness cartwishlistBusiness, IOrderBusiness orderBusiness, IQuotationsBusiness quotationBusiness, IBookingsBusiness bookingsBusiness)
        {
            _customerBusiness = customerBusiness;
            _commonBusiness = commonBusiness;
            _eventBusiness = eventBusiness;
            _cartwishlistBusiness = cartwishlistBusiness;
             _orderBusiness=orderBusiness;
            _quotationBusiness = quotationBusiness;
            _bookingsBusiness = bookingsBusiness;
        }
        #endregion Constructor_Injection
        Const constants = new Const();

        #region shoppingCart 

        [HttpPost]
        public object GetCustomerShoppingCart(ShoppingCart CartWishObj)
        {
            try
            {
                List<Cart_WishlistAppViewModel> CartList = Mapper.Map<List<ShoppingCart>, List<Cart_WishlistAppViewModel>>(_cartwishlistBusiness.GetCustomerShoppingCart(CartWishObj.CustomerID));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        #endregion shoppingCart
        
        #region Wishlist
        [HttpPost]
        public object GetCustomerWishlist(Wishlist WishlistObj)
        {
            try
            {
                List<Cart_WishlistAppViewModel> CartList = Mapper.Map<List<Wishlist>, List<Cart_WishlistAppViewModel>>(_cartwishlistBusiness.GetCustomerWishlist(WishlistObj.ID));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        #endregion Wishlist

        #region Orders

        [HttpPost]
        public object GetCustomerOrders(Order OrderObj)
        {
            try
            {
                bool Ishistory = false;
                List<OrderAppViewModel> CartList = Mapper.Map<List<Order>, List<OrderAppViewModel>>(_orderBusiness.GetCustomerOrders(OrderObj.CustomerID, Ishistory));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object GetCustomerOrdersHistory(Order OrderObj)
        {
            try
            {
                bool Ishistory = true;
                List<OrderAppViewModel> CartList = Mapper.Map<List<Order>, List<OrderAppViewModel>>(_orderBusiness.GetCustomerOrders(OrderObj.CustomerID, Ishistory));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        #endregion Orders

        #region Quotations
        [HttpPost]
        public object GetCustomerQuotations(Quotations QuotationsObj)
        {
            try
            {
                bool Ishistory = false;
                List<QuotationsAppViewModel> CartList = Mapper.Map<List<Quotations>, List<QuotationsAppViewModel>>(_quotationBusiness.GetCustomerQuotations(QuotationsObj.CustomerID, Ishistory));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }


        [HttpPost]
        public object GetCustomerQuotationsHistory(Quotations QuotationsObj)
        {           
            try
            {
                bool Ishistory = true;
                List<QuotationsAppViewModel> CartList = Mapper.Map<List<Quotations>, List<QuotationsAppViewModel>>(_quotationBusiness.GetCustomerQuotations(QuotationsObj.CustomerID, Ishistory));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }



        #endregion Quotations

        #region Bookings
        [HttpPost]
        public object GetCustomerBookings(Bookings bookingsObj)
        {
            try
            {
                bool Ishistory = false;
                List<BookingsAppViewModel> CartList = Mapper.Map<List<Bookings>, List<BookingsAppViewModel>>(_bookingsBusiness.GetCustomerBookings(bookingsObj.CustomerID, Ishistory));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object GetCustomerBookingsHistory(Bookings bookingsObj)
        {
            try
            {
                bool Ishistory = true;
                List<BookingsAppViewModel> CartList = Mapper.Map<List<Bookings>, List<BookingsAppViewModel>>(_bookingsBusiness.GetCustomerBookings(bookingsObj.CustomerID, Ishistory));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        #endregion Bookings


        [HttpPost]
        public object InsertUpdateCustomerAddress(Customer addressObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                addressObj.logDetailsObj = new LogDetails();
                addressObj.logDetailsObj.CreatedBy = "AppUser";
                addressObj.logDetailsObj.CreatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_customerBusiness.InsertUpdateCustomerAddress(addressObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object GetCustomerAddress(CustomerAddress addressObj)
        {
            try
            {
              
                List<CustomerAddressViewModel> CartList = Mapper.Map<List<CustomerAddress>, List<CustomerAddressViewModel>>(_customerBusiness.GetAllCustomerAddresses(addressObj.CustomerID));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }


        [HttpPost]
        public object DeleteCustomerAddress(CustomerAddress addressObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {            
                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_customerBusiness.DeleteAddress(addressObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }


        [HttpPost]
        public object RegisterUser (Customer customerObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                customerObj.logDetailsObj = new LogDetails();
                customerObj.logDetailsObj.CreatedBy = "AppUser";
                customerObj.logDetailsObj.CreatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_customerBusiness.InsertCustomer(customerObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object UpdateUser(Customer customerObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                customerObj.logDetailsObj = new LogDetails();
                customerObj.logDetailsObj.UpdatedBy = "AppUser";
                customerObj.logDetailsObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_customerBusiness.UpdateCustomer(customerObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
    }
}
