﻿using AutoMapper;
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
    public class Cart_WishlistController : Controller
    {
        #region Constructor_Injection

        ICart_WishlistBusiness _cartWishlistBusiness;
        ICommonBusiness _commonBusiness; 

        public Cart_WishlistController(ICart_WishlistBusiness cart_WishlistBusiness, ICommonBusiness commonBusiness)
        {
            _cartWishlistBusiness = cart_WishlistBusiness;
            _commonBusiness = commonBusiness;           
        }
        #endregion Constructor_Injection



        // GET: Cart_Wishlist
        public ActionResult Index()
        {
            return View();
        }


        #region GetAllCustomerCartWishlistSummary
        [HttpGet]
        public string GetAllCustomerCartWishlistSummary(Cart_WishlistViewModel eventObj)
        {
            try
            {
                List<Cart_WishlistViewModel> eventList = Mapper.Map<List<Cart_Wishlist>, List<Cart_WishlistViewModel>>(_cartWishlistBusiness.GetAllCustomerCartWishlistSummary());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = eventList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion  GetAllCustomerCartWishlistSummary

        #region GetCustomerShoppingCart

        [HttpGet]
        public string GetCustomerShoppingCart(string ID)
        {
            try
            {
                List<Cart_WishlistViewModel> eventsLogList = Mapper.Map<List<Cart_Wishlist>, List<Cart_WishlistViewModel>>(_cartWishlistBusiness.GetCustomerShoppingCart(Int32.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = eventsLogList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion GetCustomerShoppingCart

        #region GetCustomerWishlist

        [HttpGet]
        public string GetCustomerWishlist(string ID)
        {
            try
            {
                List<Cart_WishlistViewModel> eventsLogList = Mapper.Map<List<Cart_Wishlist>, List<Cart_WishlistViewModel>>(_cartWishlistBusiness.GetCustomerWishlist(Int32.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = eventsLogList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion GetCustomerWishlist

        #region ChangeButtonStyle
        [HttpGet]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "Edit":
                  

                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";

                    break;
                case "Add":
                 

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