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
    public class Cart_WishlistController : Controller
    {
        #region Constructor_Injection

        ICart_WishlistBusiness _cart_WishlistBusiness;
        ICommonBusiness _commonBusiness; 

        public Cart_WishlistController(ICart_WishlistBusiness cart_WishlistBusiness, ICommonBusiness commonBusiness)
        {
            _cart_WishlistBusiness = cart_WishlistBusiness;
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
                List<Cart_WishlistViewModel> eventList = Mapper.Map<List<Cart_Wishlist>, List<Cart_WishlistViewModel>>(_cart_WishlistBusiness.GetAllCustomerCartWishlistSummary());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = eventList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion  GetAllCustomerCartWishlistSummary
    }
}