﻿using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.RepositoryServices.Contracts
{
    public interface IQuotationsRepository
    {
        List<Quotations> GetAllQuotations();
        Quotations GetQuotations(int QuotationsID);
        OperationsStatus UpdateQuotations(Quotations quotationsObj);

        //App
        List<Quotations> GetCustomerQuotations(int CustomerID,bool Ishistory);
        OperationsStatus InsertQuotations(Quotations quotationsObj);
    }
}
