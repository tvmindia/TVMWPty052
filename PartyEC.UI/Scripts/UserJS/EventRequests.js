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
               { "data": "EventTypeID", "defaultContent": "<i>-</i>" },
               { "data": "EventDateTime", "defaultContent": "<i>-</i>" },
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
});

//---------------------------------------Get All Events-----------------------------------------------//
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


function Edit(currentObj) {
   
    //Tab Change
    $('#tabeventRequestDetails').trigger('click');
    var rowData = DataTables.eventTable.row($(currentObj).parents('tr')).data();
    //Event Request Case
    if ((rowData != null) && (rowData.ID != null)) {
        var thisEvent= GetEventRequest(rowData.ID);
        if (thisEvent != null) {
            debugger;
            $("#ID").val(thisEvent.ID);
            document.getElementById('lblEventReqNo').innerHTML = thisEvent.EventReqNo;
           // document.getElementById('').innerHTML = thisEvent.;
            document.getElementById('lblEventTitle').innerHTML = thisEvent.EventTitle;
            document.getElementById('lblEventDateTime').innerHTML = thisEvent.EventDateTime;
            document.getElementById('lblEventStatus').innerHTML = thisEvent.lblEventStatus;
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
            var thisEvent = GetCustomer(rowData.CustomerID);
            if (thisEvent != null) {

                $("#CustomerID").val(thisEvent.CustomerID);

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