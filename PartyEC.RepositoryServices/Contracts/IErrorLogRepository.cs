﻿using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.RepositoryServices.Contracts
{
    public interface IErrorLogRepository
    {
            OperationsStatus InsertErrorLog(ErrorLog ErrorObj);
    }
}