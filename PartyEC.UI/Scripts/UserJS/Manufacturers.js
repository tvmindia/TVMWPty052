var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function () {
    ChangeButtonPatchView("Manufacturers", "btnPatchManufacturerstab2", "Add"); //ControllerName,id of the container div,Name of the action
    try {       
        DataTables.ManufacturersTable = $('#tblManufacturers').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllManufacturers(),
             columns: [
             { "data": "ID" },
               { "data": "Name" },
               { "data": "country" },
               { "data": "country", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ], columnDefs: [
              {"render": function (data, type, row) {               
                      return data.Code;
                    }, "targets": 2
              },
              {"render": function (data, type, row) {
                      return data.Name;
                    }, "targets": 3
              }]
         });
    }
    catch (e) {
        notyAlert('error', e.message);

    }
});

function GetAllManufacturers() {
    try {
        debugger;
        var data = {};
        var ds = {};
        ds = GetDataFromServer("Manufacturers/GetAllManufacturers/", data);
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

//---------------------------------------Get Manufacturers Details By ID-------------------------------------//
function GetManufacturersByID(id) {
    try {
        debugger;
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Manufacturers/GetManufacturers/", data);
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


//---------------------------------------Edit Manufacturers--------------------------------------------------//
function Edit(currentObj) {
    //Tab Change on edit click
    debugger;
    $('#tabManufacturersDetails').trigger('click');
    ChangeButtonPatchView("Manufacturers", "btnPatchManufacturerstab2", "Edit");//ControllerName,id of the container div,Name of the action

    var rowData = DataTables.ManufacturersTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        fillManufacturers(rowData.ID);
    }
}
//---------------------------------------Fill Manufacturers--------------------------------------------------//
function fillManufacturers(ID) {
    debugger;
    var thisManufacturers = GetManufacturersByID(ID); //Binding Data  
    $("#ID").val(thisManufacturers.ID);
    $("#Name").val(thisManufacturers.Name)
    $("#deleteId").val(thisManufacturers.ID);
    $("#ddlcountry").val(thisManufacturers.country.Code);
  
    //$("#").attr('disabled', true);
}
//---------------------------------------Clear Fields-----------------------------------------------------//
function clearfields() {
    debugger;
    ChangeButtonPatchView("Manufacturers", "btnPatchManufacturerstab2", "Add");
    $("#ID").val("0")//ID is zero for New
    $("#deleteId").val("0")
    $("#Name").val("")
    $("#ddlcountry").val("");
    
    //$("#").attr('disabled', false);  
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
        fillManufacturers($("#ID").val())
        ResetForm();
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
    BindAllManufacturers();
    var i = JSON.parse(data)
    debugger;

    switch (i.Result) {

        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            var returnId = i.Record.ReturnValues
            fillManufacturers(returnId);
            ChangeButtonPatchView("Manufacturers", "btnPatchManufacturerstab2", "Edit");
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
    BindAllManufacturers();

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
    $('#tabManufacturersList').trigger('click');
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
    $('#tabManufacturersDetails').trigger('click');
    ChangeButtonPatchView("Manufacturers", "btnPatchManufacturerstab2", "Add"); //ControllerName,id of the container div,Name of the action
    clearfields();
}

//---------------------------------------Bind All Manufacturers----------------------------------------------//
function BindAllManufacturers() {
    try {
        DataTables.ManufacturersTable.clear().rows.add(GetAllManufacturers()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}