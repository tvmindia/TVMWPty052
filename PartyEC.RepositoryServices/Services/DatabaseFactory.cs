
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PartyEC.RepositoryServices.Services
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private SqlConnection SQLCon = null;
       

        public SqlConnection GetDBConnection()
        {
            try
            {
                SQLCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PartyECConnection"].ConnectionString);
                //if (SQLCon.State == ConnectionState.Closed)
                //{

                //    SQLCon.Open();
                //}

            }
            catch (Exception ex)
            {
               
                throw ex;

            }
            return SQLCon;
        }


        public Boolean DisconectDB()
        {
            try
            {
                if (SQLCon.State == ConnectionState.Open)
                {
                    SQLCon.Close();
                    SQLCon.Dispose();
                    return true;
                }
               

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
    }
}