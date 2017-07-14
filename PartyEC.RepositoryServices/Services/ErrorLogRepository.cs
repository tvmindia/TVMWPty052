using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.RepositoryServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using System.Data.SqlClient;
using System.Data;

namespace PartyEC.RepositoryServices.Services
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        Const ConstObj = new Const();
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public ErrorLogRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        #endregion DataBaseFactory

        #region Methods

        public OperationsStatus InsertErrorLog(ErrorLog ErrorObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter outparameter, outparameter1 = null;

                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[InsertErrorLog]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        if(ErrorObj.ErrorID!=null)
                            cmd.Parameters.Add("@ErrorID", SqlDbType.VarChar, 50).Value = ErrorObj.ErrorID;
                        cmd.Parameters.Add("@Description", SqlDbType.NVarChar, -1).Value = ErrorObj.Description;
                        cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = (ErrorObj.Date == null) ? ErrorObj.commonObj.CreatedDate : DateTime.Parse(ErrorObj.Date);
                        cmd.Parameters.Add("@Module", SqlDbType.NVarChar, 50).Value = ErrorObj.Module;
                        cmd.Parameters.Add("@ErrorSource", SqlDbType.NVarChar, 25).Value = ErrorObj.ErrorSource;
                        cmd.Parameters.Add("@IsMobile", SqlDbType.Bit).Value = ErrorObj.IsMobile;
                        cmd.Parameters.Add("@AppBuild", SqlDbType.NVarChar, -1).Value = ErrorObj.AppBuild;
                        cmd.Parameters.Add("@AppLogCat", SqlDbType.NVarChar, -1).Value = ErrorObj.AppLogCat;
                        cmd.Parameters.Add("@Version", SqlDbType.NVarChar, 50).Value = ErrorObj.Version;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 50).Value = ErrorObj.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = ErrorObj.commonObj.CreatedDate;

                        outparameter = cmd.Parameters.Add("@InsertStatus", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        outparameter1 = cmd.Parameters.Add("@OutErrorID", SqlDbType.UniqueIdentifier);
                        outparameter1.Direction = ParameterDirection.Output;
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
                                operationsStatusObj.ReturnValues = Int32.Parse(outparameter1.Value.ToString());
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

        #endregion
    }
}