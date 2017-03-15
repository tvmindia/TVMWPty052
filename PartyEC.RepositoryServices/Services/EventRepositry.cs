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
    public class EventRepositry : IEventRepositry

    {
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public EventRepositry(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        #endregion DataBaseFactory

        #region Methods

        public List<Event> GetAllEvents()
        {
            List<Event> Eventlist = null;
            try
            {//GetAllEvents
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[GetAllEvents]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Eventlist = new List<Event>();
                                while (sdr.Read())
                                {
                                    Event eventObj = new Event();
                                    {
                                        eventObj.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : eventObj.ID);
                                        eventObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : eventObj.Name);
                                        eventObj.RelatedCategoriesCSV= (sdr["RelatedCategoriesCSV"].ToString() != "" ? sdr["RelatedCategoriesCSV"].ToString() : eventObj.RelatedCategoriesCSV);

                                    }
                                    Eventlist.Add(eventObj);
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

            return Eventlist;
        }

        public Event GetEvent(int EventID, OperationsStatus Status)
        {
            Event myEvent = null;
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
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = EventID;
                        cmd.CommandText = "[GetEvent]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    myEvent = new Event();
                                    myEvent.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : myEvent.ID);
                                    myEvent.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : myEvent.Name);
                                    myEvent.RelatedCategoriesCSV = (sdr["RelatedCategoriesCSV"].ToString() != "" ? sdr["RelatedCategoriesCSV"].ToString() : myEvent.RelatedCategoriesCSV);                              
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
            return myEvent;
        }

        public OperationsStatus InsertEvent(Event EventObj)
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
                        cmd.CommandText = "[InsertEvents]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = EventObj.Name;
                        cmd.Parameters.Add("@RelatedCategoriesCSV", SqlDbType.NVarChar, 250).Value = EventObj.RelatedCategoriesCSV;
                        cmd.Parameters.Add("@EventImageID", SqlDbType.UniqueIdentifier).Value = null;//EventObj.EventImageID;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 10).Value = EventObj.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = EventObj.commonObj.CreatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Not Successfull!";
                                break;
                            case "1":
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

        public OperationsStatus UpdateEvent(Event EventObj)
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
                        cmd.CommandText = "[UpdateEvents]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = EventObj.ID;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = EventObj.Name;
                        cmd.Parameters.Add("@RelatedCategoriesCSV", SqlDbType.NVarChar, 250).Value = EventObj.RelatedCategoriesCSV;
                        cmd.Parameters.Add("@EventImageID", SqlDbType.UniqueIdentifier).Value = null; //EventObj.EventImageID;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 10).Value = EventObj.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = EventObj.commonObj.UpdatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":                                
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = "Updation Not Successfull!";
                                break;
                            case "1":                               
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = "Updation Successfull!";
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

        public OperationsStatus DeleteEvent(int EventID, OperationsStatus Status)
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
                        cmd.CommandText = "[DeleteEvent]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar, 50).Value = EventID;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                Status.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                Status.StatusMessage = "Deletion Not Successfull!";
                                break;
                            case "1":
                                Status.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                Status.StatusMessage = "Deletion Successfull!";
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

        #endregion Methods
    }
}