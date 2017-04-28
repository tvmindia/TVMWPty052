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
        notyAlert('error', e.message);
    }

    try {
        ChangeButtonPatchView("Users", "UserToolBox", "Add"); //ControllerName,id of the container div,Name of the action

        $("#tabUserDetails").click(function () {
            ChangeButtonPatchView("Users", "UserToolBox", "Save"); //ControllerName,id of the container div,Name of the action
        });
        $("#tabUserList").click(function () {
            ChangeButtonPatchView("Users", "UserToolBox", "Add"); //ControllerName,id of the container div,Name of the action
        });
    }
    catch (e)
    {
        notyAlert('error', e.message);
    }

});


function ConstructRoleList()
{
    var selectedroles = [];
  
    $(".checkbox-inline input:checked").each(function () {
        selectedroles.push(JSON.parse(this.value));
    });
    $("#RoleList").val(selectedroles);
    //$("#RoleCheckbox1").prop('checked')==true
}

function Edit(curobj)
{
    try
    {
       
        ClearForm();
        $('#tabUserDetails a').trigger('click');
        var rowData = DataTables.userTable.row($(curobj).parents('tr')).data();
        if ((rowData != null) && (rowData.ID != null)) {
            var thisuser = GetUserDetails(rowData.ID);
            if ((thisuser)&&(thisuser.length>0)) {
                var _user = thisuser[0];
                for (index = 0; index < thisuser.length; ++index) {
                    {
                        if (thisuser[index].RoleObj.RoleName == 'Manager') {
                            $("#RoleCheckbox2").prop('checked', true);
                        }
                      
                        if (thisuser[index].RoleObj.RoleName == 'Admin') {
                            $("#RoleCheckbox1").prop('checked', true);
                        }
                        
                    }
                }
               
                $("#UserName").val(_user.UserName);
                $("#RoleID").val(_user.RoleObj.ID);
                $("#LoginName").val(_user.LoginName);
                $("#LoginName").attr({ "readonly": "readonly" });
                $("#userForm").attr("data-ajax-begin", "return true");
                //$("#Password").val('*******');
                //$("#ConfirmPassword").val('*******');
                $("#ID").val(_user.ID);
                $("#userID").val(_user.ID);
             
                }
        }
        ChangeButtonPatchView("Users", "UserToolBox", "Edit"); //ControllerName,id of the container div,Name of the action
    }
    catch(e)
    {
        notyAlert('error', e.message);
    }
}

function AddUser()
{
    try
    {
        ClearForm();
        $('#tabUserDetails a').trigger('click');


    }
    catch(e)
    {
        notyAlert('error', e.message);
    }
}

function SaveUser() {
    try {
    
        $("#btnuserSubmit").click();

    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function ValidatePassword()
{
  var pas=  $("#Password").val();
  var cpas = $("#ConfirmPassword").val();
  if((pas)&&(cpas))
  {
      return true;
  }
  notyAlert('error', 'Password Required');
  return false;
}

function DeleteUser()
{
    try
    {
        $("#btndeleteuserSubmit").click();
    }
    catch(e)
    {
        notyAlert('error', e.message);
    }
}

function usersaveSuccess(data, status, xhr) {
    try
    {
        var JsonResult = JSON.parse(data)
        switch (JsonResult.Result) {
            case "OK":
                notyAlert('success', JsonResult.Record.StatusMessage);
                RefreshUserTable();
                goback();
                break;
            case "ERROR":
                notyAlert('error', JsonResult.Record.StatusMessage);
                break;
            case "VALIDATION":
                notyAlert('error', JsonResult.Message);
            default:
                break;
        }
    }
    catch(e)
    {
        notyAlert('error', e.message);
    }
   

}
function userDeleteFailure(data, status, xhr)
{
    notyAlert('error', status);
}
 
function userDeleteSuccess(data, status, xhr)
{
    try
    {
        var JsonResult = JSON.parse(data)
        switch (JsonResult.Result) {
            case "OK":
                notyAlert('success', JsonResult.Record.StatusMessage);
                RefreshUserTable();
                goback();
                break;
            case "ERROR":
                notyAlert('error', JsonResult.Record.StatusMessage);
                break;
            default:
                break;
        }
    }
    catch(e)
    {
        notyAlert('error', e.message);
    }
}


function goback() {
    ClearForm();
    $("#tabUserList a").click();
    
}

function ClearForm()
{
    try
    {
        var validator = $("#userForm").validate();
        $('#userForm').find('.field-validation-error span').each(function () {
            validator.settings.success($(this));
        });
        $('#userForm')[0].reset();
        $("#ID").val(0);
        $("#UserRoleLinkID").val(0);
        $("#LoginName").removeAttr("readonly");
        $("#userForm").attr("data-ajax-begin", "return ValidatePassword()");
    }
    catch(e)
    {
        notyAlert('error', e.message);
    }
}

function RefreshUserTable() {
    try {
        DataTables.userTable.clear().rows.add(GetAllSystemUsers()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}


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
        notyAlert('error', e.message);
    }
}

function GetUserDetails(id)
{
    try
    {
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Users/GetUserDetailsByUser/", data);
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

    }
}