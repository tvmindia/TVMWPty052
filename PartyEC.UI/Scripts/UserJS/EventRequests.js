var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function () { 
    try { 
        var EventRequestsViewModel = new Object();
        DataTables.eventTable = $('#tbleventRequest').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllEventRequests(),
             columns: [
               { "data": "ID" },
               { "data": "CustomerID" },
               { "data": "EventReqNo" },
               { "data": "EventTitle", "defaultContent": "<i>-</i>" },
               { "data": "EventType", "defaultContent": "<i>-</i>" },
               { "data": "EventDateTime", "defaultContent": "<i>-</i>" },
               { "data": "CustomerName", "defaultContent": "<i>-</i>" },
               { "data": "ContactName", "defaultContent": "<i>-</i>" },
               { "data": "Phone", "defaultContent": "<i>-</i>" },
               { "data": "EventDesc", "defaultContent": "<i>-</i>" },
               { "data": "FollowUpDate", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{ "targets": [0], "visible": false, "searchable": false },
                          { "targets": [1], "visible": false, "searchable": false }]
         });
    }
    catch (e) {
        notyAlert('error', e.message);

    }
    //----------------------------------------Disabling controls-------------------//
    $("#CurrencyCode").attr('disabled', true);
    $("#CurrencyRate").attr('disabled', true);
});

//---------------------------------------Get All Events Requests-----------------------------------------------//
function GetAllEventRequests() {
    try {
        debugger;
        var data = {};
        var ds = {};
        ds = GetDataFromServer("EventRequests/GetAllEventRequests/", data);
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

 
function goback() {
    $('#tabeventRequestsList').trigger('click');
    try {
        DataTables.eventTable.clear().rows.add(GetAllEventRequests()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function tabeventRequestListClick()
{
    $('#tabeventRequestDetails').addClass('disabled');
    $('#tabeventRequestDetails a').attr('data-toggle', '');
}


function Edit(currentObj) {
    //Tab Change
    ChangeButtonPatchView("EventRequests", "btnPatcheventRequeststab2", "Edit"); //ControllerName,id of the container div,Name of the action
    $('#tabeventRequestDetails').removeClass('disabled');
    $('#tabeventRequestDetails a').attr('data-toggle', 'tab');
    $('#tabeventRequestDetails a').trigger('click');
    debugger;
    var rowData = DataTables.eventTable.row($(currentObj).parents('tr')).data();
    //Event Request Case
    if ((rowData != null) && (rowData.ID != null))
    {
        BindEventRequest(rowData.ID)//function call
        if ((rowData.CustomerID != null))
        {
            BindCustomer(rowData.CustomerID)//function call
        }
        BindComments();//function call
    } 
}
function BindEventRequest(id)
{
    var thisEvent = GetEventRequest(id);
    if (thisEvent != null) {
        debugger;
        $("#ID").val(thisEvent.ID);
        $(".UpdateId").val(thisEvent.ID);
        $("#TotalAmt").val(thisEvent.TotalAmt);
        $("#TotalTaxAmt").val(thisEvent.TotalTaxAmt);
        $("#TotalDiscountAmt").val(thisEvent.TotalDiscountAmt);
        $("#EventStatus").val(thisEvent.EventStatus);
        $("#AdminRemarks").val(thisEvent.AdminRemarks);
        $("#FollowUpDate").val(thisEvent.FollowUpDate.substring(0, 10));

        document.getElementById('lblEventReqNo').innerHTML = thisEvent.EventReqNo;
        document.getElementById('lblEventType').innerHTML = thisEvent.EventType;
        document.getElementById('lblEventTitle').innerHTML = thisEvent.EventTitle;
        document.getElementById('lblEventDate').innerHTML = thisEvent.EventDateTime.substring(0, 10);
        document.getElementById('lblEventTime').innerHTML = thisEvent.EventDateTime.substring(11);
        document.getElementById('lblEventDesc').innerHTML = thisEvent.EventDesc;
        document.getElementById('lblLookingFor').innerHTML = thisEvent.LookingFor;
        document.getElementById('lblRequirementSpec').innerHTML = thisEvent.RequirementSpec;
        document.getElementById('lblMessage').innerHTML = thisEvent.Message;
        document.getElementById('lblNoOfPersons').innerHTML = thisEvent.NoOfPersons;
        document.getElementById('lblBudget').innerHTML = thisEvent.Budget;
        document.getElementById('lblContactName').innerHTML = thisEvent.ContactName;
        document.getElementById('lblEmail').innerHTML = thisEvent.Email;
        document.getElementById('lblPhone').innerHTML = thisEvent.Phone;
        document.getElementById('lblContactType').innerHTML = thisEvent.ContactType;
    }
}

function BindCustomer(id)
{
    var thisEvent = GetCustomer(id);
    if (thisEvent != null) {
        document.getElementById('lblcust_ID').innerHTML = thisEvent.ID;
        document.getElementById('lblName').innerHTML = thisEvent.Name;
        document.getElementById('lblCust_No').innerHTML = thisEvent.Mobile;
        document.getElementById('lblCust_Email').innerHTML = thisEvent.Email;
    }
}

function BindComments()   // To Display Previous Comment history
{
    debugger;
    $("#CommentsDisplay").empty();
    id = $("#ID").val();// assigning id for binding comments.
    var thisCommentList = GetEventsLog(id);
    if (thisCommentList != null) {
        debugger;
        for (var i = 0; i < thisCommentList.length; i++)
        {
            var cnt = $('<li id="Comment' + i + '" class="list-group-item col-md-12"><span class="badge">'+ thisCommentList[i].CommentDate.substring(1, 10) + '</span>' + thisCommentList[i].PrevComment + '</li>');
            $("#CommentsDisplay").append(cnt);
        }
    }
}

//---------------------------------------Get Events Request Details By ID-------------------------------------//
function GetEventRequest(id) {
    try {
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("EventRequests/GetEventRequest/", data);
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
//---------------------------------------Get Customer Details By ID-------------------------------------//
function GetCustomer(id) {
    try {
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Customer/GetCustomer/", data);
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
//---------------------------------------Get Events Logs Details By ID-------------------------------------//
function GetEventsLog(id) {
    try {
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("EventRequests/GetEventsLog/", data);
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

function ValidationCommercialInfo() {
    debugger;
    //setting Update Flag as 1 indicating that  Update called from Commercial Info on save click
    $("#Updateflag").val(1);
}

function SaveSuccess(data, status, xhr) {
    debugger;
    $("#Comments").val("");
    var i = JSON.parse(data)
    switch (i.Result) {
        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            $('#divOverlayimage').hide();
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

function UpdateEvntRq_Complete() {
   
    $("#CurrencyCode").attr('disabled', true);
    $("#CurrencyRate").attr('disabled', true);
}
function UpdateEvntRq_OnBegin() {
   
    $("#CurrencyCode").attr('disabled', false);
    $("#CurrencyRate").attr('disabled', false);
}
function BindEvntRq_Remarks() {
    debugger;
    id = $("#ID").val();
    BindEventRequest(id);
}

 

 