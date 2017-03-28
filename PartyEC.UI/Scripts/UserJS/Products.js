﻿var DataTables = {};

$(document).ready(function () {
    $("#HeaderTagsPicker").on({
        focusout: function () {
            var txt = this.value.replace(/[^a-z0-9\+\-\.\#]/ig, '');
            if (txt)
            {
                var h = $("<span/>", { text: txt }).attr({ 'class': 'label label-primary Htags', 'onclick': 'removeme(this)' });
                $('#keywordsDiv').append(h);
                this.value = "";
            }
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
  
    //$("#detailDetailTags").on({

    //    focusout: function () {
    //        var txt = this.value.replace(/[^a-z0-9\+\-\.\#]/ig, '');
    //        if (txt) $("<span/>", { text: txt.toLowerCase(), insertAfter: this }).attr({ 'class': 'Dtags', 'onclick': 'removeme(this)' });
    //        this.value = "";
    //    },
    //    keypress: function (ev) {
    //        if (ev.keyCode == 13) {
    //            if (/(188|13)/.test(ev.which)) $(this).focusout();
    //            var callbacks = $.Callbacks();
    //            callbacks.disable();
    //            return false;
    //        }
    //    }
    //});
    
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
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="Edit(this)"><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
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
               { "data": null,"defaultContent":'' },
               { "data": "ID" },
               { "data": "Name" },
               { "data": "ProductType", "defaultContent": "<i>-</i>" },
               { "data": "EnableYN", "defaultContent": "<i>-</i>" },
               { "data": "SupplierID", "defaultContent": "<i>-</i>" },
               { "data": "SKU", "defaultContent": "<i>-</i>" },
               { "data": "BaseSellingPrice", "defaultContent": "<i>-</i>" },
               { "data": "Qty", "defaultContent": "<i>-</i>" },
               { "data": "StockAvailableYN", "defaultContent": "<i>-</i>" }
         
               
             ],
             columnDefs: [{
                 orderable: false,
                 className: 'select-checkbox',
                 targets: 0
             }],
             select: {
                 style: 'multi',
                 selector: 'td:first-child'
             },
             order: [[1, 'asc']]

         });
      
    }
    catch (e) {
        notyAlert('errror', e.message);
    }

    try {

        DataTables.UNRelatedproductsTable = $('#tblUNRelatedproducts').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: null,
             columns: [
               {"data":null},
               { "data": "ID" },
               { "data": "Name" },
               { "data": "ProductType", "defaultContent": "<i>-</i>" },
               { "data": "EnableYN", "defaultContent": "<i>-</i>" },
               { "data": "SupplierID", "defaultContent": "<i>-</i>" },
               { "data": "SKU", "defaultContent": "<i>-</i>" },
               { "data": "BaseSellingPrice", "defaultContent": "<i>-</i>" },
               { "data": "Qty", "defaultContent": "<i>-</i>" },
               { "data": "StockAvailableYN", "defaultContent": "<i>-</i>" }
             
             ],
             columnDefs: [
              {//hiding hidden column 
                  "targets": [0],
                  "visible": true,
                  "searchable": false,
                  "render": function (data, type, full, meta) {
                      if (type === 'display') {
                          data = '<input type="checkbox" class="dt-checkboxes">';
                      }
                      return data;
                  }
              }
             ]
         });

        $('#tblUNRelatedproducts').on('change', 'input[type="checkbox"]', function () {
            $(this).closest('tr').toggleClass('selected');
        });
    }
    catch (e) {
        notyAlert('errror', e.message);
    }

    try {

        DataTables.AssociatedProductsTable = $('#tblAssociatedProducts').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: null,
             pageLength: 2,
             columns: [

               { "data": "ID", "defaultContent": "<i>-</i>" },
               { "data": "ProductName", "defaultContent": "<i>-</i>" },
               { "data": "ProductAttributes", "defaultContent": "<i>-</i>" },
               { "data": "BaseSellingPrice", "defaultContent": "<i>-</i>" },
               { "data": "PriceDifference", "defaultContent": "<i>-</i>" },
               { "data": null, "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="EditAssocProduct(this)"><i class="glyphicon glyphicon-pencil" aria-hidden="true"></i>Edit</a>' }
             ],
             columnDefs: [
              {
                  "render": function (data, type, row) {
                      var returnstring='';
                      if (data)
                      {
                          for(var ik=0;ik<data.length;ik++)
                          {
                              returnstring = returnstring + '<span>' + data[ik].Caption + ':' + (data[ik].Value != "" && data[ik].Value != null ? data[ik].Value : ' - ') + '</span><br/>';
                           }
                      }
                      return returnstring;
              },
                  "targets": 2
              }
             ]
            
         });
    }
    catch (e) {
        notyAlert('error', e.message);
    } 
});
//remove header tags
function removeme(current)
{
    $(current).remove();
}

