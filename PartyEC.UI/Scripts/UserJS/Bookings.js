var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function () {

    try {

        DataTables.BookingsTable = $('#tblBookingsList').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllBookings(),
             columns: [
               { "data": "ID" },
               { "data": "BookingNo" },
               { "data": "BookingDate" },
               { "data": "customerObj.Name", "defaultContent": "<i>-</i>" },
               { "data": "customerObj.Mobile", "defaultContent": "<i>-</i>" },
               { "data": "RequiredDate" },
               { "data": "StatusText", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{//hiding hidden column 
                 
                 "targets": [0],
                 "visible": false,
                 "searchable": false
             }]
         });
    }
    catch (e) {
        notyAlert('error', e.message);
    }
    try {
        DataTables.BookingsItemTable = $('#tblBookingsItemDetail').DataTable(
            {
                dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
                order: [],
                ordering: false,
                searching: false,
                paging: false,
                info: false,
                data: null,
                columns: [
                     { "data": "ProductID" },
                    { "data": "AttributeValues" },
                  { "data": "StatusText" },
                     { "data": "Qty" },
                  { "data": "Price" },
                  { "data": "SubTotal" },
                  { "data": "TaxAmt" },
                  { "data": "DiscountAmt" },
                  { "data": "Total" }
                ],
                columnDefs: [{
                    "targets": [0],
                    "visible": false,
                    "searchable": false,
                }
                , {
                    "targets": [1],
                    "render": function (data, type, row)
                    {
                        var returnstring = '';
                        var product = row.ProductName;
                        if (data) {
                            for (var ik = 0; ik < data.length; ik++) {
                                returnstring = returnstring + '<span><b>' + data[ik].Caption + '</b> : ' + (data[ik].Value != "" && data[ik].Value != null ? data[ik].Value : ' - ') + '</span><br/>';
                            }
                        }
                        return product + '<br/>' + returnstring;
                    }
                }]
            });
    }
    catch (e) {
        notyAlert('error', e.message);
    }
    try {
        DataTables.BookingsItemTableInvoiceRegion = $('#tblBookingDetailsInvoiceRegion').DataTable(
            {
                dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
                order: [],
                ordering: false,
                searching: false,
                paging: false,
                info: false,
                data: null,
                columns: [
                     { "data": "ProductID" },
                    { "data": "AttributeValues" },
                  { "data": "StatusText" },
                     { "data": "Qty" },
                  { "data": "Price" },
                  { "data": "SubTotal" },
                  { "data": "TaxAmt" },
                  { "data": "DiscountAmt" },
                  { "data": "Total" }
                ],
                columnDefs: [{
                    "targets": [0],
                    "visible": false,
                    "searchable": false,
                }
                , {
                    "targets": [1],
                    "render": function (data, type, row) {
                        var returnstring = '';
                        var product = row.ProductName;
                        if (data) {
                            for (var ik = 0; ik < data.length; ik++) {
                                returnstring = returnstring + '<span><b>' + data[ik].Caption + '</b> : ' + (data[ik].Value != "" && data[ik].Value != null ? data[ik].Value : ' - ') + '</span><br/>';
                            }
                        }
                        return product + '<br/>' + returnstring;
                    }
                }]
            });
    }
    catch (e) {
        notyAlert('error', e.message);
    }
});



