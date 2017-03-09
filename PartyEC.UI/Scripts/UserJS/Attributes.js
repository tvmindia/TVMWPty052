var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function ()
{ 
    try {
        debugger;
        var AttributesViewModel = new Object();
        DataTables.attributeTable = $('#tblattributes').DataTable(
         {
             dom: '<"top"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllAttributes(AttributesViewModel),
             columns: [
               { "data": "ID" },
               { "data": "Name" },
               { "data": "Caption", "defaultContent": "<i>-</i>" },
               { "data": "AttributeType", "defaultContent": "<i>-</i>" },
               { "data": "CSValues", "defaultContent": "<i>-</i>" },
               { "data": "EntityType", "defaultContent": "<i>-</i>" },
               { "data": "ConfigurableYN", "defaultContent": "<i>-</i>" },
               { "data": "FilterYN", "defaultContent": "<i>-</i>" },
               { "data": "MandatoryYN", "defaultContent": "<i>-</i>" },
               { "data": "ComparableYN", "defaultContent": "<i>-</i>" },                 
               { "data": null, "orderable": false, "defaultContent": '<a onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{ "targets": [0], "visible": false, "searchable": false }]
         });
    }
    catch (e) {
        alert(e.message);
    }
});

//------------------------------$$$$$----FUNCTIONS----$$$$$-----------------------------------------------//

//---------------------------------------Edit Attributes--------------------------------------------------//
function Edit(currentObj) {
    //Tab Change on edit click
    $('#tabattributeDetails').trigger('click');
    var rowData = DataTables.attributeTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        fillAttributes(rowData.ID);
    }
}
//---------------------------------------Fill Attributes--------------------------------------------------//
function fillAttributes(ID)
{
    debugger;
    var thisattribute = GetAttributesDetailsByID(ID); //Binding Data
    //Hidden
    $("#ID").val(thisattribute.ID)
    $("#deleteId").val(thisattribute.ID)//for delete action
    
    //Textboxes
    $("#Name").val(thisattribute.Name);
    $("#Caption").val(thisattribute.Caption)
    $("#AttributeType").val(thisattribute.AttributeType)
    $("#CSValues").val(thisattribute.CSValues)
    //Dropdown
    $("#EntityType").val(thisattribute.EntityType) 
    //Radiobutton
    if (thisattribute.ConfigurableYN == false)
    { $("#married-false").prop('checked', true); }
    else { $("#married-true").prop('checked', true); }
    //CheckBoxes
    if (thisattribute.FilterYN == false)
    { $("#FilterYN").prop('checked', false); }
    else { $("#FilterYN").prop('checked', true); }
    if (thisattribute.MandatoryYN == false)
    { $("#MandatoryYN").prop('checked', false); }
    else { $("#MandatoryYN").prop('checked', true); }
    if (thisattribute.ComparableYN == false)
    { $("#ComparableYN").prop('checked', false); }
    else { $("#ComparableYN").prop('checked', true); }
}
//---------------------------------------Clear Fields-----------------------------------------------------//
function clearfields() {
    debugger;
    $("#ID").val("0")//ID is zero for New
    $("#Name").val("")
    $("#Caption").val("")
    $("#AttributeType").val("")
    $("#CSValues").val("")
    $("#EntityType").val("")
    $("#ComparableYN").prop('checked', false);
    $("#MandatoryYN").prop('checked', false);
    $("#FilterYN").prop('checked', false);
    $("#married-false").prop('checked', false);
    $("#married-true").prop('checked', false);
}
//---------------------------------------Reset the  Feilds------------------------------------------------//
function btnreset() {
    if ($("#ID").val() == 0) {
        clearfields();
    }
    else {
        fillAttributes($("#ID").val())
    }
}
//---------------------------------------Save Click-------------------------------------------------------//
function clicksave() {
    debugger;
    var res = Validation();
    if (res) {
        $("#btnFormSave").click();
    }
    else {
      
    }
}

function clickdelete() {
    debugger;

    $("#btnFormDelete").click();
}
//---------------------------------------Validation-------------------------------------------------------//
function Validation() {

//client side validation
    return true;
}


//---------------------------------------Add New Click----------------------------------------------------//
function btnAddNew() {
    $('#tabattributeDetails').trigger('click');
    clearfields();
}
//---------------------------------------Save Click alerts------------------------------------------------//
function attributeSaveSuccess() {
    //alert("success");
    BindAllAttributes();
}
function attributeDeleteSuccess() {
    //alert("success");
    BindAllAttributes();
}

function attributeSaveFailure() {
    alert("Failure");
}
function attributeSaveConfirm() {
    alert("Save Confirm");
}
//---------------------------------------Get Attributes Details By ID-------------------------------------//
function GetAttributesDetailsByID(id) {
    try {
        var data = { "ID":id };
        var ds = {};
        ds = GetDataFromServer("Attributes/GetAttributes/", data);
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
    catch (e) { }
}
//---------------------------------------Get All Attributes-----------------------------------------------//
function GetAllAttributes(id) {
    try {
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Attributes/GetAllAttributes/", data);
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
    catch (e) { }
}
//---------------------------------------Bind All Attributes----------------------------------------------//
function BindAllAttributes() {
    try {
        DataTables.attributeTable.clear().rows.add(GetAllAttributes()).draw(false);
    }
    catch (e) {

    }
}


