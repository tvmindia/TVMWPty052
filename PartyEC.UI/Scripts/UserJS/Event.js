var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function ()
{
    clearfields();
    try {       
        var EventViewModel = new Object();
        DataTables.eventTable = $('#tblevent').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllEvents(EventViewModel),
             columns: [
               { "data": "ID" },
               { "data": "Name" }, 
               { "data": "RelatedCategoriesCSV", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{ "targets": [0], "visible": true, "searchable": false }]
         });
    }
    catch (e) {
        notyAlert('error', e.message);
      
    }
});

//------------------------------$$$$$----FUNCTIONS----$$$$$-----------------------------------------------//

//---------------------------------------Edit Events--------------------------------------------------//
function Edit(currentObj) {    
    debugger;
    $('#tabeventDetails').trigger('click');
    var rowData = DataTables.eventTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        fillevent(rowData.ID);
    }
}
//---------------------------------------Fill Events--------------------------------------------------//
function fillevent(ID)
{
    debugger;
    var thisevent = GetEventDetailsByID(ID); //Binding Data
    $("#ID").val(thisevent.ID)
    $("#deleteId").val(thisevent.ID)//for delete action 
    $("#Name").val(thisevent.Name);
    $("#RelatedCategoriesCSV").val(thisevent.RelatedCategoriesCSV);
    //bind check boxes here, by spliting Related Categories CSV
    var CSVarray=  thisevent.RelatedCategoriesCSV.split(",");
    for (var i = 0 ; i<CSVarray.length;i++)
    {
    $("#"+CSVarray[i]).prop('checked', true);
    }   
} 
//---------------------------------------Clear Fields-----------------------------------------------------//
function clearfields() {
    debugger;
    $("#ID").val("0")//ID is zero for New
    $("#deleteId").val("0")
    $("#Name").val("")
    $("#RelatedCategoriesCSV").val("") 
    $('input:checkbox').prop('checked', false);
    $("#EventImageUpload").val("")
  
  
}
//---------------------------------------Button Patch Click Events------------------------------------------------//
function btnreset() {
    debugger;
    if ($("#ID").val() == 0) {
        clearfields();
    }
    else {
        fillevent($("#ID").val())
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
    $('#tabeventList').trigger('click');
}
//---------------------------------------Delete-------------------------------------------------------//
function clickdelete() {
    debugger;
    var id = $("#ID").val();
    if (id != 0) {
        $("#btnFormDelete").click();
    }
    else {
        notyAlert('error', 'Please Select Event');       
    }    
}
//---------------------------------------Validation Functions---------------------------------------------//  
function Validation() {
    debugger;
/*--------------Checking the Checkboxes Checked 
    and ID's saved in array. array value filled in RelatedCategoriesCSV---------------------*/
    var checkboxCount = $("input:checked").length;
    if (checkboxCount > 0){
        var checked = [];
        $(":checkbox").each(function () {
            if (this.checked) {
                checked.push(this.id);
            }
        });
        var CSV = checked.toString();
        $("#RelatedCategoriesCSV").val(CSV);
        return true;
    }
    else {
        notyAlert('error', 'Please Checked Related Categories');
        return false;
    }  
}
//---------------------------------------Add New Click----------------------------------------------------//
function btnAddNew() {
    $('#tabeventDetails').trigger('click');
    clearfields();
}
//---------------------------------------Save Click alerts------------------------------------------------//
function SaveSuccess(data, status, xhr) {
    debugger;
    BindAllEvent();
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
function DeleteSuccess(data, status, xhr) {
    BindAllEvent();
    clearfields();
    var i = JSON.parse(data)
    switch (i.Result) {
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

    function SaveConfirm() {
        alert("Save Confirm");
    }
    function DeleteConfirm() {
        alert("Save Confirm");
    }
    //---------------------------------------Get Events Details By ID-------------------------------------//
    function GetEventDetailsByID(id) {
        try {
            var data = { "ID": id };
            var ds = {};
            ds = GetDataFromServer("Event/GetEvent/", data);
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
    //---------------------------------------Get All Events-----------------------------------------------//
    function GetAllEvents(id) {
        try {
            var data = { "ID": id };
            var ds = {};
            ds = GetDataFromServer("Event/GetAllEvents/", data);
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
    //---------------------------------------Bind All Events----------------------------------------------//
    function BindAllEvent() {
        try {
            DataTables.attributeTable.clear().rows.add(GetAllEvents()).draw(false);
        }
        catch (e) {
            notyAlert('error', e.message);
        }
    }