function GetAllBookings() {
    try { 
        var data = {};
        var ds = {};
        ds = GetDataFromServer("Bookings/GetAllBookings/", data);
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

function GetBookingsByID(id) {
    try {
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Bookings/GetBookings/", data);
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

//---------------------------------------Edit Bookings--------------------------------------------------//
function Edit(currentObj) {
    //Tab Change on edit click   

    $('#tabBookingsDetails').removeClass('disabled');
    $('#tabBookingsDetails a').attr('data-toggle', 'tab');
    $('#tabBookingsDetails a').trigger('click');

    var rowData = DataTables.BookingsTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        fillBookings(rowData.ID);
    }
}

//---------------------------------------Fill Quotations--------------------------------------------------//
function fillBookings(ID) {
    var thisBookings = GetBookingsByID(ID);

    //Binding Data  
    $("#ID").val(thisBookings.ID);
    $("#BookingId").val(thisBookings.ID);
    $("#EventsLogViewObj_ParentID").val(thisBookings.ID);

    $("#lblBookingNo").text(thisBookings.BookingNo);
    $("#lblBookingDate").text(thisBookings.BookingDate);
    $("#lblRequiredDate").text(thisBookings.RequiredDate);
    $("#lblSourceIP").text(thisBookings.SourceIP);
    $("#lblBookingstatus").text(thisBookings.StatusText);

    $("#lblCustomerName").text(thisBookings.customerObj.Name);
    $("#lblContactNo").text(thisBookings.customerObj.Mobile);
    $("#lblCustomerEmail").text(thisBookings.customerObj.Email);
    $("#imgPreviewCustomer").attr("src", thisBookings.ImageUrl);
    $("#lblMessage").text(thisBookings.Message);
    $("#ProductName").val(thisBookings.ProductName);
    $("#Price").val(thisBookings.Price);
    $("#AdditionalCharges").val(thisBookings.AdditionalCharges);
    $("#DiscountAmt").val(thisBookings.DiscountAmt);
    $("#TaxAmt").val(thisBookings.TaxAmt);
    $("#Status").val(thisBookings.Status);
    $("#Qty").val(thisBookings.Qty);

    $("#lblTaxAmt").text(thisBookings.TaxAmt);
    $("#lblDiscountAmt").text(thisBookings.DiscountAmt);
    $("#lblSubTotal").text(thisBookings.SubTotal);
    $("#lblGrandTotal").text(thisBookings.GrandTotal);
    $("#lblAdditionalCharges").text(thisBookings.AdditionalCharges); 
    BindTablBookingsDetailList(thisBookings.ID);
    //region comments

    $("#mailViewModelObj_CustomerEmail").val(thisBookings.customerObj.Email);
    $("#mailViewModelObj_CustomerName").val(thisBookings.customerObj.Name);
    $("#BookingNo").val(thisBookings.BookingNo);
    BindComments();
    if ($("#Price").val() == 0 && $("#lblGrandTotal").text() == 0) {
        ChangeButtonPatchView("Bookings", "btnPatchBookings", "Edit_List");
    }
    else {
        ChangeButtonPatchView("Bookings", "btnPatchBookings", "Send");
    }
    $("#lblInvoiceTaxAmt").text(thisBookings.TaxAmt);
    $("#lblInvoiceDiscountAmt").text(thisBookings.DiscountAmt);
    $("#lblInvoiceSubTotal").text(thisBookings.SubTotal);
    $("#lblInvoiceGrandTotal").text(thisBookings.GrandTotal);
    $("#lblInvoiceAdditionalCharges").text(thisBookings.AdditionalCharges);
    BindGeneralSectionInvoiceRegion(thisBookings);
    BindAccountSectionInvoiceRegion(thisBookings);
}

function BindTablBookingsDetailList(ID) {
    DataTables.BookingsItemTable.clear().rows.add(GetBookingsDetailsByID(ID)).draw(false);
}
function TabActionInvoiceRegion()
{
    ChangeButtonPatchView("Bookings", "btnPatchBookings", "Invoice");
    DataTables.BookingsItemTableInvoiceRegion.clear().rows.add(GetBookingsDetailsByID($("#ID").val())).draw(false);
}
function BindGeneralSectionInvoiceRegion(Result) {
  
    $("#lblBookingNoInvoiceRegion").text(Result.BookingNo);
    $("#lblBookingDateInvoiceRegion").text(Result.BookingDate);
    $("#lblRequiredDateInvoiceRegion").text(Result.RequiredDate);
    $("#lblSourceIPInvoiceRegion").text(Result.SourceIP);
    $("#lblStatusInvoiceRegion").text(Result.StatusText);

}
function BindAccountSectionInvoiceRegion(Result) {
    $('#imgPreviewCustomerInvoiceRegion').attr('src', Result.ImageUrl);
    $("#lblCustomerNameInvoiceRegion").text(Result.customerObj.Name);
    $("#lblContactNoInvoiceRegion").text(Result.customerObj.Mobile);
    $("#lblCustomerEmailInvoiceRegion").text(Result.customerObj.Email);
    //$("#hdnAccountEmailIDInvoiceRegion").val(Result.CustomerEmail);
    //$("#hdnAccountCustomerNameInvoiceRegion").val(Result.CustomerName);
}
function GetBookingsDetailsByID(id) {
    try {
    
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Bookings/GetBookingsDetails/", data);
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

function BindBookingsTable() {
    DataTables.BookingsTable.clear().rows.add(GetAllBookings()).draw(false);
}

function SaveSuccess(data, status, xhr) {
    BindBookingsTable();
    var i = JSON.parse(data)
    switch (i.Result) {
        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            var returnId = i.Record.ReturnValues
            fillBookings(returnId);
            BindComments();
            break;
        case "Error":
            notyAlert('error', i.Record.StatusMessage);
            break;
        case "ERROR":
            notyAlert('error', i.Message);
            break;
        default:
            break;
    }
}

function BindComments()   // To Display Previous Comment history
{
    $("#Comments").val("");
    $("#CommentsDisplay").empty();
    id = $("#ID").val();// assigning id for binding comments.
    var CommentList = GetEventsLog(id);
    if (CommentList != null) {

        for (var i = 0; i < CommentList.length; i++) {
            var cnt = $('<li id="Comment' + i + '" class="list-group-item col-md-12" style="background-color:#f4f0f0;">' + CommentList[i].Comment + '<span class="badge">' + CommentList[i].CommentDate + '</span></li>');
            $("#CommentsDisplay").append(cnt);
        }
    }
}

//---------------------------------------Get Events Logs Details By ID-------------------------------------//
function GetEventsLog(id) {
    try {
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Bookings/GetEventsLog/", data);
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

function goback() {
    $('#tabBookingsList').trigger('click');
}
function tabListClick() {
    ChangeButtonPatchView("Bookings", "btnPatchBookings", "List");
    $('#tabBookingsDetails').addClass('disabled');
    $('#tabBookingsDetails a').attr('data-toggle', '');
}

function SendBookingsMail() {
    var BookingsViewModel = new Object();
    BookingsViewModel.ID = $("#ID").val();
    BookingsViewModel.BookingNo = $("#lblBookingNo").text();
    BookingsViewModel.BookingDate = $("#lblBookingDate").text();
    BookingsViewModel.ProductName = $("#ProductName").val();
    BookingsViewModel.RequiredDate = $("#lblRequiredDate").text();
    BookingsViewModel.SourceIP = $("#lblSourceIP").text();
    BookingsViewModel.StatusText = $("#lblBookingstatus").text();
    BookingsViewModel.Message = $("#lblMessage").text();
    BookingsViewModel.Price = $("#Price").val();
    BookingsViewModel.Qty = $("#Qty").val();
    BookingsViewModel.AdditionalCharges = $("#AdditionalCharges").val();
    BookingsViewModel.DiscountAmt = $("#DiscountAmt").val();
    BookingsViewModel.TaxAmt = $("#TaxAmt").val();
    BookingsViewModel.Status = $("#Status").val();
    BookingsViewModel.SubTotal = $("#lblSubTotal").text();
    BookingsViewModel.GrandTotal = $("#lblGrandTotal").text();

    var CustomerViewModel = new Object();
    CustomerViewModel.Email = $("#mailViewModelObj_CustomerEmail").val();
    CustomerViewModel.Name = $("#lblCustomerName").text();
    BookingsViewModel.customerObj = CustomerViewModel;

    var data = "{'bookingsObj':" + JSON.stringify(BookingsViewModel) + "}";
    PostDataToServer('Bookings/SendBooking/', data, function (JsonResult) {
        if (JsonResult != '') {
            switch (JsonResult.Result) {
                case "OK":
                    notyAlert('success', JsonResult.Message);
                    break;
                case "ERROR":
                    notyAlert('error', JsonResult.Message);
                    break;
                default:
                    break;
            }
        }
    })
}
function SubmitInvoice() {
    var ID = $('#ID').val();
    var DetailList = [];
    var TableDetail = DataTables.BookingsItemTableInvoiceRegion.rows().data();
    var InvoiceViewModel = new Object();
    InvoiceViewModel.ID = null;
    InvoiceViewModel.InvoiceNo = null;
    InvoiceViewModel.ParentID = ID;
    InvoiceViewModel.ParentType = "Booking";
    InvoiceViewModel.InvoiceDate = null;
    InvoiceViewModel.PaymentStatus = $("#lblStatusInvoiceRegion").text();
    for (var i = 0; i < TableDetail.length; i++) {
        var InvoiceDetailViewModel = new Object();
        InvoiceDetailViewModel.ID = null;
        InvoiceDetailViewModel.InvoiceID = ID;
        InvoiceDetailViewModel.OrderItemID = TableDetail[i].ItemID;
        InvoiceDetailViewModel.InvoiceAmt = TableDetail[i].GrandTotal;
        DetailList.push(InvoiceDetailViewModel);
    }
    InvoiceViewModel.DetailList = DetailList;
    var data = "{'InvoiceViewObj':" + JSON.stringify(InvoiceViewModel) + "}";
    PostDataToServer('Bookings/InsertInvoice/', data, function (JsonResult) {
        if (JsonResult != '') {
            switch (JsonResult.Result) {
                case "OK":
                    notyAlert('success', JsonResult.Records.StatusMessage);
                    break;
                case "ERROR":
                    notyAlert('error', JsonResult.Records.StatusMessage);
                    break;
                default:
                    break;
            }
        }
    })
}





