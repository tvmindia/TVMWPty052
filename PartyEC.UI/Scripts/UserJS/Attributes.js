var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function ()
{
    clearfields();
    try {       
        var AttributesViewModel = new Object();
        DataTables.attributeTable = $('#tblattributes').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllAttributes(AttributesViewModel),
             columns: [
               { "data": "ID" },
               { "data": "Name" },
               { "data": "Caption", "defaultContent": "<i>-</i>" },
               { "data": "AttributeType", "defaultContent": "<i>-</i>" },            
               { "data": "ConfigurableYN", "defaultContent": "<i>-</i>" },//simple,configurable
               { "data": "FilterYN", "defaultContent": "<i>-</i>" },
               { "data": "CSValues", "defaultContent": "<i>-</i>" },
               { "data": "EntityType", "defaultContent": "<i>-</i>" },
               { "data": "MandatoryYN", "defaultContent": "<i>-</i>" },
               { "data": "ComparableYN", "defaultContent": "<i>-</i>" },                 
               { "data": null, "orderable": false, "defaultContent": '<a onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{ "targets": [0], "visible": false, "searchable": false }, {
                 "render": function (data, type, row) {
                     if (row.ConfigurableYN == true) {
                         return 'configurable';
                     }
                     else{
                         return 'simple';
                     }
                 },
                 "targets": 4
             }]
         });
    }
    catch (e) {
        notyAlert('error', e.message);
      
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
    
    DataTypeOnChange();
  
}
//---------------------------------------Clear Fields-----------------------------------------------------//
function clearfields() {
    debugger;
    $("#ID").val("0")//ID is zero for New
    $("#deleteId").val("0")
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
//---------------------------------------Button Patch Click Events------------------------------------------------//
function btnreset() {
    debugger;
    if ($("#ID").val() == 0) {
        clearfields();
    }
    else { 
        if ($("#AttributeType").val() != 'C') {
            $("#AttributeType").attr('disabled', false);
        }
        fillAttributes($("#ID").val())
    }
}
//---------------------------------------Save-------------------------------------------------------//
function clicksave() {
    var res = Validation();
    if (res) {
        $("#btnFormSave").click();
    }  
}
//---------------------------------------Back-------------------------------------------------------//
function goback() {    
    $('#tabattibutesList').trigger('click');
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
//---------------------------------------Validation Functions---------------------------------------------//
function DataTypeOnChange(){
    debugger;
    var DataType = $("#AttributeType").val()
    if (DataType == "C") {
        $("#CSValues").attr('disabled', false);
        if ($("#AttributeType").val()==true)
        $("#AttributeType").attr('disabled', true);
    }
    else {
        $("#CSValues").val("");
        $("#CSValues").attr('disabled', true);
    }
}
function ConfigurableOnChange() {
    debugger;
    $("#AttributeType").val("C");
    $("#AttributeType").attr('disabled', true);
    $("#CSValues").attr('disabled', false);
}
function SimpleOnChange() {
    debugger; 
    $("#AttributeType").attr('disabled', false);
}
function Validation() {
    debugger;   
    if ($("#CSValues").val() == "")
    {
        if ($("#married-true").is(":checked"))
        {
            $("#CSValues").focus();
            notyAlert('error', 'Please Enter Configurable Values');
            return false;
        }     
    }  
        $("#AttributeType").attr('disabled', false);
        $("#CSValues").attr('disabled', false);
        return true; 
}

//---------------------------------------Add New Click----------------------------------------------------//
function btnAddNew() {
    $('#tabattributeDetails').trigger('click');
    clearfields();
}
//---------------------------------------Save Click alerts------------------------------------------------//
function attributeSaveSuccess(data, status, xhr) {
    debugger;
    BindAllAttributes();
    var i = JSON.parse(data)
    switch(i.Result){
        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            break;
        case "ERROR":
            notyAlert('error', i.Message);
            break;
        default:
            break;
    }
  
}
function attributeDeleteSuccess(data, status, xhr) {
    BindAllAttributes();
    clearfields();
    var i = JSON.parse(data)
    switch(i.Result){
        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            break;
        case "ERROR":
            notyAlert('error', i.Message);
            break;
        default:
            break;
}

function attributeSaveConfirm() {
    alert("Save Confirm");
}
function attributeDeleteConfirm() {
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
    catch (e) {
        notyAlert('error', e.message);
    }
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
    catch (e) {
        notyAlert('error', e.message);
    }
}
//---------------------------------------Bind All Attributes----------------------------------------------//
function BindAllAttributes() {
    try {
        DataTables.attributeTable.clear().rows.add(GetAllAttributes()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
} 
