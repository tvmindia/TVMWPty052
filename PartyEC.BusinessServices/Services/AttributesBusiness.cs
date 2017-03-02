using PartyEC.BusinessServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;

namespace PartyEC.BusinessServices.Services
{
    public class AttributesBusiness : IAttributesBusiness
    {

        #region ConstructorInjection
        
        private IAttributesRepository _attributesRepository;

        public AttributesBusiness (IAttributesRepository attributeRepository)
        {
            _attributesRepository = attributeRepository;
        }
        #endregion ConstructorInjection

        public List<Attributes> GetAllAttributes(Attributes attributesObj)
        {
            List<Attributes> attributelist = null;
                 try
            {
                attributelist = _attributesRepository.GetAllAttributes(attributesObj);

            }
            catch (Exception)
            {

            }
            return attributelist;
        }

        public OperationsStatus InsertAttributes(Attributes attributesObj)
        {
            return _attributesRepository.InsertAttributes(attributesObj);
        }

        public OperationsStatus UpdateAttributes(Attributes attributesObj)
        {
            return _attributesRepository.UpdateAttributes(attributesObj);
        }
    }
}