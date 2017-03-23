var DataTables = {};

$(document).ready(function () {
    try {
        debugger;
        var Cart_WishlistViewModel = new Object();
        DataTables.CustomersListTable = $('#tblCustomersList').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllCustomerCartWishlistSummary(),
             columns: [
               { "data": "ID" },
               { "data": "Name" },
               { "data": "Email", "defaultContent": "<i>-</i>"},
               { "data": "Mobile", "defaultContent": "<i>-</i>" },
               { "data": "CartCount", "defaultContent": "<i>-</i>" },
               { "data": "WishCount", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a onclick="View(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ]
         });

        DataTables.CustomersCartTable = $('#tblShoppingCart').DataTable(
          {
              dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
              order: [],
              searching: true,
              paging: true,
              data: null,
              columns: [{ "data": "CustomerName" },
                        { "data": "ProductID" },
                        { "data": "ProductName" },
                        { "data": "ProductSpecXML", "defaultContent": "<i>-</i>" },
                        { "data": "Qty", "defaultContent": "<i>-</i>" },
                        { "data": "CurrencyCode", "defaultContent": "<i>-</i>" },
                        { "data": "Price", "defaultContent": "<i>-</i>" },
                        { "data": "ItemStatus", "defaultContent": "<i>-</i>" },
                        { "data": "CreatedDate", "defaultContent": "<i>-</i>" }
              ], columnDefs: [{
                  "targets": [0], "visible": false, "searchable": false, "render": function (data, type, full, meta) { 
                      if (data != "") {
                          debugger;
                          $("#SC_TableHead").text(data + "'s ShoppingCart");
                      }
                      else {
                          $("#SC_TableHead").text("")
                      }
                     
                      return data;
                  }
              }, {
                     "targets": [8], "render": function (data, type, full, meta) {
                         var str = Date.parse(data); 
                         var res = ConvertJsonToDate('' + str + '');
                         return res;
                     }
                 }]
          });

        DataTables.CustomersWishlistTable = $('#tblWishlist').DataTable(
            {
                dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
                order: [],
                searching: true,
                paging: true,
                data: null,
                columns: [  { "data": "CustomerName" },
                            { "data": "ProductID" },
                            { "data": "ProductName" },
                            { "data": "ProductSpecXML", "defaultContent": "<i>-</i>" },
                            { "data": "CreatedDate", "defaultContent": "<i>-</i>" },
                            { "data": "DaysinWL", "defaultContent": "<i>-</i>" }
                ], columnDefs: [{
                    "targets": [0], "visible": false, "searchable": false, "render": function (data, type, full, meta) {
                        debugger;
                        if (data != "") {
                            $("#WL_TableHead").text(data + "'s WishList ");
                        }
                        else {
                            $("#SC_TableHead").text("")
                        }
                        return data;
                    }
                }, {
                    "targets": [4], "render": function (data, type, full, meta) {
                        var str = Date.parse(data);
                        debugger;
                        var res = ConvertJsonToDate('' + str + '');
                        return res;
                    }
                }]
            });
    
    }
    catch (e) {
        notyAlert('error', e.message);

    } 
});
//------------------------------------GetAllCustomerCartWishlistSummary--------------------------------------//
function GetAllCustomerCartWishlistSummary() {
    try {
        var data = {};
        var ds = {};
        ds = GetDataFromServer("Cart_Wishlist/GetAllCustomerCartWishlistSummary/", data);
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
//---------------------------------Edit Click-----------------------------------------//
function View(currentObj) {
    ChangeButtonPatchView("EventRequests", "btnPatcheventRequeststab2", "Edit"); //ControllerName,id of the container div,Name of the action
    $('#tabshoppingCartWishlist').removeClass('disabled');
    $('#tabshoppingCartWishlist a').attr('data-toggle', 'tab');
    $('#tabshoppingCartWishlist a').trigger('click');
    
    var rowData = DataTables.CustomersListTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null))
    {
        try {
            DataTables.CustomersCartTable.clear().rows.add(GetCustomerShoppingCart(rowData.ID)).draw(false);
        }
        catch (e) {
            notyAlert('error', e.message);
        }
        try {
            DataTables.CustomersWishlistTable.clear().rows.add(GetCustomerWishlist(rowData.ID)).draw(false);
        }
        catch (e) {
            notyAlert('error', e.message);
        }
       
        
      
      
    }
}
//------------------------------GetCustomerShoppingCart--------------------------------------------//
function GetCustomerShoppingCart(id) {

    try {
        debugger;
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Cart_Wishlist/GetCustomerShoppingCart/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
            if (ds.Records == 0) {
                $("#SC_TableHead").text("Shopping Cart"); 
            }
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
//--------------------------------GetCustomerWishlist------------------------------------------//
function GetCustomerWishlist(id) {

    try {
        debugger;
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Cart_Wishlist/GetCustomerWishlist/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
            if (ds.Records == 0) {
               
                $("#WL_TableHead").text("Wishlist");
            }
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
//-------------------------------CLick Events-------------------------------------------//
function Tab1Click() {
    $("#SC_TableHead").text("");
    $("#WL_TableHead").text("");
    $('#tabshoppingCartWishlist').addClass('disabled');
    $('#tabshoppingCartWishlist a').attr('data-toggle', '');
}
function goback() {
    $('#tabCustomerList').trigger('click');
    try {
        DataTables.CustomersListTable.clear().rows.add(GetAllCustomerCartWishlistSummary()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}