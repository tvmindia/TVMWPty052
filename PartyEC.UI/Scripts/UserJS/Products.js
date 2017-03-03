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
               { "data": null, "orderable": false, "defaultContent": '<a class="circlebtn circlebtn-info" onclick="EditChurch(this)"><i class="halflings-icon white edit""></i></a><a class="circlebtn circlebtn-danger"><i class="halflings-icon white trash" onclick="RemoveChurch(this)"></i></a>' },
               { "data": null, "orderable": false, "defaultContent": '<a class="circlebtn circlebtn-info" onclick="EditChurch(this)"><i class="halflings-icon white edit""></i></a><a class="circlebtn circlebtn-danger"><i class="halflings-icon white trash" onclick="RemoveChurch(this)"></i></a>' }
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
    catch (e) {
        alert(e.message);
    }

 
});

function GetAllProducts(ProductViewModel)
{
    debugger;
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
