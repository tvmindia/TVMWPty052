using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System.Data.SqlClient;
using System.Data;

namespace PartyEC.RepositoryServices.Services
{
    public class CategoriesRepository :ICategoriesRepository
    {
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public CategoriesRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        #endregion DataBaseFactory

        #region Methods
      

        public List<Categories> GetAllCategory()
        {
            List<Categories> Categorylist = null;
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Categorylist;
        }

        public Categories GetCategory(int CategoryID)
        {
            Categories myCategory = new Categories();
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
                        cmd.CommandText = "[GetCategoryDetailByID]";
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = CategoryID;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                while (sdr.Read())
                                {
                                    
                                        myCategory.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : myCategory.ID);
                                        myCategory.Name = sdr["Name"].ToString();
                                        myCategory.Description = sdr["Description"].ToString();
                                        myCategory.ParentID=(sdr["ParentID"].ToString() != "" ? Int16.Parse(sdr["ParentID"].ToString()) : myCategory.ParentID);
                                    myCategory.ImageID = sdr["ImageID"].ToString();
                                    myCategory.URL= sdr["URL"].ToString();
                                    myCategory.Navigation = sdr["Navigation"].ToString()!=""?bool.Parse(sdr["Navigation"].ToString()):false;
                                    myCategory.System= sdr["System"].ToString() != "" ? bool.Parse(sdr["System"].ToString()):false;
                                    myCategory.Filter= sdr["Filter"].ToString() != "" ? bool.Parse(sdr["Filter"].ToString()):false;
                                    myCategory.Enable = sdr["Enable"].ToString() != "" ? bool.Parse(sdr["Enable"].ToString()):false;

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
            return myCategory;
        }

        public OperationsStatus InsertCategory(Categories CategoryObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return operationsStatusObj;
        }

        public OperationsStatus UpdateCategory(Categories CategoryObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return operationsStatusObj;
        }

        public OperationsStatus DeleteCategory(int CategoryID, OperationsStatus Status)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {

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