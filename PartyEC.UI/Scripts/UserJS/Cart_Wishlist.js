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
    ChangeButtonPatchView("EventRequests", "btnPatcheventRequeststab2", "Edit"); //ControllerName,id of the container div,Name of the action
   
    debugger;
    var rowData = DataTables.CustomersListTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null))
    {
        DataTables.CustomersCartTable = $('#tblShoppingCart').DataTable(
            {
               dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
               order: [],
               searching: true,
               paging: true,
               data: GetCustomerShoppingCart(rowData.ID),
               columns: [{ "data": "ProductID" },
                         { "data": "ProductName" },
                         { "data": "ProductSpecXML", "defaultContent": "<i>-</i>" },
                         { "data": "Qty", "defaultContent": "<i>-</i>" },
                         { "data": "CurrencyCode", "defaultContent": "<i>-</i>" },
                         { "data": "Price", "defaultContent": "<i>-</i>" },
                         { "data": "ItemStatus", "defaultContent": "<i>-</i>" }
               ]
            });

        DataTables.CustomersWishlistTable = $('#tblWishlist').DataTable(
            {
                dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
                order: [],
                searching: true,
                paging: true,
                data: GetCustomerWishlist(rowData.ID),
                columns: [  { "data": "ProductID" },
                            { "data": "Product Name" },
                            { "data": "ProductSpecXML", "defaultContent": "<i>-</i>" },
                            { "data": "", "defaultContent": "<i>-</i>" },
                            { "data": "", "defaultContent": "<i>-</i>" },
                ]
            });
    }
}