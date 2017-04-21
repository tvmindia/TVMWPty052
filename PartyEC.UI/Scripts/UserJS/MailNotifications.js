var DataTables = {};
$(document).ready(function () {

    try {

        DataTables.mailnotificatoinTable = $('#tblMailNotifications').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllMailNotifications(),
             columns: [
                 { "data": "customer" },
               { "data": "ID" },
               { "data": "customer", "defaultContent": "<i>-</i>" },
               { "data": "Title", "defaultContent": "<i>-</i>" },
                { "data": "Message", "defaultContent": "<i>-</i>" },
                 { "data": "logDetailsObj", "defaultContent": "<i>-</i>" },
                 { "data": "Status", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="EditNotification(this)"><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }

             ],
             columnDefs: [
                 {
                     "render": function (data, type, row) {
                         if (data) {
                             return data.ID;
                         }
                     },
                     "visible": false,
                     "searchable": false,
                     "targets": 0
                 },
          {
              "render": function (data, type, row) {
                  if (data) {
                      return data.Name;
                  }
              },
              "targets": 2
          },
          {
              "render": function (data, type, row) {
                  if (data) {
                      return ConvertJsonToDate(data.CreatedDate);
                  }
              },
              "targets": 5
          },
          {
              "render": function (data, type, row) {
                  return (data != 0 ? "Success" : "Failed");
              },
              "targets": 6
          }
             ]

         });
    }
    catch (e) {
        notyAlert('errror', e.message);
    }


    try {

        DataTables.customerinNotificaton = $('#tblCustomersInNotificatoin').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             select: true,
             autoWidth: false,
             order: [],
             searching: true,
             paging: true,
             pageLength: 5,
             data: GetAllCustomers(),
             columns: [

               { "data": null, "width": "3%", "defaultContent": '' },
               { "data": null, "width": "3%", "defaultContent": '<span class="glyphicon glyphicon-user" aria-hidden="true"></span>' },
               { "data": "Name", "width": "24%", "defaultContent": "<i>-</i>" },
               { "data": "Email", "width": "20%", "defaultContent": "<i>-</i>" },
               { "data": "Address", "width": "50%", "defaultContent": "<i>-</i>" },
                { "data": "ID", "defaultContent": "<i>-</i>" }


             ],
             columnDefs: [

                 {
                     orderable: false,
                     className: 'select-checkbox',
                     targets: 0
                 },
                 {
                     ordering: false,
                     targets: 1
                 },
                  {
                      visible: false,
                      searchable: false,
                      targets: 5
                  }

             ],
             select: {
                 style: 'multi',
                 selector: 'tr'
             },
             order: [[1, 'asc']]




         });
        //$('#tblCustomersInNotificatoin tbody').on('click', 'tr', function () {
        //    $(this).toggleClass('selected');
        //});
        $('#tblCustomersInNotificatoin tbody').on('click', 'tr', function () {

            $("#RadioSelected").prop('checked', true);
            BindCustomerNameFromGrid();
            //  $("#CustomerName").val(rowData.Name);
            $("#customer_ID").val(0);
        });
    }
    catch (e) {
        notyAlert('errror', e.message);
    }

    ChangeButtonPatchView("Notifications", "NotificationToolBox", "Add"); //ControllerName,id of the container div,Name of the action

    $("#tabNotificationsList").click(function () {
        ChangeButtonPatchView("Notifications", "NotificationToolBox", "Add"); //ControllerName,id of the container div,Name of the action
    });
    $("#tabNotificationsDetails").click(function () {
        ChangeButtonPatchView("Notifications", "NotificationToolBox", "Push"); //ControllerName,id of the container div,Name of the action
    });
});



