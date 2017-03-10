using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;

namespace PartyEC.RepositoryServices.Services
{
    public class CategoryRepository :ICategoryRepository
    {
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public CategoryRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        #endregion DataBaseFactory

        #region Methods
      

        public List<Category> GetAllCategory()
        {
            List<Category> Categorylist = null;
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Categorylist;
        }

        public Category GetCategory(int CategoryID, OperationsStatus Status)
        {
            Category myCategory = null;
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myCategory;
        }

        public OperationsStatus InsertCategory(Category CategoryObj)
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

        public OperationsStatus UpdateCategory(Category CategoryObj)
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