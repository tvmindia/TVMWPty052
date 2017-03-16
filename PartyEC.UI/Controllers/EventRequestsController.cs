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
        ICommonBusiness _commonBusiness;
        public EventRequestsController(IEventRequestBusiness eventRequestsBusiness, ICommonBusiness commonBusiness)
        {
            _eventRequestsBusiness = eventRequestsBusiness;           
            _commonBusiness = commonBusiness;
        }
        #endregion Constructor_Injection
        // GET: EventRequests
        public ActionResult Index()
        {
            return View();
        }

        //#region GetAllEventRequests
        //[HttpGet]
        //public string GetAllEventRequests(EventRequestsViewModel eventObj)
        //{
        //    try
        //    {
        //        List<EventRequestsViewModel> eventList = Mapper.Map<List<EventRequests>, List<EventRequestsViewModel>>(_eventRequestsBusiness.GetAllEventRequests());
        //        return JsonConvert.SerializeObject(new { Result = "OK", Records = eventList });
        //    }
        //    catch (Exception ex)
        //    {
        //        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}
        //#endregion  GetAllEvents

        //#region GetEventRequestsByID

        //[HttpGet]
        //public string GetEventRequest(string ID)
        //{
        //    try
        //    {
        //        OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
        //        EventRequestsViewModel attribute = Mapper.Map<EventRequests, EventRequestsViewModel>(_eventRequestsBusiness.GetEventRequest(Int32.Parse(ID), Mapper.Map<OperationsStatusViewModel, OperationsStatus>(operationsStatus)));
        //        return JsonConvert.SerializeObject(new { Result = "OK", Records = attribute });
        //    }
        //    catch (Exception ex)
        //    {
        //        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}


        //#endregion GetEventRequestsByID
    }
}