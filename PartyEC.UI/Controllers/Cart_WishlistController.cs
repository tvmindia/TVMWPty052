using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.CustomAttributes;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    [CustomAuthenticationFilter]
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
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult Index()
        {
            return View();
        }


        #region GetAllCustomerCartWishlistSummary
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetAllCustomerCartWishlistSummary(CustomerViewModel eventObj)
        {
            try
            {
                List<CustomerViewModel> customerList = Mapper.Map<List<Customer>, List<CustomerViewModel>>(_cartWishlistBusiness.GetAllCustomerCartWishlistSummary());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = customerList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion  GetAllCustomerCartWishlistSummary

        #region GetCustomerShoppingCart

        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetCustomerShoppingCart(ShoppingCart cartObj)
        {

            try
            {
                cartObj.logDetails = new LogDetails();
                cartObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();
                cartObj.LocationID =0;
                List<ShoppingCartViewModel> eventsLogList = Mapper.Map<List<ShoppingCart>, List<ShoppingCartViewModel>>(_cartWishlistBusiness.GetCustomerShoppingCart(cartObj));
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
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetCustomerWishlist(string ID)
        {
            try
            {
                List<WishlistViewModel> eventsLogList = Mapper.Map<List<Wishlist>, List<WishlistViewModel>>(_cartWishlistBusiness.GetCustomerWishlist(Int32.Parse(ID),null));
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
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
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
                case "CustomersList":
                    ToolboxViewModelObj.backbtn.Visible = false;
                    break;
                default:
                    return Content("Nochange");
            }
            return PartialView("_ToolboxView", ToolboxViewModelObj);
        }
        #endregion ChangeButtonStyle
    }
}