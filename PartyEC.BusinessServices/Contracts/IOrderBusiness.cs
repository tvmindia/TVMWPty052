using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Contracts
{
    public interface IOrderBusiness
    {
        List<Order> GetAllOrderHeader();
        Order GetSalesStatistics(int CustomerID, DateTime CurrentDate);
        List<Order> GetOrderSummary(int CustomerID);
    }
}