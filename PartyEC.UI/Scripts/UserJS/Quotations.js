var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function () {
    try {
        //ChangeButtonPatchView("Quotations", "btnPatchtab1", "Add");
        DataTables.QuotationsTable = $('#tblQuotationsList').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllQuotations(),
             columns: [
               { "data": "ID" },
               { "data": "QuotationNo" },
               { "data": "QuotationDate" },
               { "data": "customerObj.Name", "defaultContent": "<i>-</i>" },
               { "data": "customerObj.Mobile", "defaultContent": "<i>-</i>" },
               { "data": "RequiredDate" },
               { "data": "Status", "defaultContent": "<i>-</i>" },
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
    try
    {
        DataTables.QuotationsItemTable = $('#tblQuotationsItemDetail').DataTable(
            {
                dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
                order: [],
                ordering:false,
                searching: false,
                paging: false,
                info: false,
                data: null,
                columns: [
                     { "data": "ProductID" },
                    { "data": "AttributeValues" },
                  { "data": "Qty" },      
                  { "data": "Status" },
                  { "data": "Price" },
                  { "data": "SubTotal" },
                  { "data": "TaxAmt" },
                  { "data": "DiscountAmt" },
                  { "data": "GrandTotal"}
                ],
                columnDefs: [{ 
                     "targets": [0],
                     "visible": false,
                     "searchable": false,
                }, {
                 "targets": [1],
                 "render": function (data, type, row) {
                     var returnstring = '';
                     if (data) {
                         debugger;
                         for (var ik = 0; ik < data.length; ik++) {
                             returnstring = returnstring + '<span><b>' +data[ik].Caption + '</b> : ' +(data[ik].Value != "" && data[ik].Value != null ? data[ik].Value: ' - ') + '</span><br/>';
                         }
                             }
                     return returnstring;
                     }
                     }]
            });
    }
    catch (e) {
        notyAlert('error', e.message);
    }
});

function GetAllQuotations() {
    try {   
        var data = {};
        var ds = {};
        ds = GetDataFromServer("Quotations/GetAllQuotations/", data);
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


function GetQuotationsByID(id) {
    try { 
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Quotations/GetQuotations/", data);
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

//---------------------------------------Edit Quotations--------------------------------------------------//
function Edit(currentObj) {
    //Tab Change on edit click 
    $('#tabQuotationsDetails').trigger('click');

    var rowData = DataTables.QuotationsTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        fillQuotations(rowData.ID); 
    }
}

//---------------------------------------Fill Quotations--------------------------------------------------//
function fillQuotations(ID) {

    var thisQuotations = GetQuotationsByID(ID); //Binding Data  
    $("#ID").val(thisQuotations.ID);
    $("#QuotationId").val(thisQuotations.ID);
    $("#EventsLogViewObj_ParentID").val(thisQuotations.ID);

    $("#lblQuotationsNo").text(thisQuotations.QuotationNo);
    $("#lblQuotationDate").text(thisQuotations.QuotationDate);
    $("#lblRequiredDate").text(thisQuotations.RequiredDate);
    $("#lblSourceIP").text(thisQuotations.SourceIP);
    debugger;
    $("#lblQuotationstatus").text(thisQuotations.Status); 

    $("#lblCustomerName").text(thisQuotations.customerObj.Name);
    $("#lblContactNo").text(thisQuotations.customerObj.Mobile);
    $("#lblCustomerEmail").text(thisQuotations.customerObj.Email);
    $("#imgPreviewCustomer").attr("src", thisQuotations.ImageUrl);

    $("#lblMessage").text(thisQuotations.Message);


    $("#Price").val(thisQuotations.Price);
    $("#AdditionalCharges").val(thisQuotations.AdditionalCharges);
    $("#DiscountAmt").val(thisQuotations.DiscountAmt);
    $("#TaxAmt").val(thisQuotations.TaxAmt);
    $("#Status").val(thisQuotations.Status);

    $("#lblTaxAmt").text(thisQuotations.TaxAmt);
    $("#lblDiscountAmt").text(thisQuotations.DiscountAmt);
    $("#lblSubTotal").text(thisQuotations.SubTotal);
    $("#lblGrandTotal").text(thisQuotations.GrandTotal);
    $("#lblAdditionalCharges").text(thisQuotations.AdditionalCharges);
    
   
    BindTablQuotationDetailList(thisQuotations.ID);
    //region comments
    
    $("#mailViewModelObj_CustomerEmail").val(thisQuotations.customerObj.Email);
    $("#mailViewModelObj_CustomerName").val(thisQuotations.customerObj.Name);
    $("#QuotationNo").val(thisQuotations.QuotationNo);
    BindComments();
}

function BindTablQuotationDetailList(ID) {
    DataTables.QuotationsItemTable.clear().rows.add(GetQuotationsDetailsByID(ID)).draw(false);
}

function GetQuotationsDetailsByID(id) {
    try { 
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Quotations/GetQuotationsDetails/", data);
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

function BindQuotationsTable() {  
    DataTables.QuotationsTable.clear().rows.add(GetAllQuotations()).draw(false);
    }

function SaveSuccess(data, status, xhr) {
    debugger;
    BindQuotationsTable();
    var i = JSON.parse(data) 
    switch (i.Result) { 
        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            var returnId = i.Record.ReturnValues
            fillQuotations(returnId);
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
        debugger;
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
        ds = GetDataFromServer("Quotations/GetEventsLog/", data);
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
    $('#tabQuotationsList').trigger('click');
}
 

