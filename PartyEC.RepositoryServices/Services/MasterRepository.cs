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
    public class MasterRepository: IMasterRepository
    {
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public MasterRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        public List<Manufacturer> GetAllManufacturers()
        {
            List<Manufacturer> manufacturesList = null;
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
                        cmd.CommandText = "[GetMasterManufacturers]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                manufacturesList = new List<Manufacturer>();
                                while (sdr.Read())
                                {
                                    Manufacturer _manufacturer = new Manufacturer();
                                    {
                                        _manufacturer.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : _manufacturer.ID);
                                        _manufacturer.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _manufacturer.Name);
                                        _manufacturer.country.Code = (sdr["CountryCode"].ToString() != "" ? sdr["CountryCode"].ToString() : _manufacturer.country.Code);
                                        _manufacturer.country.Name = (sdr["CountryName"].ToString() != "" ? sdr["CountryName"].ToString() : _manufacturer.country.Name);
                                   
                                    }
                                    manufacturesList.Add(_manufacturer);
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

            return manufacturesList;


        }


        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> suppliersList = null;
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
                        cmd.CommandText = "[GetMasterSuppliers]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                suppliersList = new List<Supplier>();
                                while (sdr.Read())
                                {
                                    Supplier _supplier = new Supplier();
                                    {
                                        _supplier.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : _supplier.ID);
                                        _supplier.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _supplier.Name);
                                     

                                    }
                                    suppliersList.Add(_supplier);
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

            return suppliersList;


        }




    }
}