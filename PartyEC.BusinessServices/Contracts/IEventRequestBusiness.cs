using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.BusinessServices.Contracts
{
    public interface IEventRequestBusiness
    {
        OperationsStatus InsertEventRequests(EventRequests eventObj);
        List<EventRequests> GetAllEventRequests();
        EventRequests GetEventRequest(int EventRequestsID);
        List<EventRequests> GetEventsLog(int EventRequestsID);

        OperationsStatus UpdateEventRequests(EventRequests eventObj);
        OperationsStatus InsertEventsLog(EventRequests eventObj);

        
    }
}
