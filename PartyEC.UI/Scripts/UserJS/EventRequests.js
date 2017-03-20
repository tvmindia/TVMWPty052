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
               { "data": "EventStatus", "defaultContent": "<i>-</i>" },
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
    var rowData = DataTables.eventTable.row($(currentObj).parents('tr')).data();
    //Event Request Case
    if ((rowData != null) && (rowData.ID != null)) {
        var thisEvent= GetEventRequest(rowData.ID);
        if (thisEvent != null) {
            debugger;
            $("#ID").val(thisEvent.ID);
            $(".UpdateId").val(thisEvent.ID);
            //$("#CurrencyCode").val(thisEvent.CurrencyCode);
            //$("#CurrencyRate").val(thisEvent.CurrencyRate);
            $("#TotalAmt").val(thisEvent.TotalAmt);
            $("#TotalTaxAmt").val(thisEvent.TotalTaxAmt);
            $("#TotalDiscountAmt").val(thisEvent.TotalDiscountAmt);

            $("#EventStatus").val(thisEvent.EventStatus);
            $("#AdminRemarks").val(thisEvent.AdminRemarks);
            $("#FollowUpDate").val(thisEvent.FollowUpDate);

            document.getElementById('lblEventReqNo').innerHTML = thisEvent.EventReqNo;
            document.getElementById('lblEventType').innerHTML = thisEvent.EventType;
            document.getElementById('lblEventTitle').innerHTML = thisEvent.EventTitle;
            document.getElementById('lblEventDateTime').innerHTML = thisEvent.EventDateTime;
            document.getElementById('lblEventStatus').innerHTML = thisEvent.EventStatus;
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
        if ((rowData.CustomerID != null)) {
            debugger;
            var thisEvent = GetCustomer(rowData.CustomerID);
            if (thisEvent != null) {
                
                document.getElementById('lblcust_ID').innerHTML = thisEvent.ID;
                document.getElementById('lblName').innerHTML = thisEvent.Name;
                document.getElementById('lblCust_No').innerHTML = thisEvent.Mobile;
                document.getElementById('lblCust_Email').innerHTML = thisEvent.Email;
                
            }
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

function ValidationCommercialInfo() {
    debugger;
    $("#CurrencyCode").attr('disabled', false);
    $("#CurrencyRate").attr('disabled', false);
    $("#Updateflag").val(1);//setting Update Flag as 1 indicating that  Update called from Commercial Info on save click
}

 

 