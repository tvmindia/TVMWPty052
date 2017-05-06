﻿var DataTables = {};
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
        notyAlert('error', e.message);
    }

    try
    {
        DataTables.orderDetailstableInvoiceRegion = $('#tblOrderDetailsInvoiceRegion').DataTable(
    {
        dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
        order: [],
        searching: true,
        paging: true,
        data: null,
        columns: [
          { "data": "OrderDetailID" },
          { "data": "ProductSpecXML" },
          { "data": "ItemStatus" },
          { "data": "Qty" },
          { "data": "Price" },
          { "data": "Total" },
          { "data": "ShippingAmt" },
          { "data": "TaxAmt" },
          { "data": "DiscountAmt" },
          { "data": "TotalDiscountAmt" },
          { "data": "SubTotal" }
        ],
        columnDefs: [
         {
             "targets": [0],
             "visible": true,
             "searchable": true
         },
         {//hiding hidden column 
             "targets": [1],
             "visible": true,
             "searchable": true,
             "render": function (data, type, full, meta) {
               
                 var Name = "<b>" + data.split("||")[0] + "</b>";
                 var Spec = (data.split("||")[1]).split("><");
                 for (var i = 0; i < Spec.length - 1; i++) {
                     if (i > 0) {
                         var html = Spec[i].replace(">", " : ");
                         Name = Name + "</br>" + (html.split("</")[0]);
                     }

                 }
                 return Name;
             }
         }
        ]
    });
    }
    catch(e)
    {
        notyAlert('error', e.message);
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
        notyAlert('error', e.message);
    }
}

function EditInvoice(curobj)
{
    try
    {
        var rowData = DataTables.invoiceTable.row($(curobj).parents('tr')).data();
        if (rowData)
        {
            $('#tabInvoiceDetails a').trigger('click');
            var Result = GetOrderDetails(rowData.ParentID);
            if(Result)
            {
                BindGeneralSectionInvoiceRegion(Result);
                BindAccountSectionInvoiceRegion(Result);
                BindPaymentInformationInvoiceRegion(Result);
                BindShippingHandlingSectionInvoiceRegion(Result);
                BindTableOrderDetailListInvoiceRegion(rowData.ParentID);
                BindOrderSummeryInvoiceRegion(Result);
            }

        }
       

    }
    catch(e)
    {
        notyAlert('error', e.message);
    }
}
function BindTableOrderDetailListInvoiceRegion(ID) {
    DataTables.orderDetailstableInvoiceRegion.clear().rows.add(GetAllOrdersList(ID)).draw(false);
}
function GetAllOrdersList(ID) {
   
    try {
        data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Order/GetAllOrdersList/", data);
        if (ds != '') { ds = JSON.parse(ds); }
        if (ds.Result == "OK") { return ds.Records; }
        if (ds.Result == "ERROR") { alert(ds.Message); }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
function BindGeneralSectionInvoiceRegion(Result) {
 
    $("#lblOrderNoInvoiceRegion").text(Result.OrderNo);
    $("#lblOrderDateInvoiceRegion").text(Result.OrderDate);
    $("#lblOrderStatusInvoiceRegion").text(Result.OrderStatus);
    $("#lblSourceIPInvoiceRegion").text(Result.SourceIP);
    $("#lblRevNoInvoiceRegion").text(Result.OrderRev);

}
function BindAccountSectionInvoiceRegion(Result) {
    $('#imgPreviewCustomerInvoiceRegion').attr('src', Result.CustomerURL);
    $("#lblCustomerNameInvoiceRegion").text(Result.CustomerName);
    $("#lblContactNoInvoiceRegion").text(Result.ContactNo);
    $("#lblCustomerEmailInvoiceRegion").text(Result.CustomerEmail);
   
}
function BindPaymentInformationInvoiceRegion(Result) {
   
    $('#lblPaymentTypeInvoiceRegion').text(Result.PaymentType);
    $('#lblCurrencyCodeInvoiceRegion').text(Result.CurrencyCode);
    switch(Result.PayStatusCode)
    {
        case 0: $('#PaymentStatusList').text('In process');
            break;
        case 1: $('#PaymentStatusList').text('Success');
            break;
        case 2: $('#PaymentStatusList').text('Failes');
            break;
        default:
            $('#PaymentStatusList').text('_');
            break;
    }
  
}
function BindShippingHandlingSectionInvoiceRegion(Result) {
    $('#lblShippingLocationInvoiceRegion').text(Result.ShippingLocationName)
    $('#lblShippingAmtInvoiceRegion').text(Result.TotalShippingAmt)
}

function BindOrderSummeryInvoiceRegion(Result) {
  
    var OrderSummeryList = GetOrderSummery(Result.ID);
    $('#tdSubTotalInvoiceRegion').text(OrderSummeryList.SubTotalOrderSummery + Result.CurrencyCode)
    $('#tdTaxTotalInvoiceRegion').text(OrderSummeryList.TaxAmtOrderSummery + Result.CurrencyCode)
    $('#tdDeliveryCostsInvoiceRegion').text(OrderSummeryList.ShippingCostOrderSummery + Result.CurrencyCode)
    $('#tdOrderDiscountInvoiceRegion').text(OrderSummeryList.DiscountAmtOrderSummery + Result.CurrencyCode)
    $('.strGrandTotal').text(OrderSummeryList.GrandTotalOrderSummery + Result.CurrencyCode)
}


function GetOrderDetails(ID) {
    try {

        var ds = {};
        data = { "ID": ID };
        ds = GetDataFromServer("Order/GetOrderDetails/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            alert(ds.Message);
        }

    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
function GetOrderSummery(ID) {
    try {
        data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Order/GetOrderSummery/", data);
        if (ds != '') { ds = JSON.parse(ds); }
        if (ds.Result == "OK") { return ds.Records; }
        if (ds.Result == "ERROR") { alert(ds.Message); }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}