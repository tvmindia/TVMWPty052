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
        public Order GetSalesStatistics(int CustomerID, DateTime CurrentDate)
        {
            Order orderObj = null;
            try
            {
                orderObj= _orderRepository.GetSalesStatistics(CustomerID, CurrentDate);
            }
            catch(Exception ex)
            {

            }
            return orderObj;
        }
        public List<Order> GetOrderSummary(int CustomerID)
        {
            return _orderRepository.GetOrderSummary(CustomerID);
        }
    }
}