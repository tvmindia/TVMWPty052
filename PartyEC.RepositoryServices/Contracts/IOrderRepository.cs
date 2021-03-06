﻿using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.RepositoryServices.Contracts
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrderHeader();
        List<OrderDetail> GetAllOrdersList(string ID);
        List<Order> GetLatestOrders();
        Order GetSalesStatistics(int CustomerID,DateTime CurrentDate);
        List<Order> GetOrderSummaryList(int CustomerID);
        Order GetOrderDetails(string ID);
        Order GetOrderSummary(int ID);
        OperationsStatus CancelOrder(Order orderObj);
        OperationsStatus UpdateBillingDetails(Order orderObj);
        OperationsStatus UpdateShipingDetails(Order orderObj);
        OperationsStatus UpdatePayType(Order orderObj);
        OperationsStatus InsertOrderHeaderForApp(Order orderObj);
        OperationsStatus InsertOrderHeader(Order orderObj);
        OperationsStatus InsertOrderDetail(OrderDetail orderDetailObj); 
       // List<Order> GetCustomerOrders(int CustomerID,bool Ishistory);
        List<OrderDetail> GetOrderExcludesShip(int ID);
        OperationsStatus UpdateOrderPaymentStatus(Order orderObj);

    }
}