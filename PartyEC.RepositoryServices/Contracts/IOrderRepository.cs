using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.RepositoryServices.Contracts
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrderHeader();
        List<Order> GetAllOrdersList(string ID);
        Order GetSalesStatistics(int CustomerID,DateTime CurrentDate);
        List<Order> GetOrderSummary(int CustomerID);
        Order GetOrderDetails(string ID);
        Order GetOrderSummery(int ID);
        OperationsStatus UpdateBillingDetails(Order orderObj);
        OperationsStatus UpdateShipingDetails(Order orderObj);
        //For App
        List<Order> GetCustomerOrders(int CustomerID);

    }
}