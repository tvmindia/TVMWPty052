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

        public List<Attributes> GetAllAttributes()
        {
            List<Attributes> attributelist = null;
                 try
            {
                attributelist = _attributesRepository.GetAllAttributes();

            }
            catch (Exception)
            {

            }
            return attributelist;
        }

        public Attributes GetAttributes(int AttributeID, OperationsStatus Status)
        {
            Attributes myattribute = null;
            try
            {
                myattribute = _attributesRepository.GetAttributes(AttributeID, Status);

            }
            catch (Exception)
            {

            }
            return myattribute;
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
    public class AttributeSetBusiness:IAttributeSetBusiness
    {
        private IAttributeSetRepository _attributeSetRepository;
        public AttributeSetBusiness (IAttributeSetRepository attributeSetRepository)
        {
            _attributeSetRepository = attributeSetRepository;
        }
        public List<AttributeSet> GetAllAttributeSet()
        {
            return _attributeSetRepository.GetAllAttributeSet();
        }
        public OperationsStatus InsertAttributeSet(AttributeSet attributeSetObj)
        {
            return _attributeSetRepository.InsertAttributeSet(attributeSetObj);
        }
        public OperationsStatus UpdateAttributeSet(AttributeSet attributeSetObj, int ID)
        {
            return _attributeSetRepository.UpdateAttributeSet(attributeSetObj, ID);
        }
    }
    public class AttributeToSetLinks : IAttributeToSetLinks
    {
        private IAttributeToSetLinksRepository _attributeToSetLinksRepository;
        public AttributeToSetLinks(IAttributeToSetLinksRepository attributeToSetLinksRepository)
        {
            _attributeToSetLinksRepository = attributeToSetLinksRepository;
        }
        public OperationsStatus TreeViewUpdateAttributeSetLink(List<AttributeSetLink> TreeViewData,int ID)
        {
            OperationsStatus OPObj = new OperationsStatus();
            _attributeToSetLinksRepository.DeleteAttributeSetLink(ID);
            foreach(AttributeSetLink i in TreeViewData)
            {
                if(i.AttributeSetID!=0)
                {
                    i.AttributeSetID = ID;
                    _attributeToSetLinksRepository.InsertAttributeSetLink(i);
                }
            }
            //return _attributeToSetLinksRepository.InsertAttributeSetLink();
            return OPObj;
        }

    }
}