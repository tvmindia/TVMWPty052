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
    public class MasterRepository: IMasterRepository
    {
        Const ConstObj = new Const();

        private IDatabaseFactory _databaseFactory;
        
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>

        public MasterRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public OperationsStatus InsertImage(OtherImages otherimgObj)
        {
            OperationsStatus operrationstatusObj = null;
            try
            {
                SqlParameter outparameter = null;
                SqlParameter outparameterID = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[InsertOtherImage]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ImageType", SqlDbType.NVarChar, 250).Value = otherimgObj.ImageType;
                        cmd.Parameters.Add("@URL", SqlDbType.NVarChar, -1).Value = otherimgObj.URL;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = otherimgObj.LogDetails.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = otherimgObj.LogDetails.CreatedDate;
                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameterID = cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier);
                        outparameter.Direction = ParameterDirection.Output;
                        outparameterID.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operrationstatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                // not Successfull

                                operrationstatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operrationstatusObj.StatusMessage = "Insertion Not Successfull!";

                                break;
                            case "1":
                                //Insert Successfull
                                operrationstatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operrationstatusObj.StatusMessage = "Insertion Successfull!";
                                operrationstatusObj.ReturnValues = Guid.Parse(outparameterID.Value.ToString());
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
            return operrationstatusObj;
        }
        public OperationsStatus InsertEventsLog(EventsLog eventLogObj)
        {
            OperationsStatus operationsStatusObj = null;

            try
            {
                SqlParameter outparameter = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[InsertEventsLog]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@ParentID", SqlDbType.SmallInt).Value = eventLogObj.ParentID;
                        cmd.Parameters.Add("@ParentType", SqlDbType.NVarChar, 20).Value = eventLogObj.ParentType;
                        cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, -1).Value = eventLogObj.Comment;
                        cmd.Parameters.Add("@CustomerNotifiedYN", SqlDbType.Bit).Value = eventLogObj.CustomerNotifiedYN;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 50).Value = eventLogObj.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = eventLogObj.commonObj.CreatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.InsertFailure;
                                break;
                            case "1":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.InsertSuccess;
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
        public List<EventsLog> GetEventsLog(int ID,string ParentType)
        {
            List<EventsLog> Requestslist = null;
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
                        cmd.Parameters.Add("@ParentID", SqlDbType.Int).Value = ID;
                        cmd.Parameters.Add("@ParentType", SqlDbType.NVarChar, 20).Value = ParentType;
                        cmd.CommandText = "[GetEventLogs]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Requestslist = new List<EventsLog>();
                                while (sdr.Read())
                                {
                                    EventsLog _eventRequestsObj = new EventsLog();
                                    {
                                        _eventRequestsObj.Comment = (sdr["Comment"].ToString() != "" ? sdr["Comment"].ToString() : _eventRequestsObj.Comment);
                                        _eventRequestsObj.CommentDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : "");
                                    }
                                    Requestslist.Add(_eventRequestsObj);
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
            return Requestslist;
        }
        public List<OrderStatusMaster> GetAllOrderStatus()
        {
            List<OrderStatusMaster> orderstatusList = null;
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
                        cmd.CommandText = "[GetMasterOrderStatus]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                orderstatusList = new List<OrderStatusMaster>();
                                while (sdr.Read())
                                {
                                    OrderStatusMaster _orderstatus = new OrderStatusMaster();
                                    {
                                        _orderstatus.Code = (sdr["Code"].ToString() != "" ? int.Parse(sdr["Code"].ToString()) : _orderstatus.Code);
                                        _orderstatus.Description = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : _orderstatus.Description);

                                    }
                                    orderstatusList.Add(_orderstatus);
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

            return orderstatusList;


        }

        public List<QuotationStatusMaster> GetAllQuotationStatus()
        {
            List<QuotationStatusMaster> QuotationStatusList = null;
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
                        cmd.CommandText = "[GetMasterQuotationStatus]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                QuotationStatusList = new List<QuotationStatusMaster>();
                                while (sdr.Read())
                                {
                                    QuotationStatusMaster _quotationstatus = new QuotationStatusMaster();
                                    {
                                        _quotationstatus.Code = (sdr["Code"].ToString() != "" ? int.Parse(sdr["Code"].ToString()) : _quotationstatus.Code);
                                        _quotationstatus.Description = (sdr["Description"].ToString() != "" ? sdr["Description"].ToString() : _quotationstatus.Description);

                                    }
                                    QuotationStatusList.Add(_quotationstatus);
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

            return QuotationStatusList;


        }

        public List<OtherImages> GetAllStickers()
        {
            List<OtherImages> otherImagesList = null;

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
                        cmd.CommandText = "[GetAllStickers]";
                        cmd.Parameters.Add("@ImageType", SqlDbType.NVarChar, 50).Value = ImageTypesPreffered.Sticker;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                otherImagesList = new List<OtherImages>();
                                while (sdr.Read())
                                {
                                    OtherImages _otherimages = new OtherImages();
                                    {
                                        _otherimages.ID = (sdr["ID"].ToString() != "" ? Guid.Parse(sdr["ID"].ToString()) : _otherimages.ID);
                                        _otherimages.URL = (sdr["URL"].ToString() != "" ? sdr["URL"].ToString() : _otherimages.URL);
                                    }
                                    otherImagesList.Add(_otherimages);
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

            return otherImagesList;


        }

        public List<Country> GetAllCountries()
        {
            List<Country> CountryList = null;
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
                        cmd.CommandText = "[GetMasterCountries]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                CountryList = new List<Country>();
                                while (sdr.Read())
                                {
                                    Country _countries = new Country();
                                    { 
                                        _countries.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _countries.Name);
                                        _countries.Code = (sdr["Code"].ToString() != "" ? sdr["Code"].ToString() : _countries.Code);                                   

                                    }
                                    CountryList.Add(_countries);
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

            return CountryList;


        }
    

        #region Suppliers
        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> suppliersList = null;
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
                        cmd.CommandText = "[GetMasterSuppliers]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                suppliersList = new List<Supplier>();
                                while (sdr.Read())
                                {
                                    Supplier _supplier = new Supplier();
                                    {
                                        _supplier.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _supplier.ID);
                                        _supplier.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _supplier.Name);
                                        _supplier.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : _supplier.CreatedDate);



                                    }
                                    suppliersList.Add(_supplier);
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

            return suppliersList;


        }

        public Supplier GetSupplier(int SupplierID, OperationsStatus Status)
        {

            Supplier mySupplier = null;
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
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = SupplierID;
                        cmd.CommandText = "[GetSupplier]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    mySupplier = new Supplier();
                                    mySupplier.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : mySupplier.ID);
                                    mySupplier.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : mySupplier.Name);
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


            return mySupplier;
        }

        public OperationsStatus InsertSupplier(Supplier supplierObj)
        {
            OperationsStatus operationsStatusObj = null;

            try
            {
                SqlParameter outparameter = null;
                SqlParameter outparameterID = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[InsertSupplier]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = supplierObj.Name;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = supplierObj.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = supplierObj.commonObj.CreatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        outparameterID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                        outparameterID.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                // not Successfull

                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.InsertFailure;
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.InsertSuccess;
                                operationsStatusObj.ReturnValues = int.Parse(outparameterID.Value.ToString());
                                break;
                            case "2":
                                //Duplicate Entry
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.Duplicate;
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

        public OperationsStatus UpdateSupplier(Supplier supplierObj)
        {
            OperationsStatus operationsStatusObj = null;

            try
            {
                SqlParameter outparameter = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[UpdateSupplier]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = supplierObj.ID;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = supplierObj.Name;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = supplierObj.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = supplierObj.commonObj.UpdatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.UpdateFailure;
                                break;
                            case "1":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.UpdateSuccess;
                                operationsStatusObj.ReturnValues = supplierObj.ID;
                                break;
                            case "2":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.Duplicate;
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

        public OperationsStatus DeleteSupplier(int supplierID)
        {
            OperationsStatus OperationsStatusObj = new OperationsStatus();
            try
            {
                SqlParameter outparameter = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[DeleteSupplier]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = supplierID;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                OperationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                OperationsStatusObj.StatusMessage = ConstObj.DeleteFailure;
                                break;
                            case "1":
                                OperationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                OperationsStatusObj.StatusMessage = ConstObj.DeleteSuccess;
                                break;
                            case "2":
                                OperationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                OperationsStatusObj.StatusMessage = ConstObj.FKviolation;
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
            return OperationsStatusObj;
        }
        #endregion Suppliers

        #region ShippingLocation


                public List<ShippingLocations> GetAllShippingLocation()
        {
            List<ShippingLocations> ShippingLocationlist = null;
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
                        cmd.CommandText = "[GetMasterShippingLocations]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                ShippingLocationlist = new List<ShippingLocations>();
                                while (sdr.Read())
                                {
                                    ShippingLocations _shippingloc = new ShippingLocations();
                                    {
                                        _shippingloc.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _shippingloc.ID);
                                        _shippingloc.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _shippingloc.Name);
                                        _shippingloc.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : _shippingloc.CreatedDate);



                                    }
                                    ShippingLocationlist.Add(_shippingloc);
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

            return ShippingLocationlist;


        }

        public ShippingLocations GetShippingLocation(int ShippingLocationID, OperationsStatus Status)
        {

            ShippingLocations myShippingloc = null;
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
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ShippingLocationID;
                        cmd.CommandText = "[GetShippingLocation]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    myShippingloc = new ShippingLocations();
                                    myShippingloc.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : myShippingloc.ID);
                                    myShippingloc.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : myShippingloc.Name);
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


            return myShippingloc;
        }

        public OperationsStatus InsertShippingLocation(ShippingLocations shipping_locObj)
        {
            OperationsStatus operationsStatusObj = null;

            try
            {
                SqlParameter outparameter = null;
                SqlParameter outparameterID = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[InsertShippingLocation]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = shipping_locObj.Name;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = shipping_locObj.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = shipping_locObj.commonObj.CreatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        outparameterID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                        outparameterID.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.InsertFailure;
                                break;
                            case "1":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.InsertSuccess;
                                operationsStatusObj.ReturnValues = int.Parse(outparameterID.Value.ToString());
                                break;
                            case "2":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.Duplicate;
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

        public OperationsStatus UpdateShippingLocation(ShippingLocations shipping_locObj)
        {
            OperationsStatus operationsStatusObj = null;

            try
            {
                SqlParameter outparameter = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[UpdateShippingLocation]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = shipping_locObj.ID;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = shipping_locObj.Name;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = shipping_locObj.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = shipping_locObj.commonObj.UpdatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.UpdateFailure;
                                break;
                            case "1":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.UpdateSuccess;
                                operationsStatusObj.ReturnValues = shipping_locObj.ID;
                                break;
                            case "2":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.Duplicate;
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

        public OperationsStatus DeleteShippingLocation(int ShippingLocationID)
        {
            OperationsStatus OperationsStatusObj = new OperationsStatus();
            try
            {
                SqlParameter outparameter = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[DeleteShippingLocation]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ShippingLocationID;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                OperationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                OperationsStatusObj.StatusMessage = ConstObj.DeleteFailure;
                                break;
                            case "1":
                                OperationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                OperationsStatusObj.StatusMessage = ConstObj.DeleteSuccess;
                                break;
                            case "2":
                                OperationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                OperationsStatusObj.StatusMessage = ConstObj.FKviolation;
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
            return OperationsStatusObj;
        }        
        #endregion ShippingLocation

        #region SupplierLocations


        public List<SupplierLocations> GetAllSupplierLocations()
        {
            List<SupplierLocations> SupplierLocationslist = null;
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
                        cmd.CommandText = "[GetMasterSupplierLocations]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                SupplierLocationslist = new List<SupplierLocations>();
                                while (sdr.Read())
                                {
                                    SupplierLocations _supLoc = new SupplierLocations();
                                    {
                                        _supLoc.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _supLoc.ID);
                                        _supLoc.LocationID = (sdr["LocationID"].ToString() != "" ? int.Parse(sdr["LocationID"].ToString()) : _supLoc.LocationID);
                                        _supLoc.SupplierID = (sdr["SupplierID"].ToString() != "" ? int.Parse(sdr["SupplierID"].ToString()) : _supLoc.SupplierID);
                                        _supLoc.SupplierName = (sdr["SupplierName"].ToString() != "" ? sdr["SupplierName"].ToString() : _supLoc.SupplierName);
                                        _supLoc.LocationName = (sdr["LocationName"].ToString() != "" ? sdr["LocationName"].ToString() : _supLoc.LocationName);
                                        _supLoc.ShippingCharge = (sdr["ShippingCharge"].ToString() != "" ? decimal.Parse(sdr["ShippingCharge"].ToString()) : _supLoc.ShippingCharge);



                                    }
                                    SupplierLocationslist.Add(_supLoc);
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

            return SupplierLocationslist;


        }

        public SupplierLocations GetSupplierLocations(int SupplierLocationID, OperationsStatus Status)
        {

            SupplierLocations mySupplierloc = null;
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
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = SupplierLocationID;
                        cmd.CommandText = "[GetSupplierLocations]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    mySupplierloc = new SupplierLocations();
                                    mySupplierloc.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : mySupplierloc.ID);
                                    mySupplierloc.LocationID = (sdr["LocationID"].ToString() != "" ? int.Parse(sdr["LocationID"].ToString()) : mySupplierloc.LocationID);
                                    mySupplierloc.SupplierID = (sdr["SupplierID"].ToString() != "" ? int.Parse(sdr["SupplierID"].ToString()) : mySupplierloc.SupplierID);
                                    mySupplierloc.SupplierName = (sdr["SupplierName"].ToString() != "" ? sdr["SupplierName"].ToString() : mySupplierloc.LocationName);
                                    mySupplierloc.LocationName = (sdr["LocationName"].ToString() != "" ? sdr["LocationName"].ToString() : mySupplierloc.LocationName);
                                    mySupplierloc.ShippingCharge = (sdr["ShippingCharge"].ToString() != "" ? decimal.Parse(sdr["ShippingCharge"].ToString()) : mySupplierloc.ShippingCharge);

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


            return mySupplierloc;
        }

        public OperationsStatus InsertSupplierLocations(SupplierLocations supplier_locObj)
        {
            OperationsStatus operationsStatusObj = null;

            try
            {
                SqlParameter outparameter = null;
                SqlParameter outparameterID = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[InsertSupplierLocations]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@LocationID", SqlDbType.Int).Value = supplier_locObj.LocationID;
                        cmd.Parameters.Add("@SupplierID", SqlDbType.Int).Value = supplier_locObj.SupplierID;
                        cmd.Parameters.Add("@ShippingCharge", SqlDbType.Decimal).Value = supplier_locObj.ShippingCharge;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = supplier_locObj.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = supplier_locObj.commonObj.CreatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        outparameterID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                        outparameterID.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.InsertFailure;
                                break;
                            case "1":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.InsertSuccess;
                                operationsStatusObj.ReturnValues = int.Parse(outparameterID.Value.ToString());
                                break;
                            case "2":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.Duplicate;
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

        public OperationsStatus UpdateSupplierLocations(SupplierLocations supplier_locObj)
        {
            OperationsStatus operationsStatusObj = null;

            try
            {
                SqlParameter outparameter = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[UpdateSupplierLocations]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = supplier_locObj.ID;
                        cmd.Parameters.Add("@LocationID", SqlDbType.Int).Value = supplier_locObj.LocationID;
                        cmd.Parameters.Add("@SupplierID", SqlDbType.Int).Value = supplier_locObj.SupplierID;
                        cmd.Parameters.Add("@ShippingCharge", SqlDbType.Decimal).Value = supplier_locObj.ShippingCharge;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = supplier_locObj.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = supplier_locObj.commonObj.UpdatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.UpdateFailure;
                                break;
                            case "1":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.UpdateSuccess;
                                operationsStatusObj.ReturnValues = supplier_locObj.ID;
                                break;
                            case "2":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.Duplicate;
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

        public OperationsStatus DeleteSupplierLocations(int SupplierLocationID)
        {
            OperationsStatus OperationsStatusObj = new OperationsStatus();
            try
            {
                SqlParameter outparameter = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[DeleteSupplierLocations]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = SupplierLocationID;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                OperationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                OperationsStatusObj.StatusMessage = ConstObj.DeleteFailure;
                                break;
                            case "1":
                                OperationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                OperationsStatusObj.StatusMessage = ConstObj.DeleteSuccess;
                                break;
                            case "2":
                                OperationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                OperationsStatusObj.StatusMessage = ConstObj.FKviolation;
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
            return OperationsStatusObj;
        }
        #endregion SupplierLocations

        #region Manufacturer

        public List<Manufacturer> GetAllManufacturers()
        {
            List<Manufacturer> manufacturesList = null;
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
                        cmd.CommandText = "[GetMasterManufacturers]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                manufacturesList = new List<Manufacturer>();
                                while (sdr.Read())
                                {
                                    Manufacturer _manufacturer = new Manufacturer();
                                    {
                                        _manufacturer.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _manufacturer.ID);
                                        _manufacturer.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _manufacturer.Name);
                                        _manufacturer.country = new Country();
                                        _manufacturer.country.Code = (sdr["CountryCode"].ToString() != "" ? sdr["CountryCode"].ToString() : _manufacturer.country.Code);
                                        _manufacturer.country.Name = (sdr["CountryName"].ToString() != "" ? sdr["CountryName"].ToString() : _manufacturer.country.Name);

                                    }
                                    manufacturesList.Add(_manufacturer);
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

            return manufacturesList;


        }

        public Manufacturer GetManufacturer(int ManufacturerID, OperationsStatus Status)
        {

            Manufacturer myManufacturer = null;
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
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ManufacturerID;
                        cmd.CommandText = "[GetManufacturer]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    myManufacturer = new Manufacturer();
                                    myManufacturer.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : myManufacturer.ID);
                                    myManufacturer.Name = (sdr["ManufacturerName"].ToString() != "" ? sdr["ManufacturerName"].ToString() : myManufacturer.Name);
                                    myManufacturer.country = new Country();
                                    myManufacturer.country.Code = (sdr["CountryCode"].ToString() != "" ? sdr["CountryCode"].ToString() : myManufacturer.country.Code);
                                    myManufacturer.country.Name = (sdr["CountryName"].ToString() != "" ? sdr["CountryName"].ToString() : myManufacturer.country.Name);
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


            return myManufacturer;
        }

        public OperationsStatus InsertManufacturer(Manufacturer ManufacturerObj)
        {
            OperationsStatus operationsStatusObj = null;

            try
            {
                SqlParameter outparameter = null;
                SqlParameter outparameterID = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[InsertManufacturer]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = ManufacturerObj.Name;
                        cmd.Parameters.Add("@CountryCode", SqlDbType.NVarChar, 3).Value = ManufacturerObj.country.Code;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = ManufacturerObj.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = ManufacturerObj.commonObj.CreatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        outparameterID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                        outparameterID.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                // not Successfull

                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.InsertFailure;
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.InsertSuccess;
                                operationsStatusObj.ReturnValues = int.Parse(outparameterID.Value.ToString());
                                break;
                            case "2":
                                //Duplicate Entry
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.Duplicate;
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

        public OperationsStatus UpdateManufacturer(Manufacturer ManufacturerObj)
        {
            OperationsStatus operationsStatusObj = null;

            try
            {
                SqlParameter outparameter = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[UpdateManufacturer]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ManufacturerObj.ID;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = ManufacturerObj.Name;
                        cmd.Parameters.Add("@CountryCode", SqlDbType.NVarChar, 3).Value = ManufacturerObj.country.Code;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = ManufacturerObj.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = ManufacturerObj.commonObj.UpdatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.UpdateFailure;
                                break;
                            case "1":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.UpdateSuccess;
                                operationsStatusObj.ReturnValues = ManufacturerObj.ID;
                                break;
                            case "2":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.Duplicate;
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

        public OperationsStatus DeleteManufacturer(int ManufacturerID)
        {
            OperationsStatus OperationsStatusObj = new OperationsStatus();
            try
            {
                SqlParameter outparameter = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[DeleteManufacturer]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ManufacturerID;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                OperationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                OperationsStatusObj.StatusMessage = ConstObj.DeleteFailure;
                                break;
                            case "1":
                                OperationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                OperationsStatusObj.StatusMessage = ConstObj.DeleteSuccess;
                                break;
                            case "2":
                                OperationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                OperationsStatusObj.StatusMessage = ConstObj.FKviolation;
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
            return OperationsStatusObj;
        }

        #endregion Manufacturer


    }
}