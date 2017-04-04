﻿using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PartyEC.RepositoryServices.Services
{
    public class OrderRepository:IOrderRepository
    {
        Const ConstObj = new Const();
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        private IAttributeToSetLinksRepository _attributeSetLinkRepository;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public OrderRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;

        }
        #endregion DataBaseFactory
        public List<Order> GetAllOrderHeader()
        {
            List<Order> OrderHeaderList = null;
            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[GetAllOrderHeaders]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                OrderHeaderList = new List<Order>();
                                while (sdr.Read())
                                {
                                    Order orderObj = new Order();
                                    {
                                        orderObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : orderObj.ID);
                                        orderObj.OrderNo = sdr["OrderNo"].ToString();
                                        orderObj.OrderRev = sdr["OrderRev"].ToString();
                                        orderObj.OrderDate =sdr["OrderDate"].ToString()!=""?DateTime.Parse(sdr["OrderDate"].ToString()).ToString("dd-MMM-yyyy") :orderObj.OrderDate;
                                        orderObj.CustomerName =sdr["CustomerName"].ToString();
                                        orderObj.ContactNo =sdr["ContactNo"].ToString();
                                        orderObj.TotalOrderAmt = (sdr["TotalOrderAmt"].ToString() != "" ? float.Parse(sdr["TotalOrderAmt"].ToString()) : orderObj.ID);
                                        orderObj.OrderStatus = sdr["OrderStatus"].ToString();

                                    }
                                    OrderHeaderList.Add(orderObj);
                                }
                            }//if
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return OrderHeaderList;
        }

        public Order GetSalesStatistics(int CustomerID, DateTime CurrentDate)
        {
            Order orderObj=null;
            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[GetSalesStatistics]";
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;
                        cmd.Parameters.Add("@CurrentDate", SqlDbType.Date).Value = CurrentDate;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    orderObj = new Order();
                                    {
                                        orderObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : orderObj.ID);
                                        orderObj.LifeTimeSales = (sdr["LifeTimeSales"].ToString()!=""? sdr["LifeTimeSales"].ToString():orderObj.LifeTimeSales);
                                        orderObj.AverageSales = (sdr["AverageSales"].ToString() != "" ? sdr["AverageSales"].ToString() : orderObj.AverageSales);
                                        orderObj.LastMonthSales = (sdr["LastMonthSales"].ToString() != "" ? sdr["LastMonthSales"].ToString() : orderObj.LastMonthSales);
                                    }
                                 }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return orderObj;
        }


        public List<Order> GetOrderSummary(int CustomerID)
        {
            List<Order> OrderHeaderList = null;
            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[GetOrderSummary]";
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                OrderHeaderList = new List<Order>();
                                while (sdr.Read())
                                {
                                    Order orderObj = new Order();
                                    {
                                        orderObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : orderObj.ID);
                                        orderObj.OrderNo = (sdr["OrderNo"].ToString()!=""? sdr["OrderNo"].ToString() : orderObj.OrderNo);
                                        orderObj.OrderDate = (sdr["OrderDate"].ToString()!=""? DateTime.Parse(sdr["OrderDate"].ToString()).ToString("dd-MMM-yyyy"):orderObj.OrderDate);
                                        orderObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : orderObj.CustomerID);
                                        orderObj.TotalOrderAmt = sdr["TotalOrderAmt"].ToString() != "" ? float.Parse(sdr["TotalOrderAmt"].ToString()) : orderObj.TotalOrderAmt;
                                        orderObj.BillFirstName = (sdr["BillFirstName"].ToString()!=""? sdr["BillFirstName"].ToString():orderObj.BillFirstName);
                                        orderObj.BillMidName = (sdr["BillMidName"].ToString() != "" ? sdr["BillMidName"].ToString() : orderObj.BillMidName);
                                        orderObj.BillLastName = (sdr["BillLastName"].ToString() != "" ? sdr["BillLastName"].ToString() : orderObj.BillLastName);
                                        orderObj.ShipFirstName= (sdr["ShipFirstName"].ToString() != "" ? sdr["ShipFirstName"].ToString() : orderObj.ShipFirstName);
                                        orderObj.ShipMidName = (sdr["ShipMidName"].ToString() != "" ? sdr["ShipMidName"].ToString() : orderObj.ShipMidName);
                                        orderObj.ShipLastName = (sdr["ShipLastName"].ToString() != "" ? sdr["ShipLastName"].ToString() : orderObj.ShipLastName);
                                    }
                                    OrderHeaderList.Add(orderObj);
                                }
                            }//if
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return OrderHeaderList;
        }
    }
}