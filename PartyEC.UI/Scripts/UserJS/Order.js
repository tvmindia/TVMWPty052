var DataTables = {};
$(document).ready(function () {
    DataTables.orderHeadertable = $('#tblOrderList').DataTable(
    {
        dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
        order: [],
        searching: true,
        paging: true,
        data: GetAllOrderHeader(),
        columns: [
          { "data": "ID" },
          { "data": "OrderNo"},
          { "data": "OrderDate" },
          { "data": "CustomerName" },
          { "data": "ContactNo" },
          { "data": "TotalOrderAmt" },
          { "data": "OrderStatus" },
          { "data": null, "orderable": false, "defaultContent": '<a onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
        ],
        columnDefs: [
         {
             "targets": [0],
             "visible": true,
             "searchable": true
         }
        ]
    });
    DataTables.orderDetailstable = $('#tblOrderDetailList').DataTable(
    {
        dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
        order: [],
        searching: true,
        paging: true,
        data: null,
        columns: [
          { "data": "OrderDetailID" },
          { "data": "ItemStatus" },
          { "data": "Qty" },
          { "data": "Price" },
          { "data": "Total" },
          { "data": "ShippingAmt" },
          { "data": "TaxAmt" },
          { "data": "DiscountAmt" },
          { "data": "SubTotal" }
        ],
        columnDefs: [
         {
             "targets": [0],
             "visible": true,
             "searchable": true
         }
        ]
    });
});
//Table Data Bind for Order Header
function GetAllOrderHeader()
{
    try {
        var data = "";
        var ds = {};
        ds = GetDataFromServer("Order/GetAllOrderHeader/", data);
        if (ds != '') {ds = JSON.parse(ds);}
        if (ds.Result == "OK") {return ds.Records;}
        if (ds.Result == "ERROR") {alert(ds.Message);}
    }
    catch (e) {
        notyAlert('error', e.message);
    }
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
//Edit For Order Header
function Edit(this_obj)
{
    debugger;
    $('#tabOrderDetails').trigger('click');

    var rowData = DataTables.orderHeadertable.row($(this_obj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        $("#ID").val(rowData.ID);
        var Result = GetOrderDetails(rowData.ID);
        BindGeneralSection(Result);
        BindAccountSection(Result);
        BindBillDetails(Result);
        BindShippingDetails(Result);
        BindPaymentInformation(Result);

        BindTableOrderDetailList(Result.ID)
    }
}
function BindGeneralSection(Result)
{
    $("#lblOrderNo").text(Result.OrderNo);
    $("#lblOrderDate").text(Result.OrderDate);
    $("#lblOrderStatus").text(Result.OrderStatus);
   // $("#lblSourceIP").text(Result.OrderNo);
}
function BindAccountSection(Result)
{
    $('#imgPreviewCustomer').attr('src', Result.CustomerURL);
    $("#lblCustomerName").text(Result.CustomerName);
    $("#lblContactNo").text(Result.ContactNo);
    $("#lblCustomerEmail").text(Result.CustomerEmail);
}
function BindBillDetails(Result)
{
    $('#lblName').text(Result.BillFirstName + "." + Result.BillMidName + "." + Result.BillLastName)
    $('#lblAddress').text(Result.BillAddress)
    $('#lblAddress').append('<br/>' + Result.BillCity)
    $('#lblAddress').append('<br/>' + Result.BillStateProvince)
    $('#lblAddress').append('<br/>' + Result.BillCountryCode)

    $('#lblContact').text(Result.BillContactNo)
    $('#BillFirstName').val(Result.BillFirstName);
    $('#BillMidName').val(Result.BillMidName);
    $('#BillLastName').val(Result.BillLastName);
    $('#BillAddress').val(Result.BillAddress);
    $('#BillCity').val(Result.BillCity);
    $('#BillContactNo').val(Result.BillContactNo);
    $('#BillStateProvince').val(Result.BillStateProvince);
    $('#CustomerID').val(Result.CustomerID);
    $('#hdnOrderHID').val(Result.ID);
    $('#BillCountryCode').val(Result.BillCountryCode);
}
function BindPaymentInformation(Result)
{
    $('#lblPaymentType').text(Result.PaymentType)
    $('#lblCurrencyCode').text(Result.CurrencyCode)
    $('#lblPaymentStatus').text(Result.PaymentStatus)
}
function BindShippingDetails(Result)
{
    $('#lblShipDisName').text(Result.ShipFirstName + "." + Result.ShipMidName + "." + Result.ShipLastName)
    $('#lblShipDisAddress').text(Result.ShipAddress)
    $('#lblShipDisAddress').append('<br/>' + Result.ShipCity)
    $('#lblShipDisAddress').append('<br/>' + Result.ShipStateProvince)
    $('#lblShipDisAddress').append('<br/>' + Result.ShipCountryCode)
    $('#lblShipDisContact').text(Result.ShipContactNo)

    $('#ShipFirstName').val(Result.ShipFirstName);
    $('#ShipMidName').val(Result.ShipMidName);
    $('#ShipLastName').val(Result.ShipLastName);
    $('#ShipAddress').val(Result.ShipAddress);
    $('#ShipCity').val(Result.ShipCity);
    $('#ShipContactNo').val(Result.ShipContactNo);
    $('#ShipStateProvince').val(Result.ShipStateProvince);
    $('#hdnOrderHIDShip').val(Result.ID);
    $('#ShipCountryCode').val(Result.ShipCountryCode);
}
function BindTableOrderDetailList(ID)
{
    DataTables.orderDetailstable.clear().rows.add(GetAllOrdersList(ID)).draw(false);
}
function CheckinBillingChanges(data)
{
    var i = JSON.parse(data.responseText)
    switch (i.Result) {
        case "OK":
            notyAlert('success', i.Records.StatusMessage);
            var Result = GetOrderDetails($("#hdnOrderHID").val());
            BindBillDetails(Result);
            $('.close').click();
            break;
        case "ERROR":
            notyAlert('success', i.Records.StatusMessage);
            break;

    }
}
function CheckinShipingChanges(data) {
    var i = JSON.parse(data.responseText)
    switch (i.Result) {
        case "OK":
            notyAlert('success', i.Records.StatusMessage);
            var Result = GetOrderDetails($("#hdnOrderHIDShip").val());
            BindShippingDetails(Result);
            $('.close').click();
            break;
        case "ERROR":
            notyAlert('success', i.Records.StatusMessage);
            break;

    }
}
function GetOrderDetails(ID)
{
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

    }
}