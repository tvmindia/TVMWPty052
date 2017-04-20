using System;
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

        public CustomerController(ICustomerBusiness customerBusiness, ICommonBusiness commonBusiness, IEventBusiness eventBusiness, ICart_WishlistBusiness cartwishlistBusiness, IOrderBusiness orderBusiness)
        {
            _customerBusiness = customerBusiness;
            _commonBusiness = commonBusiness;
            _eventBusiness = eventBusiness;
            _cartwishlistBusiness = cartwishlistBusiness;
             _orderBusiness=orderBusiness;
        }
        #endregion Constructor_Injection
        Const constants = new Const();

        #region shoppingCart 

        [HttpPost]
        public object GetCustomerShoppingCart(Cart_Wishlist CartWishObj)
        {
            try
            {
                List<Cart_WishlistAppViewModel> CartList = Mapper.Map<List<Cart_Wishlist>, List<Cart_WishlistAppViewModel>>(_cartwishlistBusiness.GetCustomerShoppingCart(CartWishObj.ID));
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
        public object GetCustomerWishlist(Cart_Wishlist CartWishObj)
        {
            try
            {
                List<Cart_WishlistAppViewModel> CartList = Mapper.Map<List<Cart_Wishlist>, List<Cart_WishlistAppViewModel>>(_cartwishlistBusiness.GetCustomerWishlist(CartWishObj.ID));
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
                List<OrderAppViewModel> CartList = Mapper.Map<List<Order>, List<OrderAppViewModel>>(_orderBusiness.GetCustomerOrders(OrderObj.CustomerID));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        #endregion Orders

        #region Bookings

        #endregion Bookings

        #region Quotations

        #endregion Quotations

        #region history

        #endregion history

    }
}
