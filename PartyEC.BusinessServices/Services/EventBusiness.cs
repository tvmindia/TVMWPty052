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
            throw new NotImplementedException();
        }

        public Event GetEvent(int EventID, OperationsStatus Status)
        {
            throw new NotImplementedException();
        }

        public OperationsStatus InsertEvent(Event EventObj)
        {
            throw new NotImplementedException();
        }

        public OperationsStatus UpdateEvent(Event EventObj)
        {
            throw new NotImplementedException();
        }

        public OperationsStatus DeleteEvent(int EventID, OperationsStatus Status)
        {
            throw new NotImplementedException();
        }

        #endregion Method
    }
}