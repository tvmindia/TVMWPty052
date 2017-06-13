using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using System.Data.SqlClient;
using System.Data;

namespace PartyEC.RepositoryServices.Services
{
    public class EventRequestsRepositry : IEventRequestsRepositry
    {
        Const ConstObj = new Const();
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public EventRequestsRepositry(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        #endregion DataBaseFactory

        #region Methods

        public OperationsStatus InsertEventRequests(EventRequests eventObj)
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
                        cmd.CommandText = "[InsertEventRequest]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@EventTypeID", SqlDbType.Int).Value = eventObj.EventType;
                        cmd.Parameters.Add("@EventTitle", SqlDbType.NVarChar, 250).Value = eventObj.EventTitle;
                        cmd.Parameters.Add("@EventDateTime", SqlDbType.DateTime).Value = eventObj.EventDateTime;
                        cmd.Parameters.Add("@NoOfPersons", SqlDbType.Int).Value = eventObj.NoOfPersons;
                        cmd.Parameters.Add("@Budget", SqlDbType.Decimal).Value = eventObj.Budget;
                        cmd.Parameters.Add("@LookingFor", SqlDbType.NVarChar, -1).Value = eventObj.LookingFor;
                        cmd.Parameters.Add("@RequirementSpec", SqlDbType.NVarChar, -1).Value = eventObj.RequirementSpec;
                        if(eventObj.CustomerID!=0)cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = eventObj.CustomerID;
                        cmd.Parameters.Add("@ContactName", SqlDbType.NVarChar, 250).Value = eventObj.ContactName;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar, -1).Value = eventObj.Email;
                        cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = eventObj.Phone;
                        cmd.Parameters.Add("@ContactType", SqlDbType.NVarChar, 10).Value = eventObj.ContactType;
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, -1).Value = eventObj.Message;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 50).Value = eventObj.logDetailsObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = eventObj.logDetailsObj.CreatedDate;

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

        public List<EventRequests> GetAllEventRequests()
        {
            List<EventRequests> Requestslist = null;
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
                        cmd.CommandText = "[GetAllEventRequests]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Requestslist = new List<EventRequests>();
                                while (sdr.Read())
                                {
                                    EventRequests _eventRequestsObj = new EventRequests();
                                    {
                                        _eventRequestsObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _eventRequestsObj.ID);
                                        _eventRequestsObj.EventReqNo = (sdr["EventReqNo"].ToString() != "" ? sdr["EventReqNo"].ToString() : _eventRequestsObj.EventReqNo);
                                        _eventRequestsObj.EventType = (sdr["EventType"].ToString() != "" ? sdr["EventType"].ToString() : _eventRequestsObj.EventType);
                                        _eventRequestsObj.EventTitle = (sdr["EventTitle"].ToString() != "" ? sdr["EventTitle"].ToString() : _eventRequestsObj.EventTitle);
                                        _eventRequestsObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : _eventRequestsObj.CustomerID);
                                        _eventRequestsObj.CustomerName = (sdr["CustomerName"].ToString() != "" ?sdr["CustomerName"].ToString() : _eventRequestsObj.CustomerName);
                                        _eventRequestsObj.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : _eventRequestsObj.ContactName);
                                        _eventRequestsObj.EventDateTime = (sdr["EventDateTime"].ToString() != "" ? DateTime.Parse(sdr["EventDateTime"].ToString().ToString()).ToString("dd-MMM-yyyy") : _eventRequestsObj.EventDateTime);
                                        _eventRequestsObj.EventTime = (sdr["EventTime"].ToString() != "" ?sdr["EventTime"].ToString(): _eventRequestsObj.EventTime);
                                        _eventRequestsObj.Phone = (sdr["Phone"].ToString() != "" ? sdr["Phone"].ToString() : _eventRequestsObj.Phone);
                                        _eventRequestsObj.EventDesc = (sdr["EventDesc"].ToString() != "" ?sdr["EventDesc"].ToString() : _eventRequestsObj.EventDesc);
                                        _eventRequestsObj.FollowUpDate = (sdr["FollowUpDate"].ToString() != "" ? DateTime.Parse( sdr["FollowUpDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : _eventRequestsObj.FollowUpDate);                                        
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

        public EventRequests GetEventRequest(int EventRequestsID)
        {
            EventRequests myEventRequests = null;
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
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = EventRequestsID;
                        cmd.CommandText = "[GetEventRequest]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    myEventRequests = new EventRequests();
                                    myEventRequests.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : myEventRequests.ID);
                                    myEventRequests.EventReqNo = (sdr["EventReqNo"].ToString() != "" ? sdr["EventReqNo"].ToString() : myEventRequests.EventReqNo);
                                    myEventRequests.EventType = (sdr["EventType"].ToString() != "" ? sdr["EventType"].ToString() : myEventRequests.EventType);
                                    myEventRequests.EventTitle = (sdr["EventTitle"].ToString() != "" ? sdr["EventTitle"].ToString() : myEventRequests.EventTitle);
                                    myEventRequests.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : myEventRequests.ContactName);
                                    myEventRequests.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : myEventRequests.Email);
                                    myEventRequests.Phone = (sdr["Phone"].ToString() != "" ? sdr["Phone"].ToString() : myEventRequests.Phone);
                                    myEventRequests.EventStatus = (sdr["EventStatus"].ToString() != "" ? int.Parse(sdr["EventStatus"].ToString()) : myEventRequests.EventStatus);
                                    myEventRequests.EventDesc = (sdr["EventDesc"].ToString() != "" ? sdr["EventDesc"].ToString() : myEventRequests.EventDesc);
                                    myEventRequests.FollowUpDate = (sdr["FollowUpDate"].ToString() != "" ? DateTime.Parse(sdr["FollowUpDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : myEventRequests.FollowUpDate);
                                    myEventRequests.EventDateTime = (sdr["EventDateTime"].ToString() != "" ? DateTime.Parse(sdr["EventDateTime"].ToString().ToString()).ToString("dd-MMM-yyyy") : myEventRequests.EventDateTime);
                                    myEventRequests.EventTime = (sdr["EventTime"].ToString() != "" ? sdr["EventTime"].ToString() : myEventRequests.EventTime);
                                    myEventRequests.NoOfPersons = (sdr["NoOfPersons"].ToString() != "" ? int.Parse(sdr["NoOfPersons"].ToString()) : myEventRequests.NoOfPersons);
                                    myEventRequests.Budget = (sdr["Budget"].ToString() != "" ? Decimal.Parse(sdr["Budget"].ToString()) : myEventRequests.Budget);
                                    myEventRequests.LookingFor = (sdr["LookingFor"].ToString() != "" ? sdr["LookingFor"].ToString() : myEventRequests.LookingFor);
                                    myEventRequests.RequirementSpec = (sdr["RequirementSpec"].ToString() != "" ? sdr["RequirementSpec"].ToString() : myEventRequests.RequirementSpec);
                                    myEventRequests.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : myEventRequests.ContactName);
                                    myEventRequests.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : myEventRequests.Email);
                                    myEventRequests.Phone = (sdr["Phone"].ToString() != "" ? sdr["Phone"].ToString() : myEventRequests.Phone);
                                    myEventRequests.ContactType = (sdr["ContactType"].ToString() != "" ? sdr["ContactType"].ToString() : myEventRequests.ContactType);
                                    myEventRequests.Message = (sdr["Message"].ToString() != "" ? sdr["Message"].ToString() : myEventRequests.Message);
                                    myEventRequests.AdminRemarks = (sdr["AdminRemarks"].ToString() != "" ? sdr["AdminRemarks"].ToString() : myEventRequests.AdminRemarks);
                                    myEventRequests.CurrencyCode = (sdr["CurrencyCode"].ToString() != "" ? sdr["CurrencyCode"].ToString() : myEventRequests.CurrencyCode);
                                    myEventRequests.CurrencyRate = (sdr["CurrencyRate"].ToString() != "" ? Decimal.Parse(sdr["CurrencyRate"].ToString()) : myEventRequests.CurrencyRate);
                                    myEventRequests.TotalAmt = (sdr["TotalAmt"].ToString() != "" ? Decimal.Parse(sdr["TotalAmt"].ToString()) : myEventRequests.TotalAmt);
                                    myEventRequests.TotalTaxAmt = (sdr["TotalTaxAmt"].ToString() != "" ? Decimal.Parse(sdr["TotalTaxAmt"].ToString()) : myEventRequests.TotalTaxAmt);
                                    myEventRequests.TotalDiscountAmt = (sdr["TotalDiscountAmt"].ToString() != "" ? Decimal.Parse(sdr["TotalDiscountAmt"].ToString()) : myEventRequests.TotalDiscountAmt);
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
            return myEventRequests;
        }

        public List<EventRequests> GetEventRequestsOfCustomer(int customerID)
        {
            List<EventRequests> Requestslist = null;
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
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerID;
                        cmd.CommandText = "[GetEventRequestsOfCustomer]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                    Requestslist = new List<EventRequests>();
                                    while (sdr.Read())
                                    {
                                        EventRequests myEventRequests = new EventRequests();
                                        {
                                            myEventRequests.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : myEventRequests.ID);
                                            myEventRequests.EventReqNo = (sdr["EventReqNo"].ToString() != "" ? sdr["EventReqNo"].ToString() : myEventRequests.EventReqNo);
                                            myEventRequests.EventType = (sdr["EventType"].ToString() != "" ? sdr["EventType"].ToString() : myEventRequests.EventType);
                                            myEventRequests.EventTitle = (sdr["EventTitle"].ToString() != "" ? sdr["EventTitle"].ToString() : myEventRequests.EventTitle);
                                            myEventRequests.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : myEventRequests.ContactName);
                                            myEventRequests.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : myEventRequests.Email);
                                            myEventRequests.Phone = (sdr["Phone"].ToString() != "" ? sdr["Phone"].ToString() : myEventRequests.Phone);
                                            myEventRequests.EventStatus = (sdr["EventStatus"].ToString() != "" ? int.Parse(sdr["EventStatus"].ToString()) : myEventRequests.EventStatus);
                                            myEventRequests.EventDesc = (sdr["EventDesc"].ToString() != "" ? sdr["EventDesc"].ToString() : myEventRequests.EventDesc);
                                            myEventRequests.FollowUpDate = (sdr["FollowUpDate"].ToString() != "" ? DateTime.Parse(sdr["FollowUpDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : myEventRequests.FollowUpDate);
                                            myEventRequests.EventDateTime = (sdr["EventDateTime"].ToString() != "" ? DateTime.Parse(sdr["EventDateTime"].ToString().ToString()).ToString("dd-MMM-yyyy") : myEventRequests.EventDateTime);
                                            myEventRequests.EventTime = (sdr["EventTime"].ToString() != "" ? sdr["EventTime"].ToString() : myEventRequests.EventTime);
                                            myEventRequests.NoOfPersons = (sdr["NoOfPersons"].ToString() != "" ? int.Parse(sdr["NoOfPersons"].ToString()) : myEventRequests.NoOfPersons);
                                            myEventRequests.Budget = (sdr["Budget"].ToString() != "" ? Decimal.Parse(sdr["Budget"].ToString()) : myEventRequests.Budget);
                                            myEventRequests.LookingFor = (sdr["LookingFor"].ToString() != "" ? sdr["LookingFor"].ToString() : myEventRequests.LookingFor);
                                            myEventRequests.RequirementSpec = (sdr["RequirementSpec"].ToString() != "" ? sdr["RequirementSpec"].ToString() : myEventRequests.RequirementSpec);
                                            myEventRequests.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : myEventRequests.ContactName);
                                            myEventRequests.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : myEventRequests.Email);
                                            myEventRequests.Phone = (sdr["Phone"].ToString() != "" ? sdr["Phone"].ToString() : myEventRequests.Phone);
                                            myEventRequests.ContactType = (sdr["ContactType"].ToString() != "" ? sdr["ContactType"].ToString() : myEventRequests.ContactType);
                                            myEventRequests.Message = (sdr["Message"].ToString() != "" ? sdr["Message"].ToString() : myEventRequests.Message);
                                            myEventRequests.AdminRemarks = (sdr["AdminRemarks"].ToString() != "" ? sdr["AdminRemarks"].ToString() : myEventRequests.AdminRemarks);
                                            myEventRequests.CurrencyCode = (sdr["CurrencyCode"].ToString() != "" ? sdr["CurrencyCode"].ToString() : myEventRequests.CurrencyCode);
                                            myEventRequests.CurrencyRate = (sdr["CurrencyRate"].ToString() != "" ? Decimal.Parse(sdr["CurrencyRate"].ToString()) : myEventRequests.CurrencyRate);
                                            myEventRequests.TotalAmt = (sdr["TotalAmt"].ToString() != "" ? Decimal.Parse(sdr["TotalAmt"].ToString()) : myEventRequests.TotalAmt);
                                            myEventRequests.TotalTaxAmt = (sdr["TotalTaxAmt"].ToString() != "" ? Decimal.Parse(sdr["TotalTaxAmt"].ToString()) : myEventRequests.TotalTaxAmt);
                                            myEventRequests.TotalDiscountAmt = (sdr["TotalDiscountAmt"].ToString() != "" ? Decimal.Parse(sdr["TotalDiscountAmt"].ToString()) : myEventRequests.TotalDiscountAmt);
                                        }
                                        Requestslist.Add(myEventRequests);
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
            return Requestslist;
        }

        public OperationsStatus UpdateEventRequests(EventRequests eventObj)
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
                        cmd.CommandText = "[UpdateEventRequests]";
                        cmd.CommandType = CommandType.StoredProcedure;
                      
                        cmd.Parameters.Add("@ID", SqlDbType.SmallInt).Value = eventObj.ID;
                        cmd.Parameters.Add("@Updateflag", SqlDbType.SmallInt).Value = eventObj.Updateflag;

                        cmd.Parameters.Add("@CurrencyCode", SqlDbType.NVarChar, 3).Value = eventObj.CurrencyCode;
                        cmd.Parameters.Add("@CurrencyRate", SqlDbType.Decimal).Value = eventObj.CurrencyRate;
                        cmd.Parameters.Add("@TotalAmt", SqlDbType.Decimal).Value = eventObj.TotalAmt;
                        cmd.Parameters.Add("@TotalTaxAmt", SqlDbType.Decimal).Value = eventObj.TotalTaxAmt;
                        cmd.Parameters.Add("@TotalDiscountAmt", SqlDbType.Decimal).Value = eventObj.TotalDiscountAmt;

                        cmd.Parameters.Add("@AdminRemarks", SqlDbType.NVarChar,-1).Value = eventObj.AdminRemarks;
                        cmd.Parameters.Add("@EventStatus", SqlDbType.SmallInt).Value = eventObj.EventStatus;

                        if (eventObj.FollowUpDate!=null && eventObj.FollowUpDate!="")
                        {
                            cmd.Parameters.Add("@FollowUpDate", SqlDbType.DateTime).Value = DateTime.Parse(eventObj.FollowUpDate);
                        }
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 50).Value = eventObj.logDetailsObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = eventObj.logDetailsObj.UpdatedDate;

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
      
        public OperationsStatus InsertEventsLog(EventRequests eventObj)
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

                        cmd.Parameters.Add("@ParentID", SqlDbType.SmallInt).Value = eventObj.ID;
                        cmd.Parameters.Add("@ParentType", SqlDbType.NVarChar, 20).Value = eventObj.ParentType;
                        cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, -1).Value = eventObj.Comments;
                        cmd.Parameters.Add("@CustomerNotifiedYN", SqlDbType.Bit).Value = null;

                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 50).Value = eventObj.logDetailsObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = eventObj.logDetailsObj.CreatedDate;

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

        public List<EventRequests> GetEventsLog(int EventRequestsID)
        {
            List<EventRequests> Requestslist = null;
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
                        cmd.Parameters.Add("@ParentID", SqlDbType.Int).Value = EventRequestsID;
                        cmd.Parameters.Add("@ParentType", SqlDbType.NVarChar,20).Value = "EventRequests";
                        cmd.CommandText = "[GetEventLogs]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Requestslist = new List<EventRequests>();
                                while (sdr.Read())
                                {
                                    EventRequests _eventRequestsObj = new EventRequests();
                                    {
                                       
                                        _eventRequestsObj.PrevComment = (sdr["Comment"].ToString() != "" ? sdr["Comment"].ToString() : _eventRequestsObj.PrevComment);
                                      
                                        _eventRequestsObj.CommentDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : _eventRequestsObj.CommentDate);

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
        #endregion Methods

    }
}