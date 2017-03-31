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
        Const messages = new Const();

        [HttpPost]
        public object RequestEvent(EventRequests eventRequestObject)
        {
            try
            {
                OperationsStatus operationStatus = _eventRequestBusiness.InsertEventsLog(eventRequestObject);
                if (operationStatus.StatusCode == 0) throw operationStatus.Exception;
                return JsonConvert.SerializeObject(new { Result = true, Records = operationStatus.StatusMessage });
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
                if (EventTypesList.Count == 0) throw new Exception(messages.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = EventTypesList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
    }
}
