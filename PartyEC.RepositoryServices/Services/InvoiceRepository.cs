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
    public class InvoiceRepository: IInvoiceRepository
    {
        Const constObj = new Const();

        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public InvoiceRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        #endregion DataBaseFactory
        public OperationsStatus InsertInvoiceHeader(Invoice invoiceObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null, ID = null;

                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[InsertInvoiceHeader]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@InvoiceDate", SqlDbType.DateTime).Value = invoiceObj.LogDetails.CreatedDate;
                        cmd.Parameters.Add("@ParentID", SqlDbType.Int).Value = invoiceObj.ParentID;
                        cmd.Parameters.Add("@ParentType", SqlDbType.NVarChar, 20).Value = invoiceObj.ParentType;
                        cmd.Parameters.Add("@PaymentStatus", SqlDbType.Int).Value = invoiceObj.PaymentStatus;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = invoiceObj.LogDetails.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = invoiceObj.LogDetails.CreatedDate;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        ID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                        statusCode.Direction = ParameterDirection.Output;
                        ID.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Not Successfull!";
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Successfull!";
                                operationsStatusObj.ReturnValues = ID.Value;
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
        public OperationsStatus InsertInvoiceDetail(InvoiceDetail invoiceDetailsObj)
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
                        cmd.CommandText = "[InsertInvoiceDetail]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = invoiceDetailsObj.InvoiceID;
                        cmd.Parameters.Add("@OrderItemID", SqlDbType.Int).Value = invoiceDetailsObj.OrderItemID;
                        cmd.Parameters.Add("@InvoiceAmt", SqlDbType.Float).Value = invoiceDetailsObj.InvoiceAmt;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar,250).Value = invoiceDetailsObj.LogDetails.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = invoiceDetailsObj.LogDetails.CreatedDate;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Not Successfull!";
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
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


        public List<Invoice> GetAllInvoices()
        {
            List<Invoice> invoiceList = null;
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
                        cmd.CommandText = "[GetAllInvoices]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                invoiceList = new List<Invoice>();
                                while (sdr.Read())
                                {
                                    Invoice _invoice = new Invoice();
                                    {
                                        _invoice.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _invoice.ID);
                                        _invoice.InvoiceNo = (sdr["InvoiceNo"].ToString() != "" ? sdr["InvoiceNo"].ToString() : _invoice.InvoiceNo);
                                        _invoice.ParentID= (sdr["ParentID"].ToString() != "" ? int.Parse(sdr["ParentID"].ToString()) : _invoice.ParentID);
                                        _invoice.ParentType= (sdr["ParentType"].ToString() != "" ? sdr["ParentType"].ToString() : _invoice.ParentType);
                                        _invoice.InvoiceDate= (sdr["InvoiceDate"].ToString() != "" ? sdr["InvoiceDate"].ToString() : _invoice.InvoiceDate);
                                        _invoice.PaymentStatus= (sdr["PaymentStatus"].ToString() != "" ? int.Parse(sdr["PaymentStatus"].ToString()) : _invoice.PaymentStatus);
                                        _invoice.LogDetails = new LogDetails();
                                        _invoice.LogDetails.CreatedDate = ((sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _invoice.LogDetails.CreatedDate));
                                        
                                    }
                                    invoiceList.Add(_invoice);
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
            return invoiceList;
        }
    }

}