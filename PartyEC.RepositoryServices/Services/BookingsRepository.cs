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
                      
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = bookingsObj.ProductID;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = bookingsObj.CustomerID;
                        cmd.Parameters.Add("@RequiredDate", SqlDbType.SmallDateTime).Value = bookingsObj.RequiredDate;
                        cmd.Parameters.Add("@SourceIP", SqlDbType.NVarChar, 50).Value = bookingsObj.SourceIP;
                        cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = bookingsObj.Qty;
                        cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = bookingsObj.Price;
                        cmd.Parameters.Add("@AdditionalCharges", SqlDbType.Decimal).Value = bookingsObj.AdditionalCharges;
                        cmd.Parameters.Add("@TaxAmt", SqlDbType.Decimal).Value = bookingsObj.TaxAmt;
                        cmd.Parameters.Add("@DiscountAmt", SqlDbType.Decimal).Value = bookingsObj.DiscountAmt;
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, -1).Value = bookingsObj.Message;

                        cmd.Parameters.Add("@CustomerAddressID", SqlDbType.Int).Value = bookingsObj.CustomerAddress.ID;
                        cmd.Parameters.Add("@BillLocation", SqlDbType.Int).Value = bookingsObj.CustomerAddress.Location;
                        cmd.Parameters.Add("@BillPrefix", SqlDbType.NVarChar, 100).Value = bookingsObj.CustomerAddress.Prefix;
                        cmd.Parameters.Add("@BillFirstName", SqlDbType.NVarChar, 100).Value = bookingsObj.CustomerAddress.FirstName;
                        cmd.Parameters.Add("@BillMidName", SqlDbType.NVarChar, 100).Value = bookingsObj.CustomerAddress.MidName;
                        cmd.Parameters.Add("@BillLastName", SqlDbType.NVarChar, 100).Value = bookingsObj.CustomerAddress.LastName;
                        cmd.Parameters.Add("@BillAddress", SqlDbType.NVarChar, 100).Value = bookingsObj.CustomerAddress.Address;
                        cmd.Parameters.Add("@BillCity", SqlDbType.NVarChar, 100).Value = bookingsObj.CustomerAddress.City;
                        cmd.Parameters.Add("@BillCountryCode", SqlDbType.NVarChar, 3).Value = bookingsObj.CustomerAddress.CountryCode;
                        cmd.Parameters.Add("@BillStateProvince", SqlDbType.NVarChar, 100).Value = bookingsObj.CustomerAddress.StateProvince;
                        cmd.Parameters.Add("@BillContactNo", SqlDbType.NVarChar, 20).Value = bookingsObj.CustomerAddress.ContactNo;

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

        public List<Bookings> GetAllBookings()
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
                        cmd.CommandText = "[GetAllBookings]";
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
                                        bookingsObj.Status = (sdr["Status"].ToString() != "" ? int.Parse(sdr["Status"].ToString()) : bookingsObj.Status);
                                        bookingsObj.customerObj = new Customer();
                                        bookingsObj.customerObj.Name = (sdr["CustomerName"].ToString() != "" ? sdr["CustomerName"].ToString() : bookingsObj.customerObj.Name);
                                        bookingsObj.customerObj.Mobile = (sdr["ContactNo"].ToString() != "" ? sdr["ContactNo"].ToString() : bookingsObj.customerObj.Mobile);

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

        public Bookings GetBookings(int BookingID)
        {
            Bookings mybookings = null;
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
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = BookingID;
                        cmd.CommandText = "[GetBookings]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    mybookings = new Bookings();
                                    mybookings.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : mybookings.ID);
                                    mybookings.BookingNo = (sdr["BookingNo"].ToString() != "" ? sdr["BookingNo"].ToString() : mybookings.BookingNo);
                                    mybookings.BookingDate = (sdr["BookingDate"].ToString() != "" ? DateTime.Parse(sdr["BookingDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : mybookings.BookingDate);
                                    mybookings.RequiredDate = (sdr["RequiredDate"].ToString() != "" ? DateTime.Parse(sdr["RequiredDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : mybookings.RequiredDate);
                                    mybookings.SourceIP = (sdr["SourceIP"].ToString() != "" ? sdr["SourceIP"].ToString() : mybookings.SourceIP);
                                    mybookings.Status = (sdr["Status"].ToString() != "" ? int.Parse(sdr["Status"].ToString()) : mybookings.Status);
                                    mybookings.StatusText = (sdr["StatusText"].ToString() != "" ? sdr["StatusText"].ToString() : mybookings.StatusText);
                                    mybookings.ProductName = (sdr["ProductName"].ToString() != "" ? sdr["ProductName"].ToString() : mybookings.ProductName);
                                    mybookings.ProductID = (sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString()) : mybookings.ProductID);
                                    mybookings.CustomerID = (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : mybookings.CustomerID);

                                    mybookings.Qty = (sdr["Qty"].ToString() != "" ? int.Parse(sdr["Qty"].ToString()) : mybookings.Qty);
                                    mybookings.Price = (sdr["Price"].ToString() != "" ? decimal.Parse(sdr["Price"].ToString()) : mybookings.Price);
                                    mybookings.AdditionalCharges = (sdr["AdditionalCharges"].ToString() != "" ? decimal.Parse(sdr["AdditionalCharges"].ToString()) : mybookings.AdditionalCharges);
                                    mybookings.DiscountAmt = (sdr["DiscountAmt"].ToString() != "" ? decimal.Parse(sdr["DiscountAmt"].ToString()) : mybookings.DiscountAmt);
                                    mybookings.TaxAmt = (sdr["TaxAmt"].ToString() != "" ? decimal.Parse(sdr["TaxAmt"].ToString()) : mybookings.TaxAmt);
                                    mybookings.SubTotal = (sdr["SubTotal"].ToString() != "" ? decimal.Parse(sdr["SubTotal"].ToString()) : mybookings.SubTotal);
                                    mybookings.Total = (sdr["Total"].ToString() != "" ? decimal.Parse(sdr["Total"].ToString()) : mybookings.Total);
                                    mybookings.GrandTotal = (sdr["GrandTotal"].ToString() != "" ? decimal.Parse(sdr["GrandTotal"].ToString()) : mybookings.GrandTotal);
                                    mybookings.ProductSpecXML = (sdr["ProductSpecXML"].ToString() != "" ? sdr["ProductSpecXML"].ToString() : mybookings.ProductSpecXML);

                                    mybookings.customerObj = new Customer();
                                    mybookings.customerObj.Name = (sdr["CustomerName"].ToString() != "" ? sdr["CustomerName"].ToString() : mybookings.customerObj.Name);
                                    mybookings.customerObj.Mobile = (sdr["CustomerMobile"].ToString() != "" ? sdr["CustomerMobile"].ToString() : mybookings.customerObj.Mobile);
                                    mybookings.customerObj.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : mybookings.customerObj.Email);
                                    mybookings.ImageUrl = (sdr["ImageUrl"].ToString() != "" ? sdr["ImageUrl"].ToString() : mybookings.ImageUrl);

                                    mybookings.Message = (sdr["Message"].ToString() != "" ? sdr["Message"].ToString() : mybookings.Message);
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
            return mybookings;
        }

        public OperationsStatus UpdateBookings(Bookings bookingsObj)
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
                        cmd.CommandText = "[UpdateBookings]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = bookingsObj.ID;
                        cmd.Parameters.Add("@BookingStatus", SqlDbType.Int).Value = bookingsObj.Status;
                        cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = bookingsObj.Price;
                        cmd.Parameters.Add("@AdditionalCharges", SqlDbType.Decimal).Value = bookingsObj.AdditionalCharges;
                        cmd.Parameters.Add("@TaxAmt", SqlDbType.Decimal).Value = bookingsObj.TaxAmt;
                        cmd.Parameters.Add("@DiscountAmt", SqlDbType.Decimal).Value = bookingsObj.DiscountAmt;
                        
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = bookingsObj.logDetails.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.SmallDateTime).Value = bookingsObj.logDetails.UpdatedDate;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.UpdateFailure;
                                break;
                            case "1":
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.UpdateSuccess;
                                operationsStatusObj.ReturnValues = bookingsObj.ID;
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