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
                                        _shipmentObj.ShipmentDateString = (sdr["ShipmentDate"].ToString()!=""?((DateTime.Parse(sdr["ShipmentDate"].ToString()).ToString("dd-MMM-yyyy"))) : _shipmentObj.ShipmentDateString);
                                        _shipmentObj.DeliveredDate = (sdr["DeliveredDate"].ToString() != "" ? ((DateTime.Parse(sdr["DeliveredDate"].ToString()).ToString("dd-MMM-yyyy"))) : _shipmentObj.DeliveredDate);
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
                                        _shipmentDetailObj.OrderDetailObj.Qty= (sdr["Qty"].ToString() != "" ? int.Parse(sdr["Qty"].ToString()) : _shipmentDetailObj.OrderDetailObj.Qty);
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

        public OperationsStatus InsertShipmentHeader(Shipment shipmentObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null, ID = null;

                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[InsertShipmentHeader]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = shipmentObj.OrderID;
                        cmd.Parameters.Add("@ShipmentDate", SqlDbType.DateTime).Value = shipmentObj.log.CreatedDate;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = shipmentObj.log.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = shipmentObj.log.CreatedDate;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        ID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                        statusCode.Direction = ParameterDirection.Output;
                        ID.Direction = ParameterDirection.Output;
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
                                operationsStatusObj.ReturnValues = ID.Value;
                                break;
                            default:
                                break;
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
        public OperationsStatus InsertShipmentDetail(ShipmentDetail shipmentDetailObj)
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
                        cmd.CommandText = "[InsertShipmentDetail]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ShipmentID", SqlDbType.Int).Value = shipmentDetailObj.ShipmentID;
                        cmd.Parameters.Add("@OrderItemID", SqlDbType.Int).Value = shipmentDetailObj.OrderItemID;
                        cmd.Parameters.Add("@ShippedQty", SqlDbType.Int).Value = shipmentDetailObj.ShippedQty;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = shipmentDetailObj.log.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = shipmentDetailObj.log.CreatedDate;
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
                throw ex;
            }

            return operationsStatusObj;
        }
    }
}