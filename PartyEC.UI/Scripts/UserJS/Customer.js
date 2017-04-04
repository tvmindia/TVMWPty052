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
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="EditCustomer(this)"><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
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
    catch (e) {
        notyAlert('errror', e.message);
    }

    try
    {
        DataTables.orderTable = $('#tblOrderSummary').DataTable(
                {
                    dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
                    order: [],
                    searching: true,
                    paging: true,
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
                         "searchable": true
                     }
                    ]
                });
    }
    catch(e)
    {

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
            var thiscustomer = GetCustomerDetail(rowData.ID);
            if(thiscustomer)
            {
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

