﻿var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function ()
{
    //ChangeButtonPatchView("Attributes", "btnPatchAttributestab2", "Add"); //ControllerName,id of the container div,Name of the action
    //clearfields();
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
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{ "targets": [0], "visible": false, "searchable": false }, {
                 "render": function (data, type, row) {
                     if (row.ConfigurableYN == true) {
                         return 'Product Options';
                     }
                     else{
                         return 'Other Attributes';
                     }
                 },
                 "targets": 4
             },
             {
                 "render": function (data, type, row) {
                     if (row.MandatoryYN == true) {
                         return 'Yes';
                     }
                     else {
                         return 'No';
                     }
                 },
                 "targets": 8
             },
             {
                 "render": function (data, type, row) {
                     if (row.ComparableYN == true) {
                         return 'Yes';
                     }
                     else {
                         return 'No';
                     }
                 },
                 "targets": 9
             },
                {
                    "render": function (data, type, row) {
                        if (row.FilterYN == true) {
                            return 'Yes';
                        }
                        else {
                            return 'No';
                        }
                    },
                    "targets": 5
                },
                {
                    "render": function (data, type, row) {
                        if (row.AttributeType == "D") {
                            return 'Date';
                        }
                        else if (row.AttributeType == "N") {
                            return 'Number';
                        }
                        else if(row.AttributeType == "C")
                        {
                            return 'Combo';
                        } 
                        else
                        {
                            return 'Text';
                        }
                    },
                    "targets": 3
                }]
         });
    }
    catch (e) {
        notyAlert('error', e.message);
      
    }
    InitializeAttributes();
});

//------------------------------$$$$$----FUNCTIONS----$$$$$-----------------------------------------------//

//---------------------------- Initialization-------------------------------------------------//

function InitializeAttributes()
{
    ChangeButtonPatchView("Attributes", "btnPatchAttribute", "AttributeList"); //ControllerName,id of the container div,Name of the action
}

