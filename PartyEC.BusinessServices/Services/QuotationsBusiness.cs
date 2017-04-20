using PartyEC.BusinessServices.Contracts;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using System.Xml;

namespace PartyEC.BusinessServices.Services
{
    public class QuotationsBusiness: IQuotationsBusiness
    {

        private IQuotationsRepository _QuotationsRepository;
        private IAttributesRepository _attributesRepository;

        public QuotationsBusiness(IQuotationsRepository QuatationsRepository, IAttributesRepository attributesRepository)
        {
            _QuotationsRepository = QuatationsRepository;
            _attributesRepository = attributesRepository;
        }

        public List<Quotations> GetAllQuotations()
        {
            List<Quotations> Quotationslist = null;
            try
            {
                Quotationslist = _QuotationsRepository.GetAllQuotations();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Quotationslist;
        }

        public Quotations GetQuotations(int QuotationsID)
        {
            Quotations QuotationsObj = null;
            try
            {
                QuotationsObj = _QuotationsRepository.GetQuotations(QuotationsID);
                QuotationsObj.AttributeValues = GetAttributeValueFromXML(QuotationsObj.ProductSpecXML); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return QuotationsObj;
        }
        
        //have to write as common function GetAttributeValueFromXML
        private List<AttributeValues> GetAttributeValueFromXML(string XML)
        {
            List<AttributeValues> myAttributeValueList = null;
            List<Attributes> attributelist = null; 
            try
            {
                attributelist = _attributesRepository.GetAllAttributes();//Selecting Attributes List

                myAttributeValueList = new List<AttributeValues>();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(XML);

                XmlNodeList dataNodes = xmlDoc.SelectNodes("//options");
                foreach (XmlNode node in dataNodes)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        AttributeValues myAttributeValues = new AttributeValues();
                        //checking the attribute list for Caption by Comparing XML Node
                        List<Attributes> Matchingattribute = attributelist.Where(attr => attr.Name == childNode.Name).ToList();
                        if (Matchingattribute.Count > 0)
                        {//if Matching attribute found its captions will be taken for display
                            myAttributeValues.Caption = Matchingattribute[0].Caption;
                            myAttributeValues.Value = childNode.InnerXml;
                        }
                        else
                        {//if Matching attribute not found Childnode Name will be taken for display
                            myAttributeValues.Caption = childNode.Name;
                            myAttributeValues.Value = childNode.InnerXml;
                        }
                        myAttributeValueList.Add(myAttributeValues);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return myAttributeValueList;

        }

        public OperationsStatus UpdateQuotations(Quotations quotationsObj)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _QuotationsRepository.UpdateQuotations(quotationsObj);
            }
            catch (Exception)
            {
            }
            return OperationsStatusObj;
        }

        public List<Quotations> GetCustomerQuotations(int CustomerID)
        {
            List<Quotations> Quotationslist = null;
            try
            {
                Quotationslist = _QuotationsRepository.GetCustomerQuotations(CustomerID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Quotationslist;
        }
    }
}