using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyEC.DataAccessObject.DTO;

namespace PartyEC.RepositoryServices.Contracts
{
   public interface IEventRepositry
    {
        List<Event> GetAllEvents();
        Event GetEvent(int EventID, OperationsStatus Status);
        OperationsStatus InsertEvent(Event EventObj);
        OperationsStatus UpdateEvent(Event EventObj);
        OperationsStatus DeleteEvent(int EventID);
    }
}
