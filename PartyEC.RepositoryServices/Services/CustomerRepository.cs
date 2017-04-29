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
        Const constObj = new Const();
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
                                        _customerObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _customerObj.ID);
                                        _customerObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _customerObj.Name);
                                        _customerObj.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : _customerObj.Email);
                                        _customerObj.Mobile = (sdr["Mobile"].ToString() != "" ? sdr["Mobile"].ToString() : _customerObj.Mobile);
                                        _customerObj.Language = (sdr["Language"].ToString() != "" ? sdr["Language"].ToString() : _customerObj.Language);
                                        _customerObj.Gender = (sdr["Gender"].ToString() != "" ? sdr["Gender"].ToString() : _customerObj.Gender);
                                        _customerObj.ProfileImageID = (sdr["ProfileImageID"].ToString() != "" ? Guid.Parse(sdr["ProfileImageID"].ToString()) : _customerObj.ProfileImageID);
                                        _customerObj.OrdersCount = (sdr["OrdersCount"].ToString() != "" ? int.Parse(sdr["OrdersCount"].ToString()) : _customerObj.OrdersCount);
                                        _customerObj.BookingsCount = (sdr["BookingsCount"].ToString() != "" ? int.Parse(sdr["BookingsCount"].ToString()) : _customerObj.BookingsCount);
                                        _customerObj.QuotationsCount = (sdr["QuotationsCount"].ToString() != "" ? int.Parse(sdr["QuotationsCount"].ToString()) : _customerObj.QuotationsCount);
                                        _customerObj.OrdersCountHistory = (sdr["OrdersCountHistory"].ToString() != "" ? int.Parse(sdr["OrdersCountHistory"].ToString()) : _customerObj.OrdersCountHistory);
                                        _customerObj.BookingsCountHistory = (sdr["BookingsCountHistory"].ToString() != "" ? int.Parse(sdr["BookingsCountHistory"].ToString()) : _customerObj.BookingsCountHistory);
                                        _customerObj.QuotationsCountHistory = (sdr["QuotationsCountHistory"].ToString() != "" ? int.Parse(sdr["QuotationsCountHistory"].ToString()) : _customerObj.QuotationsCountHistory);
                                        _customerObj.IsActive = bool.Parse(sdr["ActiveYN"].ToString());
                                   
                                        _customerObj.Address = (sdr["CustomerAddress"].ToString() != "" ? sdr["CustomerAddress"].ToString() : _customerObj.Address);
                                        _customerObj.logDetailsObj = new LogDetails();
                                        _customerObj.logDetailsObj.CreatedDate= (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _customerObj.logDetailsObj.CreatedDate);

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
                                    mycustomer.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : mycustomer.ID);
                                    mycustomer.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : mycustomer.Name);
                                    mycustomer.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : mycustomer.Email);
                                    mycustomer.Mobile = (sdr["Mobile"].ToString() != "" ? sdr["Mobile"].ToString() : mycustomer.Mobile);
                                    mycustomer.Language = (sdr["Language"].ToString() != "" ? sdr["Language"].ToString() : mycustomer.Language);
                                    mycustomer.Gender = (sdr["Gender"].ToString() != "" ? sdr["Gender"].ToString() : mycustomer.Gender);
                                    mycustomer.ProfileImageID = (sdr["ProfileImageID"].ToString() != "" ? Guid.Parse(sdr["ProfileImageID"].ToString()) : mycustomer.ProfileImageID);
                                    mycustomer.OrdersCount = (sdr["OrdersCount"].ToString() != "" ? int.Parse(sdr["OrdersCount"].ToString()) : mycustomer.OrdersCount);
                                    mycustomer.BookingsCount = (sdr["BookingsCount"].ToString() != "" ? int.Parse(sdr["BookingsCount"].ToString()) : mycustomer.BookingsCount);
                                    mycustomer.QuotationsCount = (sdr["QuotationsCount"].ToString() != "" ? int.Parse(sdr["QuotationsCount"].ToString()) : mycustomer.QuotationsCount);
                                    mycustomer.OrdersCountHistory = (sdr["OrdersCountHistory"].ToString() != "" ? int.Parse(sdr["OrdersCountHistory"].ToString()) : mycustomer.OrdersCountHistory);
                                    mycustomer.BookingsCountHistory = (sdr["BookingsCountHistory"].ToString() != "" ? int.Parse(sdr["BookingsCountHistory"].ToString()) : mycustomer.BookingsCountHistory);
                                    mycustomer.QuotationsCountHistory = (sdr["QuotationsCountHistory"].ToString() != "" ? int.Parse(sdr["QuotationsCountHistory"].ToString()) : mycustomer.QuotationsCountHistory);
                                    mycustomer.IsActive = bool.Parse(sdr["ActiveYN"].ToString());
                                    mycustomer.customerAddress = new CustomerAddress();
                                    mycustomer.customerAddress.Address = (sdr["Address"].ToString() != "" ? sdr["Address"].ToString() : mycustomer.customerAddress.Address);
                                    mycustomer.customerAddress.City = (sdr["City"].ToString() != "" ? sdr["City"].ToString() : mycustomer.customerAddress.City);
                                    mycustomer.customerAddress.StateProvince = (sdr["StateProvince"].ToString() != "" ? sdr["StateProvince"].ToString() : mycustomer.customerAddress.StateProvince);
                                    mycustomer.customerAddress.country = new Country();
                                    mycustomer.customerAddress.country.Name = (sdr["CountryName"].ToString() != "" ? sdr["CountryName"].ToString() : mycustomer.customerAddress.country.Name);
                                    mycustomer.logDetailsObj = new LogDetails();
                                    mycustomer.logDetailsObj.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : mycustomer.logDetailsObj.CreatedDate);
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

        public OperationsStatus CustomerEnableORDisable(Customer customer)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null;
                SqlParameter OutFlag = null;

                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[ActivateORDeactivateCustomer]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customer.ID;
                        cmd.Parameters.Add("@ActivateFlag", SqlDbType.Bit).Value = customer.IsActive;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = customer.logDetailsObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.SmallDateTime).Value = customer.logDetailsObj.UpdatedDate;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        OutFlag = cmd.Parameters.Add("@OutFlag", SqlDbType.Bit);
                        OutFlag.Direction = ParameterDirection.Output;
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "updation Not Successfull!";
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "updation Successfull!";
                                operationsStatusObj.ReturnValues = OutFlag.Value;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                operationsStatusObj.StatusMessage =ex.Message;
                throw ex;
            }

            return operationsStatusObj;
        }

        public OperationsStatus InsertUpdateCustomerAddress(Customer customer)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null;
                SqlParameter addressID = null;
               

                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        switch(customer.customerAddress.ID)
                        {
                            case 0:
                                cmd.CommandText = "[InsertCustomerAddress]";
                                cmd.CommandType = CommandType.StoredProcedure;
                                
                                cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customer.ID;
                                cmd.Parameters.Add("@Prefix", SqlDbType.NVarChar, 4).Value = customer.customerAddress.Prefix;
                                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value = customer.customerAddress.FirstName;
                                cmd.Parameters.Add("@MidName", SqlDbType.NVarChar, 100).Value = customer.customerAddress.MidName;
                                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value = customer.customerAddress.LastName;
                                cmd.Parameters.Add("@Address", SqlDbType.NVarChar, -1).Value = customer.customerAddress.Address;
                                cmd.Parameters.Add("@City", SqlDbType.NVarChar, -1).Value = customer.customerAddress.City;
                                if (customer.customerAddress.Location !=null)
                                {
                                    cmd.Parameters.Add("@Location", SqlDbType.Int).Value = int.Parse(customer.customerAddress.Location);
                                }
                                cmd.Parameters.Add("@CountryCode", SqlDbType.NVarChar, 3).Value = customer.customerAddress.CountryCode;
                                cmd.Parameters.Add("@StateProvince", SqlDbType.NVarChar, 100).Value = customer.customerAddress.StateProvince;
                                cmd.Parameters.Add("@ContactNo", SqlDbType.NVarChar, 20).Value = customer.customerAddress.ContactNo;
                                cmd.Parameters.Add("@BillDefaultYN", SqlDbType.Bit, 20).Value = customer.customerAddress.BillDefaultYN;
                                cmd.Parameters.Add("@ShipDefaultYN", SqlDbType.Bit, 20).Value = customer.customerAddress.ShipDefaultYN;
                                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = customer.customerAddress.logDetailsObj.CreatedBy;
                                cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 250).Value = customer.customerAddress.logDetailsObj.CreatedDate;
                                statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                                addressID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                                addressID.Direction= ParameterDirection.Output;
                                statusCode.Direction = ParameterDirection.Output;
                                cmd.ExecuteNonQuery();
                                operationsStatusObj = new OperationsStatus();
                                switch (statusCode.Value.ToString())
                                {
                                    case "0":
                                        // not Successfull                                
                                        operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                        operationsStatusObj.StatusMessage = constObj.InsertFailure;
                                        break;
                                    case "1":
                                        //Insert Successfull
                                        operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                        operationsStatusObj.StatusMessage = constObj.InsertSuccess;
                                        operationsStatusObj.ReturnValues = addressID.Value.ToString();
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                cmd.CommandText = "[UpdateCustomerAddress]";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = customer.customerAddress.ID;
                                cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customer.ID;
                                cmd.Parameters.Add("@Prefix", SqlDbType.NVarChar, 4).Value = customer.customerAddress.Prefix;
                                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value = customer.customerAddress.FirstName;
                                cmd.Parameters.Add("@MidName", SqlDbType.NVarChar, 100).Value = customer.customerAddress.MidName;
                                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value = customer.customerAddress.LastName;
                                cmd.Parameters.Add("@Address", SqlDbType.NVarChar, -1).Value = customer.customerAddress.Address;
                                cmd.Parameters.Add("@City", SqlDbType.NVarChar, -1).Value = customer.customerAddress.City;
                                if (customer.customerAddress.Location != null)
                                {
                                    cmd.Parameters.Add("@Location", SqlDbType.Int).Value = int.Parse(customer.customerAddress.Location);
                                }
                                cmd.Parameters.Add("@CountryCode", SqlDbType.NVarChar, 3).Value = customer.customerAddress.CountryCode;
                                cmd.Parameters.Add("@StateProvince", SqlDbType.NVarChar, 100).Value = customer.customerAddress.StateProvince;
                                cmd.Parameters.Add("@ContactNo", SqlDbType.NVarChar, 20).Value = customer.customerAddress.ContactNo;
                                cmd.Parameters.Add("@BillDefaultYN", SqlDbType.Bit, 20).Value = customer.customerAddress.BillDefaultYN;
                                cmd.Parameters.Add("@ShipDefaultYN", SqlDbType.Bit, 20).Value = customer.customerAddress.ShipDefaultYN;
                                cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = customer.customerAddress.logDetailsObj.UpdatedBy;
                                cmd.Parameters.Add("@UpdatedDate", SqlDbType.SmallDateTime).Value = customer.customerAddress.logDetailsObj.UpdatedDate;
                                statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                                statusCode.Direction = ParameterDirection.Output;
                               
                                cmd.ExecuteNonQuery();
                                operationsStatusObj = new OperationsStatus();
                                switch (statusCode.Value.ToString())
                                {
                                    case "0":
                                        // not Successfull                                
                                        operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                        operationsStatusObj.StatusMessage = constObj.UpdateFailure;
                                        break;
                                    case "1":
                                        //update Successfull
                                        operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                        operationsStatusObj.StatusMessage = constObj.UpdateSuccess;
                                        

                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                      
                      
                       
                    }
                }
            }
            catch (Exception ex)
            {
                operationsStatusObj.StatusMessage = ex.Message;
                throw ex;
            }

            return operationsStatusObj;
        
    }

        public List<CustomerAddress> GetAllCustomerAddresses(int CustomerID)
        {
            List<CustomerAddress> CustomerAddresslist = null;
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
                        cmd.CommandText = "[GetAllAddressesByCustomer]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = CustomerID;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                CustomerAddresslist = new List<CustomerAddress>();
                                while (sdr.Read())
                                {
                                    CustomerAddress _customerAddresObj = new CustomerAddress();
                                    {
                                        _customerAddresObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _customerAddresObj.ID);
                                        _customerAddresObj.CustomerID= (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : _customerAddresObj.CustomerID);
                                        _customerAddresObj.Prefix = (sdr["Prefix"].ToString() != "" ? sdr["Prefix"].ToString() : _customerAddresObj.Prefix);
                                        _customerAddresObj.FirstName = (sdr["FirstName"].ToString() != "" ? sdr["FirstName"].ToString() : _customerAddresObj.FirstName);
                                        _customerAddresObj.MidName = (sdr["MidName"].ToString() != "" ? sdr["MidName"].ToString() : _customerAddresObj.MidName);
                                        _customerAddresObj.LastName = (sdr["LastName"].ToString() != "" ? sdr["LastName"].ToString() : _customerAddresObj.LastName);
                                        _customerAddresObj.Address = (sdr["Address"].ToString() != "" ? sdr["Address"].ToString() : _customerAddresObj.Address);
                                        _customerAddresObj.Location = (sdr["Location"].ToString() != "" ? sdr["Location"].ToString() : _customerAddresObj.Location);
                                        _customerAddresObj.City = (sdr["City"].ToString() != "" ? sdr["City"].ToString() : _customerAddresObj.City);
                                        _customerAddresObj.country = new Country();
                                        _customerAddresObj.country.Code = (sdr["CountryCode"].ToString() != "" ? sdr["CountryCode"].ToString() : _customerAddresObj.country.Code);
                                        _customerAddresObj.country.Name = (sdr["CountryName"].ToString() != "" ? sdr["CountryName"].ToString() : _customerAddresObj.country.Name);
                                        _customerAddresObj.StateProvince = (sdr["StateProvince"].ToString() != "" ? sdr["StateProvince"].ToString() : _customerAddresObj.StateProvince);
                                        _customerAddresObj.ContactNo = (sdr["ContactNo"].ToString() != "" ? sdr["ContactNo"].ToString() : _customerAddresObj.ContactNo);
                                        _customerAddresObj.BillDefaultYN = (sdr["BillDefaultYN"].ToString() != "" ? bool.Parse(sdr["BillDefaultYN"].ToString()) : _customerAddresObj.BillDefaultYN);
                                        _customerAddresObj.ShipDefaultYN = (sdr["ShipDefaultYN"].ToString() != "" ? bool.Parse(sdr["ShipDefaultYN"].ToString()) : _customerAddresObj.ShipDefaultYN);
                                        _customerAddresObj.logDetailsObj = new LogDetails();
                                        _customerAddresObj.logDetailsObj.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _customerAddresObj.logDetailsObj.CreatedDate);
                                    }
                                    CustomerAddresslist.Add(_customerAddresObj);
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
            return CustomerAddresslist;
        }

        public CustomerAddress GetAddressByAddress(int AddressID)
        {
            CustomerAddress _customerAddresObj = null;
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
                        cmd.CommandText = "[GetAddressByCustomerAddress]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = AddressID;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                             if (sdr.Read())
                                {
                                    _customerAddresObj = new CustomerAddress();
                                    {
                                        _customerAddresObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _customerAddresObj.ID);
                                        _customerAddresObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : _customerAddresObj.CustomerID);
                                        _customerAddresObj.Prefix = (sdr["Prefix"].ToString() != "" ? sdr["Prefix"].ToString() : _customerAddresObj.Prefix);
                                        _customerAddresObj.FirstName = (sdr["FirstName"].ToString() != "" ? sdr["FirstName"].ToString() : _customerAddresObj.FirstName);
                                        _customerAddresObj.MidName = (sdr["MidName"].ToString() != "" ? sdr["MidName"].ToString() : _customerAddresObj.MidName);
                                        _customerAddresObj.LastName = (sdr["LastName"].ToString() != "" ? sdr["LastName"].ToString() : _customerAddresObj.LastName);
                                        _customerAddresObj.Address = (sdr["Address"].ToString() != "" ? sdr["Address"].ToString() : _customerAddresObj.Address);
                                        _customerAddresObj.City = (sdr["City"].ToString() != "" ? sdr["City"].ToString() : _customerAddresObj.City);
                                        _customerAddresObj.country = new Country();
                                        _customerAddresObj.country.Code = (sdr["CountryCode"].ToString() != "" ? sdr["CountryCode"].ToString() : _customerAddresObj.country.Code);
                                        _customerAddresObj.country.Name = (sdr["CountryName"].ToString() != "" ? sdr["CountryName"].ToString() : _customerAddresObj.country.Name);
                                        _customerAddresObj.StateProvince = (sdr["StateProvince"].ToString() != "" ? sdr["StateProvince"].ToString() : _customerAddresObj.StateProvince);
                                        _customerAddresObj.ContactNo = (sdr["ContactNo"].ToString() != "" ? sdr["ContactNo"].ToString() : _customerAddresObj.ContactNo);
                                        _customerAddresObj.BillDefaultYN = (sdr["BillDefaultYN"].ToString() != "" ? bool.Parse(sdr["BillDefaultYN"].ToString()) : _customerAddresObj.BillDefaultYN);
                                        _customerAddresObj.ShipDefaultYN = (sdr["ShipDefaultYN"].ToString() != "" ? bool.Parse(sdr["ShipDefaultYN"].ToString()) : _customerAddresObj.ShipDefaultYN);
                                        _customerAddresObj.logDetailsObj = new LogDetails();
                                        _customerAddresObj.logDetailsObj.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _customerAddresObj.logDetailsObj.CreatedDate);
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
            return _customerAddresObj;
        }

        public OperationsStatus DeleteAddress(CustomerAddress customerAddress)
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
                        cmd.CommandText = "[DeleteCustomerAddressByAddress]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerAddress.CustomerID;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = customerAddress.ID;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.DeleteFailure;
                                break;
                            case "1":
                                //Deletion Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.DeleteSuccess;
                                
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                operationsStatusObj.StatusMessage = ex.Message;
                throw ex;
            }

            return operationsStatusObj;
        }

        public OperationsStatus InsertCustomer(Customer customer)
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
                        cmd.CommandText = "[InsertCustomer]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = customer.Name;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar, -1).Value = customer.Email;
                        cmd.Parameters.Add("@Gender", SqlDbType.NVarChar,1).Value = customer.Gender;
                        cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 50).Value = customer.Mobile;
                        cmd.Parameters.Add("@Language", SqlDbType.NVarChar, 10).Value = customer.Language;
                        cmd.Parameters.Add("@ProfileImageID", SqlDbType.UniqueIdentifier).Value = customer.ProfileImageID; 

                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = customer.logDetailsObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = customer.logDetailsObj.CreatedDate;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.InsertFailure;
                                break;
                            case "1":
                                //Insert Successfull
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
                operationsStatusObj.StatusMessage = ex.Message;
                throw ex;
            }

            return operationsStatusObj;
        }

        public OperationsStatus UpdateCustomer(Customer customer)
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
                        cmd.CommandText = "[UpdateCustomer]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@ID", SqlDbType.NVarChar, 250).Value = customer.ID;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = customer.Name;
                       // cmd.Parameters.Add("@Email", SqlDbType.NVarChar, -1).Value = customer.Email;
                        cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 1).Value = customer.Gender;
                        cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 50).Value = customer.Mobile;
                        cmd.Parameters.Add("@Language", SqlDbType.NVarChar, 10).Value = customer.Language;
                        cmd.Parameters.Add("@ProfileImageID", SqlDbType.UniqueIdentifier).Value = customer.ProfileImageID;

                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = customer.logDetailsObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.SmallDateTime).Value = customer.logDetailsObj.UpdatedDate;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.UpdateFailure;
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.UpdateSuccess;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                operationsStatusObj.StatusMessage = ex.Message;
                throw ex;
            }

            return operationsStatusObj;
        }

        public Customer GetCustomerVerification(string Email)
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
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar,-1).Value = Email;
                        cmd.CommandText = "[GetCustomerVerification]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    mycustomer = new Customer();
                                    mycustomer.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : mycustomer.ID);
                                    mycustomer.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : mycustomer.Name);
                                    mycustomer.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : mycustomer.Email);
                                    mycustomer.Mobile = (sdr["Mobile"].ToString() != "" ? sdr["Mobile"].ToString() : mycustomer.Mobile);
                                    mycustomer.Language = (sdr["Language"].ToString() != "" ? sdr["Language"].ToString() : mycustomer.Language);
                                    mycustomer.Gender = (sdr["Gender"].ToString() != "" ? sdr["Gender"].ToString() : mycustomer.Gender);
                                    mycustomer.ProfileImageID = (sdr["ProfileImageID"].ToString() != "" ? Guid.Parse(sdr["ProfileImageID"].ToString()) : mycustomer.ProfileImageID);
                                    mycustomer.OrdersCount = (sdr["OrdersCount"].ToString() != "" ? int.Parse(sdr["OrdersCount"].ToString()) : mycustomer.OrdersCount);
                                    mycustomer.BookingsCount = (sdr["BookingsCount"].ToString() != "" ? int.Parse(sdr["BookingsCount"].ToString()) : mycustomer.BookingsCount);
                                    mycustomer.QuotationsCount = (sdr["QuotationsCount"].ToString() != "" ? int.Parse(sdr["QuotationsCount"].ToString()) : mycustomer.QuotationsCount);
                                    mycustomer.OrdersCountHistory = (sdr["OrdersCountHistory"].ToString() != "" ? int.Parse(sdr["OrdersCountHistory"].ToString()) : mycustomer.OrdersCountHistory);
                                    mycustomer.BookingsCountHistory = (sdr["BookingsCountHistory"].ToString() != "" ? int.Parse(sdr["BookingsCountHistory"].ToString()) : mycustomer.BookingsCountHistory);
                                    mycustomer.QuotationsCountHistory = (sdr["QuotationsCountHistory"].ToString() != "" ? int.Parse(sdr["QuotationsCountHistory"].ToString()) : mycustomer.QuotationsCountHistory);
                                    mycustomer.IsActive = bool.Parse(sdr["ActiveYN"].ToString());
                                    mycustomer.customerAddress = new CustomerAddress();
                                    mycustomer.customerAddress.Address = (sdr["Address"].ToString() != "" ? sdr["Address"].ToString() : mycustomer.customerAddress.Address);
                                    mycustomer.customerAddress.City = (sdr["City"].ToString() != "" ? sdr["City"].ToString() : mycustomer.customerAddress.City);
                                    mycustomer.customerAddress.StateProvince = (sdr["StateProvince"].ToString() != "" ? sdr["StateProvince"].ToString() : mycustomer.customerAddress.StateProvince);
                                    mycustomer.customerAddress.country = new Country();
                                    mycustomer.customerAddress.country.Name = (sdr["CountryName"].ToString() != "" ? sdr["CountryName"].ToString() : mycustomer.customerAddress.country.Name);
                                    mycustomer.logDetailsObj = new LogDetails();
                                    mycustomer.logDetailsObj.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : mycustomer.logDetailsObj.CreatedDate);
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

        public OperationsStatus SetDefaultAddress(int CustomerID, int AddressID)
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
                        cmd.CommandText = "[SetDefaultAddress]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.NVarChar, 250).Value = CustomerID;
                        cmd.Parameters.Add("@AddressID", SqlDbType.NVarChar, 250).Value = AddressID;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.UpdateFailure;
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.UpdateSuccess;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                operationsStatusObj.StatusMessage = ex.Message;
                throw ex;
            }

            return operationsStatusObj;

        }


        #endregion Methods


    }
}