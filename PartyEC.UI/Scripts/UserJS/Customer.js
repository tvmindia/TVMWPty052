var DataTables = {};
$(document).ready(function () {


    try {

        DataTables.customerTable = $('#tblCustomers').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllCustomers(),
             columns: [
               { "data": "ID" },
               { "data": "Name" },
               { "data": "Email", "defaultContent": "<i>-</i>" },
               { "data": "Mobile", "defaultContent": "<i>-</i>" },
               { "data": "Language", "defaultContent": "<i>-</i>" },
               { "data": "Gender", "defaultContent": "<i>-</i>" },
               { "data": "OrdersCount", "defaultContent": "<i>-</i>" },
               { "data": "BookingsCount", "defaultContent": "<i>-</i>" },
               { "data": "QuotationsCount", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="EditCustomer(this)"><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' },
               { "data": "IsActive", "defaultContent": "<i>-</i>" }
             ],
             columnDefs: [
              {//hiding hidden column 
                  "targets": [10],
                  "visible": false,
                  "searchable": false
              }
             ]
         });
    }
    catch (e) {
        notyAlert('errror', e.message);
    }

    try
    {
        DataTables.orderTable = $('#tblOrderSummary').DataTable(
                {
                   
                    ordering: false,
                    paging:   false,
                    info:     false,
                    searching: false,
                    data: null,
                    columns: [
                      { "data": "OrderNo" },
                      { "data": "OrderDate", "defaultContent": "<i>-</i>" },
                      { "data": "BillFirstName", "defaultContent": "<i>-</i>" },
                      { "data": "ShipFirstName", "defaultContent": "<i>-</i>" },
                      { "data": "TotalOrderAmt", "defaultContent": "<i>-</i>" }
                     
                    ],
                    columnDefs: [
                     {//hiding hidden column 
                         "targets": [0],
                         "visible": true,
                         "searchable": false
                     }
                    ]
                });
    }
    catch(e)
    {
        notyAlert('errror', e.message);
    }


    try {
        DataTables.cartTable = $('#tblCart').DataTable(
       {
           ordering: false,
           paging: false,
           info: false,
           searching: false,
           data: null,
           columns: [
             { "data": "ProductID" },
             { "data": "ProductName" },
             { "data": "Qty", "defaultContent": "<i>-</i>" },
             { "data": "Price", "defaultContent": "<i>-</i>" },
             { "data": "Total", "defaultContent": "<i>-</i>" }
           ],
           columnDefs: [
            {//hiding hidden column 
                "render": function (data, type, row)
                {
                 return row.Price * row.Qty;
                },
               "targets": 4
            }
            

           ]
       });
    }
    catch (e) {
        notyAlert('errror', e.message);
    }

    try
    {
        DataTables.wishListTable = $('#tblWishlist').DataTable(
       {
           ordering: false,
           paging: false,
           info: false,
           searching: false,
           data: null,
           columns: [
             { "data": "ProductID" },
             { "data": "ProductName" },
             { "data": "CreatedDate", "defaultContent": "<i>-</i>" },
             { "data": "DaysinWL", "defaultContent": "<i>-</i>" },
           ],
           columnDefs: [
            {//hiding hidden column 
                "targets": [0],
                "visible": true,
                "searchable": true
            }
           ]
       });
    }
    catch(e)
    {
        notyAlert('errror', e.message);
    }


});

