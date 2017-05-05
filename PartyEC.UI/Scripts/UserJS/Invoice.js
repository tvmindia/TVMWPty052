var DataTables = {};
$(document).ready(function () {

    try {

        DataTables.invoiceTable = $('#tblInvoices').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllInvoices(),
             columns: [
               { "data": "ID", "defaultContent": "<i>-</i>" },
               { "data": "InvoiceNo", "defaultContent": "<i>-</i>" },
               { "data": "ParentID", "defaultContent": "<i>-</i>" },
               { "data": "ParentType", "defaultContent": "<i>-</i>" },
               { "data": "InvoiceDate", "defaultContent": "<i>-</i>" },
               { "data": "PaymentStatus", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="EditInvoice(this)"><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }

             ],
             columnDefs: [
                
                    
             ]

         });
    }
    catch (e) {
        notyAlert('errror', e.message);
    }
});




function GetAllInvoices() {
    try {
     
        var data = {};
        var ds = {};
        ds = GetDataFromServer("Invoice/GetAllInvoices/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.Message);
        }
    }
    catch (e) {
        notyAlert('errror', e.message);
    }
}

function EditInvoice(curobj)
{
    try
    {
        $('#tabInvoiceDetails a').trigger('click');
    }
    catch(e)
    {
        notyAlert('error', e.message);
    }
}
