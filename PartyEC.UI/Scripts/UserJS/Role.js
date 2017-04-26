var DataTables = {};
$(document).ready(function () {

    try
    {
        DataTables.rolesTable = $('#tblRoles').DataTable(
       {
           dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
           order: [],
           searching: true,
           paging: true,
           data: GetAllSystemRoles(),
           columns: [
               { "data": "ID" },
               { "data": "RoleName", "defaultContent": "<i>-</i>" }
             
              ]
         });
    }
    catch(e)
    {
        notyAlert('errror', e.message);
    }

});


function GetAllSystemRoles()
{
    try { 
        var data = "";
        var ds = {};
        ds = GetDataFromServer("Roles/GetAllRolesOfSystem/", data);
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
    catch(e)
    {
        notyAlert('errror', e.message);
    }
}