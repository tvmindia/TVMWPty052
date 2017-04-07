var DataTables = {};
$(document).ready(function () {

    try {

        DataTables.customerTable = $('#tblCustomers').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllNotifications(),
             columns: [
               { "data": "ID" },
               { "data": "Name", "defaultContent": "<i>-</i>" },
               { "data": "Title", "defaultContent": "<i>-</i>" },
                { "data": "Message", "defaultContent": "<i>-</i>" },
                 { "data": "Date", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="EditCustomer(this)"><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             
             ]
             
         });
    }
    catch (e) {
        notyAlert('errror', e.message);
    }


});


function GetAllNotifications() {
    try {
        var data = "";
        var ds = {};
        ds = GetDataFromServer("Notifications/GetAllNotifications/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {

            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.Message);
        }
    }
    catch (e) {
        notyAlert('errror', e.message);
    }
}