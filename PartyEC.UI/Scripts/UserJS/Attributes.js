var DataTables = {};
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

function Edit(currentObj) {
    //Tab Change on edit click
    $('#tabattributeDetails').trigger('click');
    debugger;
    var rowData = DataTables.attributeTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {

        var thisattribute = GetAttributesDetailsByID(rowData.ID);
        $("#ID").val(thisattribute.ID)
        $("#Name").val(thisattribute.Name);
        $("#Caption").val(thisattribute.Caption)       
        $("#AttributeType").val(thisattribute.AttributeType)        
        $("#CSValues").val(thisattribute.CSValues)
        $("#EntityType").val(thisattribute.EntityType)        
        if (thisattribute.ConfigurableYN == false) {
            $("#married-false").attr('checked', true);
        }
        else {
            $("#married-true").attr('checked', true);
        }

        if (thisattribute.FilterYN == false) {
            $("#FilterYN").attr('checked', false);
        }
        else {
            $("#FilterYN").attr('checked', true);         
        }
        if (thisattribute.MandatoryYN == false) {
            $("#MandatoryYN").attr('checked', false);
        }
        else {
            $("#MandatoryYN").attr('checked', true);
        }
        if (thisattribute.ComparableYN == false) {
            $("#ComparableYN").attr('checked', false);
        }
        else {
            $("#ComparableYN").attr('checked', true);
        }      
    }
}

function GetAttributesDetailsByID(id) {
    try {
        debugger;
        var data = id;
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

function GetAllAttributes(id) {
    try {
        debugger;
        var data = id;
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

function BindAllAttributes() {
    try {
        DataTables.attributeTable.clear().rows.add(GetAllAttributes()).draw(false);
    }
    catch (e) {

    }
}


function attributeSaveSuccess() {
    alert("success");
    BindAllAttributes();
}
function attributeSaveFailure() {
    alert("Failure");
}
function attributeSaveConfirm() {
    alert("Save Confirm"); 
}