﻿using PartyEC.BusinessServices.Contracts;
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
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _attributesRepository.InsertAttributes(attributesObj);
            }
            catch (Exception)
            {
            }
            return OperationsStatusObj; 
        }

        public OperationsStatus UpdateAttributes(Attributes attributesObj)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _attributesRepository.UpdateAttributes(attributesObj);
            }
            catch (Exception)
            {
            }
            return OperationsStatusObj; 
        }

        public OperationsStatus DeleteAttributes(int AttributeID)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj= _attributesRepository.DeleteAttributes(AttributeID);
            }
            catch (Exception)
            {
            }
            return OperationsStatusObj;
        }
        
        public List<Attributes> GetAllAttributeBySet(int AttributeSetID,bool IsConfigurable,string EntityType="Product")
        {
            List<Attributes> attributelist = null;
            try
            {
                //Filters the list with  IsConfigurable boolean,
                //sorts the list by  DisplayOrder
                attributelist = _attributesRepository.GetAllAttributeBySet(AttributeSetID);
                attributelist= attributelist.Where((attr => attr.ConfigurableYN == IsConfigurable && attr.EntityType== EntityType))
                .OrderBy(attr => attr.attributeSetLinkObj.DisplayOrder).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return attributelist;
        }

        public List<AttributeValues> GetAttributeContainer(int AttributeSetID, string type)
        {//Used for web api to get rating attributes
            List<AttributeValues> myattribute = null;
            try
            {
                myattribute = _attributesRepository.GetAttributeContainer(AttributeSetID, type);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myattribute;
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
        public OperationsStatus DeleteAttributeSet(int ID)
        {
                return _attributeSetRepository.DeleteAttributeSet(ID);
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

            OperationsStatus operationsStatusObj = new OperationsStatus();
            try
            {
                //Delete the link data usng AttributeSet ID
                operationsStatusObj= _attributeToSetLinksRepository.DeleteAttributeSetLink(ID);
                //
                foreach (AttributeSetLink i in TreeViewData)
                {
                    if (i.AttributeSetID != 0)
                    {
                        i.AttributeSetID = ID;
                        operationsStatusObj=_attributeToSetLinksRepository.InsertAttributeSetLink(i);
                    }
                }

            }
            catch(Exception ex)
            {
                operationsStatusObj.StatusMessage = ex.Message;
                operationsStatusObj.StatusCode = 0;
            }
            return operationsStatusObj;
        }

    }
}