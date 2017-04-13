var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function () {
    try {
        //ChangeButtonPatchView("Quotations", "btnPatchtab2", "Edit");
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
                   //  { "data": "ProductSpecXML" },
                  { "data": "Qty" },      
                  { "data": "Status" },
                  { "data": "Price" },
                  { "data": "SubTotal" },
                  { "data": "TaxAmt" },
                  { "data": "DiscountAmt" },
                  { "data": "GrandTotal"}
                ],
                columnDefs: [
                 { 
                     "targets": [0],
                     "visible": false,
                     "searchable": false,
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
  //  ChangeButtonPatchView("Reviews", "btnPatchtab2", "Edit");//ControllerName,id of the container div,Name of the action

    var rowData = DataTables.QuotationsTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        fillQuotations(rowData.ID); 
    }
}

//---------------------------------------Fill Quotations--------------------------------------------------//
function fillQuotations(ID) {
   
  //  ChangeButtonPatchView("Supplier", "btnPatchtab2", "Edit");
    var thisQuotations = GetQuotationsByID(ID); //Binding Data  
    debugger;
    $("#ID").val(thisQuotations.ID);
    $("#EventLogParentID").val(thisQuotations.ID);
    $("#lblQuotationsNo").text(thisQuotations.QuotationNo);
    $("#lblQuotationDate").text(thisQuotations.QuotationDate);
    $("#lblRequiredDate").text(thisQuotations.RequiredDate);
    $("#lblSourceIP").text(thisQuotations.SourceIP);
    $("#lblCustomerName").text(thisQuotations.customerObj.Name);
    $("#lblContactNo").text(thisQuotations.customerObj.Mobile);
    $("#lblCustomerEmail").text(thisQuotations.customerObj.Email);
    $("#imgPreviewCustomer").attr("src", thisQuotations.ImageUrl);
    $("#lblMessage").text(thisQuotations.Message);

    $("#Status").val(thisQuotations.Status);
    $("#lblSubTotal").text(thisQuotations.SubTotal);
    $("#lblGrandTotal").text(thisQuotations.GrandTotal);
    $("#lblAdditionalCharges").text(thisQuotations.AdditionalCharges); 
   


    BindTablQuotationDetailList(thisQuotations.ID);
}

function BindTablQuotationDetailList(ID) {
    debugger;
    DataTables.QuotationsItemTable.clear().rows.add(GetQuotationsDetailsByID(ID)).draw(false);
}

function GetQuotationsDetailsByID(id) {
    try {
        debugger;
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