function btnAddNewProduct() {
    //$('#tabproductDetails').removeClass('disabled');
    //$('#tabproductDetails a').attr('data-toggle', 'tab');
   
    //$('#tabproductList').addClass('disabled');
    //$('#tabproductList a').attr('data-toggle', '');
    $('#tabproductDetails a').attr({ 'data-toggle': 'tab', 'href': '#productDetails' });
    $('#tabproductList a').removeAttr('data-toggle href');
    $('#tabproductDetails a').trigger('click');
    $("#productDetails h4").text('New Product');
    $("#AttributeSetID").removeAttr('disabled');
    $("#ProductType").removeAttr('disabled');
    clearform();
}
function goback() {
   
    $('#tabproductList a').attr({ 'data-toggle': 'tab', 'href': '#productList' });
    //Remove attributes from current tab
    
    $('#tabproductDetails a').removeAttr('data-toggle href');
    $('#tabproductList a').trigger('click');
    // tabproductList
    clearform();
}


function Edit(currentObj)
{
    //Tab Change
    ChangeButtonPatchView("Products", "btnPatchProductDetails", "Edit"); //ControllerName,id of the container div,Name of the action
    $('#tabproductDetails a').attr({ 'data-toggle': 'tab', 'href': '#productDetails' });
    $('#tabproductList a').removeAttr('data-toggle href');
    $('#tabproductDetails a').trigger('click');
    //$('#tabproductDetails').removeClass('disabled');
   // $('#tabproductDetails a').attr('data-toggle', 'tab');
    //$('#tabproductDetails a').trigger('click');

    //Make General tab active
    $('#tabGeneral').trigger('click');
    //$("#LHSNavbarProductDetails li").removeClass('active');
   // $("#LHSNavbarProductDetails li.active").removeClass('active');
   // $("#LHSNavbarProductDetails li").first().addClass('active');
  

    var rowData = DataTables.productTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null))
    {
        var thisproduct = GetProduct(rowData.ID);
        if (thisproduct != null)
        {
            $("#productDetails h4").text(thisproduct.Name + '(' + (thisproduct.ProductType == "S" ? 'Simple' : 'Configurable') + ')');
            //disables some drop downs
            
            ((thisproduct.ProductType == "C") && (thisproduct.ProductDetails.length > 1) ? $("#AttributeSetID").attr({ 'disabled': 'disabled' }) : $("#AttributeSetID").removeAttr('disabled'));
            
            $("#ProductType").attr({ 'disabled': 'disabled' });
            $("#Name").val(thisproduct.Name);
            $("#SKU").val(thisproduct.SKU);
            if (thisproduct.Enabled == true)
            { $("#Enabled").prop('checked', true); }
            else { $("#Enabled").prop('checked', false); }
            $("#Unit").val(thisproduct.Unit);
            $("#URL").val(thisproduct.URL);
            $('#imgProduct').attr('src', '');
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
                    $("#keywordsDiv").append($("<span/>", { text: tagar[index] }).attr({ 'class': 'label label-primary Htags', 'onclick': 'removeme(this)' }));
                   
                }
            }
            else
            {
                //Removes span tags
                $('.Htags').remove();
            }
            

            //ProductID
            $(".productID").val(thisproduct.ID);
            //ProductDetailID
            $("#productdetailsID").val((thisproduct.ProductDetails.length != 0 ? thisproduct.ProductDetails[0].ID : 0));
            RefreshRelatedProducts(thisproduct.ID);
            RefreshUNRelatedProducts(thisproduct.ID);
        }
       
    }

   
}
function BindImages() {
    debugger;
    var Table = GetRelatedImages($('#ID').val());
    var MainFlag = 0;
    $('#ulOtherImages').empty();
    for(var i=0;i<Table.length;i++)
    {
        if(Table[i].MainImage)
        {
            MainFlag=1;
            $('#imgProduct').attr('src', (Table[i].ImageURL != "" && Table[i].ImageURL != null ? Table[i].ImageURL + '?' + new Date().getTime() : "/Content/images/NoImageFound.png"));
            $('#ImageID').val(Table[i].ImageID);
        }
        else
        {
            $('#ulOtherImages').append(' <li class="col-sm-3"><a class="thumbnail" onclick="SelectForDelete(this)" id="' + Table[i].ImageID + '"><img style="width: 100px;height: 100px;object-fit: cover;" src="' + Table[i].ImageURL + '?' + new Date().getTime() + '">'
                +'<a style="top: 2%;left: 14%;position: absolute;background: white;" class="fa fa-search-plus" href="' + Table[i].ImageURL + '?' + new Date().getTime() + '" data-lightbox="roadtrip"/></a></li>')
        }
    }
    if(Table.length==0&&MainFlag==0)
    {
        $('#imgProduct').attr('src','/Content/images/NoImageFound.png');
    }
}
function DeleteOtherImage()
{
    debugger;
    var DeletedImageID = [];
    var objectset = $('#ulOtherImages a.Selected');
    for(var i=0;i<objectset.length;i++)
    {
        DeletedImageID.push(objectset[i].id);
    }
    var ProductViewModel=new Object();
    ProductViewModel.IDSet=DeletedImageID;
    var data = "{'productViewObj':" + JSON.stringify(ProductViewModel) + "}";
    PostDataToServer('Products/DeleteProductOtherImages/', data, function (JsonResult)
    {
        if (JsonResult != '') {
            switch (JsonResult.Result)
            {
                case "OK":
                    notyAlert('success', JsonResult.Record.StatusMessage);
                    BindImages();
                    ChangeButtonPatchView("Products", "buttonPatchOtherImages", "CancelDelete");
                    break;
                case "ERROR":
                    notyAlert('error', JsonResult.Record.StatusMessage);
                    break;
                default:
                    break;
            }
        }
    })
    }
