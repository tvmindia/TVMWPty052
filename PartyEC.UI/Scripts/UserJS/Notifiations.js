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
               { "data": "ID" },
               { "data": "customer", "defaultContent": "<i>-</i>" },
               { "data": "Title", "defaultContent": "<i>-</i>" },
                { "data": "Message", "defaultContent": "<i>-</i>" },
                 { "data": "logDetailsObj", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="EditCustomer(this)"><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             
             ],
             columnDefs: [
          {
              "render": function (data, type, row) {
                  if (data) {
                      return data.Name;
                      }
                  },
              "targets": 1
          },
          {
              "render": function (data, type, row) {
                  if (data) {
                      return ConvertJsonToDate(data.CreatedDate);
                  }
              },
              "targets": 4
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
             data: GetAllCustomers(),
             columns: [
               { "data": null, "defaultContent": '' },
               { "data": null, "defaultContent": '<span class="glyphicon glyphicon-user" aria-hidden="true"></span>' },
               { "data": "Name", "defaultContent": "<i>-</i>" },
               { "data": "Mobile", "defaultContent": "<i>-</i>" },
               { "data": "Address", "defaultContent": "<i>-</i>" }
              

             ],
             columnDefs: [
                 {
                     orderable: false,
                     className: 'select-checkbox',
                     targets: 0
                 }
             ]
                 
            


         });
        //$('#tblCustomersInNotificatoin tbody').on('click', 'tr', function () {
        //    $(this).toggleClass('selected');
        //});
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