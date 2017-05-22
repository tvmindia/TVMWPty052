using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.RepositoryServices.Contracts
{
    public interface IInvoiceRepository
    {
        OperationsStatus InsertInvoiceHeader(Invoice invoiceObj);
        OperationsStatus InsertInvoiceDetail(InvoiceDetail invoiceDetailObj);
        List<Invoice> GetAllInvoices();
        bool CheckInvoicedOrNot(int ID);
    }
}