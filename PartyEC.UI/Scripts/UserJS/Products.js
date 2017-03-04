var DataTables = {};

$(document).ready(function () {
   // $("#tblproducts").DataTable();
   try {
        var ProductViewModel = new Object();
        DataTables.productTable = $('#tblproducts').DataTable(
         {
             dom: '<"top"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllProducts(ProductViewModel),
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
               { "data": null, "orderable": false, "defaultContent": '<a onclick="RatingPopup(this)">Rating</a>' },
               { "data": null, "orderable": false, "defaultContent": '<a onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [
              {//hiding hidden column field churchid
                  "targets": [0],
                  "visible": false,
                  "searchable": false
              }
             ]
         });
    }
    catch (e)
    {
        alert(e.message);
    }

 
});

function Edit(currentObj)
{
   //Tab Change
    $('#tabproductDetails').trigger('click');
    debugger;
    var rowData = DataTables.productTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null))
    {
        alert(rowData.Name);
        GetProductDetailsByID(id);
    }

   
}

function GetProductDetailsByID(id)
{
    try {
        var data = "{ID:" + id + "}";
        var ds = {};
        ds = GetDataFromServer("Products/GetAllProductsByID/", data);
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

    }
}

function GetAllProducts(ProductViewModel)
{
   
try {
        var data = "{'productObj':" + JSON.stringify(ProductViewModel) + "}";
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
            alert(ds.Message);
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
