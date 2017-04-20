var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function ()
{
    //ChangeButtonPatchView("Event", "btnPatchEdit", "Add"); //ControllerName,id of the container div,Name of the action
    //clearfields();
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
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{ "targets": [0], "visible": true, "searchable": true }]
         });
    }
    catch (e) {
        notyAlert('error', e.message);
      
    }
    InitializeEvents();
    //---------------------------------------Delete Event Image-------------------------------------------//
    $("#EventDelete").click(function () {
        var result = confirm("Are you sure you want to delete?");
        if (result) {
            try {
                var EventViewModel = new Object();
                EventViewModel.EventImageID = $("#EventImageID").val();
                EventViewModel.ImageType = "Event";
                var data = "{'EventObj':" + JSON.stringify(EventViewModel) + "}";
                PostDataToServer('Event/DeleteOtherImage/', data, function (JsonResult) {
                    if (JsonResult != '') {
                        switch (JsonResult.Result) {
                            case "OK":
                                notyAlert('success', JsonResult.Record.StatusMessage);
                                $("#imgEvents").attr('src', "/Content/images/NoImageFound.png");
                                $("#EventImage").attr('href', "/Content/images/NoImageFound.png");
                                $("#EventDelete").hide();
                                $("#EventImage").hide();
                                $('#EventImageID').val("");
                                break;
                            case "ERROR":
                                notyAlert('error', JsonResult.Record.StatusMessage);
                                break;
                            default:
                                break;
                        }
                    }
                });
            }
            catch (e) {

            }
        }
        else {

        }
    })
});

//------------------------------$$$$$----FUNCTIONS----$$$$$-----------------------------------------------//

//------------------------------ Initialize Events------------------------------------------------------//

function InitializeEvents()
{
    ChangeButtonPatchView("Event", "btnPatchEdit", "EventList"); //ControllerName,id of the container div,Name of the action
}

//---------------------------------------Edit Events--------------------------------------------------//
function Edit(currentObj) {    
    
    ChangeButtonPatchView("Event", "btnPatchEdit", "Edit"); //ControllerName,id of the container div,Name of the action
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
    $("#imgEvents").css("display", "");
    var thisevent = GetEventDetailsByID(ID); //Binding Data
    $("#ID").val(thisevent.ID)
    $("#deleteId").val(thisevent.ID)//for delete action 
    $("#Name").val(thisevent.Name);
    $("#RelatedCategoriesCSV").val(thisevent.RelatedCategoriesCSV);
    $("#EventImageID").val(thisevent.EventImageID);
    $("#imgEvents").attr('src', (thisevent.URL != "" ? (thisevent.URL + '?' + new Date().getTime()) : "/Content/images/NoImageFound.png"));
    $("#EventImage").attr('href', (thisevent.URL != "" ? (thisevent.URL + '?' + new Date().getTime()) : "/Content/images/NoImageFound.png"));
    if (thisevent.EventImageID.length > 0)
    {
        $("#EventDelete").show();
        $("#EventImage").show();
    }
    else
    {
        $("#EventDelete").hide();
        $("#EventImage").hide();
    }
    //bind check boxes here, by spliting Related Categories CSV
    var CSVarray = thisevent.RelatedCategoriesCSV.split(",");
    $('input:checkbox').prop('checked', false);
    for (var i = 0 ; i<CSVarray.length;i++)
    {
    $("#Cat_"+CSVarray[i]).prop('checked', true);
    }
    $('#divOverlayimage').hide();
} 
//---------------------------------------Clear Fields-----------------------------------------------------//
function clearfields() {
    
    $("#ID").val("0")//ID is zero for New
    $("#deleteId").val("0")
    $("#Name").val("")
    $("#RelatedCategoriesCSV").val("") 
    $('input:checkbox').prop('checked', false);
    $("#EventImageID").val("")
    $("#imgEvents").attr('src', "/Content/images/NoImageFound.png");
    $('#divOverlayimage').show();
    ResetForm();
}
//---------------------------------------Button Patch Click Events------------------------------------------------//
//tab clicks
function TabeventDetails() {
   // ChangeButtonPatchView("Event", "btnPatchEdittab2", "Edit"); //ControllerName,id of the container div,Name of the action
    clearfields();
}

function btnreset() {
    
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
    
/*--------------Checking the Checkboxes Checked 
    and ID's saved in array. array value filled in RelatedCategoriesCSV---------------------*/
    var checkboxCount = $("input:checked").length;
    if (checkboxCount > 0){
        var checked = [];
        $(":checkbox").each(function () {
            if (this.checked) { 
                var cat_Id = new Array();
                cat_Id = this.id.split("_");
                checked.push(cat_Id[1]);
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
function btnAddNew(id) {
    if (id != 1)
    {
        $('#tabeventDetails').trigger('click');
    }
    ChangeButtonPatchView("Event", "btnPatchEdit", "Add"); //ControllerName,id of the container div,Name of the action
    clearfields();
}
//-----------------------------------------Reset Validation Messages--------------------------------------//
function ResetForm() {
    var validator = $("#formIns_Up").validate();
    $('#formIns_Up').find('.field-validation-error span').each(function () {
        validator.settings.success($(this));
    });
    validator.resetForm();
}
//---------------------------------------Save Click alerts------------------------------------------------//
function SaveSuccess(data, status, xhr) {
    
    BindAllEvent();
    //clearfields();
    //goback();
    ChangeButtonPatchView("Event", "btnPatchEdit", "Add"); //ControllerName,id of the container div,Name of the action
    var i = JSON.parse(data)
    switch (i.Result) {
       
        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            $('#divOverlayimage').hide();
            $("#ID").val(i.Record.ReturnValues);
            $("#EventDelete").hide();
            $("#EventImage").hide();
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
    BindAllEvent();
    clearfields();
    goback();

    ChangeButtonPatchView("Event", "btnPatchEdit", "Add"); //ControllerName,id of the container div,Name of the action
    var i = JSON.parse(data)
    switch (i.Result) {
        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            $('#divOverlayimage').show();
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

    function SaveConfirm() {
       // alert("Save Confirm");
    }
    function DeleteConfirm() {
        alert("Delete Confirm");
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
            DataTables.eventTable.clear().rows.add(GetAllEvents()).draw(false);
        }
        catch (e) {
            notyAlert('error', e.message);
        }
    }