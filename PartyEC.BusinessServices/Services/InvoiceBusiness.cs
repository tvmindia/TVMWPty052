using PartyEC.BusinessServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Services
{
    public class InvoiceBusiness:IInvoiceBusiness
    {
        private IInvoiceBusiness _invoiceRepository;
        public InvoiceBusiness(IInvoiceBusiness invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

    }
}