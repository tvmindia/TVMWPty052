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
        List<EventRequests> GetAllEventRequests();
        EventRequests GetEventRequest(int EventRequestsID, OperationsStatus Status);
        OperationsStatus UpdateEventRequests(EventRequests eventObj);
    }
}
