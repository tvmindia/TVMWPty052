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
               { "data": "EventTime", "defaultContent": "<i>-</i>" },
               { "data": "CustomerName", "defaultContent": "<i>-</i>" },
               { "data": "ContactName", "defaultContent": "<i>-</i>" },
               { "data": "Phone", "defaultContent": "<i>-</i>" },
               { "data": "EventDesc", "defaultContent": "<i>-</i>" },
               { "data": "FollowUpDate", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{ "targets": [0], "visible": false, "searchable": false },{ "targets": [1], "visible": false, "searchable": false },
                 { "targets": [5], "render": function (data, type, full, meta) {                         
                       
                         var res = ConvertJsonToDate(data);
                         return res;
                 }},
                 { "targets": [11], "render": function (data, type, full, meta) {                         
                        
                         var res = ConvertJsonToDate(data);
                         return res;
                     }}]
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

//---------------------------------------Edit Bind Functions-------------------------------------//
function Edit(currentObj) {
    //Tab Change
    ChangeButtonPatchView("EventRequests", "btnPatcheventRequeststab2", "Edit"); //ControllerName,id of the container div,Name of the action
    $('#tabeventRequestDetails').removeClass('disabled');
    $('#tabeventRequestDetails a').attr('data-toggle', 'tab');
    $('#tabeventRequestDetails a').trigger('click');
    
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
        
        $("#ID").val(thisEvent.ID);
        $(".UpdateId").val(thisEvent.ID);
        $("#TotalAmt").val(thisEvent.TotalAmt);
        $("#TotalTaxAmt").val(thisEvent.TotalTaxAmt);
        $("#TotalDiscountAmt").val(thisEvent.TotalDiscountAmt);
        $("#EventStatus").val(thisEvent.EventStatus);
        $("#AdminRemarks").val(thisEvent.AdminRemarks);
        if (thisEvent.FollowUpDate!=null)
        $("#FollowUpDate").val(thisEvent.FollowUpDate.substring(0, 10));
        //labels
        $('#lblEventReqNo').text(thisEvent.EventReqNo);
        $('#lblEventType').text(thisEvent.EventType);
        $('#lblEventTitle').text(thisEvent.EventTitle);

        //formating Date using function in custom js 
      
        var resultdate = ConvertJsonToDate(thisEvent.EventDateTime);
        $('#lblEventDate').text(resultdate);      
        $('#lblEventTime').text(thisEvent.EventTime);
        $('#lblEventDesc').text(thisEvent.EventDesc);
        $('#lblLookingFor').text(thisEvent.LookingFor);
        $('#lblRequirementSpec').text(thisEvent.RequirementSpec);
        $('#lblMessage').text(thisEvent.Message);
        $('#lblNoOfPersons').text(thisEvent.NoOfPersons);
        $('#lblBudget').text(thisEvent.Budget);
        $('#lblContactName').text(thisEvent.ContactName);
        $('#lblEmail').text(thisEvent.Email);
        $('#lblPhone').text(thisEvent.Phone);
        $('#lblContactType').text(thisEvent.ContactType);
    }
}
function BindCustomer(id)
{
    var thisEvent = GetCustomer(id);
    if (thisEvent != null) {
        $('#lblcust_ID').text(thisEvent.ID);
        $('#lblName').text(thisEvent.Name);
        $('#lblCust_No').text(thisEvent.Mobile);
        $('#lblCust_Email').text(thisEvent.Email);
    }
}
function BindComments()   // To Display Previous Comment history
{
    
    $("#CommentsDisplay").empty();
    id = $("#ID").val();// assigning id for binding comments.
    var thisCommentList = GetEventsLog(id);
    if (thisCommentList != null) {
        
        for (var i = 0; i < thisCommentList.length; i++)
        {
            var resultdate = ConvertJsonToDate(thisCommentList[i].CommentDate);
            
            var cnt = $('<li id="Comment' + i + '" class="list-group-item col-md-12"><span class="badge">' + resultdate + '</span>' + thisCommentList[i].PrevComment + '</li>');
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
            return ds.Record;
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
    
    //setting Update Flag as 1 indicating that  Update called from Commercial Info on save click
    $("#Updateflag").val(1);
}

function SaveSuccess(data, status, xhr) {
    
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
    
    id = $("#ID").val();
    BindEventRequest(id);
}

 

 