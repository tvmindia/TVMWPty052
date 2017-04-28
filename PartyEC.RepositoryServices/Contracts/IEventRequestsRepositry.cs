using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyEC.DataAccessObject.DTO;

namespace PartyEC.RepositoryServices.Contracts
{
    public interface IEventRequestsRepositry
    {
        OperationsStatus InsertEventRequests(EventRequests eventObj);
        List<EventRequests> GetAllEventRequests();
        EventRequests GetEventRequest(int EventRequestsID);
        List<EventRequests> GetEventsLog(int EventRequestsID);

        OperationsStatus UpdateEventRequests(EventRequests eventObj);
        OperationsStatus InsertEventsLog(EventRequests eventObj);
        
    }
}
