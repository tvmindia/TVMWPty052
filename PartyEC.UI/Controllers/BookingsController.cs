using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.CustomAttributes;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    [CustomAuthenticationFilter]
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
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult Index()
        {
            BookingsViewModel status_obj = new BookingsViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();

            List<BookingStatusViewModel> orderstatusListVM = Mapper.Map<List<BookingStatusMaster>, List<BookingStatusViewModel>>(_masterBusiness.GetAllBookingStatus());
            foreach (BookingStatusViewModel ovm in orderstatusListVM)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = ovm.Description,
                    Value = ovm.Code.ToString(),
                    Selected = false
                });
            }
            status_obj.BookingsstatusList = selectListItem;
            ViewBag.UserName = _commonBusiness.GetUA().UserName;
            return View(status_obj); 
        }
        #endregion Index

        #region GetAllBookings
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
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
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
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

        #region GetBookingsDetails
        /// <summary>
        /// to display in table 
        /// changing booking object to list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetBookingsDetails(string id)
        {
            try
            {

                List<BookingsViewModel> bookingdetailslist = null;
                BookingsViewModel booking = Mapper.Map<Bookings, BookingsViewModel>(_bookingsBusiness.GetBookings(Int32.Parse(id)));
                bookingdetailslist = new List<BookingsViewModel>();
                bookingdetailslist.Add(booking);
                return JsonConvert.SerializeObject(new { Result = "OK", Records = bookingdetailslist });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion GetBookingsDetails

        #region  UpdateBookings

        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string UpdateBookings(BookingsViewModel bookingObj)
        {
            if (ModelState.IsValid)
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;
                try
                {
                    bookingObj.logDetails = new LogDetailsViewModel();
                    bookingObj.logDetails.UpdatedBy = _commonBusiness.GetUA().UserName;
                    bookingObj.logDetails.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_bookingsBusiness.UpdateBookings(Mapper.Map<BookingsViewModel, Bookings>(bookingObj)));
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }

                if (OperationsStatusViewModelObj.StatusCode == 1)
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "Error", Record = OperationsStatusViewModelObj });
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }

        #endregion UpdateQuotations

        #region InsertEventsLog
        /// <summary>
        /// Insert into event logs after notyfing the customer by mail if checked true
        /// </summary>
        /// <param name="bookingObj"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string InsertEventsLog(BookingsViewModel bookingObj)
        {
            if (ModelState.IsValid)
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;
                bool Mailstatus = false;
                try
                {
                    bookingObj.EventsLogViewObj.commonObj = new LogDetailsViewModel();
                    bookingObj.EventsLogViewObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                    bookingObj.EventsLogViewObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                    if (bookingObj.mailViewModelObj.CustomerEmail != "" && bookingObj.EventsLogViewObj.CustomerNotifiedYN == true)
                    {
                        bookingObj.mailViewModelObj.OrderNo = bookingObj.EventsLogViewObj.ParentID;
                        bookingObj.mailViewModelObj.OrderComment = bookingObj.EventsLogViewObj.Comment;

                        Mail _mail = new Mail();
                        using (StreamReader reader = new StreamReader(HttpContext.Server.MapPath("~/PartyEcTemplates/Notifications.html")))
                        {
                            _mail.Body = reader.ReadToEnd();
                        }
                        _mail.Body = _mail.Body.Replace("{CustomerName}", bookingObj.mailViewModelObj.CustomerName);
                        _mail.Body = _mail.Body.Replace("{Message}", bookingObj.EventsLogViewObj.Comment);
                        _mail.IsBodyHtml = true;
                        _mail.Subject = "Booking No:" + bookingObj.BookingNo;
                        _mail.To = bookingObj.mailViewModelObj.CustomerEmail;
                        Mailstatus = _mailBusiness.SendMail(_mail);
                        bookingObj.EventsLogViewObj.CustomerNotifiedYN = Mailstatus;
                    }
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_masterBusiness.InsertEventsLog(Mapper.Map<EventsLogViewModel, EventsLog>(bookingObj.EventsLogViewObj)));
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }

                if (OperationsStatusViewModelObj.StatusCode == 1)
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "Error", Record = OperationsStatusViewModelObj });
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }


        #endregion InsertEventsLog

        #region GetEventsLog
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetEventsLog(string ID)
        {
            try
            {
                List<EventsLogViewModel> eventsLogList = Mapper.Map<List<EventsLog>, List<EventsLogViewModel>>(_masterBusiness.GetEventsLog(int.Parse(ID), "Bookings"));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = eventsLogList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion GetEventsLog 

        #region SendBooking

        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public async Task<string> SendBooking(BookingsViewModel bookingsObj)
        {
            if (ModelState.IsValid)
            {
                bool sendsuccess;


                try
                {
                    bookingsObj.logDetails = new LogDetailsViewModel();
                    bookingsObj.logDetails.CreatedBy = _commonBusiness.GetUA().UserName;
                    bookingsObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();

                    sendsuccess = await _bookingsBusiness.BookingsEmail(Mapper.Map<BookingsViewModel, Bookings>(bookingsObj));
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }

                if (sendsuccess)
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Message = "Quotation Send Sucessfully" });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "Error", Message = "Quotation Sending Failed" });
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }


        #endregion SendBooking



        #region ChangeButtonStyle
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "List":
                    ToolboxViewModelObj.backbtn.Visible = false;
                    ToolboxViewModelObj.sendbtn.Visible = false;
                    break;
                case "Edit_List":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    ToolboxViewModelObj.sendbtn.Visible = true;
                    ToolboxViewModelObj.sendbtn.Disable = true;
                    break;
                case "Send":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    ToolboxViewModelObj.sendbtn.Visible = true;
                    ToolboxViewModelObj.sendbtn.Event = "SendBookingsMail()";
                    ToolboxViewModelObj.sendbtn.Title = "Send";
                    break;

                default:
                    return Content("Nochange");
            }
            return PartialView("_ToolboxView", ToolboxViewModelObj);
        }


        #endregion ChangeButtonStyle
    }
}