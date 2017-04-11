var DataTables = {};
$(document).ready(function () {

    try {

        DataTables.notificatoinTable = $('#tblNotifications').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllNotifications(),
             columns: [
                 {"data":"customer"},
               { "data": "ID" },
               { "data": "customer", "defaultContent": "<i>-</i>" },
               { "data": "Title", "defaultContent": "<i>-</i>" },
                { "data": "Message", "defaultContent": "<i>-</i>" },
                 { "data": "logDetailsObj", "defaultContent": "<i>-</i>" },
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
             order: [],
             searching: true,
             paging: true,
             pageLength: 5,
             data: GetAllCustomers(),
             columns: [
               { "data": null,"defaultContent": '' },
               { "data": null,"orderable": false, "defaultContent": '<span class="glyphicon glyphicon-user" aria-hidden="true"></span>' },
               { "data": "Name", "defaultContent": "<i>-</i>" },
               { "data": "Mobile", "defaultContent": "<i>-</i>" },
               { "data": "Address", "defaultContent": "<i>-</i>" },
                { "data": "ID", "defaultContent": "<i>-</i>" }
              

             ],
             columnDefs: [
                 {
                     orderable: false,
                     className: 'select-checkbox',
                     targets: 0
                 },
                  {
                      visible: false,
                      searchable: false,
                      targets: 5
                  }
                
             ]
             //select: {
             //    style: 'multi',
             //    selector: 'td:first-child'
             //},
             //order: [[1, 'asc']]
                 
            


         });
        //$('#tblCustomersInNotificatoin tbody').on('click', 'tr', function () {
        //    $(this).toggleClass('selected');
        //});
        $('#tblCustomersInNotificatoin tbody').on('click', 'tr', function () {
           
            var rowData = DataTables.customerinNotificaton.row(".selected").data();
            $("#CustomerName").val(rowData.Name);
            $("#customer_ID").val(rowData.ID);
        });
    }
    catch (e) {
        notyAlert('errror', e.message);
    }
    //$('#tblCustomersInNotificatoin tbody').on('click', 'tr', function () {
    //    $(this).toggleClass('selected');
    //});

});
function SelectCustomerTable(curObje)
{
    //alert(curObje.value);
    switch(curObje.value)
    {
        case "All":
            DataTables.customerinNotificaton.rows().select();
            break;
        case "Selected":
            DataTables.customerinNotificaton.rows().deselect();
            break;
    }
   
    return false;
}



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


function EditNotification(curObj)
{

    var rowData = DataTables.notificatoinTable.row($(curObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null))
    {
        $('#tabNotificationsDetails a').trigger('click');
    
        var thisnoti = GetNotificationDetail(rowData.ID);
        if (thisnoti)
        {
            $("#customer_ID").val(thisnoti.customer.ID);
            $('#customer_Name').val(thisnoti.customer.Name);
            $('#Title').val(thisnoti.Title);
            $('#Message').val(thisnoti.Message);
        }
      


    }


}

function GetNotificationDetail(id)
{
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

function PushNotification()
{
    $("#btnNotificationSubmit").trigger("click");
}
function RefreshNotificationTable()
{
    try {
        DataTables.notificatoinTable.clear().rows.add(GetAllNotifications()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}


function NotificationPushSuccess(data, status, xhr)
{
    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result)
    {
        case "OK":
            notyAlert('success', JsonResult.Record.StatusMessage);
            RefreshNotificationTable();
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

function goback() {
    $("#tabNotificationsList a").click();
}

function AddNotification()
{
try
{
    $("#RadioSelected").prop('checked', true);
    DataTables.customerinNotificaton.rows().deselect();
    ChangeButtonPatchView("Notifications", "NotificationPushToolbox", "Push"); //ControllerName,id of the container div,Name of the action
    $("#tabNotificationsDetails a").click();
}
catch(e)
{
   notyAlert('error', e.Message);
}
    

}

function ClearForm() {
    try {
        var validator = $("#Addressform").validate();
        $('#Addressform').find('.field-validation-error span').each(function () {
            validator.settings.success($(this));
        });
        $('#Addressform')[0].reset();
        $('#customerAddress_ID').val(0);

    }
    catch (e) {
        notyAlert('errror', e.message);
    }

}