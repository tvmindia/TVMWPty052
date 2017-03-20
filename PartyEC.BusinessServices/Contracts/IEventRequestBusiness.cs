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
        List<EventRequests> GetAllEventRequests();
        EventRequests GetEventRequest(int EventRequestsID, OperationsStatus Status);
        OperationsStatus UpdateEventRequests(EventRequests eventObj);
    }
}
