using PartyEC.RepositoryServices.Contracts;
using PartyEC.RepositoryServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using System.Data.SqlClient;
using System.Data;

namespace PartyEC.RepositoryServices.Services
{
    public class CustomerRepository:ICustomerRepository
    {
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public CustomerRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }


        #endregion DataBaseFactory

        #region Methods

        public List<Customer> GetAllCustomers()
        {
            List<Customer> Customerlist = null;
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
                        cmd.CommandText = "[GetAllCustomers]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Customerlist = new List<Customer>();
                                while (sdr.Read())
                                {
                                    Customer _customerObj = new Customer();
                                    {
                                        _customerObj.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : _customerObj.ID);
                                        _customerObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _customerObj.Name);
                                        _customerObj.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : _customerObj.Email);
                                        _customerObj.Mobile = (sdr["Mobile"].ToString() != "" ? sdr["Mobile"].ToString() : _customerObj.Mobile);
                                        _customerObj.Language = (sdr["Language"].ToString() != "" ? sdr["Language"].ToString() : _customerObj.Language);
                                        _customerObj.Gender = (sdr["Gender"].ToString() != "" ? sdr["Gender"].ToString() : _customerObj.Gender);
                                        _customerObj.ProfileImageID = (sdr["ProfileImageID"].ToString() != "" ? Int16.Parse(sdr["ProfileImageID"].ToString()) : _customerObj.ProfileImageID);
                                        _customerObj.OrdersCount = (sdr["OrdersCount"].ToString() != "" ? Int16.Parse(sdr["OrdersCount"].ToString()) : _customerObj.OrdersCount);
                                        _customerObj.BookingsCount = (sdr["BookingsCount"].ToString() != "" ? Int16.Parse(sdr["BookingsCount"].ToString()) : _customerObj.BookingsCount);
                                        _customerObj.QuotationsCount = (sdr["QuotationsCount"].ToString() != "" ? Int16.Parse(sdr["QuotationsCount"].ToString()) : _customerObj.QuotationsCount);
                                        _customerObj.OrdersCountHistory = (sdr["OrdersCountHistory"].ToString() != "" ? Int16.Parse(sdr["OrdersCountHistory"].ToString()) : _customerObj.OrdersCountHistory);
                                        _customerObj.BookingsCountHistory = (sdr["BookingsCountHistory"].ToString() != "" ? Int16.Parse(sdr["BookingsCountHistory"].ToString()) : _customerObj.BookingsCountHistory);
                                        _customerObj.QuotationsCountHistory = (sdr["QuotationsCountHistory"].ToString() != "" ? Int16.Parse(sdr["QuotationsCountHistory"].ToString()) : _customerObj.QuotationsCountHistory);

                                    }
                                    Customerlist.Add(_customerObj);
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
            return Customerlist;
        }

        public Customer GetCustomer(int CustomerID, OperationsStatus Status)
        {
            Customer mycustomer = null;
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
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = CustomerID;
                        cmd.CommandText = "[GetCustomer]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    mycustomer = new Customer();

                                    mycustomer.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : mycustomer.ID);
                                    mycustomer.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : mycustomer.Name);
                                    mycustomer.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : mycustomer.Email);
                                    mycustomer.Mobile = (sdr["Mobile"].ToString() != "" ? sdr["Mobile"].ToString() : mycustomer.Mobile);
                                    mycustomer.Language = (sdr["Language"].ToString() != "" ? sdr["Language"].ToString() : mycustomer.Language);
                                    mycustomer.Gender = (sdr["Gender"].ToString() != "" ? sdr["Gender"].ToString() : mycustomer.Gender);
                                    mycustomer.ProfileImageID = (sdr["ProfileImageID"].ToString() != "" ? Int16.Parse(sdr["ProfileImageID"].ToString()) : mycustomer.ProfileImageID);
                                    mycustomer.OrdersCount = (sdr["OrdersCount"].ToString() != "" ? Int16.Parse(sdr["OrdersCount"].ToString()) : mycustomer.OrdersCount);
                                    mycustomer.BookingsCount = (sdr["BookingsCount"].ToString() != "" ? Int16.Parse(sdr["BookingsCount"].ToString()) : mycustomer.BookingsCount);
                                    mycustomer.QuotationsCount = (sdr["QuotationsCount"].ToString() != "" ? Int16.Parse(sdr["QuotationsCount"].ToString()) : mycustomer.QuotationsCount);
                                    mycustomer.OrdersCountHistory = (sdr["OrdersCountHistory"].ToString() != "" ? Int16.Parse(sdr["OrdersCountHistory"].ToString()) : mycustomer.OrdersCountHistory);
                                    mycustomer.BookingsCountHistory = (sdr["BookingsCountHistory"].ToString() != "" ? Int16.Parse(sdr["BookingsCountHistory"].ToString()) : mycustomer.BookingsCountHistory);
                                    mycustomer.QuotationsCountHistory = (sdr["QuotationsCountHistory"].ToString() != "" ? Int16.Parse(sdr["QuotationsCountHistory"].ToString()) : mycustomer.QuotationsCountHistory);
                                    
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
            return mycustomer;
        }

        #endregion Methods


    }
}