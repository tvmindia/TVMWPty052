var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function () {
    ChangeButtonPatchView("Supplier", "btnPatchSuppliers", "Add"); //ControllerName,id of the container div,Name of the action
    try {
        debugger; 
        DataTables.supplierTable = $('#tblsuppliers').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllSuppliers(),
             columns: [
               { "data": "ID" },
               { "data": "Name" },
               { "data": "CreatedDate", "defaultContent": "<i>-</i>" }, 
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ]
         });
    }
    catch (e) { 
        notyAlert('error', e.message);

    }
    InitializeEvents();
});

function GetAllSuppliers() {
    try {
        var data = { };
        var ds = {};
        ds = GetDataFromServer("Supplier/GetAllSuppliers/", data);
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

//---------------------------------------Get Suppliers Details By ID-------------------------------------//
function GetSupplierByID(id) {
    try {
        debugger;
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Supplier/GetSupplier/", data);
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
//------------------------------ Initialize Events------------------------------------------------------//

function InitializeEvents() {
    ChangeButtonPatchView("Supplier", "btnPatchSuppliers", "SuppliersList"); //ControllerName,id of the container div,Name of the action
}

//---------------------------------------Edit Suppliers--------------------------------------------------//
function Edit(currentObj) {
    //Tab Change on edit click
    debugger;
    $('#tabSupplierDetails').trigger('click');
    ChangeButtonPatchView("Supplier", "btnPatchSuppliers", "Edit");//ControllerName,id of the container div,Name of the action
 
    var rowData = DataTables.supplierTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        fillSupplier(rowData.ID);
    }
}
//---------------------------------------Fill Suppliers--------------------------------------------------//
function fillSupplier(ID) {
    debugger;
    ResetForm();
    ChangeButtonPatchView("Supplier", "btnPatchSuppliers", "Edit");
    var thissupplier = GetSupplierByID(ID); //Binding Data  
    $("#ID").val(thissupplier.ID);
    $("#deleteId").val(thissupplier.ID);
    $("#SuppliersId").val(thissupplier.ID);
    $("#Name").val(thissupplier.Name); 
 
}
//---------------------------------------Clear Fields-----------------------------------------------------//
function clearfields() {
    ResetForm();
    $("#ID").val("0")//ID is zero for New
    $("#deleteId").val("0")
    $("#Name").val("")
    $("#SuppliersId").val("New");
  
}

function ResetForm() {
    var validator = $("#mainform").validate();
    $('#mainform').find('.field-validation-error span').each(function () {
        validator.settings.success($(this));
    });
    validator.resetForm();
}
//---------------------------------------Button Patch Click Events------------------------------------------------//
function btnreset() {
    if ($("#ID").val() == 0) {
        clearfields();
    }
    else { 
        fillSupplier($("#ID").val())
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
    BindAllSuppliers(); 
    var i = JSON.parse(data)
    debugger;

    switch (i.Result) {

        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            var returnId = i.Record.ReturnValues
            fillSupplier(returnId);
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
    BindAllSuppliers();
  

    var i = JSON.parse(data)
    debugger;

    switch (i.Result) {

        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            clearfields();
            ChangeButtonPatchView("Supplier", "btnPatchSuppliers", "Add");
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
    $('#tabSupplierList').trigger('click');
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
function btnAddNew(id) {
    if (id != 1)
    {
        $('#tabSupplierDetails').trigger('click');
    }
    ChangeButtonPatchView("Supplier", "btnPatchSuppliers", "Add"); //ControllerName,id of the container div,Name of the action
    clearfields();
}

//---------------------------------------Bind All Suppliers----------------------------------------------//
function BindAllSuppliers() {
    try {
        DataTables.supplierTable.clear().rows.add(GetAllSuppliers()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}