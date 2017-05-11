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
        private ICommonBusiness _commonBusiness;

        public QuotationsBusiness(IQuotationsRepository QuatationsRepository, IAttributesRepository attributesRepository, IMailBusiness mailBusiness,ICommonBusiness commonBusiness)
        {
            _QuotationsRepository = QuatationsRepository;
            _attributesRepository = attributesRepository;
            _mailBusiness = mailBusiness;
            _commonBusiness = commonBusiness;
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
                    QuotationsObj.AttributeValues = _commonBusiness.GetAttributeValueFromXML(QuotationsObj.ProductSpecXML);
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return QuotationsObj;
        }

       

        public OperationsStatus UpdateQuotations(Quotations quotationsObj)
        {
            OperationsStatus OperationsStatusObj = null;
            try
            {
                OperationsStatusObj = _QuotationsRepository.UpdateQuotations(quotationsObj);
            }
            catch (Exception ex)
            {
                throw ex;
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
                    _mail.Body = _mail.Body.Replace("{Status}", quotationsObj.StatusText.ToString());
                    _mail.IsBodyHtml = true;
                    _mail.Subject = "Quotation No:" + quotationsObj.QuotationNo;
                    _mail.To = quotationsObj.customerObj.Email;
                    sendsuccess = await _mailBusiness.MailSendAsync(_mail);
                    //quotationsObj.EventsLogViewObj.CustomerNotifiedYN = Mailstatus;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return sendsuccess;
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
            catch (Exception ex)
            {
                throw ex;
            }
            return OSatObj;
        }
    }
}