function SelectForDelete(this_Obj)
{
    debugger;
    $(this_Obj).toggleClass('Selected');
    if($('#ulOtherImages a.Selected').length>0)
    {
        ChangeButtonPatchView("Products", "buttonPatchOtherImages", "Delete");
    }
    else
    {
        ChangeButtonPatchView("Products", "buttonPatchOtherImages", "CancelDelete");
    }
}
function GetRelatedImages(id)
{
    try {
        debugger;
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Products/GetRelatedImages/", data);
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
    catch (e) {

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

function GetUNRelatedProducts(id) {
    try {
        var data = { "id": id };
        var ds = {};
        ds = GetDataFromServer("Products/GetUNRelatedProducts/", data);
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

function RefreshUNRelatedProducts(id) {
    try {
        DataTables.UNRelatedproductsTable.clear().rows.add(GetUNRelatedProducts(id)).draw(false);
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
            $(".productID").val(JsonResult.Record.ReturnValues);
            RefreshProducts();
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Record.StatusMessage);
            break;
        default:
            notyAlert('error', JsonResult.Record.Message);
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

function ProductSave()
{
    $('#btnProductSubmit').trigger('click');
}



function clearform()
{
    //Clear form
    $('#productform')[0].reset();
    //Clear Hidden form fields
    //$("#productform input:hidden").val('').trigger('change');
    $(".productID").val(0);
    $("#productdetailsID").val(0);
    $("#productDetailhdf").val('');
    
}

function RelatedProductsModel()
{
    //popsup the model
    $('#btnmodelrelproduct').trigger('click');
}

function ConstructRelatdProductIDList()
{
  try {
        var AddList = [];
        var tabledata = DataTables.UNRelatedproductsTable.rows('.selected').data();
        if (tabledata.length > 0)
        {
            for (var i = 0; i < tabledata.length; i++) {
                AddList.push(tabledata[i].ID);
            }
            $(".IDList").val(AddList);
        }
        
    }
    catch (e) {
        notyAlert('error', e.Message);
    }
}
function CallbtnRelatedProductSubmit()
{
    $('#btnRelatedProductSubmit').trigger('click');
}
function CallbtnDeleteRelatedProductSubmit()
{
    $('#btnDeleteRelatedProductSubmit').trigger('click');
}

function RelatedproductSaveSuccess(data, status, xhr)
{
    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result)
    {
        case "OK":
            notyAlert('success', JsonResult.Record.StatusMessage);
            var id= $(".productID").val();
            RefreshRelatedProducts(id);
            RefreshUNRelatedProducts(id);
            RelatedProductsModel();
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Record.StatusMessage);
            break;
        default:
            break;
    }

}

function ConstructRelatdProductIDListForDelete()
{
   try {
        var AddList = [];
        var tabledata = DataTables.RelatedproductsTable.rows('.selected').data();
        if (tabledata.length > 0) {
            for (var i = 0; i < tabledata.length; i++) {
                AddList.push(tabledata[i].ID);
            }
            $("#relatedproductlistID").val(AddList);
        }
    }
    catch (e) {
        notyAlert('error', e.Message);
    }
}

function RelatedproductDeleteSuccess(data, status, xhr)
{
    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
            notyAlert('success', JsonResult.Record.StatusMessage);
            var id = $(".productID").val();
            RefreshRelatedProducts(id);
            RefreshUNRelatedProducts(id);
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Record.StatusMessage);
            break;
        default:
            break;
    }
}

function HideProductDetalsToolBox()
{
    $('#btnPatchProductDetails').hide();
    BindImages();
}
function ShowProductDetalsToolBox()
{
    // $('#btnPatchProductDetails').show();
    $("#btnPatchProductDetails").css('visibility', 'visible');
}
function RenderContentsForAttributes()
{
   
    HideProductDetalsToolBox();
    try {
       
        var atsetID = $("#AttributeSetID").val();
        var proid = $('.productID').val();
        if ((atsetID) && (proid)) {
            if ((atsetID>0) && (proid>0))
            {
                var Isconfig = false;
                var pview = RenderPartialTemplateForAttributes(atsetID, Isconfig);
                //clear otherattributes div
                $("#dynamicOtherAttributes").empty();
                //append dynamic html to div from partialview
                $("#dynamicOtherAttributes").html(pview);
                //date picker reloading
                $('input[type="date"]').datepicker({
                    format: "yyyy-mm-dd",//dd-M-yyyy",
                    maxViewMode: 0,
                    todayBtn: "linked",
                    clearBtn: true,
                    autoclose: true,
                    todayHighlight: true
                });
            }
            else {
                //clear otherattributes div
                $("#dynamicOtherAttributes").empty();
                //append dynamic html to div from partialview
                $("#dynamicOtherAttributes").html('<div class="col-sm-6 col-md-6"><div class="alert-message alert-message-warning"> <p>Please Create a product from general section and come back:).</p></div></div>');

            }
           
        }
        else {
            //clear otherattributes div
            $("#dynamicOtherAttributes").empty();
            //append dynamic html to div from partialview
            $("#dynamicOtherAttributes").html('<div class="col-sm-6 col-md-6"><div class="alert-message alert-message-warning"> <p>Please Create a product from general section and come back:).</p></div></div>');

        }
        
    }
    catch (e) {
        notyAlert('error', e.Message);
    }

}
function RenderPartialTemplateForAttributes(atsetID, Isconfig) {
    var data = { setID: atsetID, Isconfigurable: Isconfig };
    var ds = {};
    ds = GetDataFromServer("Attributes/EditTemplateForAttributes/", data);
    if (ds != '') {
        return ds;
    }
}

function OtherAttributeSave()
{
    try
    {   //Serialize dynamic other attribute elements
        var otherAttrValues = $('#dynamicOtherAttributes').find('select,input').serializeArray();
        var prodid=$('.productID').val();
        if ((otherAttrValues)&&(prodid>0))
        {
            var ProductAttributesList = [];
            var ProductViewModel = new Object();
            ProductViewModel.ID = prodid;
         
            for (var at = 0; at < otherAttrValues.length; at++)
            {
                var AttributeValuesViewModel = new Object();
                AttributeValuesViewModel.Name = otherAttrValues[at].name;
                AttributeValuesViewModel.Value = otherAttrValues[at].value;
                ProductAttributesList.push(AttributeValuesViewModel);
            }
            ProductViewModel.ProductOtherAttributes = ProductAttributesList;
            var data = "{'productObj':" + JSON.stringify(ProductViewModel) + "}";
            PostDataToServer('Products/UpdateProductHeaderOtherAttributes/', data, function (JsonResult)
            {
              if (JsonResult != '') {
                   switch (JsonResult.Result)
                    {
                        case "OK":
                            notyAlert('success', JsonResult.Record.StatusMessage);
                            break;
                        case "ERROR":
                            notyAlert('error', JsonResult.Record.StatusMessage);
                            break;
                        default:
                            break;
                    }
                }
            })
            

        }

    }
    catch(e)
    {
        notyAlert('error', e.Message);
    }
}

function RenderContentsForAssocProdAttributes()
{
    HideProductDetalsToolBox();
    try {
        var proid = $('.productID').val();
        var atsetID = $("#AttributeSetID").val();
        if ((atsetID) && (proid)) {
            if ((atsetID > 0) && (proid > 0))
            {
                var Isconfig = true;
                var pview = RenderPartialTemplateForAttributes(atsetID, Isconfig);
                //clear otherattributes div
                $("#dynamicAssociatedProducts").empty();
                //append dynamic html to div from partialview
                $("#dynamicAssociatedProducts").html(pview);
                //date picker reloading
                $('input[type="date"]').datepicker({
                    format: "yyyy-mm-dd",//dd-M-yyyy",
                    maxViewMode: 0,
                    todayBtn: "linked",
                    clearBtn: true,
                    autoclose: true,
                    todayHighlight: true
                });
                $("#DivtblAssociatedProducts").show();
                //Refresh associated products table
                RefreshAssociatedProducts(proid);
            }
            else
            {
                $("#DivtblAssociatedProducts").hide();
                //clear otherattributes div
                $("#dynamicAssociatedProducts").empty();
                //append dynamic html to div from partialview
                $("#dynamicAssociatedProducts").html('<div class="col-sm-6 col-md-6"><div class="alert-message alert-message-warning"> <p>Please Create a product from general section and come back:).</p></div></div>');
           }
        }
        else {
            $("#DivtblAssociatedProducts").hide();
            //clear otherattributes div
            $("#dynamicAssociatedProducts").empty();
            //append dynamic html to div from partialview
            $("#dynamicAssociatedProducts").html('<div class="col-sm-6 col-md-6"><div class="alert-message alert-message-warning"> <p>Please Create a product from general section and come back:).</p></div></div>');
        }
    }
    catch (e) {
        notyAlert('error', e.Message);
    }
}

function RenderContentForPrice()
{
    try {
        ShowProductDetalsToolBox();
        var prodtype = $("#ProductType").val();
        switch (prodtype) {
            case "C":
                //Hide General detail entries
                $(".productDetailGroup").hide();
                $(".divMsgconfigurable").show();

                break;
            case "S":
                $(".productDetailGroup").show();
                $(".divMsgconfigurable").hide();
                break;
            default:
                break;

        }
    }
    catch (e) {
    }
}

function RenderContentForInventory()
{
    try {
        ShowProductDetalsToolBox();
        var prodtype = $("#ProductType").val();
        switch (prodtype) {
            case "C":
                //Hide General detail entries
                $(".productDetailGroup").hide();
                $(".divMsgconfigurable").show();

                break;
            case "S":
                $(".productDetailGroup").show();
                $(".divMsgconfigurable").hide();
                break;
            default:
                break;

        }
    }
    catch (e) {
    }
}

function ProductTypeOnChange()
{
    
}

function AssociatedProductSave()
{
    try {   //Serialize dynamic other attribute elements
        //var Associatedpro = $('#dynamicAssociatedProducts').find('select,input').serializeArray();
        var Associatedpro = $('#dynamicAssociatedProductContents').find('select,input').serializeArray();
        var prodid = $('.productID').val();
        if ((Associatedpro) && (prodid > 0)) {
          
            
            var ProductViewModel = new Object();
            var ProductDetailViewModel = new Object();
            var ProductAttributesList = [];
            var ProductDetailList = [];
            ProductViewModel.ID = prodid;

            for (var at = 0; at < Associatedpro.length; at++) {
                var AttributeValuesViewModel = new Object();
                AttributeValuesViewModel.Name = Associatedpro[at].name;
                AttributeValuesViewModel.Value = ((Associatedpro[at].value != "" && Associatedpro[at].value!=-1)?Associatedpro[at].value:"");
                ProductAttributesList.push(AttributeValuesViewModel);
            }
            var prodetid=$("#productDetailID").val();
            ProductDetailViewModel.ID = (prodetid != "" ? prodetid : "");
            ProductDetailViewModel.ProductAttributes = ProductAttributesList;
            var detqty=$("#detailQty").val();
            ProductDetailViewModel.Qty = (detqty != "" ? detqty : "");
            var outstockqty = $("#detailOutOfStockAlertQty").val();
            ProductDetailViewModel.OutOfStockAlertQty = (outstockqty != "" ? outstockqty : "");
            var stockavail = $("#detailStockAvailable").val();
            ProductDetailViewModel.StockAvailable = (stockavail != "" ? stockavail : "");
            var detailDiscAmount = $("#detailDiscountAmount").val();
            ProductDetailViewModel.detailDiscountAmount = (detailDiscAmount != "" ? detailDiscAmount : "");
            var detailPriceDiff = $("#detailPriceDifference").val();
            ProductDetailViewModel.PriceDifference = (detailPriceDiff != "" ? detailPriceDiff : "");
            var detailDiscStart = $("#detailDiscountStartDate").val();
            ProductDetailViewModel.DiscountStartDate = (detailDiscStart != "" ? detailDiscStart : "");
            var detailDiscEnd = $("#detailDiscountEndDate").val();
            ProductDetailViewModel.DiscountEndDate = (detailDiscEnd != "" ? detailDiscEnd : "");

            ProductDetailList.push(ProductDetailViewModel);
            ProductViewModel.ProductDetails = ProductDetailList;

            var data = "{'productObj':" + JSON.stringify(ProductViewModel) + "}";
            PostDataToServer('Products/InsertUpdateProductDetails/', data, function (JsonResult) {
                if (JsonResult != '') {
                    switch (JsonResult.Result) {
                        case "OK":
                            notyAlert('success', JsonResult.Record.StatusMessage);
                            $("#productDetailID").val(JsonResult.Record.ReturnValues);
                            RefreshAssociatedProducts(prodid);
                            break;
                        case "ERROR":
                            notyAlert('error', JsonResult.Record.StatusMessage);
                            break;
                        default:
                            break;
                    }
                }
            })


        }

    }
    catch (e) {
        notyAlert('error', e.Message);
    }
}

function RefreshAssociatedProducts(id) {
 
    try {
        DataTables.AssociatedProductsTable.clear().rows.add(GetProductDetailsByProductId(id)).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function GetProductDetailsByProductId(id)
{
 
    try {
        var data = { "id": id };
        var ds = {};
        ds = GetDataFromServer("Products/GetProductDetailByProduct/", data);
       
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

function AssociatedProductDelete()
{
  
    var prodetid = $("#productDetailID").val();
    var prodid = $('.productID').val();
    if ((prodetid) && (prodid))
    {
        if (confirm("Are you Sure!") == true) {
            var ProductDetailViewModel = new Object();
            ProductDetailViewModel.ID = prodetid;
            ProductDetailViewModel.ProductID = prodid;
            var data = "{'productDeails':" + JSON.stringify(ProductDetailViewModel) + "}";
            PostDataToServer('Products/DeleteProductDetail/', data, function (JsonResult) {
                if (JsonResult != '') {
                    switch (JsonResult.Result) {
                        case "OK":
                 
                            notyAlert('success', JsonResult.Record.StatusMessage);
                            RefreshAssociatedProducts(prodid);
                            clearAssociatedProductform();
                            break;
                        case "ERROR":
                            notyAlert('error', JsonResult.Record.StatusMessage);
                            break;
                        default:
                            break;
                    }
                }
            })
        }
        

    }
    else {
        
    }
 
    
}
function EditAssocProduct(currentObj) {
    var rowData = DataTables.AssociatedProductsTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        var thisproduct = GetProductDetailsByProductDetailID(rowData.ProductID,rowData.ID );
   
        if (thisproduct) {
            $("#productDetailID").val(thisproduct.ID);
            $("#detailQty").val(thisproduct.Qty);
            $("#detailOutOfStockAlertQty").val(thisproduct.OutOfStockAlertQty);
            if (thisproduct.StockAvailable == true)
            { $("#detailStockAvailable").prop('checked', true); }
            else { $("#detailStockAvailable").prop('checked', false); }
            $("#detailDiscountAmount").val(thisproduct.DiscountAmount);
            $("#detailPriceDifference").val(thisproduct.PriceDifference);
            $("#detailDiscountStartDate").val(thisproduct.DiscountStartDate);
            $("#detailDiscountEndDate").val(thisproduct.DiscountEndDate);
            if (thisproduct.Enabled == true)
            { $("#detailEnable").prop('checked', true); }
            else { $("#detailEnable").prop('checked', false); }
            if (thisproduct.DefaultOption == true)
            { $("#detailDefaultOption").prop('checked', true); }
            else { $("#detailDefaultOption").prop('checked', false); }
            $("#detailDetailTags").val(thisproduct.DetailTags);

            if(thisproduct.ProductAttributes)
            {
                for (var jk = 0; jk < thisproduct.ProductAttributes.length; jk++)
                {
                    $("#" + thisproduct.ProductAttributes[jk].Name).val(thisproduct.ProductAttributes[jk].Value);
                }
               
            }
          

            //$("#DiscountAmount").val((thisproduct.ProductDetails.length != 0 ? thisproduct.ProductDetails[0].DiscountAmount : 0.00));
            //$("#DiscountStartDate").val((thisproduct.ProductDetails.length != 0 ? thisproduct.ProductDetails[0].DiscountStartDate : ""));
            //$("#DiscountEndDate").val((thisproduct.ProductDetails.length != 0 ? thisproduct.ProductDetails[0].DiscountEndDate : ""));
            //$("#ShortDescription").val(thisproduct.ShortDescription);
            //$("#LongDescription").val(thisproduct.LongDescription);
            //if (thisproduct.StockAvailable == true)
            //{ $("#StockAvailable").prop('checked', true); }
            //else { $("#StockAvailable").prop('checked', false); }

            //$("#Qty").val((thisproduct.ProductDetails.length != 0 ? thisproduct.ProductDetails[0].Qty : ""));
            //$("#OutOfStockAlertQty").val((thisproduct.ProductDetails.length != 0 ? thisproduct.ProductDetails[0].OutOfStockAlertQty : ""));
            ////Tags
            //if (thisproduct.HeaderTags != null) {
            //    $('.Htags').remove();
            //    var tagar = thisproduct.HeaderTags.split(",");
            //    for (index = 0; index < tagar.length; ++index) {
            //        //Tag creation when binding
            //        $("#headertagsdiv").append($("<span/>", { text: tagar[index] }).attr({ 'class': 'Htags', 'onclick': 'removeme(this)' }));
            //    }
            //}
            //else {
            //    //Removes span tags
            //    $('.Htags').remove();
            //}


            ////ProductID
            //$(".productID").val(thisproduct.ID);
            ////ProductDetailID
            //$("#productdetailsID").val((thisproduct.ProductDetails.length != 0 ? thisproduct.ProductDetails[0].ID : 0));
        }
    }
}

function GetProductDetailsByProductDetailID(id,detailid)
{
    try {
        var data = {"productID": id,"productDetailID":detailid};
        var ds = {};
        ds = GetDataFromServer("Products/GetProductDetailsByProductDetailID/", data);
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
    catch (e) {
        notyAlert('error', e.message);
    }

}

function clearAssociatedProductform() {
    //Clear form
    //drop down clear
    $('#dynamicAssociatedProductContents').find('select,input').val(-1);
    //normal text box clear
    $('#dynamicAssociatedProductContents').find('input').val('');
    $("#productDetailID").val(0);
    $('#associatedStaticfields').find('input').val('');
    $('#associatedStaticfields').find('.check-box').prop('checked', false);
 
}

//-----------------------------------------------------Products Review-------------------------------------------------//

function BindProductReviews()   // To Display Previous Comment history
{
    HideProductDetalsToolBox();
  
    $("#ReviewsDisplay").empty();
    id =   $(".productID").val();// assigning id for binding reviews.
    var thisReviewList = GetProductReviews(id);
    if (thisReviewList) {
       
        for (var i = 0; i < thisReviewList.length; i++) {
            var str = Date.parse(thisReviewList[i].ReviewCreatedDate.substring(0, 10));
            var resultdate = ConvertJsonToDate('' + str + '');
            var imageurl;
            if (thisReviewList[i].ImageUrl)
                imageurl = thisReviewList[i].ImageUrl
                else
                imageurl='Content/images/NoImage60x60.png';

            var cnt = $('<div class="review-block"><div class="row">' +
                        '<div class="col-sm-3">' +
                        '<img src="'+imageurl+'" class="img-rounded">' +
                        '<div class="review-block-name"><a href="#">' + thisReviewList[i].CustomerName + '</a></div>' +
                        '<div class="review-block-date">' + resultdate + '<br />' + thisReviewList[i].DaysCount + ' days ago</div>' +
                        '</div>' +
                        '<div class="col-sm-9">' +
                        '<div id="ReviewBlockRate' + [i] + '" class="review-block-rate"></div>' +
                        '<div id=ReviewDesc' + i + 'class="review-block-description">' + thisReviewList[i].Review + '</div>'+
                        '</div><hr/></div>');
            $("#ReviewsDisplay").append(cnt);

//--------------------------------------------Rating Star dispalying region-----------------------------------------------//
            var rating = thisReviewList[i].AvgRating;
            var splitresult = rating.split(".");
            
                rating = Math.round(rating);
            var ratebtns = '';
            for (var count = 0; count < 5; count++) {
                if (count < rating) {
                    ratebtns = ratebtns + '<button type="button" class="btn btn-warning btn-xs" aria-label="Left Align">' +
                                          '<span class="glyphicon glyphicon-star" aria-hidden="true"></span></button>'
                }
                else {
                    ratebtns = ratebtns + '<button type="button" class="btn btn-default btn-xs" aria-label="Left Align">' +
                                          '<span class="glyphicon glyphicon-star" aria-hidden="true"></span></button>'
                }
            }
            ratebtns = $(ratebtns);
            $("#ReviewBlockRate" + [i]).append(ratebtns);
//----------------------------------------------------------------------------------------------------------------------//

        }
    }
}
function GetProductReviews(id) {
  
    try {
        var data = { "id": id };
        var ds = {};
        ds = GetDataFromServer("Products/GetProductReviews/", data);

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