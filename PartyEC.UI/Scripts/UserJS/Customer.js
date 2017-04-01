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
                $('#lblCustName').text(thiscustomer.Name);
                $('#lblCustAccCreate').text(ConvertJsonToDate(thiscustomer.logDetailsObj.CreatedDate));
                $('#lblEmail').text(thiscustomer.Email);
                $('#lblCustMobile').text(thiscustomer.Mobile);
                $('#lblCustGender').text(thiscustomer.Gender);
                $('#lblCustActive').text((thiscustomer.IsActive!=true?'No':'Yes'));
                $('#lblCustDefaultAddress').text(thiscustomer.customerAddress.Address);
                $('#lblCustCity').text(thiscustomer.customerAddress.City);
                $('#lblCustStateProvince').text(thiscustomer.customerAddress.StateProvince);
                $('#lblCustCountry').text(thiscustomer.customerAddress.county.Name);


            }

        }
    }
    catch(e)
    {
        notyAlert('errror', e.message);
    }
}

function GetCustomerDetail(custid)
{
    try
    {
        if ((custid) && (custid > 0)) {
            var data = { "ID" : custid };
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
    catch(e)
    {
        notyAlert('errror', e.message);
    }
   
}

function  GetCustomerOrders()
{
    try
    {
        
    }
    catch(e)
    {
        notyAlert('errror', e.message);
    }
}
function RefreshCustomerOrders()
{
    try
    {

    }
    catch(e)
    {
        notyAlert('errror', e.message);
    }
}

