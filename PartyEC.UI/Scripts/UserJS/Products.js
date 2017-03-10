var DataTables = {};

$(document).ready(function () {
    // $("#tblproducts").DataTable();
   
   try {
       
        DataTables.productTable = $('#tblproducts').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllProducts(),
             columns: [
               { "data": "ID" },
               { "data": "Name" },
               { "data": "ProductType", "defaultContent": "<i>-</i>" },
               { "data": "EnableYN", "defaultContent": "<i>-</i>" },
               { "data": "SupplierID", "defaultContent": "<i>-</i>" },
               { "data": "SKU", "defaultContent": "<i>-</i>" },
               { "data": "BaseSellingPrice", "defaultContent": "<i>-</i>" },
               { "data": "Qty", "defaultContent": "<i>-</i>" },
               { "data": "StockAvailableYN", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" data-toggle="Ratingpopover" title="Rating" data-trigger="focus" data-content="Some content inside the popover">Rating</a>' },
               { "data": null, "orderable": false, "defaultContent": '<a onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [
              {//hiding hidden column field churchid//data-toggle="popover" title="Popover Header" data-content="Some content inside the popover">Toggle popover
                  "targets": [0],
                  "visible": true,
                  "searchable": true
              }
             ]
         });
    }
    catch (e)
    {
        alert(e.message);
    }
    //Rating Popover
   $('[data-toggle="Ratingpopover"]').popover();
   
});

function Edit(currentObj)
{
   //Tab Change
    $('#tabproductDetails').trigger('click');
  
    var rowData = DataTables.productTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null))
    {
        var thisproduct = GetProduct(rowData.ID);
        $("#Name").val(thisproduct.Name);
        $("#ShortDescription").val(thisproduct.ShortDescription);
        $("#ProductType").val(thisproduct.ProductType);
    }

   
}

function GetProduct(id)
{
    try {
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Products/GetProduct/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Record;
        }
        if (ds.Result == "ERROR") {
            notyAlert('error',ds.Message);
        }

    }
    catch (e) {

    }
}

function GetAllProducts()
{
   
    try {
        debugger;
        var data = "";
        var ds = {};
        ds = GetDataFromServer("Products/GetAllProducts/", data);
        if (ds != '')
        {
            ds = JSON.parse(ds);
        }
        if(ds.Result=="OK")
        {
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.Message);
        }
       
    }
    catch (e) {
        
    }
    
}



function productSaveSuccess()
{
    alert("success");
}
function productSaveFailure()
{
    alert("Failure");
}
function onbeginProductSave()
{
    alert("onbegin");
    $('#loadingDisplay').show();
   

}
function oncomplteProductSave()
{
    alert("oncomplete");
    $('#loadProgressBar').hide();
    
}
