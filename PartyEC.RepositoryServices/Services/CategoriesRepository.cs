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
        Const ConstObj = new Const();
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
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[GetCategories]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Categorylist = new List<Categories>();
                                while (sdr.Read())
                                {
                                    Categories categoriesObj = new Categories();
                                    {
                                        categoriesObj.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : categoriesObj.ID);
                                        categoriesObj.Name = sdr["Name"].ToString();
                                        categoriesObj.Description = sdr["Description"].ToString();
                                        categoriesObj.ParentID = (sdr["ParentID"].ToString() != "" ? Int16.Parse(sdr["ParentID"].ToString()) : categoriesObj.ParentID);
                                        categoriesObj.ImageID = sdr["ImageID"].ToString();
                                        categoriesObj.URL = sdr["URL"].ToString();
                                        categoriesObj.Navigation = sdr["Navigation"].ToString() != "" ? bool.Parse(sdr["Navigation"].ToString()) : false;
                                        categoriesObj.System = sdr["System"].ToString() != "" ? bool.Parse(sdr["System"].ToString()) : false;
                                        categoriesObj.Filter = sdr["Filter"].ToString() != "" ? bool.Parse(sdr["Filter"].ToString()) : false;
                                        categoriesObj.Enable = sdr["Enable"].ToString() != "" ? bool.Parse(sdr["Enable"].ToString()) : false;
                                        categoriesObj.ChildrenCount= (sdr["ChildrenCount"].ToString() != "" ? Int16.Parse(sdr["ChildrenCount"].ToString()) : categoriesObj.ChildrenCount);
                                        categoriesObj.CategoryOrder= (sdr["CategoryOrder"].ToString() != "" ? float.Parse(sdr["CategoryOrder"].ToString()) : categoriesObj.ChildrenCount);
                                    }
                                    Categorylist.Add(categoriesObj);
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
                        cmd.CommandText = "[GetCategory]";
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
                        cmd.CommandText = "[InsertCategory]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar,250).Value = CategoryObj.Name;
                        if(CategoryObj.ParentID!=0)
                        {
                            cmd.Parameters.Add("@ParentID", SqlDbType.Int).Value = CategoryObj.ParentID;
                        }
                        cmd.Parameters.Add("@FilterYN", SqlDbType.Bit).Value = CategoryObj.Filter;
                        cmd.Parameters.Add("@NavigationYN", SqlDbType.Bit).Value = CategoryObj.Navigation;
                        cmd.Parameters.Add("@EnableYN", SqlDbType.Bit).Value = CategoryObj.Enable;
                        cmd.Parameters.Add("@Description", SqlDbType.NVarChar, -1).Value = CategoryObj.Description;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = CategoryObj.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = CategoryObj.commonObj.CreatedDate;
                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameterID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                        outparameter.Direction = ParameterDirection.Output;
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
                                //Insert Successfull
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

        public OperationsStatus UpdateCategory(Categories CategoryObj)
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
                        cmd.CommandText = "[UpdateCategory]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = CategoryObj.ID;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = CategoryObj.Name;
                        if(CategoryObj.ParentID!=0)
                        {
                            cmd.Parameters.Add("@ParentID", SqlDbType.Int).Value = CategoryObj.ParentID;
                        }                        
                        cmd.Parameters.Add("@FilterYN", SqlDbType.Bit).Value = CategoryObj.Filter;
                        cmd.Parameters.Add("@NavigationYN", SqlDbType.Bit).Value = CategoryObj.Navigation;
                        cmd.Parameters.Add("@EnableYN", SqlDbType.Bit).Value = CategoryObj.Enable;
                        cmd.Parameters.Add("@Description", SqlDbType.NVarChar, -1).Value = CategoryObj.Description;
                        if (CategoryObj.ImageID != null && CategoryObj.ImageID != "")
                        {
                            cmd.Parameters.Add("@ImageID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(CategoryObj.ImageID);
                        }
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = CategoryObj.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.SmallDateTime).Value = CategoryObj.commonObj.UpdatedDate;
                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                // not Successfull

                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.UpdateFailure;
                                break;
                            case "1":
                                //Insert Successfull
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

        public OperationsStatus UpdatePositionNo(Categories CategoryObj)
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
                        cmd.CommandText = "[UpdateCategoryLink]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = CategoryObj.ID;
                        cmd.Parameters.Add("@PositionNo", SqlDbType.Float).Value = CategoryObj.PositionNo;
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = CategoryObj.ProductID;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = CategoryObj.commonObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.SmallDateTime).Value = CategoryObj.commonObj.UpdatedDate;
                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                // not Successfull

                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.UpdateFailure;
                                break;
                            case "1":
                                //Insert Successfull
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

        public OperationsStatus DeleteCategory(int CategoryID)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                    SqlParameter outparameter = null;
                SqlParameter OutparameterURL = null;
                    using (SqlConnection con = _databaseFactory.GetDBConnection())
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            cmd.Connection = con;
                            cmd.CommandText = "[DeleteCategoryWithChild]";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 50).Value = CategoryID;
                            outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                            OutparameterURL = cmd.Parameters.Add("@ImageURL", SqlDbType.NVarChar,-1);
                            outparameter.Direction = ParameterDirection.Output;
                            OutparameterURL.Direction = ParameterDirection.Output;
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
                                if (outparameter.Value.ToString() == "1")
                                {
                                    try
                                    {
                                        if (OutparameterURL.Value.ToString() != "")
                                        {
                                            System.IO.File.Delete(HttpContext.Current.Server.MapPath(OutparameterURL.Value.ToString()));
                                        }


                                    }
                                    catch (System.IO.IOException e)
                                    {
                                        throw e;

                                    }
                                }
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
        #endregion Methods
        public bool ExistOrNot(int CategoryID)
        {
            SqlParameter outparameter = null;
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
                        cmd.CommandText = "[ChildExistOrNot]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = CategoryID;
                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.Bit);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                    }
                }
                        
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return bool.Parse(outparameter.Value.ToString());
        }
        

    }
}