using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Contracts
{
    public interface IInvoiceBusiness
    {
        OperationsStatus InsertInvoice(Invoice invoiceObj);
        OperationsStatus InsertInvoiceHeader(Invoice invoiceObj);
        OperationsStatus InsertInvoiceDetail(InvoiceDetail invoiceDetailObj);
    }
}