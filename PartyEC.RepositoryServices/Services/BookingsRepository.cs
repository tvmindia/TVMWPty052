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

        public List<Bookings> GetCustomerBookings(int customerID)
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
    }
}