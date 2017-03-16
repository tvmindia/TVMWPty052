
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
    public class DynamicUIRepository : IDynamicUIRepository
    {
      private IDatabaseFactory _databaseFactory;
        /// <summary>
      /// Constructor Injection:-Getting IDatabaseFactory implementing object
        /// </summary>
        /// <param name="databaseFactory"></param>
      public DynamicUIRepository(IDatabaseFactory databaseFactory)
      {
          _databaseFactory = databaseFactory;
      }
        /// <summary>
        /// Get All Menues
        /// </summary>
        /// <returns>Menu List</returns>
        public List<Menu> GetAllMenues()
      {
          List<Menu> menuList = null;
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
                      cmd.CommandText = "[GetAllMenuItems]";
                      cmd.CommandType = CommandType.StoredProcedure;
                      using (SqlDataReader sdr = cmd.ExecuteReader())
                      {
                          if ((sdr != null) && (sdr.HasRows))
                          {
                              menuList = new List<Menu>();
                              while (sdr.Read())
                              {
                                  Menu menuObj = new Menu();
                                  {
                                      menuObj.ID =(sdr["ID"].ToString()!=""?Int16.Parse(sdr["ID"].ToString()):menuObj.ID);
                                      menuObj.ParentID=(sdr["ParentID"].ToString()!=""?Int16.Parse(sdr["ParentID"].ToString()):menuObj.ParentID);
                                      menuObj.MenuText = sdr["MenuText"].ToString();
                                      menuObj.Controller = sdr["Controller"].ToString();
                                      menuObj.Action = sdr["Action"].ToString();
                                      menuObj.Parameters = sdr["Parameters"].ToString();
                                  }
                                  menuList.Add(menuObj);
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
        
          return menuList;
      }

        public List<Treeview> GetTreeListForAttrSet(string AttributeSetID)
        {
            List<Treeview> TreeListData = null;
            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if(con.State==ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[GetAttributesforTreeUsingAttributeSet]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(AttributeSetID);
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if((sdr != null) && (sdr.HasRows))
                            {
                                TreeListData = new List<Treeview>();
                                while(sdr.Read())
                                {
                                    Treeview TreeviewObj = new Treeview();
                                    {
                                        TreeviewObj.ID = sdr["ID"].ToString();
                                        TreeviewObj.ParentID = (sdr["ParentID"].ToString() != "" ? Int16.Parse(sdr["ParentID"].ToString()) : TreeviewObj.ParentID);
                                        TreeviewObj.Name = sdr["Name"].ToString();
                                        TreeviewObj.level = sdr["level"].ToString();
                                        TreeviewObj.icon = sdr["icon"].ToString();
                                    }
                                    TreeListData.Add(TreeviewObj);
                                }
                                                        
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return TreeListData;
        }
        public List<Treeview> GetTreeListForAttr()
        {
            List<Treeview> TreeListData = null;
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
                        cmd.CommandText = "[GetAttributesforTreeRootEntitytype]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                TreeListData = new List<Treeview>();
                                while (sdr.Read())
                                {
                                    Treeview TreeviewObj = new Treeview();
                                    {
                                        TreeviewObj.ID = sdr["ID"].ToString();
                                        TreeviewObj.ParentID = (sdr["ParentID"].ToString() != "" ? Int16.Parse(sdr["ParentID"].ToString()) : TreeviewObj.ParentID);
                                        TreeviewObj.Name = sdr["Name"].ToString();
                                        TreeviewObj.level = sdr["level"].ToString();
                                        TreeviewObj.icon = sdr["icon"].ToString();
                                    }
                                    TreeListData.Add(TreeviewObj);
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
            return TreeListData;
        }
        public List<Treeview> GetTreeListForCategories()
        {
            List<Treeview> TreeListData = null;
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
                        cmd.CommandText = "[GetTreeListForCategories]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                TreeListData = new List<Treeview>();
                                while (sdr.Read())
                                {
                                    Treeview TreeviewObj = new Treeview();
                                    {
                                        TreeviewObj.ID = sdr["ID"].ToString();
                                        TreeviewObj.ParentID = (sdr["ParentID"].ToString() != "" ? Int16.Parse(sdr["ParentID"].ToString()) : TreeviewObj.ParentID);
                                        TreeviewObj.Name = sdr["Name"].ToString();
                                        TreeviewObj.level = sdr["level"].ToString();
                                    }
                                    TreeListData.Add(TreeviewObj);
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
            return TreeListData;
        }

    }
}