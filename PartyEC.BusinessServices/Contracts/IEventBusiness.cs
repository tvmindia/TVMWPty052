﻿using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.BusinessServices.Contracts
{
  public  interface IEventBusiness
    {
        List<Event> GetAllEvents();
        Event GetEvent(int EventID, OperationsStatus Status);
        OperationsStatus InsertEventTypes(Event EventObj);
        OperationsStatus UpdateEvent(Event EventObj);
        OperationsStatus DeleteEvent(int EventID);
        OperationsStatus InsertImageEvents(Event EventObj);
        OperationsStatus DeleteOtherImage(string imageID,string type);
    }
}
