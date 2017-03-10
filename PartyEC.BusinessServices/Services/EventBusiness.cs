using PartyEC.BusinessServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;

namespace PartyEC.BusinessServices.Services
{
    public class EventBusiness : IEventBusiness
    {
        #region ConstructorInjection

        private IEventRepositry _eventRepository;

        public EventBusiness(IEventRepositry eventRepository)
        {
            _eventRepository = eventRepository;
        }
        #endregion ConstructorInjection

        #region Method

        public List<Event> GetAllEvents()
        {
            List<Event> Eventlist = null;
            try
            {
                Eventlist = _eventRepository.GetAllEvents();

            }
            catch (Exception)
            {

            }
            return Eventlist;
        }

        public Event GetEvent(int EventID, OperationsStatus Status)
        {
            Event myEvent = null;
            try
            {
                myEvent = _eventRepository.GetEvent(EventID, Status);

            }
            catch (Exception)
            {

            }
            return myEvent;
        }

        public OperationsStatus InsertEvent(Event EventObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                operationsStatusObj= _eventRepository.InsertEvent(EventObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return operationsStatusObj;
        }

        public OperationsStatus UpdateEvent(Event EventObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                operationsStatusObj = _eventRepository.UpdateEvent(EventObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return operationsStatusObj;
        }

        public OperationsStatus DeleteEvent(int EventID, OperationsStatus Status)
        {
            try
            {
                _eventRepository.DeleteEvent(EventID, Status);
            }
            catch (Exception)
            {
            }
            return Status;
        }

        #endregion Method
    }
}