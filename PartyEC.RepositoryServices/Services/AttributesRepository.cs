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

        public List<Attributes> GetAllAttributes(Attributes attributesObj)
        {
            List<Attributes> AttributesList = null;

            return AttributesList;
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
    public class AttributeToSetLinks : IAttributeToSetLinks
    {

    }


   
}