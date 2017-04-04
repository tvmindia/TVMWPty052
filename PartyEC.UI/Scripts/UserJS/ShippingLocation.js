var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function () {
    ChangeButtonPatchView("ShippingLocation", "btnPatchShippingLocationtab2", "Add"); //ControllerName,id of the container div,Name of the action
    try {
        debugger;       
        DataTables.ShippingLocationTable = $('#tblshippinglocation').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllShippingLocation(),
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
});

function GetAllShippingLocation() {
    try {
        var data = {};
        var ds = {};
        ds = GetDataFromServer("ShippingLocation/GetAllShippingLocation/", data);
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

//---------------------------------------Get ShippingLocation Details By ID-------------------------------------//
function GetShippingLocationByID(id) {
    try {
        debugger;
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("ShippingLocation/GetShippingLocation/", data);
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


//---------------------------------------Edit ShippingLocation--------------------------------------------------//
function Edit(currentObj) {
    //Tab Change on edit click
    debugger;
    $('#tabShippingLocationDetails').trigger('click');
    ChangeButtonPatchView("ShippingLocation", "btnPatchShippingLocationtab2", "Edit");//ControllerName,id of the container div,Name of the action

    var rowData = DataTables.ShippingLocationTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        fillShippingLocation(rowData.ID);
    }
}
//---------------------------------------Fill ShippingLocation--------------------------------------------------//
function fillShippingLocation(ID) {
    debugger;
    ResetForm();
    ChangeButtonPatchView("ShippingLocation", "btnPatchShippingLocationtab2", "Edit");
    var thisShippingLocation = GetShippingLocationByID(ID); //Binding Data  
    $("#ID").val(thisShippingLocation.ID);
    $("#Shipping_locId").val(thisShippingLocation.ID);
    $("#deleteId").val(thisShippingLocation.ID);
    //$("#lblShippingLocationID").text(thisShippingLocation.ID);
    $("#Name").val(thisShippingLocation.Name);
    

}
//---------------------------------------Clear Fields-----------------------------------------------------//
function clearfields() {
    $("#ID").val("0")//ID is zero for New
    $("#deleteId").val("0")
    $("#Name").val("")
    $("#Shipping_locId").val("New");
    ResetForm();

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
        fillShippingLocation($("#ID").val())
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
    BindAllShippingLocation();
    var i = JSON.parse(data)
    debugger;

    switch (i.Result) {

        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            var returnId = i.Record.ReturnValues
            fillShippingLocation(returnId);
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
    BindAllShippingLocation();
   

    var i = JSON.parse(data)
    debugger;

    switch (i.Result) {

        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            clearfields();
            ChangeButtonPatchView("ShippingLocation", "btnPatchShippingLocationtab2", "Add");
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
    $('#tabShippingLocationList').trigger('click');
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
    $('#tabShippingLocationDetails').trigger('click');
    ChangeButtonPatchView("ShippingLocation", "btnPatchShippingLocationtab2", "Add"); //ControllerName,id of the container div,Name of the action
    clearfields();
}

//---------------------------------------Bind All ShippingLocation----------------------------------------------//
function BindAllShippingLocation() {
    try {
        DataTables.ShippingLocationTable.clear().rows.add(GetAllShippingLocation()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}