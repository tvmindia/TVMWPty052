var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function () {
    ChangeButtonPatchView("SupplierLocations", "btnPatchSupplierLocationstab2", "Add"); //ControllerName,id of the container div,Name of the action
    try {
        debugger;
        DataTables.SupplierLocationsTable = $('#tblSupplierLocations').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllSupplierLocations(),
             columns: [
               { "data": "ID" },
               { "data": "SupplierName" },
               { "data": "LocationName" },
               { "data": "ShippingCharge", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ] 
         });
    }
    catch (e) {
        notyAlert('error', e.message);

    }
});

function GetAllSupplierLocations() {
    try {
        debugger;
        var data = {};
        var ds = {};
        ds = GetDataFromServer("SupplierLocations/GetAllSupplierLocations/", data);
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

//---------------------------------------Get SupplierLocations Details By ID-------------------------------------//
function GetSupplierLocationsByID(id) {
    try {
        debugger;
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("SupplierLocations/GetSupplierLocations/", data);
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


//---------------------------------------Edit SupplierLocations--------------------------------------------------//
function Edit(currentObj) {
    //Tab Change on edit click
    debugger;
    $('#tabSupplierLocationsDetails').trigger('click');
    ChangeButtonPatchView("SupplierLocations", "btnPatchSupplierLocationstab2", "Edit");//ControllerName,id of the container div,Name of the action

    var rowData = DataTables.SupplierLocationsTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        fillSupplierLocations(rowData.ID);
    }
}
//---------------------------------------Fill SupplierLocations--------------------------------------------------//
function fillSupplierLocations(ID) {
    debugger;
    var thisSupplierLocations = GetSupplierLocationsByID(ID); //Binding Data  
    $("#ID").val(thisSupplierLocations.ID);
    $("#SupplierID").val(thisSupplierLocations.SupplierID)
    $("#deleteId").val(thisSupplierLocations.ID);
  
    $("#LocationID").val(thisSupplierLocations.LocationID);
    $("#ShippingCharge").val(thisSupplierLocations.ShippingCharge);
    $("#LocationID").attr('disabled', true);
    $("#SupplierID").attr('disabled', true);

}
//---------------------------------------Clear Fields-----------------------------------------------------//
function clearfields() {
    debugger;
    ChangeButtonPatchView("SupplierLocations", "btnPatchSupplierLocationstab2", "Add");
    $("#ID").val("0")//ID is zero for New
    $("#deleteId").val("0")
    $("#Name").val("")
    $("#LocationID").val("");
    $("#SupplierID").val("");
    $("#ShippingCharge").val("");
    $("#LocationID").attr('disabled', false);
    $("#SupplierID").attr('disabled', false);

}
//---------------------------------------Button Patch Click Events------------------------------------------------//
function btnreset() {
    if ($("#ID").val() == 0) {
        clearfields();
    }
    else {
        fillSupplierLocations($("#ID").val())
    }
}
//---------------------------------------Save-------------------------------------------------------//
function clicksave() {
    debugger;
    var res = Validation();
    if (res) {
        $("#btnFormSave").click();
    }
}
function Validation() {
    return true;
}
function SaveSuccess(data, status, xhr) {
    BindAllSupplierLocations();
    var i = JSON.parse(data)
    debugger;

    switch (i.Result) {

        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            var returnId = i.Record.ReturnValues
            fillSupplierLocations(returnId);
            ChangeButtonPatchView("SupplierLocations", "btnPatchSupplierLocationstab2", "Edit");
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
function DeleteSuccess(data, status, xhr) {
    BindAllSupplierLocations(); 

    var i = JSON.parse(data)
    debugger;

    switch (i.Result) {

        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            clearfields();
           
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


//---------------------------------------Back-------------------------------------------------------//
function goback() {
    $('#tabSupplierLocationsList').trigger('click');
}
//---------------------------------------Delete-------------------------------------------------------//
function clickdelete() {
    debugger;
    var id = $("#ID").val();
    if (id != 0) {
        $("#btnFormDelete").click();
    }
    else {
        notyAlert('error', 'Please Select Attributes');
    }
}

//---------------------------------------Add New Click----------------------------------------------------//
function btnAddNew() {
    debugger;
    $('#tabSupplierLocationsDetails').trigger('click');
    ChangeButtonPatchView("SupplierLocations", "btnPatchSupplierLocationstab2", "Add"); //ControllerName,id of the container div,Name of the action
    clearfields();
}

//---------------------------------------Bind All SupplierLocations----------------------------------------------//
function BindAllSupplierLocations() {
    try {
        DataTables.SupplierLocationsTable.clear().rows.add(GetAllSupplierLocations()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}