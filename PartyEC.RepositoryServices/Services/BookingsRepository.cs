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
    public class BookingsRepository:IBookingsRepository
    {
        Const constObj = new Const();
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public BookingsRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        } 
        #endregion DataBaseFactory

        public List<Bookings> GetCustomerBookings(int customerID,bool Ishistory)
        {
            List<Bookings> BookingsList = null;
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
                        cmd.CommandText = "[GetCustomerBookings]";
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerID;
                        cmd.Parameters.Add("@Ishistory", SqlDbType.Bit).Value = Ishistory;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                BookingsList = new List<Bookings>();
                                while (sdr.Read())
                                {
                                    Bookings bookingsObj = new Bookings();
                                    {
                                        bookingsObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : bookingsObj.ID);
                                        bookingsObj.BookingNo = sdr["BookingNo"].ToString();
                                        bookingsObj.ProductID = (sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString()) : bookingsObj.ProductID);
                                        bookingsObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : bookingsObj.CustomerID);
                                        bookingsObj.RequiredDate = (sdr["RequiredDate"].ToString() != "" ? DateTime.Parse(sdr["RequiredDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : bookingsObj.RequiredDate);
                                        bookingsObj.BookingDate = (sdr["BookingDate"].ToString() != "" ? DateTime.Parse(sdr["BookingDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : bookingsObj.BookingDate);
                                        bookingsObj.SourceIP = sdr["SourceIP"].ToString();
                                        bookingsObj.Status = (sdr["Status"].ToString() != "" ? int.Parse(sdr["Status"].ToString()) : bookingsObj.Status);
                                        bookingsObj.Qty = (sdr["Qty"].ToString() != "" ? int.Parse(sdr["Qty"].ToString()) : bookingsObj.Qty);
                                        bookingsObj.Price = (sdr["Price"].ToString() != "" ? decimal.Parse(sdr["Price"].ToString()) : bookingsObj.Price);
                                        bookingsObj.AdditionalCharges = (sdr["AdditionalCharges"].ToString() != "" ? decimal.Parse(sdr["AdditionalCharges"].ToString()) : bookingsObj.AdditionalCharges);
                                        bookingsObj.TaxAmt = (sdr["TaxAmt"].ToString() != "" ? decimal.Parse(sdr["TaxAmt"].ToString()) : bookingsObj.TaxAmt);
                                        bookingsObj.DiscountAmt = (sdr["DiscountAmt"].ToString() != "" ? decimal.Parse(sdr["DiscountAmt"].ToString()) : bookingsObj.DiscountAmt);
                                        bookingsObj.Message =sdr["Message"].ToString();
                                        bookingsObj.BillPrefix =sdr["BillPrefix"].ToString();
                                        bookingsObj.BillFirstName = sdr["BillFirstName"].ToString();
                                        bookingsObj.BillMidName = sdr["BillMidName"].ToString();
                                        bookingsObj.BillLastName = sdr["BillLastName"].ToString();
                                        bookingsObj.BillAddress = sdr["BillAddress"].ToString();
                                        bookingsObj.BillCity = sdr["BillCity"].ToString();
                                        bookingsObj.BillCountryCode = sdr["BillCountryCode"].ToString();
                                        bookingsObj.BillStateProvince = sdr["BillStateProvince"].ToString();
                                        bookingsObj.BillContactNo = sdr["BillContactNo"].ToString(); 

                                    }
                                    BookingsList.Add(bookingsObj);
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

            return BookingsList;

        }

        public OperationsStatus InsertBookings(Bookings bookingsObj)
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
                        cmd.CommandText = "[InsertBookings]";
                        cmd.CommandType = CommandType.StoredProcedure;                     
                        cmd.Parameters.Add("@BookingNo", SqlDbType.NVarChar, 20).Value = bookingsObj.BookingNo;
                        cmd.Parameters.Add("@BookingDate", SqlDbType.SmallDateTime).Value = bookingsObj.BookingDate;
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = bookingsObj.ProductID;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = bookingsObj.CustomerID;

                        cmd.Parameters.Add("@RequiredDate", SqlDbType.SmallDateTime).Value = bookingsObj.RequiredDate;
                        cmd.Parameters.Add("@SourceIP", SqlDbType.NVarChar, 50).Value = bookingsObj.SourceIP;
                        cmd.Parameters.Add("@Status", SqlDbType.Int).Value = bookingsObj.Status;

                        cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = bookingsObj.Qty;
                        cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = bookingsObj.Price;
                        cmd.Parameters.Add("@AdditionalCharges", SqlDbType.Decimal).Value = bookingsObj.AdditionalCharges;
                        cmd.Parameters.Add("@TaxAmt", SqlDbType.Decimal).Value = bookingsObj.TaxAmt;
                        cmd.Parameters.Add("@DiscountAmt", SqlDbType.Decimal).Value = bookingsObj.DiscountAmt;
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, -1).Value = bookingsObj.Message;
                        cmd.Parameters.Add("@BillPrefix", SqlDbType.NVarChar, 100).Value = bookingsObj.BillPrefix;
                        cmd.Parameters.Add("@BillFirstName", SqlDbType.NVarChar, 100).Value = bookingsObj.BillFirstName;
                        cmd.Parameters.Add("@BillMidName", SqlDbType.NVarChar, 100).Value = bookingsObj.BillMidName;
                        cmd.Parameters.Add("@BillLastName", SqlDbType.NVarChar, 100).Value = bookingsObj.BillLastName;
                        cmd.Parameters.Add("@BillAddress", SqlDbType.NVarChar, 100).Value = bookingsObj.BillAddress;
                        cmd.Parameters.Add("@BillCity", SqlDbType.NVarChar, 100).Value = bookingsObj.BillCity;
                        cmd.Parameters.Add("@BillCountryCode", SqlDbType.NVarChar, 3).Value = bookingsObj.BillCountryCode;
                        cmd.Parameters.Add("@BillStateProvince", SqlDbType.NVarChar, 100).Value = bookingsObj.BillStateProvince;
                        cmd.Parameters.Add("@BillContactNo", SqlDbType.NVarChar, 20).Value = bookingsObj.BillContactNo;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = bookingsObj.logDetails.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = bookingsObj.logDetails.CreatedDate;
                        statusCode = cmd.Parameters.Add("@StatusOut", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.InsertFailure;
                                break;
                            case "1":
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.InsertSuccess;
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