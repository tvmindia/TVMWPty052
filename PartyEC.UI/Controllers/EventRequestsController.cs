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

        public EventRequestsController(IEventRequestBusiness eventRequestsBusiness, ICommonBusiness commonBusiness, ICustomerBusiness customerBusiness)
        {
            _eventRequestsBusiness = eventRequestsBusiness;           
            _commonBusiness = commonBusiness;
            _customerBusiness = customerBusiness;
        }
        #endregion Constructor_Injection
        // GET: EventRequests
        public ActionResult Index()
        {

            return View();
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
                EventRequestsViewModel attribute = Mapper.Map<EventRequests, EventRequestsViewModel>(_eventRequestsBusiness.GetEventRequest(Int32.Parse(ID), Mapper.Map<OperationsStatusViewModel, OperationsStatus>(operationsStatus)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = attribute });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }


        #endregion GetEventRequestsByID

        //UpdateEventR_CommercialInfo
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
    }
}