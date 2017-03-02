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
               { "data": "ShortDescription", "defaultContent": "<i>-</i>" },
               { "data": "ProductType", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a class="circlebtn circlebtn-info" onclick="EditChurch(this)"><i class="halflings-icon white edit""></i></a><a class="circlebtn circlebtn-danger"><i class="halflings-icon white trash" onclick="RemoveChurch(this)"></i></a>' }
             ],
             columnDefs: [
              {//hiding hidden column field churchid
                  "targets": [0, 1],
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
            ds= GetDataFromServer("Products/GetAllProducts/", data);
       
    }
    catch (e) {
        
    }
    return ds;
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
