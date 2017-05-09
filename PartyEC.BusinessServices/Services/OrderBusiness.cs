using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Services
{
    public class OrderBusiness:IOrderBusiness
    {
        private IOrderRepository _orderRepository;

        public OrderBusiness(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Order> GetAllOrderHeader()
        {

            return _orderRepository.GetAllOrderHeader();
        }
        public List<Order> GetOrderHeader()
        {
            return _orderRepository.GetAllOrderHeader().GroupBy(r => r.OrderNo)
                    .Select(g => g.OrderByDescending(i => i.ID).First())
                    .ToList();
        }
        public List<OrderDetail> GetAllOrdersList(string ID)
        {
            return _orderRepository.GetAllOrdersList(ID);
        }
        public Order GetSalesStatistics(int CustomerID, DateTime CurrentDate)
        {
            Order orderObj = null;
            try
            {
                orderObj= _orderRepository.GetSalesStatistics(CustomerID, CurrentDate);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return orderObj;
        }
        public Order GetOrderSummery(int ID)
        {
            Order orderObj = null;
            try
            {
                orderObj = _orderRepository.GetOrderSummery(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return orderObj;
        }
        public List<Order> GetOrderSummary(int CustomerID)
        {
            return _orderRepository.GetOrderSummary(CustomerID);
        }
        public Order GetOrderDetails(string ID)
        {
            return _orderRepository.GetOrderDetails(ID);
        }
        public OperationsStatus UpdateBillingDetails(Order orderObj)
        {
            return _orderRepository.UpdateBillingDetails(orderObj);
        }
        public OperationsStatus UpdateShipingDetails(Order orderObj)
        {
            return _orderRepository.UpdateShipingDetails(orderObj);
        }
        public OperationsStatus  CancelOrder(int ID)
        {
            return _orderRepository.CancelOrder(ID);
        }
        public List<Order> GetCustomerOrders(int CustomerID,bool Ishistory)
        {
            //return _orderRepository.GetCustomerOrders(CustomerID, Ishistory);
            if (Ishistory)
            {
                return GetOrderHeader().Where(t => t.CustomerID == CustomerID && t.StatusCode ==3).ToList();
            }
           
            else
            {
                return GetOrderHeader().Where(t => t.CustomerID == CustomerID && t.StatusCode !=3).ToList();

            }
        }
        public List<OrderDetail> GetOrderExcludesShip(int ID)
        {
            return _orderRepository.GetOrderExcludesShip(ID).Where(t=>t.QtyShipped!=0).ToList();
        }
        public OperationsStatus InsertReviseOrder(OrderDetail orderDetailsObj)
        {
            OperationsStatus operationStatusObj = null;
            Order orderObj = null;
            List<Order> OrderList = _orderRepository.GetAllOrderHeader().Where(t=>t.ParentOrderID==orderDetailsObj.OrderID).ToList();
            orderObj = _orderRepository.GetOrderDetails(orderDetailsObj.OrderID.ToString());
            orderObj.commonObj = orderDetailsObj.commonObj;
            orderObj.RevNo = OrderList.Count+1;
            orderObj.ParentOrderID = orderDetailsObj.OrderID;
            orderObj.StatusCode = 1;
            orderObj.PayStatusCode = 0;
            operationStatusObj= InsertOrderHeader(orderObj);
            if(operationStatusObj.StatusCode==1)
            {
                if(orderDetailsObj.OrderDetailsList!=null)
                {
                    foreach(var i in orderDetailsObj.OrderDetailsList)
                    {

                        i.OrderID = int.Parse(operationStatusObj.ReturnValues.ToString());
                        i.commonObj= orderDetailsObj.commonObj;
                        InsertOrderDetail(i);
                    }
                }
            }
            return operationStatusObj;
        }
        public OperationsStatus InsertOrderHeader(Order orderObj)
        {
            return _orderRepository.InsertOrderHeader(orderObj);
        }
        public OperationsStatus InsertOrderDetail(OrderDetail orderDetailObj)
        {
            return _orderRepository.InsertOrderDetail(orderDetailObj);
        }

        public OperationsStatus InsertOrder(Order orderObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                operationsStatusObj = InsertOrderHeader(orderObj);
                if (operationsStatusObj.StatusCode == 1)
                {
                    if (orderObj.OrderDetailsList != null)
                    {
                        foreach (var i in orderObj.OrderDetailsList)
                        {
                            i.OrderID = int.Parse(operationsStatusObj.ReturnValues.ToString());
                            i.commonObj = orderObj.commonObj;
                            operationsStatusObj=InsertOrderDetail(i);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return operationsStatusObj;
        }

        public OperationsStatus UpdateOrderPaymentStatus(Order orderObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                operationsStatusObj = _orderRepository.UpdateOrderPaymentStatus(orderObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return operationsStatusObj;
        }
    }
}