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
                                        _attributesObj.ComparableYN = (sdr["MandatoryYN"].ToString() != "" ? Boolean.Parse(sdr["ComparableYN"].ToString()) : _attributesObj.ComparableYN);
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
                                // not Successfull

                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Not Successfull!";
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
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
                                // not Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Not Successfull!";
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
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


        #endregion Methods
    }
    public class AttributeSetRepository : IAttributeSetRepository
    {
        
    }
    public class AttributeToSetLinks : IAttributeToSetLinksRepository
    {
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public AttributeToSetLinks(IDatabaseFactory databaseFactory)
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
                                operationsStatusObj.StatusMessage = "Insertion Not Successfull!";
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
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
        public OperationsStatus DeleteAttributeSetLink(string ID)
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
                                operationsStatusObj.StatusMessage = "Deletion Not Successfull!";
                                break;
                            case "1":
                                //Delete Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = "Deletion Successfull!";
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