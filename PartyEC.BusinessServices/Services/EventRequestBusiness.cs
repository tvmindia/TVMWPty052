using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Services
{
    public class EventRequestBusiness: IEventRequestBusiness
    {
        private IEventRequestsRepositry _eventRequestsRepositry;

        public EventRequestBusiness(IEventRequestsRepositry eventRequestsRepositry)
        {
            _eventRequestsRepositry = eventRequestsRepositry;
        }

        #region Methods

        public List<EventRequests> GetAllEventRequests()
        {
            List<EventRequests> RequestLists = null;
            try
            {
                RequestLists = _eventRequestsRepositry.GetAllEventRequests();

            }
            catch (Exception)
            {

            }
            return RequestLists;

        }

        public EventRequests GetEventRequest(int EventRequestsID, OperationsStatus Status)
        {
            EventRequests EventRequestsObj = null;
            try
            {
                EventRequestsObj = _eventRequestsRepositry.GetEventRequest(EventRequestsID, Status);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return EventRequestsObj;
        }

        #endregion Methods
    }
}