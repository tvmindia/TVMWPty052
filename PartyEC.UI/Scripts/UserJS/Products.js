var DataTables = {};

$(document).ready(function () {


    $("#HeaderTags").on({

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
    ChangeButtonPatchView("Products", "btnPatchProductDetails", "Edit"); //ControllerName,id of the container div,Name of the action
    //$('#tabproductDetails').removeClass('disabled');
    //$('#tabproductDetails a').attr('data-toggle', 'tab');
    $('#tabproductDetails a').trigger('click');
  
    var rowData = DataTables.productTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null))
    {
        var thisproduct = GetProduct(rowData.ID);
        if (thisproduct != null)
        {
            $("#Name").val(thisproduct.Name);
            $("#SKU").val(thisproduct.SKU);
            if (thisproduct.Enabled == true)
            { $("#Enabled").prop('checked', true); }
            else { $("#Enabled").prop('checked', false); }
            $("#Unit").val(thisproduct.Unit);
            $("#URL").val(thisproduct.URL);
            $("#ActionType").val(thisproduct.ActionType);
            $("#SupplierID").val(thisproduct.SupplierID);
            $("#ManufacturerID").val(thisproduct.ManufacturerID);
            $("#ProductType").val(thisproduct.ProductType);
            $("#AttributeSetID").val(thisproduct.AttributeSetID);
            if (thisproduct.FreeDelivery == true)
            { $("#FreeDelivery").prop('checked', true); }
            else { $("#FreeDelivery").prop('checked', false); }
            $("#CostPrice").val(thisproduct.CostPrice);
            $("#BaseSellingPrice").val(thisproduct.BaseSellingPrice);
            if (thisproduct.ShowPrice == true)
            { $("#ShowPrice").prop('checked', true); }
            else { $("#ShowPrice").prop('checked', false); }

            $("#DiscountAmount").val((thisproduct.ProductDetails.length!=0?thisproduct.ProductDetails[0].DiscountAmount:0.00));
            $("#DiscountStartDate").val((thisproduct.ProductDetails.length!=0?thisproduct.ProductDetails[0].DiscountStartDate:""));
            $("#DiscountEndDate").val((thisproduct.ProductDetails.length != 0?thisproduct.ProductDetails[0].DiscountEndDate:""));

            $("#ShortDescription").val(thisproduct.ShortDescription);

            $("#LongDescription").val(thisproduct.LongDescription);
            if (thisproduct.StockAvailable == true)
            { $("#StockAvailable").prop('checked', true); }
            else { $("#StockAvailable").prop('checked', false); }
          
            $("#Qty").val((thisproduct.ProductDetails.length != 0 ? thisproduct.ProductDetails[0].Qty : ""));
            $("#OutOfStockAlertQty").val((thisproduct.ProductDetails.length != 0 ? thisproduct.ProductDetails[0].OutOfStockAlertQty : ""));
            //Tags
            if (thisproduct.HeaderTags != null)
            {
                $('.Htags').remove();
                var tagar = thisproduct.HeaderTags.split(",");
                for (index = 0; index < tagar.length; ++index) {
                    //Tag creation when binding
                    $("#headertagsdiv").append($("<span/>", { text: tagar[index] }).attr({ 'class': 'Htags', 'onclick': 'removeme(this)' }));
                }
            }
            else
            {
                //Removes span tags
                $('.Htags').remove();
            }
            

            //ProductID
            $("#ID").val(thisproduct.ID);
            //ProductDetailID
            $("#productdetailsID").val((thisproduct.ProductDetails.length != 0 ? thisproduct.ProductDetails[0].ID : 0));
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
    var tagval = [];
    $('.Htags').each(function () {
             
        tagval.push(this.innerHTML);
    });
    debugger;
    $("#HeaderTags").val(tagval);
    
    var ProductDetailViewModel = new Object();
   
    ProductDetailViewModel.ID = ($('#productform :input[name="ProductDetailObj.ID"]').serializeArray()[0].value != "" ? $.parseJSON($('#productform :input[name="ProductDetailObj.ID"]').serializeArray()[0].value) :"");
    ProductDetailViewModel.Qty = ($('#productform :input[name="Qty"]').serializeArray()[0].value!=""?$.parseJSON($('#productform :input[name="Qty"]').serializeArray()[0].value): "");
  
   
    ProductDetailViewModel.OutOfStockAlertQty = ($('#productform :input[name="OutOfStockAlertQty"]').serializeArray()[0].value != "" ? $.parseJSON($('#productform :input[name="OutOfStockAlertQty"]').serializeArray()[0].value) : "");
    ProductDetailViewModel.StockAvailable = ($('#productform :input[name="StockAvailable"]').serializeArray()[0].value != "" ? $.parseJSON($('#productform :input[name="StockAvailable"]').serializeArray()[0].value) : "");

    ProductDetailViewModel.DiscountAmount =($('#productform :input[name="DiscountAmount"]').serializeArray()[0].value!=""?$.parseJSON($('#productform :input[name="DiscountAmount"]').serializeArray()[0].value):"");
    ProductDetailViewModel.DiscountStartDate =$('#productform :input[name="DiscountStartDate"]').serializeArray()[0].value;
    ProductDetailViewModel.DiscountEndDate = $('#productform :input[name="DiscountEndDate"]').serializeArray()[0].value;
    ProductDetailViewModel.Enabled =($('#productform :input[name="Enabled"]').serializeArray()[0].value!=""?$.parseJSON($('#productform :input[name="Enabled"]').serializeArray()[0].value):"");
    var ar = [];
    ar.push(ProductDetailViewModel);
    $("#productDetailhdf").val(JSON.stringify(ar));
    
}

function productSaveSuccess(data, status, xhr)
{
  
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
    notyAlert('error', 'Network Failure!');
}
function onbeginProductSave()
{
}
function oncomplteProductSave()
{
   // alert("oncomplete");
   //// $('#loadProgressBar').hide();
    
}
function ProductSave()
{
    $('#btnProductSubmit').trigger('click');
}

function btnAddNewProduct()
{
    //$('#tabproductDetails').removeClass('disabled');
    //$('#tabproductDetails a').attr('data-toggle', 'tab');
    //$('#tabproductList').addClass('disabled');
    //$('#tabproductList a').attr('data-toggle', '');
    $('#tabproductDetails a').trigger('click');
    clearform();
}
function goback()
{
    alert('hi');
   // tabproductList
}
function clearform()
{
    //Clear form
    $('#productform')[0].reset();
    //Clear Hidden form fields
    $("#productform input:hidden").val('').trigger('change');
}

function RelatedProductsModel()
{
    //popsup the model
    $('#btnmodelrelproduct').trigger('click');
}
