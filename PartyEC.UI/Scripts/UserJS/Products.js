var DataTables = {};

$(document).ready(function () {


    $("#HeaderTag").on({

        focusout: function () {
            var txt = this.value.replace(/[^a-z0-9\+\-\.\#]/ig, '');
            if (txt) $("<span/>", { text: txt.toLowerCase(), insertAfter: this }).attr({ 'class': 'Htags','onclick':'removeme(this)' });
            this.value = "";
        },
        keypress: function (ev) {
            if (ev.keyCode == 13) {
                if (/(188|13)/.test(ev.which)) $(this).focusout();
                var callbacks = $.Callbacks();
                callbacks.disable();
                return false;
            }
        }
    });
  
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
              {//hiding hidden column 
                  "targets": [0],
                  "visible": true,
                  "searchable": true
              }
             ]
         });
    }
    catch (e)
    {
        notyAlert('errror', e.message);
    }
    //Rating Popover
   $('[data-toggle="Ratingpopover"]').popover();
    try {

        DataTables.RelatedproductsTable = $('#tblRelatedproducts').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: null,
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
         
               { "data": null, "orderable": false, "defaultContent": '<a onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
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
//remove header tags
function removeme(current)
{
    $(current).remove();
}



function Edit(currentObj)
{
  
   //Tab Change
    $('#tabproductDetails').trigger('click');
  
    var rowData = DataTables.productTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null))
    {
        var thisproduct = GetProduct(rowData.ID);
        if (thisproduct != null)
        {
            $("#Name").val(thisproduct.Name);
            $("#SKU").val(thisproduct.SKU);
            if (thisproduct.ConfigurableYN == false)
            { $("#married-false").prop('checked', true); }
            else { $("#married-true").prop('checked', true); }
            $("#ShortDescription").val(thisproduct.ShortDescription);
            $("#ProductType").val(thisproduct.ProductType);
            $("#ID").val(thisproduct.ID);
            RefreshRelatedProducts(thisproduct.ID);
        }
       
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

function GetRelatedProducts(id) {

    try {
        
        var data = {"id":id};
        var ds = {};
        ds = GetDataFromServer("Products/GetRelatedProducts/", data);
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
    catch (e) {
        notyAlert('error', e.message);
    }

}
function RefreshProducts() {
    try {
        DataTables.productTable.clear().rows.add(GetAllProducts()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
function RefreshRelatedProducts(id) {
    try {
        DataTables.RelatedproductsTable.clear().rows.add(GetRelatedProducts(id)).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function ConstructproductDetailObject()
{
    $('.Htags').each(function () {
        debugger;
        var tagval = [];
        tagval.push(this.innerHTML);
    });


    
    var ProductDetailViewModel = new Object();
    ProductDetailViewModel.Qty =$.parseJSON($('#productform :input[name="Qty"]').serializeArray()[0].value);
    ProductDetailViewModel.OutOfStockAlertQty = $.parseJSON($('#productform :input[name="OutOfStockAlertQty"]').serializeArray()[0].value);
    ProductDetailViewModel.StockAvailable = $.parseJSON($('#productform :input[name="StockAvailable"]').serializeArray()[0].value);
    ProductDetailViewModel.DiscountAmount = $.parseJSON($('#productform :input[name="DiscountAmount"]').serializeArray()[0].value);
    ProductDetailViewModel.DiscountStartDate = $('#productform :input[name="DiscountStartDate"]').serializeArray()[0].value;
    ProductDetailViewModel.DiscountEndDate = $('#productform :input[name="DiscountEndDate"]').serializeArray()[0].value;
    ProductDetailViewModel.Enabled = $.parseJSON($('#productform :input[name="Enabled"]').serializeArray()[0].value);
    var ar = [];
    ar.push(ProductDetailViewModel);
    $("#productDetailhdf").val(JSON.stringify(ar));
    
}

function productSaveSuccess(data, status, xhr)
{
    debugger;
    var JsonResult=JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
            notyAlert('success', JsonResult.Record.StatusMessage);
            $("#ID").val(JsonResult.Record.ReturnValues);
            RefreshProducts();
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Record.StatusMessage);
            break;
        default:
            break;
    }
}
function productSaveFailure()
{
    alert("Failure");
}
function onbeginProductSave()
{
}
function oncomplteProductSave()
{
    alert("oncomplete");
   // $('#loadProgressBar').hide();
    
}
