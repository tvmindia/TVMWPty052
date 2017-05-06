using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Services
{
    public class InvoiceBusiness:IInvoiceBusiness
    {
        private IInvoiceRepository _invoiceRepository;
        public InvoiceBusiness(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public OperationsStatus InsertInvoice(Invoice invoiceObj)
        {
            OperationsStatus operationStatusObj = null;
            operationStatusObj = InsertInvoiceHeader(invoiceObj);
            if (operationStatusObj.StatusCode == 1)
            {
                if (invoiceObj.DetailList != null)
                {
                    foreach (var i in invoiceObj.DetailList)
                    {
                        i.InvoiceID = int.Parse(operationStatusObj.ReturnValues.ToString());
                        i.LogDetails = invoiceObj.LogDetails;
                        InsertInvoiceDetail(i);
                    }
                }
            }
            return operationStatusObj;
        }
        public OperationsStatus InsertInvoiceHeader(Invoice invoiceObj)
        {
            return _invoiceRepository.InsertInvoiceHeader(invoiceObj);
        }
        public OperationsStatus InsertInvoiceDetail(InvoiceDetail invoiceDetailObj)
        {
            return _invoiceRepository.InsertInvoiceDetail(invoiceDetailObj);
        }

        public List<Invoice> GetAllInvoices()
        {
            List<Invoice> invoicelist = null;
            try
            {
                invoicelist = _invoiceRepository.GetAllInvoices();
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return invoicelist;
        }
    }
}