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
               { "data": null, "orderable": false, "defaultContent": '<a onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ]
         });

        DataTables.CustomersCartTable = $('#tblShoppingCart').DataTable(
          {
              dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
              order: [],
              searching: true,
              paging: true,
              data: null,
              columns: [{ "data": "ProductID" },
                        { "data": "ProductName" },
                        { "data": "ProductSpecXML", "defaultContent": "<i>-</i>" },
                        { "data": "Qty", "defaultContent": "<i>-</i>" },
                        { "data": "CurrencyCode", "defaultContent": "<i>-</i>" },
                        { "data": "Price", "defaultContent": "<i>-</i>" },
                        { "data": "ItemStatus", "defaultContent": "<i>-</i>" },
                        { "data": "CreatedDate", "defaultContent": "<i>-</i>" }
              ], columnDefs: [{                  
                     "targets": [7], "render": function (data, type, full, meta) {
                         var str = Date.parse(data);
                         debugger;
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
                columns: [{ "data": "ProductID" },
                            { "data": "ProductName" },
                            { "data": "ProductSpecXML", "defaultContent": "<i>-</i>" },
                            { "data": "CreatedDate", "defaultContent": "<i>-</i>" },
                            //{ "data": "", "defaultContent": "<i>-</i>" }
                ], columnDefs: [{
                    "targets": [3], "render": function (data, type, full, meta) {
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

function GetAllCustomerCartWishlistSummary() {

    try {
        debugger;
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

function Edit(currentObj) {
    //Tab Change
    debugger;
    ChangeButtonPatchView("EventRequests", "btnPatcheventRequeststab2", "Edit"); //ControllerName,id of the container div,Name of the action
    $('#tabshoppingCartWishlist').trigger('click');
    
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

function GetCustomerShoppingCart(id) {

    try {
        debugger;
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Cart_Wishlist/GetCustomerShoppingCart/", data);
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

function GetCustomerWishlist(id) {

    try {
        debugger;
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Cart_Wishlist/GetCustomerWishlist/", data);
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

function goback() {
    $('#tabCustomerList').trigger('click');
    //try {
    //    DataTables.CustomersListTable.clear().rows.add(GetAllCustomerCartWishlistSummary()).draw(false);
    //}
    //catch (e) {
    //    notyAlert('error', e.message);
    //}
}