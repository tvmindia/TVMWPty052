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
    public class ShipmentRepository:IShipmentRepository
    {
        Const constObj = new Const();

        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public ShipmentRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        #endregion DataBaseFactory

        public List<Shipment> GetAllShipmentHeader()
        {
            List<Shipment> ShipmentHeaderList = null;
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
                        cmd.CommandText = "[GetAllShipmentHeaders]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                ShipmentHeaderList = new List<Shipment>();
                                while (sdr.Read())
                                {
                                    Shipment _shipmentObj = new Shipment();
                                    {
                                        _shipmentObj.ID= (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _shipmentObj.ID);
                                        _shipmentObj.OrderID = (sdr["OrderID"].ToString() != "" ? int.Parse(sdr["OrderID"].ToString()) : _shipmentObj.OrderID);
                                        _shipmentObj.ShipmentNo = sdr["ShipmentNo"].ToString();
                                        _shipmentObj.ShipmentDateString = (DateTime.Parse(sdr["ShipmentDate"].ToString())).ToString("dd-MMM-yyyy");
                                        _shipmentObj.DeliveredDate = sdr["DeliveredDate"].ToString();
                                        _shipmentObj.DeliveredBy = sdr["DeliveredBy"].ToString() ;
                                    }
                                    ShipmentHeaderList.Add(_shipmentObj);
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

            return ShipmentHeaderList;
        }

        public List<ShipmentDetail> GetAllShipmentDetail(int ID)
        {
            List<ShipmentDetail> ShipmentDetailList = null;
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
                        cmd.CommandText = "[GetAllShipmentDetails]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                ShipmentDetailList = new List<ShipmentDetail>();
                                while (sdr.Read())
                                {
                                    ShipmentDetail _shipmentDetailObj = new ShipmentDetail();
                                    {
                                        _shipmentDetailObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _shipmentDetailObj.ID);
                                        _shipmentDetailObj.ShipmentID = (sdr["ShipmentID"].ToString() != "" ? int.Parse(sdr["ShipmentID"].ToString()) : _shipmentDetailObj.ShipmentID);
                                        _shipmentDetailObj.OrderItemID = (sdr["OrderItemID"].ToString() != "" ? int.Parse(sdr["OrderItemID"].ToString()) : _shipmentDetailObj.ShipmentID);
                                        _shipmentDetailObj.ShippedQty = (sdr["ShippedQty"].ToString() != "" ? int.Parse(sdr["ShippedQty"].ToString()) : _shipmentDetailObj.ShippedQty);
                                        _shipmentDetailObj.OrderDetailObj.ProductSpecXML = sdr["ProductSpecXML"].ToString();
                                    }
                                    ShipmentDetailList.Add(_shipmentDetailObj);
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

            return ShipmentDetailList;
        }
    }
}