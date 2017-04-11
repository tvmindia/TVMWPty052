using PartyEC.BusinessServices.Contracts;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Services
{
    public class QuotationsBusiness: IQuotationsBusiness
    {

        private IQuotationsRepository _QuotationsRepository;

        public QuotationsBusiness(IQuotationsRepository QuatationsRepository)
        {
            _QuotationsRepository = QuatationsRepository;
        }
    }
}