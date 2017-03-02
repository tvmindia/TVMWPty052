using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.RepositoryServices.Services
{
    public class AttributesRepository : IAttributesRepository
    {
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public AttributesRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        #endregion DataBaseFactory


        #region Methods

        public List<Attributes> GetAllAttributes(Attributes attributesObj)
        {
            List<Attributes> AttributesList = null;

            return AttributesList;
        }

        public OperationsStatus InsertAttributes(Attributes attributesObj)
        {
            OperationsStatus operationsStatusObj = null;
            
            return operationsStatusObj;
        }

        public OperationsStatus UpdateAttributes(Attributes attributesObj)
        {
            OperationsStatus operationsStatusObj = null;

            return operationsStatusObj;
        }

        #endregion Methods

        
    }
    public class AttributeSetRepository : IAttributeSetRepository
    {

    }
    public class AttributeToSetLinks : IAttributeToSetLinks
    {

    }
}