//---------------------------------------Edit Attributes--------------------------------------------------//
function Edit(currentObj) {
    //Tab Change on edit click
    $('#tabattributeDetails').trigger('click');
    ChangeButtonPatchView("Attributes", "btnPatchAttribute", "EditDetails"); //ControllerName,id of the container div,Name of the action
    var rowData = DataTables.attributeTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        fillAttributes(rowData.ID);
    }
}
//---------------------------------------Fill Attributes--------------------------------------------------//
function fillAttributes(ID)
{
    debugger;
    ChangeButtonPatchView("Attributes", "btnPatchAttribute", "EditDetails");
    var thisattribute = GetAttributesDetailsByID(ID); //Binding Data
    //Hidden
    $("#ID").val(thisattribute.ID)
    $("#deleteId").val(thisattribute.ID)//for delete action    
    //Textboxes
    $("#Name").val(thisattribute.Name);
    $("#Name").attr('disabled', true);
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
    if (thisattribute.EntityType == "Rating")
    {
        $("#AttributeType").attr('disabled', true);
        $("#married-false").prop('checked', true);
        $("#married-false").prop('disabled', true);
        $("#married-true").prop('disabled', true);
        $("#CSValues").attr('disabled', true);
    }
    if (thisattribute.EntityType == "Order")
    {
        $("#married-false").prop('checked', true);
        $("#married-false").prop('disabled', true);
        $("#married-true").prop('disabled', true);
    }
    DataTypeOnChange();  
}
//-----------------------------------------Reset Validation Messages--------------------------------------//
function ResetForm() {
    var validator = $("#formIns_Up").validate();
    $('#formIns_Up').find('.field-validation-error span').each(function () {
        validator.settings.success($(this));
    });
    validator.resetForm();
}
//---------------------------------------Clear Fields-----------------------------------------------------//
function clearfields() {
    $("#Name").attr('disabled', false);
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
    $("#AttributeType").attr('disabled', false);
    $("#married-false").prop('checked', true);
    $("#AttributeType").attr('disabled', false);
    $("#married-false").prop('disabled', false);
    $("#married-true").prop('disabled', false);
    $("#CSValues").attr('disabled', false);
    ResetForm();
}
//---------------------------------------Button Patch Click Events------------------------------------------------//
function btnReset() {
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
function clickSave() {
    var res = Validation();
    if (res) {
        $("#btnFormSave").click();
    }  
}
//---------------------------------------Back-------------------------------------------------------//
function goBack() {    
    $('#tabattibutesList').trigger('click');
    InitializeAttributes();
}
//---------------------------------------Delete-------------------------------------------------------//
function clickDelete() {
    var id = $("#ID").val();
    if (id != 0) {
        $("#btnFormDelete").click();
    }
    else {
        notyAlert('error', 'Please Select Attributes');       
    }    
}
//---------------------------------------Entity Type Onchange---------------------------------------------//
function entityTypeOnChange() {
    var value = $("#EntityType").val();
    if(value=="Rating")
    {
        $("#AttributeType").val("N");
        $("#AttributeType").attr('disabled', true);
        $("#married-false").prop('checked', true);
        $("#married-false").prop('disabled', true);
        $("#married-true").prop('disabled', true);
        $("#CSValues").attr('disabled', true);
    }
    else if(value=="Order")
    {
    $("#married-false").prop('checked', true);
    $("#married-false").prop('disabled', true);
    $("#married-true").prop('disabled', true);
    $("#AttributeType").attr('disabled', false);
    }
    else
    {
        $("#AttributeType").val("");
        $("#AttributeType").attr('disabled', false);
        $("#married-false").prop('disabled', false);
        $("#married-true").prop('disabled', false);
        $("#CSValues").attr('disabled', false);
    }
}
//---------------------------------------Validation Functions---------------------------------------------//
function DataTypeOnChange(){
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
    $("#AttributeType").val("C");
    $("#AttributeType").attr('disabled', true);
    $("#CSValues").attr('disabled', false);
}
function SimpleOnChange() {
    $("#AttributeType").attr('disabled', false);
}
function Validation() {
    if ($("#CSValues").val() == "")
    {
        if ($("#married-true").is(":checked"))
        {
            $("#CSValues").focus();
            notyAlert('error', 'Please Enter Configurable Values');
            return false;
        }     
    }
    $("#Name").attr('disabled', false);
        $("#AttributeType").attr('disabled', false);
        $("#CSValues").attr('disabled', false);
        return true; 
}

//---------------------------------------Add New Click----------------------------------------------------//
function btnAddNew(id) {
    debugger;
    if (id!=1)
    {
        $('#tabattributeDetails').trigger('click');
    }  
    ChangeButtonPatchView("Attributes", "btnPatchAttribute", "AddNew"); //ControllerName,id of the container div,Name of the action
    clearfields();
   
}
//---------------------------------------Save Click alerts------------------------------------------------//
function attributeSaveSuccess(data, status, xhr) {
    BindAllAttributes();
    var i = JSON.parse(data)
    
    switch (i.Result) {
      
        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            fillAttributes(i.Record.ReturnValues);
            //clearfields();
            //goBack();

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
function attributeDeleteSuccess(data, status, xhr) {
    BindAllAttributes();
    //clearfields();
    //goback();
    //ChangeButtonPatchView("Attributes", "btnPatchAttributestab2", "Add"); //ControllerName,id of the container div,Name of the action
    var i = JSON.parse(data)
    switch (i.Result) {
        case "OK":
            notyAlert('success', i.Record.StatusMessage);
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

    function attributeDeleteConfirm() {
        alert("Delete Confirm");
    }

    //---------------------------------------Get Attributes Details By ID-------------------------------------//
    function GetAttributesDetailsByID(id) {
        try {
            var data = { "ID": id };
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