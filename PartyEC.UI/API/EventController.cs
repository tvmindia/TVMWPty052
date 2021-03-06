﻿using System;
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
using PartyEC.UI.CustomAttributes;

namespace PartyEC.UI.API
{
    [CustomAuthenticationFilterForMobile]
    public class EventController : ApiController
    {
        #region Constructor_Injection

        IEventRequestBusiness _eventRequestBusiness;
        ICommonBusiness _commonBusiness;
        IEventBusiness _eventBusiness;

        public EventController(IEventRequestBusiness eventRequestBusiness, ICommonBusiness commonBusiness, IEventBusiness eventBusiness)
        {
            _eventRequestBusiness = eventRequestBusiness;
            _commonBusiness = commonBusiness;
            _eventBusiness = eventBusiness;
        }
        #endregion Constructor_Injection
        Const constants = new Const();

        [HttpPost]
        public object RequestEvent(EventRequests eventRequestObject)
        {
            try
            {
                eventRequestObject.logDetailsObj = new LogDetails();
                eventRequestObject.logDetailsObj.CreatedBy = constants.AppUser;
                eventRequestObject.logDetailsObj.CreatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatus operationStatus = _eventRequestBusiness.InsertEventRequests(eventRequestObject);
                if (operationStatus.StatusCode == 0) throw operationStatus.Exception;
                return JsonConvert.SerializeObject(new { Result = true, Records = operationStatus.StatusMessage });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object GetEventRequestStatus(EventRequests eventObj)
        {
            try
            {

                List<EventRequestsAppViewModel> EventList = Mapper.Map<List<EventRequests>,List<EventRequestsAppViewModel>>(_eventRequestBusiness.GetEventRequestsOfCustomer(eventObj.CustomerID));
                if (EventList.Count==0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = EventList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object GetEventTypesAndRelatedCategories()
        {
            try
            {
                List<EventTypeAppViewModel> EventTypesList = Mapper.Map<List<Event>, List<EventTypeAppViewModel>>(_eventBusiness.GetAllEvents());
                if (EventTypesList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = EventTypesList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }


    }
}