function GetAllCustomers()
{
    try
    {
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
    catch(e)
    {
        notyAlert('errror', e.message);
    }
}

function EditCustomer(currentObj)
{
    debugger;
    try
    {
        var rowData = DataTables.customerTable.row($(currentObj).parents('tr')).data();
        if ((rowData.ID != null))
        {
            $("#tabcustomerView a").trigger('click');
            //render button patch
            switch(rowData.IsActive)
            {
                case true:
                    ChangeButtonPatchView("Customer", "customerToolBox", "Deactivate"); //ControllerName,id of the container div,Name of the action
                break;
                case false:
                    ChangeButtonPatchView("Customer", "customerToolBox", "Activate"); //ControllerName,id of the container div,Name of the action
                break;
            }
            var thiscustomer = GetCustomerDetail(rowData.ID);
            if(thiscustomer)
            {
                $('#hdfCustomerID').val(rowData.ID);
                $('#hdfCustomerActive').val(rowData.IsActive);
                $('#lblCustName').text(((thiscustomer.Name != "") && (thiscustomer.Name!=null)?thiscustomer.Name:'-'));
                $('#lblCustAccCreate').text(((thiscustomer.logDetailsObj.CreatedDate!="")&&(thiscustomer.logDetailsObj.CreatedDate!=null)?ConvertJsonToDate(thiscustomer.logDetailsObj.CreatedDate):'-'));
                $('#lblEmail').text(((thiscustomer.Email != "") && (thiscustomer.Email!=null)?thiscustomer.Email:'-'));
                $('#lblCustMobile').text(((thiscustomer.Mobile != "") && (thiscustomer.Mobile != null) ? thiscustomer.Mobile : '-'));
                $('#lblCustGender').text(((thiscustomer.Gender != "") && (thiscustomer.Gender!=null) ? thiscustomer.Gender : '-'));
                $('#lblCustActive').text((thiscustomer.IsActive != true ? 'No' : 'Yes'));
                $('.col-md-8 Address').html(((thiscustomer.customerAddress.Address != "") && (thiscustomer.customerAddress.Address != null) ? thiscustomer.customerAddress.Address : '-') + '<br>' + ((thiscustomer.customerAddress.City != "") && (thiscustomer.customerAddress.City != null) ? thiscustomer.customerAddress.City : '-') + '<br>' + ((thiscustomer.customerAddress.StateProvince != "") && (thiscustomer.customerAddress.StateProvince != null) ? thiscustomer.customerAddress.StateProvince : '-') + '<br>' + ((thiscustomer.customerAddress.county.Name != "") && (thiscustomer.customerAddress.county.Name!=null)?thiscustomer.customerAddress.county.Name:'-'));
            }
            var ordreSum = GetSalesStatistics(rowData.ID);
            if(ordreSum)
            {
                $('#lblLifeSales').text(((ordreSum.LifeTimeSales != "")&&(ordreSum.LifeTimeSales!=null)) ? ordreSum.LifeTimeSales : '-');
                $('#lblAverageSales').text(((ordreSum.AverageSales != "") && (ordreSum.AverageSales != null)) ? ordreSum.AverageSales : '-');
                $('#lblLastSale').text(((ordreSum.LastMonthSales)&&(ordreSum.LastMonthSales))?ordreSum.LastMonthSales:'-');
            }
            RefreshCustomerOrders(rowData.ID);
            RefreshCustomerCartList(rowData.ID);
            RefreshCustomerWishList(rowData.ID);
        }
    }
    catch(e)
    {
        notyAlert('errror', e.message);
    }
}

function GetCustomerDetail(custid) {
    try {
        if ((custid) && (custid > 0)) {
            var data = { "ID": custid };
            var ds = {};
            ds = GetDataFromServer("Customer/GetCustomer/", data);
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
        else {

        }
    }
    catch (e) {
        notyAlert('errror', e.message);
    }

}

function GetSalesStatistics(custid)
{
    try
    {
        if ((custid) && (custid > 0)) {
            var data = { "customerID": custid };
            var ds = {};
            ds = GetDataFromServer("Customer/GetSalesStatistics/", data);
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
    }
    catch(e)
    {
        notyAlert('errror', e.message);
    }
}

function GetCustomerOrders(custid)
{
    try
    {
        if ((custid) && (custid > 0)) {
            var data = { "customerID": custid };
            var ds = {};
            ds = GetDataFromServer("Customer/GetOrderSummary/", data);
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
    }
    catch(e)
    {
        notyAlert('errror', e.message);
    }
}
function RefreshCustomerOrders(custid)
{
   
    try
    {
        DataTables.orderTable.clear().rows.add(GetCustomerOrders(custid)).draw(false);
    }
    catch(e)
    {
        notyAlert('errror', e.message);
    }
}

function GetCustomerWishList(custid) {
    try {
        if ((custid) && (custid > 0)) {
            var data = { "customerID": custid };
            var ds = {};
            ds = GetDataFromServer("Customer/GetCustomerWishList/", data);
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
    }
    catch (e) {
        notyAlert('errror', e.message);
    }
}

function RefreshCustomerWishList(custid) {

    try {
        DataTables.wishListTable.clear().rows.add(GetCustomerWishList(custid)).draw(false);
    }
    catch (e) {
        notyAlert('errror', e.message);
    }
}

function GetCustomerCartList(custid) {
    try {
        if ((custid) && (custid > 0)) {
            var data = { "customerID": custid };
            var ds = {};
            ds = GetDataFromServer("Customer/GetCustomerCartDetails/", data);
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
    }
    catch (e) {
        notyAlert('errror', e.message);
    }
}

function RefreshCustomerCartList(custid) {

    try {
        DataTables.cartTable.clear().rows.add(GetCustomerCartList(custid)).draw(false);
    }
    catch (e) {
        notyAlert('errror', e.message);
    }
}

function RefreshCustomer() {

    try {
        DataTables.customerTable.clear().rows.add(GetAllCustomers()).draw(false);
    }
    catch (e) {
        notyAlert('errror', e.message);
    }
}

function ActivateORDeactivate()
{
    try {
        var CustomerViewModel = new Object();
        CustomerViewModel.ID = $('#hdfCustomerID').val();
        var activeflag=$('#hdfCustomerActive').val();
        if (activeflag == "false") {
            CustomerViewModel.IsActive = true;
        }
        else {
            CustomerViewModel.IsActive = false;
        }
       
        var data = "{'customer':" + JSON.stringify(CustomerViewModel) + "}";
        PostDataToServer('Customer/ActiateorDeactivateCustomer/', data, function (JsonResult) {
            if (JsonResult != '') {
                switch (JsonResult.Result) {
                    case "OK":
                        notyAlert('success', JsonResult.Record.StatusMessage);
                        $('#hdfCustomerActive').val(JsonResult.Record.ReturnValues);
                        switch (JsonResult.Record.ReturnValues)
                        {
                            case true:
                                ChangeButtonPatchView("Customer", "customerToolBox", "Deactivate"); //ControllerName,id of the container div,Name of the action
                                break;
                            case false:
                                ChangeButtonPatchView("Customer", "customerToolBox", "Activate"); //ControllerName,id of the container div,Name of the action
                                break;
                        }
                        RefreshCustomer();
                      
                        break;
                    case "ERROR":
                        notyAlert('error', JsonResult.Record.StatusMessage);
                        break;
                    default:
                        break;
                }
            }
        });
    }
    catch (e)
    {
        notyAlert('errror', e.message);
    }
}

function goback()
{
    $("#tabcustomerList a").click();
}

function AddressSave()
{
    alert("save");
}
