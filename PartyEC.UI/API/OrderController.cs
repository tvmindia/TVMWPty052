﻿using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PartyEC.UI.API
{
    public class OrderController : ApiController
    {
        Const constants = new Const();
        #region Constructor_Injection

        IOrderBusiness _OrderBusiness;
        ICommonBusiness _commonBusiness;
        IBookingsBusiness _bookingBusiness;
        IQuotationsBusiness _quotationsBusiness;
        ICart_WishlistBusiness _cart_WishlistBusiness;
        IMasterBusiness _masterBusiness;

        public OrderController(IOrderBusiness orderBusiness, ICommonBusiness commonBusiness, IBookingsBusiness bookingBusiness, IQuotationsBusiness quotationsBusiness, ICart_WishlistBusiness cart_WishlistBusiness, IMasterBusiness masterBusiness)
        {
            _OrderBusiness = orderBusiness;
            _commonBusiness = commonBusiness;
            _bookingBusiness = bookingBusiness;
            _quotationsBusiness = quotationsBusiness;
            _cart_WishlistBusiness = cart_WishlistBusiness;
            _masterBusiness = masterBusiness;
        }
        #endregion Constructor_Injection

        [HttpPost]
        public object InsertBookings (Bookings BookingsObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                BookingsObj.logDetails = new LogDetails();
                BookingsObj.logDetails.CreatedBy = "AppUser";
                BookingsObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_bookingBusiness.InsertBookings(BookingsObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object InsertQuotations(Quotations QuotationsObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                QuotationsObj.logDetails = new LogDetails();
                QuotationsObj.logDetails.CreatedBy = "AppUser";
                QuotationsObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_quotationsBusiness.InsertQuotations(QuotationsObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object AddProductToCart(ShoppingCart cartObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                cartObj.logDetails = new LogDetails();
                cartObj.logDetails.CreatedBy = "AppUser";
                cartObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_cart_WishlistBusiness.AddProductToCart(cartObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }


        [HttpPost]
        public object GetLocationDetails()
        {
            try
            {

                List<ShippingLocationViewModel> Locations = Mapper.Map<List<ShippingLocations>, List<ShippingLocationViewModel>>(_masterBusiness.GetAllShippingLocation());
                if (Locations.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = Locations });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
    }
}
