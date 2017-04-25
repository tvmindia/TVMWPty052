var DataTables = {};
$(document).ready(function () {

    try {
        DataTables.userTable = $('#tblUsers').DataTable(
       {
           dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
           order: [],
           searching: true,
           paging: true,
           data: GetAllSystemUsers(),
           columns: [
               { "data": "ID" },
               { "data": "UserName", "defaultContent": "<i>-</i>" },
               { "data": "RoleObj", "defaultContent": "<i>-</i>" },
              // { "data": "logDetails", "defaultContent": "<i>-</i>" },
              { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="Edit(this)"><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
           ],
           columnDefs: [
             {
                 "render": function (data, type, row) {

                     if (data) {
                         return data.RoleName;
                     }

                 },
                 "targets": 2
             }
             //},
             // {
             //     "render": function (data, type, row) {

             //         if (data) {
             //             return data.CreatedDate;
             //         }

             //     },
             //     "targets": 3
             // }
             
           ]
       });
    }
    catch (e) {
        notyAlert('errror', e.message);
    }

});


function GetAllSystemUsers() {
    try {
        var data = "";
        var ds = {};
        ds = GetDataFromServer("Users/GetAllUsersOfSystem/", data);
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