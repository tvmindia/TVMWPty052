﻿using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Contracts
{
    public interface IOrderBusiness
    {
        List<Order> GetAllOrderHeader();
        List<Order> GetAllOrdersList(string ID);
        Order GetSalesStatistics(int CustomerID, DateTime CurrentDate);
        List<Order> GetOrderSummary(int CustomerID);
        Order GetOrderDetails(string ID);
        OperationsStatus UpdateBillingDetails(Order orderObj);
        OperationsStatus UpdateShipingDetails(Order orderObj);
        Order GetOrderSummery(int ID);

        //For App
        List<Order> GetCustomerOrders(int CustomerID,bool Ishistory);
    }
}