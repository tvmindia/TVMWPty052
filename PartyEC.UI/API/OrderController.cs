using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.CustomAttributes;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PartyEC.UI.API
{

    [CustomAuthenticationFilterForMobile]
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
                BookingsObj.logDetails.CreatedBy = _commonBusiness.GetUA().UserName;
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
                QuotationsObj.logDetails.CreatedBy = _commonBusiness.GetUA().UserName;
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
                cartObj.logDetails.CreatedBy = _commonBusiness.GetUA().UserName;
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
        public object RemoveProductFromCart(ShoppingCart cartObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_cart_WishlistBusiness.RemoveProductFromCart(cartObj.ID));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }


        [HttpPost]
        public object GetCustomerCart(ShoppingCart cartObj)
        {
            try
            {
                cartObj.logDetails = new LogDetails();
                cartObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();
                List<ShoppingCartViewModel> Locations = Mapper.Map<List<ShoppingCart>, List<ShoppingCartViewModel>>(_cart_WishlistBusiness.GetCustomerShoppingCart(cartObj));
                if (Locations.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = Locations });
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

        [HttpPost]
        public object InsertOrder(Order OrderObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                OrderObj.commonObj = new LogDetails();
                OrderObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                OrderObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_OrderBusiness.InsertOrderForApp(OrderObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        // (Dummy True always till payment gateway is ready)
        [HttpPost]
        public object GetPaymentStatus()
        {
            try
            {
                Random rnd = new Random();                  // Random number creation  
                string ReferenceNo= "_"+ rnd.Next(100000000, 900000000).ToString();
                bool TranscationStatus= true;
                return JsonConvert.SerializeObject(new { Result = true, Records = new { Reference = ReferenceNo, Status = TranscationStatus } });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }


        [HttpPost]
        public object UpdateOrderPaymentStatus(Order OrderObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                OrderObj.commonObj = new LogDetails();
                OrderObj.commonObj.UpdatedBy = _commonBusiness.GetUA().UserName;
                OrderObj.commonObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_OrderBusiness.UpdateOrderPaymentStatus(OrderObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        } 
    }
}
