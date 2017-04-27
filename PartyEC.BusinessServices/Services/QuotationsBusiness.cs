using PartyEC.BusinessServices.Contracts;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using System.Xml;
using System.Threading.Tasks;
using System.IO;

namespace PartyEC.BusinessServices.Services
{
    public class QuotationsBusiness : IQuotationsBusiness
    {

        private IQuotationsRepository _QuotationsRepository;
        private IAttributesRepository _attributesRepository;
        private IMailBusiness _mailBusiness;

        public QuotationsBusiness(IQuotationsRepository QuatationsRepository, IAttributesRepository attributesRepository, IMailBusiness mailBusiness)
        {
            _QuotationsRepository = QuatationsRepository;
            _attributesRepository = attributesRepository;
            _mailBusiness = mailBusiness;
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
                if (QuotationsObj.ProductSpecXML != null)
                {
                    QuotationsObj.AttributeValues = GetAttributeValueFromXML(QuotationsObj.ProductSpecXML);
                }               
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

        public List<Quotations> GetCustomerQuotations(int CustomerID, bool Ishistory)
        {
            List<Quotations> Quotationslist = null;
            try
            {
                Quotationslist = _QuotationsRepository.GetCustomerQuotations(CustomerID, Ishistory);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Quotationslist;
        }

        public async Task<bool> QuotationEmail(Quotations quotationsObj)
        {
            bool sendsuccess = false;
            try
            {
                if (quotationsObj.customerObj.Email != "")
                {


                    Mail _mail = new Mail();
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/PartyEcTemplates/Quotation.html")))
                    {
                        _mail.Body = reader.ReadToEnd();
                    }
                    _mail.Body = _mail.Body.Replace("{CustomerName}", quotationsObj.customerObj.Name);
                    _mail.Body = _mail.Body.Replace("{QuotationDate}", quotationsObj.QuotationDate);
                    _mail.Body = _mail.Body.Replace("{ProductName}", quotationsObj.ProductName);
                    _mail.Body = _mail.Body.Replace("{QuotationNo}", quotationsObj.QuotationNo);
                    _mail.Body = _mail.Body.Replace("{RequiredDate}", quotationsObj.RequiredDate);
                    _mail.Body = _mail.Body.Replace("{Qty}", quotationsObj.Qty.ToString());
                    _mail.Body = _mail.Body.Replace("{Price}", quotationsObj.Price.ToString());
                    _mail.Body = _mail.Body.Replace("{tax}", quotationsObj.TaxAmt.ToString());
                    _mail.Body = _mail.Body.Replace("{additionalCharges}", quotationsObj.AdditionalCharges.ToString());
                    _mail.Body = _mail.Body.Replace("{discount}", quotationsObj.DiscountAmt.ToString());
                    _mail.Body = _mail.Body.Replace("{subTotal}", quotationsObj.SubTotal.ToString());
                    _mail.Body = _mail.Body.Replace("{grandTotal}", quotationsObj.GrandTotal.ToString());

                    _mail.IsBodyHtml = true;
                    _mail.Subject = "Quotation No:" + quotationsObj.QuotationNo;
                    _mail.To = quotationsObj.customerObj.Email;
                    sendsuccess = await _mailBusiness.MailSendAsync(_mail);
                    //quotationsObj.EventsLogViewObj.CustomerNotifiedYN = Mailstatus;
                }
            }
            catch (Exception ex)
            {
                return sendsuccess;
            }
            return sendsuccess;
        }

        public OperationsStatus InsertQuotations(Quotations quotationsObj)
        {
            OperationsStatus OSatObj = null;
            try
            {
                OSatObj = _QuotationsRepository.InsertQuotations(quotationsObj);

            }
            catch (Exception)
            {

            }
            return OSatObj;
        }
    }
}