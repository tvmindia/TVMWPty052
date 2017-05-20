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
        private IQuotationsBusiness _quotationBusiness;
        private ICommonBusiness _commonBusiness;
        private ICart_WishlistBusiness _Cart_WishlistBusiness;
        public OrderBusiness(IOrderRepository orderRepository, IQuotationsBusiness quotationBusiness,ICommonBusiness commonBusiness,ICart_WishlistBusiness Cart_WishlistBusiness)
        {
            _orderRepository = orderRepository;
            _quotationBusiness = quotationBusiness;
            _commonBusiness = commonBusiness;
            _Cart_WishlistBusiness = Cart_WishlistBusiness;
        }
        public List<Order> GetAllOrderHeader()
        {

            return _orderRepository.GetAllOrderHeader();
        }
        public List<Order> GetLatestOrders()
        {
            return _orderRepository.GetLatestOrders();
        }
        public List<Order> GetOrderHeader()
        {
            List<Order> OrderList = _orderRepository.GetAllOrderHeader();
            if(OrderList!=null)
            {
                return OrderList.GroupBy(r => r.OrderNo)
                    .Select(g => g.OrderByDescending(i => i.ID).First())
                    .ToList();
            }
            else
            {
                return OrderList;
            }
            
        }
        public List<OrderDetail> GetAllOrdersList(string ID)
        {
          //  return _orderRepository.GetAllOrdersList(ID);
            List<OrderDetail> OrderList = null;
            try
            {
                OrderList = _orderRepository.GetAllOrdersList(ID);

                for(int i=0;i<OrderList.Count;i++)
                {
                    if (OrderList[i].ProductSpecXML1 != null && OrderList[i].ProductSpecXML1!="")
                    { 
                        OrderList[i].AttributeValues = _commonBusiness.GetAttributeValueFromXML(OrderList[i].ProductSpecXML1);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OrderList;


          
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
        public Order GetOrderSummary(int ID)
        {
            Order orderObj = null;
            try
            {
                orderObj = _orderRepository.GetOrderSummary(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return orderObj;
        }
        public List<Order> GetOrderSummaryList(int CustomerID)
        {
            return _orderRepository.GetOrderSummaryList(CustomerID);
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
        public OperationsStatus  CancelOrder(Order orderObj)
        {
            return _orderRepository.CancelOrder(orderObj);
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
        public OperationsStatus InsertOrderHeaderForApp(Order orderObj)
        {
            return _orderRepository.InsertOrderHeaderForApp(orderObj);
        }
        public OperationsStatus InsertOrderHeader(Order orderObj)
        {
            return _orderRepository.InsertOrderHeader(orderObj);
        }
        public OperationsStatus InsertOrderDetail(OrderDetail orderDetailObj)
        {
            return _orderRepository.InsertOrderDetail(orderDetailObj);
        }

        public OperationsStatus InsertOrderForApp(Order orderObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                ShoppingCart cartObj = new ShoppingCart();
                cartObj.CustomerID = orderObj.CustomerID;
                cartObj.LocationID = orderObj.shippingLocationID;
                cartObj.logDetails = new LogDetails();
                cartObj.logDetails.CreatedDate = orderObj.commonObj.CreatedDate;

                List<OrderDetail> OrderDetaillist = new List<OrderDetail>() ;
                List<ShoppingCart> cartlist = null;
                cartlist = _Cart_WishlistBusiness.GetCustomerShoppingCart(cartObj);
                for (int i = 0; i < cartlist.Count; i++)
                {
                    if (cartlist[i].FreeDeliveryYN == true)
                    {   
                        cartlist[i].ShippingCharge = 0;
                    }
                }


                foreach (var i in cartlist)
                {
                    OrderDetail orderDetailObj = new OrderDetail();

                    orderDetailObj.ItemID = i.ItemID;
                    orderDetailObj.ProductID = i.ProductID;
                    orderDetailObj.ProductSpecXML = i.ProductSpecXML;//check if the value passed is correct
                    orderDetailObj.ItemStatus = "1";
                    orderDetailObj.Qty = i.Qty;
                    orderDetailObj.Price = i.CurrentPrice;
                    orderDetailObj.TaxAmt = orderDetailObj.TaxAmt;
                    orderDetailObj.DiscountAmt = i.Discount;
                    orderDetailObj.CartId = i.ID;//For Cart Status Update
                    if (i.StockAvailableYN == true)
                    {
                        OrderDetaillist.Add(orderDetailObj);
                    }
                    else
                    {
                        _Cart_WishlistBusiness.RemoveProductFromCart(i.ID);//Updating Shopping Cart Status as DisCard.
                    }

                }
                if(OrderDetaillist.Count>0)
                {
                    orderObj.OrderDetailsList = OrderDetaillist;
                    orderObj.OrderStatus = "1";
                    orderObj.CurrencyCode = "QAR";
                    operationsStatusObj = InsertOrderHeaderForApp(orderObj);
                    if (operationsStatusObj.StatusCode == 1)
                    {
                        if (orderObj.OrderDetailsList != null)
                        {
                            foreach (var i in orderObj.OrderDetailsList)
                            {
                                i.OrderID = int.Parse(operationsStatusObj.ReturnValues.ToString());
                                i.commonObj = orderObj.commonObj;
                                InsertOrderDetail(i);
                                _Cart_WishlistBusiness.UpdateShoppingCartStatus(i.CartId);//Updating Shopping Cart Status as Purchased.
                            }
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
                            operationsStatusObj = InsertOrderDetail(i);
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