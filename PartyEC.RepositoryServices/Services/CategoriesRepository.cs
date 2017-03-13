using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;

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

        public Categories GetCategory(int CategoryID, OperationsStatus Status)
        {
            Categories myCategory = null;
            try
            {

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