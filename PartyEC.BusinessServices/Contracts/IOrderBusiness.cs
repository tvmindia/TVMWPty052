using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Contracts
{
    public interface IOrderBusiness
    {
        List<Order> GetOrderHeader();
        List<OrderDetail> GetAllOrdersList(string ID);
        List<Order> GetLatestOrders();
        Order GetSalesStatistics(int CustomerID, DateTime CurrentDate);
        List<Order> GetOrderSummaryList(int CustomerID);
        Order GetOrderDetails(string ID);
        OperationsStatus CancelOrder(Order orderObj);
        OperationsStatus UpdateBillingDetails(Order orderObj);
        OperationsStatus UpdateShipingDetails(Order orderObj);
        OperationsStatus InsertReviseOrder(OrderDetail orderDetailObj);
        Order GetOrderSummary(int ID);
        List<OrderDetail> GetOrderExcludesShip(int ID);
        //For App
        List<Order> GetCustomerOrders(int CustomerID,bool Ishistory);
        OperationsStatus InsertOrder(Order orderObj);
        OperationsStatus InsertOrderForApp(Order orderObj);
        OperationsStatus UpdateOrderPaymentStatus(Order orderObj);
    }
}