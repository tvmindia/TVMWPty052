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
    public class BookingsController : Controller
    {
        #region Constructor_Injection
        IBookingsBusiness _bookingsBusiness;
        ICommonBusiness _commonBusiness;
        IMasterBusiness _masterBusiness;
        IMailBusiness _mailBusiness;
        public BookingsController(IBookingsBusiness bookingsBusiness, ICommonBusiness commonBusiness, IMasterBusiness masterBusiness, IMailBusiness mailBusiness)
        {
            _bookingsBusiness = bookingsBusiness;
            _commonBusiness = commonBusiness;
            _masterBusiness = masterBusiness;
            _mailBusiness = mailBusiness;
        }
        #endregion Constructor_Injection

        #region Index
        // GET: Bookings
        public ActionResult Index()
        {
            //BookingsViewModel status_obj = new BookingsViewModel();
            //List<SelectListItem> selectListItem = new List<SelectListItem>();

            //List<BookingStatusViewModel> orderstatusListVM = Mapper.Map<List<BookingStatusMaster>, List<BookingStatusViewModel>>(_masterBusiness.GetAllBookingStatus());
            //foreach (BookingStatusViewModel ovm in orderstatusListVM)
            //{
            //    selectListItem.Add(new SelectListItem
            //    {
            //        Text = ovm.Description,
            //        Value = ovm.Code.ToString(),
            //        Selected = false
            //    });
            //}
            //status_obj.BookingsstatusList = selectListItem;
            //return View(status_obj);

            return View("../UnderConstruction/UnderConstruction");
        }
        #endregion Index

        #region GetAllBookings
        [HttpGet]
        public string GetAllBookings()
        {
            try
            {
                List<BookingsViewModel> BookingsList = Mapper.Map<List<Bookings>, List<BookingsViewModel>>(_bookingsBusiness.GetAllBookings());

                return JsonConvert.SerializeObject(new { Result = "OK", Records = BookingsList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }

        #endregion GetAllBookings

        #region GetBookings
        [HttpGet]
        public string GetBookings(string id)
        {
            try
            {

                BookingsViewModel Booking = Mapper.Map<Bookings, BookingsViewModel>(_bookingsBusiness.GetBookings(Int32.Parse(id)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = Booking });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion GetBookings

        #region


        #endregion

        #region ChangeButtonStyle
        [HttpGet]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "List":
                    ToolboxViewModelObj.backbtn.Visible = false;
                    break;
                case "Edit_List":
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