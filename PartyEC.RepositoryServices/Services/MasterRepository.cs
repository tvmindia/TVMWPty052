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
                                        _supplier.CreatedDate= (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _supplier.CreatedDate);



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
        #endregion Suppliers

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
        
        // List<OrderStatusMaster> GetAllOrderStatus();
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

    
    }
}