function GetAllMailNotifications() {
    try {
        var frdate = $("#fromdate").val();
        var todat = $("#todate").val();
        var data = { "fromdate": (frdate != "" ? frdate : null), "todate": (todat != "" ? todat : null) };
        var ds = {};
        ds = GetDataFromServer("MailNotifications/GetAllMailNotifications/", data);
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


function AddNotification() {
    try {
        $("#RadioSelected").prop('checked', true);
        ClearForm();
        DataTables.customerinNotificaton.rows().deselect();
        ChangeButtonPatchView("Notifications", "NotificationToolBox", "Push"); //ControllerName,id of the container div,Name of the action
        $("#tabNotificationsDetails a").click();
    }
    catch (e) {
        notyAlert('error', e.Message);
    }


}
function GetAllCustomers() {
    try {
        var data = "";
        var ds = {};
        ds = GetDataFromServer("Customer/GetAllCustomers/", data);
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

function BindCustomerNameFromGrid() {
    var rowsData = DataTables.customerinNotificaton.rows(".selected").data();
    var maxrows = DataTables.customerinNotificaton.data().count();
    if ((rowsData) && (rowsData.length > 0)) {
        if (rowsData.length > 1) {
            var Namelist = [];
            for (var r = 0; r < rowsData.length; r++) {
                Namelist.push(rowsData[r].Name);
            }
            if (maxrows == rowsData.length) {
                $("#CustomerName").val('All');
            }
            else {
                $("#CustomerName").val(Namelist);
            }
        }
        else {
            $("#CustomerName").val(rowsData[0].Name);
        }
    }
    else {
        $("#CustomerName").val('');
    }
}

//clickflag avoids click loop on tab li
function goback()
{
   
    $("#tabNotificationsList a").click();
    ClearForm();
    ChangeButtonPatchView("Notifications", "NotificationToolBox", "Add"); //ControllerName,id of the container div,Name of the action
}


function ClearForm() {
    try {
        var validator = $("#NotificationForm").validate();
        $('#NotificationForm').find('.field-validation-error span').each(function () {
            validator.settings.success($(this));
        });
        $('#NotificationForm')[0].reset();
        $('#CustomerName').val('');
        $('#Title').val('');
        $('#Message').val('');
        $("#customer_ID").val(0);
        DataTables.customerinNotificaton.rows().deselect();
    }
    catch (e) {
        notyAlert('errror', e.message);
    }

}

function SelectCustomerTable(curObje) {
    //alert(curObje.value);
    switch (curObje.value) {
        case "All":
            DataTables.customerinNotificaton.rows().select();
            BindCustomerNameFromGrid();
            break;
        case "Selected":
            DataTables.customerinNotificaton.rows().deselect();
            BindCustomerNameFromGrid();
            break;
    }

    return false;
}
function PushNotification() {
    $("#btnNotificationSubmit").trigger("click");
}

function NotificationPushSuccess(data, status, xhr) {
    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
            notyAlert('success', JsonResult.Record.StatusMessage);
            RefreshNotificationTable();
            ClearForm();
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Record.StatusMessage);
            break;
        case "VALIDATION":
            notyAlert('error', JsonResult.Message);
            break;
        default:
            notyAlert('error', JsonResult.Message);
            break;
    }
}

function RefreshNotificationTable() {
    try {
        DataTables.mailnotificatoinTable.clear().rows.add(GetAllMailNotifications()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
function EditNotification(curObj) {

    var rowData = DataTables.mailnotificatoinTable.row($(curObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        $('#tabNotificationsDetails a').trigger('click');
        $("#RadioSelected").prop('checked', true);
        DataTables.customerinNotificaton.rows().deselect();
        var thisnoti = GetNotificationDetail(rowData.ID);
        if (thisnoti) {
            $("#customer_ID").val(thisnoti.customer.ID);
            $('#CustomerName').val(thisnoti.customer.Name);
            $('#Title').val(thisnoti.Title);
            $('#Message').val(thisnoti.Message);
            ChangeButtonPatchView("Notifications", "NotificationToolBox", "Edit"); //ControllerName,id of the container div,Name of the action
        }

    }
}


function GetNotificationDetail(id) {
    try {
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Notifications/GetNotification/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Record;
        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.Message);
        }

    }
    catch (e) {

    }
}


function ConstructMultiCustomersList() {
    try {
        var rowsData = DataTables.customerinNotificaton.rows(".selected").data();
        if ((rowsData) && (rowsData.length > 0)) {
            var IDlist = [];
            for (var r = 0; r < rowsData.length; r++) {
                IDlist.push(rowsData[r].ID);
            }
            $("#CustomerIDList").val('');
            $("#CustomerIDList").val(IDlist);
        }
    }
    catch (e) {
        notyAlert('errror', e.message);
    }
}

function countDays() {
    var fromdate = $("#fromdate").val();
    var todate = $("#todate").val();
    if (fromdate != "" && todate != "") {
        fromdate = ConvertDateFormats(fromdate);
        todate = ConvertDateFormats(todate);
        var date1 = new Date(fromdate);
        var date2 = new Date(todate);
        var diff = date2.getTime() - date1.getTime();
        if (diff >= 0) {
            var ONE_DAY = 1000 * 60 * 60 * 24;
            $("#dayscount").text((Math.round(diff / ONE_DAY)+1) + ' Days');
        }
        else {
            $("#dayscount").text('');
            return false;
        }
    }
    else {
       
        $("#dayscount").text('');
    }
    return true;
}


