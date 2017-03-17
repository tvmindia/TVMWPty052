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
             data: GetAllEventRequests(EventRequestsViewModel),
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
function GetAllEventRequests(id) {
    try {
        var data = { "ID": id };
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