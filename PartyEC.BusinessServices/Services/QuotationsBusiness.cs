using PartyEC.BusinessServices.Contracts;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;

namespace PartyEC.BusinessServices.Services
{
    public class QuotationsBusiness: IQuotationsBusiness
    {

        private IQuotationsRepository _QuotationsRepository;

        public QuotationsBusiness(IQuotationsRepository QuatationsRepository)
        {
            _QuotationsRepository = QuatationsRepository;
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
            catch (Exception)
            {
            }
            return OperationsStatusObj;
        }
    }
}