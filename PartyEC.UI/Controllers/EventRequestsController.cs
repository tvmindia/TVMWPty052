using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.Models;
using AutoMapper;
using Newtonsoft.Json;

namespace PartyEC.UI.Controllers
{
    public class EventRequestsController : Controller
    {
        #region Constructor_Injection

        IEventRequestBusiness _eventRequestsBusiness;
        ICustomerBusiness _customerBusiness;
        ICommonBusiness _commonBusiness;
        IMasterBusiness _masterBusiness;

        public EventRequestsController(IEventRequestBusiness eventRequestsBusiness, ICommonBusiness commonBusiness, ICustomerBusiness customerBusiness, IMasterBusiness masterBusiness)
        {
            _eventRequestsBusiness = eventRequestsBusiness;           
            _commonBusiness = commonBusiness;
            _customerBusiness = customerBusiness;
            _masterBusiness = masterBusiness;
        }
        #endregion Constructor_Injection
        // GET: EventRequests
        public ActionResult Index()
        {
            EventRequestsViewModel ordrsat_obj = new EventRequestsViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();

            List<OrderStatusViewModel> orderstatusListVM = Mapper.Map<List<OrderStatusMaster>, List<OrderStatusViewModel>>(_masterBusiness.GetAllOrderStatus());
            foreach (OrderStatusViewModel ovm in orderstatusListVM)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = ovm.Description,
                    Value = ovm.Code.ToString(),
                    Selected = false
                });
            }
            ordrsat_obj.OrderList = selectListItem;
            return View(ordrsat_obj);
           
        }

        #region GetAllEventRequests
        [HttpGet]
        public string GetAllEventRequests(EventRequestsViewModel eventObj)
        {
            try
            {
                List<EventRequestsViewModel> eventList = Mapper.Map<List<EventRequests>, List<EventRequestsViewModel>>(_eventRequestsBusiness.GetAllEventRequests());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = eventList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion  GetAllEvents

        #region GetEventRequestsByID

        [HttpGet]
        public string GetEventRequest(string ID)
        {
            try
            {
                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                EventRequestsViewModel EventRequest = Mapper.Map<EventRequests, EventRequestsViewModel>(_eventRequestsBusiness.GetEventRequest(Int32.Parse(ID), Mapper.Map<OperationsStatusViewModel, OperationsStatus>(operationsStatus)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = EventRequest });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }


        #endregion GetEventRequestsByID

        #region GetEventsLogByID

        [HttpGet]
        public string GetEventsLog(string ID)
        {
            try
            {
                List<EventRequestsViewModel> eventsLogList = Mapper.Map<List<EventRequests>, List<EventRequestsViewModel>>(_eventRequestsBusiness.GetEventsLog(Int32.Parse(ID)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = eventsLogList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }


        #endregion GetEventsLogByID

        //UpdateEventRequest
        #region UpdateEventRequest

        [HttpPost]
        public string UpdateEventRequests(EventRequestsViewModel EventObj)
        {
            if (ModelState.IsValid)
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;
                try
                {
                    EventObj.logDetailsObj = new LogDetailsViewModel();
                    EventObj.logDetailsObj.UpdatedBy = _commonBusiness.GetUA().UserName;
                    EventObj.logDetailsObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_eventRequestsBusiness.UpdateEventRequests(Mapper.Map<EventRequestsViewModel, EventRequests>(EventObj)));
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


        #endregion UpdateeventRequest
        //UpdateEventsLog
        #region UpdateEventsLog

        [HttpPost]
        public string UpdateEventsLog(EventRequestsViewModel EventObj)
        {
            if (ModelState.IsValid)
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;
                try
                {
                    EventObj.logDetailsObj = new LogDetailsViewModel();
                    EventObj.logDetailsObj.CreatedBy = _commonBusiness.GetUA().UserName;
                    EventObj.logDetailsObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_eventRequestsBusiness.InsertEventsLog(Mapper.Map<EventRequestsViewModel, EventRequests>(EventObj)));
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


        #endregion UpdateEventsLog

        #region ChangeButtonStyle
        [HttpGet]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "Edit":
                    //ToolboxViewModelObj.deletebtn.Visible = true;
                    //ToolboxViewModelObj.deletebtn.Event = "clickdelete()";
                    //ToolboxViewModelObj.deletebtn.Title = "Delete";

                    //ToolboxViewModelObj.savebtn.Visible = true;
                    //ToolboxViewModelObj.savebtn.Event = "clicksave()";
                    //ToolboxViewModelObj.savebtn.Title = "Save";

                    //ToolboxViewModelObj.resetbtn.Visible = true;
                    //ToolboxViewModelObj.resetbtn.Event = "btnreset()";

                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";

                    break;
                case "Add":
                    //ToolboxViewModelObj.deletebtn.Visible = true;
                    //ToolboxViewModelObj.deletebtn.Disable = true;
                    //ToolboxViewModelObj.savebtn.Visible = true;
                    //ToolboxViewModelObj.savebtn.Event = "clicksave()";
                    //ToolboxViewModelObj.savebtn.Title = "Save";

                    //ToolboxViewModelObj.resetbtn.Visible = true;
                    //ToolboxViewModelObj.resetbtn.Event = "btnreset()";
                    //ToolboxViewModelObj.resetbtn.Title = "Reset";

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