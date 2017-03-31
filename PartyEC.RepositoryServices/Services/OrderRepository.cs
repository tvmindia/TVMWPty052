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
                                        orderObj.OrderDate =sdr["OrderDate"].ToString()!=""?DateTime.Parse(sdr["OrderDate"].ToString()) :orderObj.OrderDate;
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
    }
}