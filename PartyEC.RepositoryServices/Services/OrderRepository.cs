using PartyEC.DataAccessObject.DTO;
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
                                        orderObj.TotalOrderAmt = (sdr["TotalOrderAmt"].ToString() != "" ? float.Parse(sdr["TotalOrderAmt"].ToString()) : 0);
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
        public List<Order> GetAllOrdersList(string ID)
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
                        cmd.CommandText = "[GetOrdersListDetails]";
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
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
                                        orderObj.OrderDetailID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : orderObj.ID);
                                        orderObj.ProductSpecXML = sdr["ProductName"].ToString() + "||" + sdr["ProductSpecXML"].ToString();
                                        orderObj.ItemStatus = sdr["ItemStatus"].ToString();
                                        orderObj.Qty = (sdr["Qty"].ToString() != "" ? int.Parse(sdr["Qty"].ToString()) : 0);
                                        orderObj.Price = (sdr["Price"].ToString() != "" ? float.Parse(sdr["Price"].ToString()) : 0);
                                        orderObj.TaxAmt = (sdr["TaxAmt"].ToString() != "" ? float.Parse(sdr["TaxAmt"].ToString()) : 0);
                                        orderObj.ShippingAmt = (sdr["ShippingAmt"].ToString() != "" ? float.Parse(sdr["ShippingAmt"].ToString()) : 0);
                                        orderObj.DiscountAmt = (sdr["DiscountAmt"].ToString() != "" ? float.Parse(sdr["DiscountAmt"].ToString()) : 0);
                                        orderObj.Total = (sdr["Total"].ToString() != "" ? float.Parse(sdr["Total"].ToString()) : 0);
                                        orderObj.SubTotal = (sdr["SubTotal"].ToString() != "" ? float.Parse(sdr["SubTotal"].ToString()) : 0);

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
        public Order GetOrderDetails(string ID)
        {
            Order orderObj = new Order();
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
                        cmd.CommandText = "[GetOrderDetails]";
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    orderObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : orderObj.ID);
                                    orderObj.OrderNo = sdr["OrderNo"].ToString();
                                    orderObj.OrderRev = sdr["OrderRev"].ToString();
                                    orderObj.ShippingLocationName = sdr["ShippingLocationName"].ToString();
                                    orderObj.SourceIP = sdr["SourceIP"].ToString();
                                    orderObj.OrderDate = sdr["OrderDate"].ToString() != "" ? DateTime.Parse(sdr["OrderDate"].ToString()).ToString("dd-MMM-yyyy") : "";
                                    orderObj.CustomerID = sdr["CustomerID"].ToString()!=null? int.Parse(sdr["CustomerID"].ToString()): orderObj.CustomerID;
                                    orderObj.CustomerName = sdr["CustomerName"].ToString();
                                    orderObj.ContactNo = sdr["ContactNo"].ToString();
                                    orderObj.CustomerEmail = sdr["CustomerEmail"].ToString();
                                    orderObj.ProfileImageID = sdr["ProfileImageID"].ToString();
                                    orderObj.CustomerURL = sdr["CustomerImgURL"].ToString();
                                    orderObj.BillFirstName = sdr["BillFirstName"].ToString();
                                    orderObj.BillLastName = sdr["BillLastName"].ToString();
                                    orderObj.BillMidName = sdr["BillMidName"].ToString();
                                    orderObj.BillAddress = sdr["BillAddress"].ToString();
                                    orderObj.BillCity = sdr["BillCity"].ToString();
                                    orderObj.BillContactNo = sdr["BillContactNo"].ToString();
                                    orderObj.BillCountryCode = sdr["BillCountryCode"].ToString();
                                    orderObj.BillStateProvince = sdr["BillStateProvince"].ToString();
                                    orderObj.BillPrefix = sdr["BillPrefix"].ToString();
                                    orderObj.ShipAddress = sdr["ShipAddress"].ToString();
                                    orderObj.ShipCity = sdr["ShipCity"].ToString();
                                    orderObj.ShipContactNo = sdr["ShipContactNo"].ToString();
                                    orderObj.ShipCountryCode = sdr["ShipCountryCode"].ToString();
                                    orderObj.ShipFirstName = sdr["ShipFirstName"].ToString();
                                    orderObj.ShipLastName = sdr["ShipLastName"].ToString();
                                    orderObj.ShipMidName = sdr["ShipMidName"].ToString();
                                    orderObj.ShipStateProvince = sdr["ShipStateProvince"].ToString();
                                    orderObj.TotalShippingAmt = sdr["TotalShippingAmt"].ToString()!=""?float.Parse(sdr["TotalShippingAmt"].ToString()):orderObj.TotalShippingAmt;
                                    orderObj.PaymentType = sdr["PaymentType"].ToString();
                                    orderObj.PaymentStatus = sdr["PaymentStatus"].ToString();
                                    orderObj.CurrencyCode = sdr["CurrencyCode"].ToString();
                                    orderObj.TotalOrderAmt = (sdr["TotalOrderAmt"].ToString() != "" ? float.Parse(sdr["TotalOrderAmt"].ToString()) : orderObj.ID);
                                    orderObj.OrderStatus = sdr["OrderStatus"].ToString();
                                }
                                }
                            }//if
                        }
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return orderObj;
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

        public Order GetOrderSummery(int ID)
        {
            Order orderObj = null;
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
                        cmd.CommandText = "[GetOrderSummeryFigure]";
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    orderObj = new Order();
                                    {
                                        orderObj.SubTotalOrderSummery = sdr["SubTotalOrderSummery"].ToString();
                                        orderObj.DiscountAmtOrderSummery = sdr["DiscountAmtOrderSummery"].ToString();
                                        orderObj.TaxAmtOrderSummery = sdr["TaxAmtOrderSummery"].ToString();
                                        orderObj.ShippingCostOrderSummery = sdr["ShippingCostOrderSummery"].ToString();
                                        orderObj.GrandTotalOrderSummery = sdr["GrandTotalOrderSummery"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
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

        public OperationsStatus UpdateBillingDetails(Order orderObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null;

                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[UpdateOrderHeaderBillingDetails]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID",SqlDbType.Int).Value=orderObj.ID;
                        cmd.Parameters.Add("@BillFirstName", SqlDbType.NVarChar, 100).Value = orderObj.BillFirstName;
                        cmd.Parameters.Add("@BillMidName", SqlDbType.NVarChar, 100).Value = orderObj.BillMidName;
                        cmd.Parameters.Add("@BillLastName", SqlDbType.NVarChar,100).Value = orderObj.BillLastName;
                        cmd.Parameters.Add("@BillAddress", SqlDbType.NVarChar, -1).Value = orderObj.BillAddress;
                        cmd.Parameters.Add("@BillCity", SqlDbType.NVarChar, 100).Value = orderObj.BillCity;
                        cmd.Parameters.Add("@BillContactNo", SqlDbType.NVarChar, 20).Value = orderObj.BillContactNo;
                        cmd.Parameters.Add("@BillCountryCode", SqlDbType.NVarChar,3).Value = orderObj.BillCountryCode;
                        cmd.Parameters.Add("@BillStateProvince", SqlDbType.NVarChar, 100).Value = orderObj.BillStateProvince;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Not Successfull!";
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Successfull!";
                                break;
                            default:
                                break;
                        }



                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return operationsStatusObj;
        }
        public OperationsStatus UpdateShipingDetails(Order orderObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null;

                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[UpdateOrderHeaderShipingDetails]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = orderObj.ID;
                        cmd.Parameters.Add("@ShipFirstName", SqlDbType.NVarChar, 100).Value = orderObj.ShipFirstName;
                        cmd.Parameters.Add("@ShipMidName", SqlDbType.NVarChar, 100).Value = orderObj.ShipMidName;
                        cmd.Parameters.Add("@ShipLastName", SqlDbType.NVarChar, 100).Value = orderObj.ShipLastName;
                        cmd.Parameters.Add("@ShipAddress", SqlDbType.NVarChar, -1).Value = orderObj.ShipAddress;
                        cmd.Parameters.Add("@ShipCity", SqlDbType.NVarChar, 100).Value = orderObj.ShipCity;
                        cmd.Parameters.Add("@ShipContactNo", SqlDbType.NVarChar, 20).Value = orderObj.ShipContactNo;
                        cmd.Parameters.Add("@ShipCountryCode", SqlDbType.NVarChar, 3).Value = orderObj.ShipCountryCode;
                        cmd.Parameters.Add("@ShipStateProvince", SqlDbType.NVarChar, 100).Value = orderObj.ShipStateProvince;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Not Successfull!";
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Successfull!";
                                break;
                            default:
                                break;
                        }



                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return operationsStatusObj;
        }

        public List<Order> GetCustomerOrders(int CustomerID)
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
                        cmd.CommandText = "[GetCustomerOrders]";
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
                                        orderObj.OrderNo = sdr["OrderNo"].ToString();
                                        orderObj.OrderRev = sdr["OrderRev"].ToString();

                                        orderObj.OrderDate = sdr["OrderDate"].ToString() != "" ? DateTime.Parse(sdr["OrderDate"].ToString()).ToString("dd-MMM-yyyy") : orderObj.OrderDate;
                                       
                                        
                                        orderObj.TotalOrderAmt = (sdr["TotalOrderAmt"].ToString() != "" ? float.Parse(sdr["TotalOrderAmt"].ToString()) : 0);
                                        orderObj.OrderStatus = sdr["OrderStatus"].ToString();

 
                                       
                                        orderObj.SourceIP = sdr["SourceIP"].ToString();
                                        orderObj.OrderDate = sdr["OrderDate"].ToString() != "" ? DateTime.Parse(sdr["OrderDate"].ToString()).ToString("dd-MMM-yyyy") : "";
                                        orderObj.CustomerID = sdr["CustomerID"].ToString() != null ? int.Parse(sdr["CustomerID"].ToString()) : orderObj.CustomerID;
                                      
                                     
                                        orderObj.BillFirstName = sdr["BillFirstName"].ToString();
                                        orderObj.BillLastName = sdr["BillLastName"].ToString();
                                        orderObj.BillMidName = sdr["BillMidName"].ToString();
                                        orderObj.BillAddress = sdr["BillAddress"].ToString();
                                        orderObj.BillCity = sdr["BillCity"].ToString();
                                        orderObj.BillContactNo = sdr["BillContactNo"].ToString();
                                        orderObj.BillCountryCode = sdr["BillCountryCode"].ToString();
                                        orderObj.BillStateProvince = sdr["BillStateProvince"].ToString();
                                        orderObj.BillPrefix = sdr["BillPrefix"].ToString();
                                        orderObj.ShipAddress = sdr["ShipAddress"].ToString();
                                        orderObj.ShipCity = sdr["ShipCity"].ToString();
                                        orderObj.ShipContactNo = sdr["ShipContactNo"].ToString();
                                        orderObj.ShipCountryCode = sdr["ShipCountryCode"].ToString();
                                        orderObj.ShipFirstName = sdr["ShipFirstName"].ToString();
                                        orderObj.ShipLastName = sdr["ShipLastName"].ToString();
                                        orderObj.ShipMidName = sdr["ShipMidName"].ToString();
                                        orderObj.ShipStateProvince = sdr["ShipStateProvince"].ToString();
                                        orderObj.TotalShippingAmt = sdr["TotalShippingAmt"].ToString() != "" ? float.Parse(sdr["TotalShippingAmt"].ToString()) : orderObj.TotalShippingAmt;
                                        orderObj.PaymentType = sdr["PaymentType"].ToString();
                                        orderObj.PaymentStatus = sdr["PaymentStatus"].ToString();
                                        orderObj.CurrencyCode = sdr["CurrencyCode"].ToString();
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
    }
}