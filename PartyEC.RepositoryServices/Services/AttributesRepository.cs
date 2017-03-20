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
    public class AttributesRepository : IAttributesRepository
    {
        Const constObj = new Const();
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public AttributesRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        #endregion DataBaseFactory
        
        #region Methods

        public List<Attributes> GetAllAttributes()
        {
            List<Attributes> AttributesList = null;

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
                        cmd.CommandText = "[GetAllAttributes]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                AttributesList = new List<Attributes>();
                                while (sdr.Read())
                                {
                                    Attributes _attributesObj = new Attributes();
                                    {
                                        _attributesObj.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : _attributesObj.ID);
                                        _attributesObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _attributesObj.Name);
                                        _attributesObj.Caption = (sdr["Caption"].ToString() != "" ? sdr["Caption"].ToString() : _attributesObj.Caption);
                                        _attributesObj.AttributeType = (sdr["AttributeType"].ToString() != "" ? sdr["AttributeType"].ToString() : _attributesObj.AttributeType);
                                        _attributesObj.ConfigurableYN = (sdr["ConfigurableYN"].ToString() != "" ? Boolean.Parse(sdr["ConfigurableYN"].ToString()) : _attributesObj.ConfigurableYN);
                                        _attributesObj.FilterYN = (sdr["FilterYN"].ToString() != "" ? Boolean.Parse(sdr["FilterYN"].ToString()) : _attributesObj.FilterYN);
                                        _attributesObj.CSValues = (sdr["CSValues"].ToString() != "" ? sdr["CSValues"].ToString() : _attributesObj.CSValues);
                                        _attributesObj.EntityType = (sdr["EntityType"].ToString() != "" ? sdr["EntityType"].ToString() : _attributesObj.EntityType);
                                        _attributesObj.MandatoryYN = (sdr["MandatoryYN"].ToString() != "" ? Boolean.Parse(sdr["MandatoryYN"].ToString()) : _attributesObj.MandatoryYN);
                                        _attributesObj.ComparableYN = (sdr["ComparableYN"].ToString() != "" ? Boolean.Parse(sdr["ComparableYN"].ToString()) : _attributesObj.ComparableYN);
                                    }
                                    AttributesList.Add(_attributesObj);
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
            return AttributesList;
        }

        public Attributes GetAttributes(int AttributeID, OperationsStatus Status)
        {

            Attributes myAttribute = null;
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
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = AttributeID;
                        cmd.CommandText = "[GetAttributes]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    myAttribute = new Attributes();

                                    myAttribute.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : myAttribute.ID);
                                    myAttribute.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : myAttribute.Name);
                                    myAttribute.Caption = (sdr["Caption"].ToString() != "" ? sdr["Caption"].ToString() : myAttribute.Caption);
                                    myAttribute.AttributeType = (sdr["AttributeType"].ToString() != "" ? sdr["AttributeType"].ToString() : myAttribute.AttributeType);
                                    myAttribute.ConfigurableYN = (sdr["ConfigurableYN"].ToString() != "" ? Boolean.Parse(sdr["ConfigurableYN"].ToString()) : myAttribute.ConfigurableYN);
                                    myAttribute.FilterYN = (sdr["FilterYN"].ToString() != "" ? Boolean.Parse(sdr["FilterYN"].ToString()) : myAttribute.FilterYN);
                                    myAttribute.CSValues = (sdr["CSValues"].ToString() != "" ? sdr["CSValues"].ToString() : myAttribute.CSValues);
                                    myAttribute.EntityType = (sdr["EntityType"].ToString() != "" ? sdr["EntityType"].ToString() : myAttribute.EntityType);
                                    myAttribute.MandatoryYN = (sdr["MandatoryYN"].ToString() != "" ? Boolean.Parse(sdr["MandatoryYN"].ToString()) : myAttribute.MandatoryYN);
                                    myAttribute.ComparableYN = (sdr["MandatoryYN"].ToString() != "" ? Boolean.Parse(sdr["ComparableYN"].ToString()) : myAttribute.ComparableYN);

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


            return myAttribute;
        } 

        public OperationsStatus InsertAttributes(Attributes attributesObj)
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
                        cmd.CommandText = "[InsertAttributes]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = attributesObj.Name;
                        cmd.Parameters.Add("@Caption", SqlDbType.NVarChar, 250).Value = attributesObj.Caption  ;
                        cmd.Parameters.Add("@AttributeType", SqlDbType.NVarChar, 10).Value = attributesObj.AttributeType ;
                        cmd.Parameters.Add("@ConfigurableYN", SqlDbType.Bit).Value = attributesObj.ConfigurableYN;
                        cmd.Parameters.Add("@FilterYN", SqlDbType.Bit).Value = attributesObj.FilterYN;
                        cmd.Parameters.Add("@CSValues", SqlDbType.NVarChar,-1).Value = attributesObj.CSValues;
                        cmd.Parameters.Add("@EntityType", SqlDbType.NVarChar, 10).Value = attributesObj.EntityType;
                        cmd.Parameters.Add("@MandatoryYN", SqlDbType.Bit).Value = attributesObj.MandatoryYN;
                        cmd.Parameters.Add("@ComparableYN", SqlDbType.Bit).Value = attributesObj.ComparableYN;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 10).Value = attributesObj.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = attributesObj.commonObj.CreatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0": 
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.InsertFailure;
                                break;
                            case "1": 
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
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

        public OperationsStatus UpdateAttributes(Attributes attributesObj)
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
                        cmd.CommandText = "[UpdateAttributes]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar, 50).Value = attributesObj.ID;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = attributesObj.Name;
                        cmd.Parameters.Add("@Caption", SqlDbType.NVarChar, 250).Value = attributesObj.Caption;
                        cmd.Parameters.Add("@AttributeType", SqlDbType.NVarChar, 10).Value = attributesObj.AttributeType;
                        cmd.Parameters.Add("@ConfigurableYN", SqlDbType.Bit).Value = attributesObj.ConfigurableYN;
                        cmd.Parameters.Add("@FilterYN", SqlDbType.Bit).Value = attributesObj.FilterYN;
                        cmd.Parameters.Add("@CSValues", SqlDbType.NVarChar, -1).Value = attributesObj.CSValues;
                        cmd.Parameters.Add("@EntityType", SqlDbType.NVarChar, 10).Value = attributesObj.EntityType;
                        cmd.Parameters.Add("@MandatoryYN", SqlDbType.Bit).Value = attributesObj.MandatoryYN;
                        cmd.Parameters.Add("@ComparableYN", SqlDbType.Bit).Value = attributesObj.ComparableYN;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 10).Value = attributesObj.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = attributesObj.commonObj.UpdatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0": 
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.UpdateFailure;
                                break;
                            case "1": 
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
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
                throw ex;
            }
            return operationsStatusObj;
        }

        public OperationsStatus DeleteAttributes(int AttributeID)
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
                        cmd.CommandText = "[DeleteAttributes]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar, 50).Value =AttributeID;                       

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();                        
                        switch (outparameter.Value.ToString())
                        {
                            case "0": 
                                OperationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                OperationsStatusObj.StatusMessage = constObj.DeleteFailure;
                                break;
                            case "1": 
                                OperationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                OperationsStatusObj.StatusMessage = constObj.DeleteSuccess;
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
        
        public List<AttributeValues> GetAttributeContainer(int AttributeSetID,string Type)
        {
            List<AttributeValues> myProductAttributeList = null;
            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    SqlCommand cmd = new SqlCommand();                    
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    cmd.Connection = con;
                    cmd.Parameters.Add("@AttributeSetID", SqlDbType.Int).Value = AttributeSetID;
                    cmd.Parameters.Add("@EntityType", SqlDbType.NVarChar).Value = Type;
                    cmd.CommandText = "[GetAttributesBySetIdAndEntityType]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        myProductAttributeList = new List<AttributeValues>();
                        if ((sdr != null) && (sdr.HasRows))
                        {
                            if (sdr.Read())
                            {
                                AttributeValues myAttribute = new AttributeValues();
                                myAttribute.Name = sdr["Name"].ToString();
                                myAttribute.Caption = sdr["Caption"].ToString();
                                myAttribute.DataType = sdr["AttributeType"].ToString();
                                myProductAttributeList.Add(myAttribute);
                            }
                        }
                    }

                }


                }
            catch (Exception)
            {
                throw;
            }
            return myProductAttributeList;
        }

        
        public string GetAttributeXML(List<AttributeValues> AttributeContainerWithValues) {
           
            string myXML = "";
            try
            {
                const string start = "<options>";
                const string end = "</options>";

                myXML = start;
                if (AttributeContainerWithValues != null) {

                    foreach (AttributeValues att in AttributeContainerWithValues) {

                        myXML += "<" + att.Name + ">";
                        myXML += "<" + att.Value + ">";
                        myXML += "</" + att.Name + ">";
                    }

                }
                myXML += end;

            }
            catch (Exception)
            {

                throw;
            }
            return myXML;
        }


        public List<Attributes> GetAllAttributeBySet(int AttributeSetID)
        {
            List<Attributes> AttributeList = null;
            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    cmd.Connection = con;
                    cmd.Parameters.Add("@AttributeSetID", SqlDbType.Int).Value = AttributeSetID;
                    
                    cmd.CommandText = "[GetAllAttributesBySetID]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        AttributeList = new List<Attributes>();
                        if ((sdr != null) && (sdr.HasRows))
                        {
                            while(sdr.Read())
                            {
                                Attributes myAttribute = new Attributes();
                                myAttribute.attributeSetLinkObj = new AttributeSetLink();
                                myAttribute.attributeSetLinkObj.ID=(sdr["AttributeSetLinkID"].ToString()!=""? int.Parse(sdr["AttributeSetLinkID"].ToString()):myAttribute.attributeSetLinkObj.ID);
                                myAttribute.attributeSetLinkObj.AttributeSetID=(sdr["AttributeSetID"].ToString()!=""? int.Parse(sdr["AttributeSetID"].ToString()):myAttribute.attributeSetLinkObj.AttributeSetID) ;
                                myAttribute.attributeSetLinkObj.DisplayOrder=(sdr["DisplayOrder"].ToString()!=""? float.Parse(sdr["DisplayOrder"].ToString()):myAttribute.attributeSetLinkObj.DisplayOrder);
                                myAttribute.ID =(sdr["AttributeID"].ToString()!=""?int.Parse(sdr["AttributeID"].ToString()):myAttribute.ID);
                                myAttribute.Name =(sdr["Name"].ToString()!=""? sdr["Name"].ToString():myAttribute.Name);
                                myAttribute.Caption = (sdr["Caption"].ToString()!=""? sdr["Caption"].ToString():myAttribute.Caption);
                                myAttribute.AttributeType = (sdr["AttributeType"].ToString() != "" ? sdr["AttributeType"].ToString() : myAttribute.AttributeType);
                                myAttribute.ConfigurableYN = (sdr["ConfigurableYN"].ToString() != "" ? bool.Parse(sdr["ConfigurableYN"].ToString()) : myAttribute.ConfigurableYN);
                                myAttribute.FilterYN = (sdr["FilterYN"].ToString()!="" ? bool.Parse(sdr["FilterYN"].ToString()):myAttribute.FilterYN);
                                myAttribute.CSValues = (sdr["CSValues"].ToString() != "" ? sdr["CSValues"].ToString() : myAttribute.CSValues);
                                myAttribute.EntityType = (sdr["EntityType"].ToString() != "" ? sdr["EntityType"].ToString() : myAttribute.EntityType);
                                myAttribute.MandatoryYN = (sdr["MandatoryYN"].ToString() != "" ? bool.Parse(sdr["MandatoryYN"].ToString()) : myAttribute.MandatoryYN);
                                myAttribute.ComparableYN = (sdr["ComparableYN"].ToString() != "" ? bool.Parse(sdr["ComparableYN"].ToString()) : myAttribute.ComparableYN);
                                AttributeList.Add(myAttribute);
                            }
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return AttributeList;
        }

        #endregion Methods
    }
    public class AttributeSetRepository : IAttributeSetRepository
    {
        Const ConstObj = new Const();
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        private IAttributeToSetLinksRepository _attributeSetLinkRepository;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public AttributeSetRepository(IDatabaseFactory databaseFactory,IAttributeToSetLinksRepository attributeSetLinkRepository)
        {
            _databaseFactory = databaseFactory;
            _attributeSetLinkRepository = attributeSetLinkRepository;

        }
        #endregion DataBaseFactory
        public List<AttributeSet> GetAllAttributeSet()
        {
            List<AttributeSet> AttributeList = null;
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
                        cmd.CommandText = "[GetAllAttributeSet]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                AttributeList = new List<AttributeSet>();
                                while (sdr.Read())
                                {
                                    AttributeSet attributeSetObj = new AttributeSet();
                                    {
                                        attributeSetObj.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : attributeSetObj.ID);
                                        attributeSetObj.Name = sdr["Name"].ToString();
                                       
                                    }
                                    AttributeList.Add(attributeSetObj);
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

            return AttributeList;
        }
        public OperationsStatus InsertAttributeSet(AttributeSet attributeSetObj)
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
                        cmd.CommandText = "[InsertAttributeSet]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = attributeSetObj.Name;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = attributeSetObj.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = attributeSetObj.commonObj.CreatedDate;

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
                                operationsStatusObj.ReturnValues = Int16.Parse(outparameterID.Value.ToString());
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
        public OperationsStatus UpdateAttributeSet(AttributeSet attributeSetObj,int ID)
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
                        cmd.CommandText = "[UpdateAttributeSet]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = attributeSetObj.Name;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = attributeSetObj.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = attributeSetObj.commonObj.UpdatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
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
        public OperationsStatus DeleteAttributeSet(int ID)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
               operationsStatusObj = _attributeSetLinkRepository.DeleteAttributeSetLink(ID);
                if (operationsStatusObj.StatusCode == 1|| operationsStatusObj.StatusCode == 0)
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
                            cmd.CommandText = "[DeleteAttributeSet]";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 50).Value = ID;
                            outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                            outparameter.Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            operationsStatusObj = new OperationsStatus();
                            switch (outparameter.Value.ToString())
                            {
                                case "0":
                                    // Delete not Successfull

                                    operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                    operationsStatusObj.StatusMessage = ConstObj.DeleteFailure;
                                    break;
                                case "1":
                                    //Delete Successfull
                                    operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                    operationsStatusObj.StatusMessage = ConstObj.DeleteSuccess;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return operationsStatusObj;
        }
    }
    public class AttributeToSetLinksRepository : IAttributeToSetLinksRepository
    {
        Const ConstObj = new Const();
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public AttributeToSetLinksRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        #endregion DataBaseFactory
        public OperationsStatus InsertAttributeSetLink(AttributeSetLink attrSetLinkObj)
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
                        cmd.CommandText = "[InsertAttributeSetLink]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@AttributeID", SqlDbType.Int).Value = attrSetLinkObj.AttributeID;
                        cmd.Parameters.Add("@AttributeSetID", SqlDbType.Int).Value = attrSetLinkObj.AttributeSetID;
                        cmd.Parameters.Add("@DisplayOrder", SqlDbType.Float).Value = attrSetLinkObj.DisplayOrder;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = attrSetLinkObj.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = attrSetLinkObj.commonObj.CreatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
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
        public OperationsStatus DeleteAttributeSetLink(int ID)
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
                        cmd.CommandText = "[DeleteAttributeSetLinkDataUsingAttributeSetID]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@AttributeSetID", SqlDbType.VarChar, 50).Value = ID;
                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                // Delete not Successfull

                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.DeleteFailure;
                                break;
                            case "1":
                                //Delete Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.DeleteSuccess;
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