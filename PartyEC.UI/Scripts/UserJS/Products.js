var DataTables = {};
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
            $("#HeaderTagsPicker").focus();
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
    $("#detailTagsPicker").on({
        focusout: function () {
            var txt = this.value.replace(/[^a-z0-9\+\-\.\#]/ig, '');
            if (txt) {
                var h = $("<span/>", { text: txt }).attr({ 'class': 'label label-primary Htags', 'onclick': 'removeme(this)' });
                $('#detailkeywordsDiv').append(h);
                this.value = "";
               // $("#lblDetailTags").trigger('click');
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
    
   
   try {
       
        DataTables.productTable = $('#tblproducts').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllProducts(),
             columns: [
               { "data": "AttributeSetID" },
               { "data": "ID" },
               { "data": "Name" },
               { "data": "ProductType", "defaultContent": "<i>-</i>" },
               { "data": "Enabled", "defaultContent": "<i>-</i>" },
               { "data": "SupplierName", "defaultContent": "<i>-</i>" },
               { "data": "SKU", "defaultContent": "<i>-</i>" },
               { "data": "BaseSellingPrice", "defaultContent": "<i>-</i>" },
               { "data": "TotalQty", "defaultContent": "<i>-</i>" },
               { "data": "StockAvailable", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="ModelProductsRating(this)">Rating</a>' },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="Edit(this)"><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [
              {//hiding hidden column 
                  "targets": [0],
                  "visible": false,
                  "searchable": false
              },
                {
                    "render": function (data, type, row) {
                        return (data == true ? "Yes" : "No");
                    },
                    "targets": 4
                },
               {
                   "render": function (data, type, row) {
                       return (data == "C" ? "Configurable" : "Simple");
                   },
                   "targets": 3
               },
               {
                   "render": function (data, type, row) {
                       return (data == true ? "In Stock" : "Empty");
                   },
                   "targets": 9
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
                 selector: 'tr'
             },
             order: [[1, 'asc']]

         });
       
        $('#tblRelatedproducts tbody').on('click', 'tr', function () {

            $(this).closest('tr').toggleClass('selected');
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
               { "data": null, "defaultContent": '' },
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
              //{//hiding hidden column 
              //    "targets": [0],
              //    "visible": true,
              //    "searchable": false,
              //    "render": function (data, type, full, meta) {
              //        if (type === 'display') {
              //            data = '<input type="checkbox" class="dt-checkboxes">';
              //        }
              //        return data;
              //    }
              //}
              {
                  orderable: false,
                  className: 'select-checkbox',
                  targets: 0
              }],
             select: {
                 style: 'multi',
                 selector: 'tr'
             },
             order: [[1, 'asc']]
         });

        //$('#tblUNRelatedproducts').on('change', 'input[type="checkbox"]', function () {
        //    $(this).closest('tr').toggleClass('selected');
        //});
        $('#tblUNRelatedproducts tbody').on('click', 'tr', function () {

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
               { "data": "ActualPrice", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="EditAssocProduct(this)"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></a><span> | </span><a href="#" onclick="AssociatedProductDelete(this)"><i class="fa fa-trash-o" aria-hidden="true"></i></a>' }
          
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
    //Validation for associatedproducts
   
    
    ChangeButtonPatchView("Products", "ProductToolBox", "Add"); //ControllerName,id of the container div,Name of the action
    $("#tabproductList").click(function () {
        ChangeButtonPatchView("Products", "ProductToolBox", "Add"); //ControllerName,id of the container div,Name of the action
    });
    $("#tabproductDetails").click(function () {
        $("#titleSpanPro").text('New Product');
        $("#AttributeSetID").removeAttr('disabled');
        $("#ProductType").removeAttr('disabled');
        $('#tabsettings').trigger('click');
        clearform();
        ChangeButtonPatchView("Products", "ProductToolBox", "Save"); //ControllerName,id of the container div,Name of the action
        DisEnableSectionAdditional();
    });
    
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
   // $('#tabproductDetails a').attr({ 'data-toggle': 'tab', 'href': '#productDetails' });
   // $('#tabproductList a').removeAttr('data-toggle href');
    $('#tabproductDetails a').trigger('click');
   
    $("#titleSpanPro").text('New Product');
   // $("#productDetails h4").text('New Product');
    $("#AttributeSetID").removeAttr('disabled');
    $("#ProductType").removeAttr('disabled');
    $('#tabsettings').trigger('click');
    clearform();
    ChangeButtonPatchView("Products", "ProductToolBox", "Save"); //ControllerName,id of the container div,Name of the action
    DisEnableSectionAdditional();
}
function goback() {
   
   // $('#tabproductList a').attr({ 'data-toggle': 'tab', 'href': '#productList' });
    //Remove attributes from current tab
    
  //  $('#tabproductDetails a').removeAttr('data-toggle href');
    $('#tabproductList a').trigger('click');
    // tabproductList
    clearform();
    ChangeButtonPatchView("Products", "ProductToolBox", "Add"); //ControllerName,id of the container div,Name of the action
}

function RenderContentForImages()
{
    // HideProductDetalsToolBox();
    ChangeButtonPatchView("Products", "ProductToolBox", "Back");
    BindImages();
    BindStickers();
}
function Edit(currentObj)
{
    //Tab Change
    //ChangeButtonPatchView("Products", "btnPatchProductDetails", "Edit"); //ControllerName,id of the container div,Name of the action
   // $('#tabproductDetails a').attr({ 'data-toggle': 'tab', 'href': '#productDetails' });
   // $('#tabproductList a').removeAttr('data-toggle href');
    $('#tabproductDetails a').trigger('click');
    //$('#tabproductDetails').removeClass('disabled');
   // $('#tabproductDetails a').attr('data-toggle', 'tab');
    //$('#tabproductDetails a').trigger('click');

    //Make General tab active
    $('#tabsettings').trigger('click');
    //$("#LHSNavbarProductDetails li").removeClass('active');
   // $("#LHSNavbarProductDetails li.active").removeClass('active');
   // $("#LHSNavbarProductDetails li").first().addClass('active');
  

    var rowData = DataTables.productTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null))
    {
        var thisproduct = GetProduct(rowData.ID);
        if (thisproduct != null)
        {
            $("#titleSpanPro").text(thisproduct.Name);
            $("#spandetailType").text((thisproduct.ProductType == "S" ? 'Simple' : 'Configurable'));
            $("#spandetailSet").text(((thisproduct.AttributeSetName != null) && (thisproduct.AttributeSetName != "") ? thisproduct.AttributeSetName : 'N/A') + '');
            //$("#titleSpanPro").text(thisproduct.Name + '-(Type:' + (thisproduct.ProductType == "S" ? 'Simple' : 'Configurable') + ',Attribute set:' + ((thisproduct.AttributeSetName != "") && (thisproduct.AttributeSetName != null)?thisproduct.AttributeSetName:'N/A') + ')');
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
            $("#ActionType").val(thisproduct.ActionType);
            $("#SupplierID").val(thisproduct.SupplierID);
            $("#ManufacturerID").val(thisproduct.ManufacturerID);
            $("#ProductType").val(thisproduct.ProductType);
            $("#ProductTypehdf").val(thisproduct.ProductType);
            $("#AttributeSetID").val(thisproduct.AttributeSetID);
            $("#AttributeSetIDhdf").val(thisproduct.AttributeSetID);
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
            if (thisproduct.HeaderTags)
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
            //RefreshRelatedProducts(thisproduct.ID);
            RefreshUNRelatedProducts(thisproduct.ID);
            EnableSectionAdditional();
        }
       
    }

   
}
function EnableSectionAdditional(){
    $('#additioanlLi').show(100);
    $('#additioanlRelatedLi').show(200);
    $('#additioanlImageLi').show(300);
    $('#additioanlAttributesLi').show(400);
    $('#additioanlProdAttributeLi').show(500);
    $('#additioanlReviewsLi').show(600);

}
function DisEnableSectionAdditional() {
    $('#additioanlLi').hide(600);
    $('#additioanlRelatedLi').hide(500);
    $('#additioanlImageLi').hide(400);
    $('#additioanlAttributesLi').hide(300);
    $('#additioanlProdAttributeLi').hide(200);
    $('#additioanlReviewsLi').hide(100);

}
function BindStickers()
{
    try
    {
        
        var thisproduct = GetProduct($('#ID').val());
        var Table = GetAllStickers()
        var ActiveSticker = 0;
        var AvailableSticker = 0;
        $('#ulStickerArea').empty();
        $('#ulMainStickerArea').empty();
        for (var i = 0; i < Table.length; i++) {
            if (thisproduct.StickerID == Table[i].ID)
            {
                ActiveSticker = 1;
                $('#ulMainStickerArea').append(' <li class="col-sm-4"><a class="thumbnail" onclick="SelectForDeleteSticker(this)" id="' + Table[i].ID + '"><img style="width: 100px;height: 100px;object-fit: cover;" src="' + Table[i].URL + '?' + new Date().getTime() + '">'
                    + '<a style="top: 2%;left: 14%;position: absolute;background: white;" class="fa fa-search-plus" href="' + Table[i].URL + '?' + new Date().getTime() + '" data-lightbox="roadtrip"/></a>'
                    + '<a onclick="UpdateStickerForProduct();" style="top: 2%;left: 22%;position: absolute;background: white;padding-left:1%;padding-right:1%;" class="fa fa-trash"/></a></li>');
            }
            else
            {
                AvailableSticker = 1;
                $('#ulStickerArea').append(' <li class="col-sm-2"><a class="thumbnail" onclick="SelectForAddSticker(this)" id="' + Table[i].ID + '"><img style="width: 100px;height: 100px;object-fit: cover;" src="' + Table[i].URL + '?' + new Date().getTime() + '">'
                    + '<a style="top: 2%;left: 14%;position: absolute;background: white;" class="fa fa-search-plus" href="' + Table[i].URL + '?' + new Date().getTime() + '" data-lightbox="roadtrip"/></a></li>');
            }
        }
        if(ActiveSticker==0)
        {
            ChangeButtonPatchView("Products", "buttonPatchStickerImages", "NoSticker");
            $('#ulMainStickerArea').append('<li class="col-sm-4"><a class="thumbnail" id="carousel-selector-0"><img src="http://placehold.it/150x150&text=zero"></a></li>');
        }
        if (AvailableSticker == 0) {
            $('#ulStickerArea').append('<li class="col-sm-4"><a class="thumbnail" id="carousel-selector-0"><img src="http://placehold.it/150x150&text=zero"></a></li>');
        }
    }
    catch(e)
    {

    }
}
function BindImages() {
    try
    {
        $('#ImageID').val(0);
        var Table = GetRelatedImages($('#ID').val());
        var MainFlag = 0;
        var OtherFlag = 0;
        $('#ulOtherImages').empty();
        for (var i = 0; i < Table.length; i++) {
            if (Table[i].MainImage) {
                MainFlag = 1;
                $('#imgProduct').attr('src', (Table[i].ImageURL != "" && Table[i].ImageURL != null ? Table[i].ImageURL + '?' + new Date().getTime() : "/Content/images/NoImageFound.png"));
                $('#ImageID').val(Table[i].ImageID);
                $('#divMainImagePro').append('<a style="top: 2%;left: 7%;position: absolute;background: white;" class="fa fa-search-plus" href="' + Table[i].ImageURL + '?' + new Date().getTime() + '" data-lightbox="roadtrip"/></a>'
                    + '<a onclick="DeleteOtherImage(' + Table[i].ImageID + ')" style="top: 2%;left: 9.2%;position: absolute;background: white;padding-left:1%;padding-right:1%;" class="fa fa-trash"/></a></li>')
            }
            else {
                OtherFlag = 1;
                //onclick="SelectImagesForDelete(this)"
                $('#ulOtherImages').append(' <li class="col-sm-4"><a class="thumbnail" id="' + Table[i].ImageID + '"><img style="width: 100px;height: 100px;object-fit: cover;" src="' + Table[i].ImageURL + '?' + new Date().getTime() + '">'
                    + '<a style="top: 2%;left: 14%;position: absolute;background: white;" class="fa fa-search-plus" href="' + Table[i].ImageURL + '?' + new Date().getTime() + '" data-lightbox="roadtrip"/></a>'
                    + '<a onclick="DeleteOtherImage('+ Table[i].ImageID +')" style="top: 2%;left: 22%;position: absolute;background: white;padding-left:1%;padding-right:1%;" class="fa fa-trash"/></a></li>')
            }
        }
        if (Table.length == 0 && MainFlag == 0) {
            $('#imgProduct').attr('src', '/Content/images/NoImageFound.png');
            $('#divMainImagePro a').remove();
        }
        if(OtherFlag==0)
        {
            $('#ulOtherImages').append('<li class="col-sm-4"><a class="thumbnail" id="carousel-selector-0"><img src="http://placehold.it/150x150&text=zero"></a></li>');
        }
    }
    catch(e)
    {

    }
   
}
function DeleteOtherImage(id)
{
    try
    {
        debugger;
        var DeletedImageID = [];
        //var objectset = $('#ulOtherImages a.Selected');
        //for (var i = 0; i < objectset.length; i++) {
        //    DeletedImageID.push(objectset[i].id);
        //}
        DeletedImageID.push(id);
        var ProductViewModel = new Object();
        ProductViewModel.IDSet = DeletedImageID;
        var data = "{'productViewObj':" + JSON.stringify(ProductViewModel) + "}";
        PostDataToServer('Products/DeleteProductOtherImages/', data, function (JsonResult) {
            if (JsonResult != '') {
                switch (JsonResult.Result) {
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
        });
    }
    catch(e)
    {

    }
   
    }
function SelectImagesForDelete(this_Obj)
{
    try
    {
        
        $(this_Obj).toggleClass('Selected');
        if ($('#ulOtherImages a.Selected').length > 0) {
            ChangeButtonPatchView("Products", "buttonPatchOtherImages", "Delete");
        }
        else {
            ChangeButtonPatchView("Products", "buttonPatchOtherImages", "CancelDelete");
        }
    }
    catch(e)
    {

    }
    
}
function SelectForAddSticker(this_Obj)
{
    
    try {
        
       
        if ($('#ulStickerArea span.highlighted').length == 0) {
            $(this_Obj).append('<span class="highlighted">&#10004;</span>');
            $('#Savesticker').attr('onclick', 'UpdateStickerForProduct()');
            //ChangeButtonPatchView("Products", "buttonPatchStickerImages", "Sticker");
        }
        else
        {
            $('span.highlighted').remove();
            $('#Savesticker').removeAttr('onclick');
            //ChangeButtonPatchView("Products", "buttonPatchStickerImages", "CancelSticker");
        }
    }
    catch (e) {

    }

}
function UpdateStickerForProduct()
{
    
    var objectset = $('#ulStickerArea span.highlighted');
    var ProductViewModel = new Object();
    if (objectset.length != 0)
    {
        ProductViewModel.StickerID = objectset[0].parentNode.id;
    }
    ProductViewModel.ID = $('#ID').val();
    var data = "{'productViewObj':" + JSON.stringify(ProductViewModel) + "}";
    PostDataToServer('Products/UpdateProductSticker/', data, function (JsonResult) {
        if (JsonResult != '') {
            
            switch (JsonResult.Result) {
                case "OK":
                    notyAlert('success', JsonResult.Record.StatusMessage);
                    ChangeButtonPatchView("Products", "buttonPatchStickerImages", "CancelSticker");
                    BindStickers();
                    break;
                case "ERROR":
                    notyAlert('error', JsonResult.Record.StatusMessage);
                    break;
                default:
                    notyAlert('error', JsonResult.Message);
                    break;
            }
        }
    });
}
function GetRelatedImages(id)
{
    try {
        
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
function GetAllStickers()
{
    try {
        
        var data = "";
        var ds = {};
        ds = GetDataFromServer("Products/GetAllStickers/", data);
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
            $(".productID").val(JsonResult.Record.ReturnValues.productid);
            RefreshProducts();
            // $("#titleSpanPro").text($("#Name").val() + '-(Type:' + ($("#ProductTypehdf").val() == "S" ? 'Simple' : 'Configurable') + ',Attribute set:' + ((JsonResult.Record.ReturnValues.attributesetname != null) && (JsonResult.Record.ReturnValues.attributesetname!="")?JsonResult.Record.ReturnValues.attributesetname:'N/A') + ')');
            $("#titleSpanPro").text($("#Name").val());
            $("#spandetail").text('(Type:' + ($("#ProductTypehdf").val() == "S" ? 'Simple' : 'Configurable') + ',Attribute set:' + ((JsonResult.Record.ReturnValues.attributesetname != null) && (JsonResult.Record.ReturnValues.attributesetname != "") ? JsonResult.Record.ReturnValues.attributesetname : 'N/A') + ')');
            RefreshUNRelatedProducts(JsonResult.Record.ReturnValues.productid);
            EnableSectionAdditional();
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Record.StatusMessage);
            break;
        default:
            notyAlert('error', JsonResult.Message);
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

    //Bubble logic should be here

    if($('.field-validation-error').length>0)
    {
        notyAlert('error', 'Need to fill all mandatory fields to save the product info!');
    }
    //$('.field-validation-error').each(function () {

    //    var ab = $(this);

    //}); 

}

function clearform()
{
    //Clear form
   
    //Clear Hidden form fields
    //$("#productform input:hidden").val('').trigger('change');
    var validator = $("#productform").validate();
    $('#productform').find('.field-validation-error span').each(function () {
        validator.settings.success($(this));
    });
    $('#productform')[0].reset();
    $(".productID").val(0);
    $("#productdetailsID").val(0);
    $("#productDetailhdf").val('');
    $("#ProductTypehdf").val('');
    $("#AttributeSetIDhdf").val('');
    //tags removal
    $('.Htags').remove();
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
            return true;
        }
        else
        {
            //not selected table items
            notyAlert('error', 'Please Select products to delete!');
            return false;
        }
    }
    catch (e) {
        notyAlert('error', e.Message);
        return false;
    }
}

function ValidateTableSelection()
{
    try
    {

        var tabledata = DataTables.RelatedproductsTable.rows('.selected').data();
        if(tabledata.length > 0)
        {
            return true;
        }
        return false;

    }
    catch(e)
    {
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

function RenderContentForRelatedProducts()
{
    try
    {
        var proid = $('.productID').val();
        if ((proid) && (proid>0))
        {
            $("#divtablerelatedproducts").show();
            RefreshRelatedProducts(proid);
         
            $(".divMsgconfigurable").hide();
            ChangeButtonPatchView("Products", "ProductToolBox", "RPAdd"); //ControllerName,id of the container div,Name of the action
        }
        else
        {
            $("#divtablerelatedproducts").hide();
            $(".divMsgconfigurable").show();
            ChangeButtonPatchView("Products", "ProductToolBox", "Back"); //ControllerName,id of the container div,Name of the action
        }
    
    }
    catch(e)
    {
        notyAlert('error', e.Message);
    }
   

   
   
}
function ShowProductDetalsToolBox()
{
    ChangeButtonPatchView("Products", "ProductToolBox", "Save"); //ControllerName,id of the container div,Name of the action
  
}
function RenderContentsForAttributes()
{
   
 
    try {
       
        var atsetID = $("#AttributeSetIDhdf").val();
        var proid = $('.productID').val();
        if ((atsetID) && (proid)) {
            if ((atsetID>0) && (proid>0))
            {
                var Isconfig = false;
                var pview = RenderPartialTemplateForAttributes(atsetID, Isconfig);
                if (pview.trim() != "")
                {
                    //clear otherattributes div
                    $("#dynamicOtherAttributes").empty();
                    //append dynamic html to div from partialview
                    $("#dynamicOtherAttributes").html(pview);
                    //date picker reloading
                    $('input[type="date"]').datepicker({
                        format: "dd-M-yy",
                        monthNamesShort: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                        maxViewMode: 0,
                        todayBtn: "linked",
                        clearBtn: true,
                        autoclose: true,
                        todayHighlight: true
                    });
                    ChangeButtonPatchView("Products", "ProductToolBox", "OASave"); //ControllerName,id of the container div,Name of the action
                }
                else
                {
                    //clear otherattributes div
                    $("#dynamicOtherAttributes").empty();
                    //append dynamic html to div from partialview
                    $("#dynamicOtherAttributes").html('<div class="col-sm-6 col-md-6"><div class="alert-message alert-message-warning"> <p>No Attributes Available for this product.</p></div></div>');
                    ChangeButtonPatchView("Products", "ProductToolBox", "Back"); //ControllerName,id of the container div,Name of the action
                }
 
            }
            else {
                //clear otherattributes div
                $("#dynamicOtherAttributes").empty();
                //append dynamic html to div from partialview
                $("#dynamicOtherAttributes").html('<div class="col-sm-6 col-md-6"><div class="alert-message alert-message-warning"> <p>Please Create a product from general section and come back.</p></div></div>');

            }
           
        }
        else {
            //clear otherattributes div
            $("#dynamicOtherAttributes").empty();
            //append dynamic html to div from partialview
            $("#dynamicOtherAttributes").html('<div class="col-sm-6 col-md-6"><div class="alert-message alert-message-warning"> <p>Please Create a product from general section and come back.</p></div></div>');
        }
        
    }
    catch (e) {
        notyAlert('error', e.Message);
    }

}
function RenderPartialTemplateForAttributes(atsetID, Isconfig)
{
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
  
    try {
        var proid = $('.productID').val();
        var atsetID = $("#AttributeSetID").val();
        var protype = $("#ProductTypehdf").val();
        if (protype != 'S') {
            if ((atsetID) && (proid)) {
                if ((atsetID > 0) && (proid > 0)) {
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
                    $("#associatedStaticfields").show();
                    //Refresh associated products table
                    RefreshAssociatedProducts(proid);
                    //Button patch
                    ChangeButtonPatchView("Products", "ProductToolBox", "APAdd"); //ControllerName,id of the container div,Name of the action
                }
                else {
                    $("#associatedStaticfields").hide();
                    $("#DivtblAssociatedProducts").hide();
                    //clear otherattributes div
                    $("#dynamicAssociatedProducts").empty();
                    //append dynamic html to div from partialview
                    $("#dynamicAssociatedProducts").html('<div class="col-sm-6 col-md-6"><div class="alert-message alert-message-warning"> <p>Please Create a product from general section and come back.</p></div></div>');
                }
            }
            else {
                $("#associatedStaticfields").hide();
                $("#DivtblAssociatedProducts").hide();
                //clear otherattributes div
                $("#dynamicAssociatedProducts").empty();
                //append dynamic html to div from partialview
                $("#dynamicAssociatedProducts").html('<div class="col-sm-6 col-md-6"><div class="alert-message alert-message-warning"> <p>Please Create a product from general section and come back.</p></div></div>');
            }
        }
        else
        {
            $("#associatedStaticfields").hide();
            $("#DivtblAssociatedProducts").hide();
            //clear otherattributes div
            $("#dynamicAssociatedProducts").empty();
            //append dynamic html to div from partialview
            $("#dynamicAssociatedProducts").html('<div class="col-sm-6 col-md-6"><div class="alert-message alert-message-warning"> <p>Associated tab is not available for simple products.</p></div></div>');
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
        var prodtype = $("#ProductTypehdf").val();
        switch (prodtype) {
            case "C":
                //Hide General detail entries
                $(".productDetailGroup").hide();
                $(".divMsgconfigurable").show();
                $("#price .form-group").show();
                $(".divPriceMessage").hide();

                break;
            case "S":
                $(".productDetailGroup").show();
                $(".divMsgconfigurable").hide();

                $("#price .form-group").show();
                $(".divPriceMessage").hide();
                break;
            default:
                $("#price .form-group").hide();
                $(".divPriceMessage").show();
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
        var prodtype = $("#ProductTypehdf").val();
        switch (prodtype) {
            case "C":
                //Hide General detail entries
                $(".productDetailGroup").hide();
                $(".divMsgconfigurable").show();

                $("#inventory .form-group").show();
                $(".divInventoryMessage").hide();
                break;
            case "S":
                $(".productDetailGroup").show();
                $(".divMsgconfigurable").hide();
                $("#inventory .form-group").show();
                $(".divInventoryMessage").hide();
                break;
            default:
                $("#inventory .form-group").hide();
                $(".divInventoryMessage").show();
                break;

        }
    }
    catch (e) {
    }
}

function ProductTypeOnChange(curobj)
{
    try
    {
        if (curobj.value != "") {
            notyAlert('warning', 'Once selected can not be changed!');
        }
        $("#ProductTypehdf").val(curobj.value);
    }
    catch(e)
    {

    }
  
}
function attributeSetOnChange(curobj)
{
    try
    {
    $("#AttributeSetIDhdf").val(curobj.value);
    }
    catch(e)
    {

    }
}


function ValidateAssociatedProducts()
{
    $("#errorQty").hide();
    $("#errorAlertQty").hide();
    var flagval = false;
    if($("#detailQty").val()=="")
    {
        $("#errorQty").show();
        flagval = true;
    }

    if($("#detailOutOfStockAlertQty").val()=="")
    {
        $("#errorAlertQty").show();
        flagval = true;
    }
    return flagval;
}

function AssociatedProductSave()
{
   
    try {
        if (!ValidateAssociatedProducts())
        {
            //Serialize dynamic other attribute elements
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
                    AttributeValuesViewModel.Value = ((Associatedpro[at].value != "" && Associatedpro[at].value != -1) ? Associatedpro[at].value : "");
                    ProductAttributesList.push(AttributeValuesViewModel);
                }
                var prodetid = $("#productDetailID").val();
                ProductDetailViewModel.ID = (prodetid != "" ? prodetid : "");
                ProductDetailViewModel.ProductAttributes = ProductAttributesList;
                var detqty = $("#detailQty").val();
                ProductDetailViewModel.Qty = (detqty != "" ? detqty : "");
                var outstockqty = $("#detailOutOfStockAlertQty").val();
                ProductDetailViewModel.OutOfStockAlertQty = (outstockqty != "" ? outstockqty : "");
                var stockavail = $("#detailStockAvailable").val();
                ProductDetailViewModel.StockAvailable = (stockavail != "" ? stockavail : "");
                var detailDiscAmount = $("#detailDiscountAmount").val();
                ProductDetailViewModel.DiscountAmount = (detailDiscAmount != "" ? detailDiscAmount : "");
                var detailPriceDiff = $("#detailPriceDifference").val();
                ProductDetailViewModel.PriceDifference = (detailPriceDiff != "" ? detailPriceDiff : "");
                var detailDiscStart = $("#detailDiscountStartDate").val();
                ProductDetailViewModel.DiscountStartDate = (detailDiscStart != "" ? detailDiscStart : "");
                var detailDiscEnd = $("#detailDiscountEndDate").val();
                ProductDetailViewModel.DiscountEndDate = (detailDiscEnd != "" ? detailDiscEnd : "");
                var detailEnable = $("#detailEnable").val();
                ProductDetailViewModel.Enabled = detailEnable;
                var detailDefaultOption = $("#detailDefaultOption").val();
                ProductDetailViewModel.DefaultOption = detailDefaultOption;
                var tagval = [];
                $('#detailkeywordsDiv .Htags').each(function () {

                    tagval.push(this.innerHTML);
                });
                $("#detailDetailTags").val(tagval);

                var detailnewtags = $("#detailDetailTags").val();
                ProductDetailViewModel.DetailTags = detailnewtags;
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

function AssociatedProductDelete(this_Obj)
{
    debugger;
    var rowData = DataTables.AssociatedProductsTable.row($(this_Obj).parents('tr')).data();
    if (!rowData.DefaultOptionYN)
    {
        //rowData.ProductID, rowData.ID
        var prodetid = rowData.ProductID;//$("#productDetailID").val();
        var prodid = rowData.ID//$('.productID').val();
        if ((prodetid) && (prodid)) {
            if (confirm("Are you Sure!") == true) {
                var ProductDetailViewModel = new Object();
                ProductDetailViewModel.ID = prodid;
                ProductDetailViewModel.ProductID = prodetid;
                var data = "{'productDeails':" + JSON.stringify(ProductDetailViewModel) + "}";
                PostDataToServer('Products/DeleteProductDetail/', data, function (JsonResult) {
                    if (JsonResult != '') {
                        switch (JsonResult.Result) {
                            case "OK":

                                notyAlert('success', JsonResult.Record.StatusMessage);
                                RefreshAssociatedProducts(prodetid);
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
            notyAlert('error', 'Please Select a product');
        }

    }
    else {
        notyAlert('error', 'Default Option cant be deleted');
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
            $("#detailDiscountStartDate").val(ConvertJsonToDate(thisproduct.DiscountStartDate));
            $("#detailDiscountEndDate").val(ConvertJsonToDate(thisproduct.DiscountEndDate));
            if (thisproduct.Enabled == true)
            { $("#detailEnable").prop('checked', true); }
            else { $("#detailEnable").prop('checked', false); }
            if (thisproduct.DefaultOption == true)
            { $("#detailDefaultOption").prop('checked', true); }
            else { $("#detailDefaultOption").prop('checked', false); }
            $("#detailDetailTags").val(thisproduct.DetailTags);

            if (thisproduct.DetailTags) {
                $('.Htags').remove();
                var tagar = thisproduct.DetailTags.split(",");
                for (index = 0; index < tagar.length; ++index) {
                    //Tag creation when binding
                    $("#detailkeywordsDiv").append($("<span/>", { text: tagar[index] }).attr({ 'class': 'label label-primary Htags', 'onclick': 'removeme(this)' }));

                }
            }
            else {
                //Removes span tags
                $('.Htags').remove();
            }

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

    
    //associatedStaticfields
 
}

//-----------------------------------------------------Products Review-------------------------------------------------//

function BindProductReviews()   // To Display Previous Comment history
{
    
    ChangeButtonPatchView("Products", "ProductToolBox", "Back");
    
    var id = $(".productID").val();// assigning id for binding reviews.
    var attributesetId = $("#AttributeSetID").val();

    if (attributesetId)
    {
        //Rating
        var thisRatingSummary = GetRatingSummary(id, attributesetId);
        $("#RatingDisplay").empty();
        if (thisRatingSummary.length > 0) {
            var attributecount = thisRatingSummary[0].ProductRatingAttributes.length
            var ratinglists = ""
            var TotalRating = parseFloat(0);
            var AvgRating;


            for (var i = 0; i < attributecount; i++) {
                var ratingstar = parseFloat(thisRatingSummary[0].ProductRatingAttributes[i].Value);

                TotalRating = TotalRating + ratingstar; //Total Rating of each attribute is saved here
                ratingstar = Math.round(ratingstar); //count of Rating to display as star
                var ratebtnstring = '';
                for (var count = 0; count < 5; count++) {
                    if (count < ratingstar) {
                        ratebtnstring = ratebtnstring + '<button type="button" class="btn btn-warning btn-sm" aria-label="Left Align"><span class="glyphicon glyphicon-star" aria-hidden="true"></span></button>'
                    }
                    else {
                        ratebtnstring = ratebtnstring + '<button type="button" class="btn btn-default btn-sm" aria-label="Left Align"><span class="glyphicon glyphicon-star" aria-hidden="true"></span></button>'
                    }
                }
                ratinglists = ratinglists + '<div class="col-xs-4 text-right">' + thisRatingSummary[0].ProductRatingAttributes[i].Caption + '</div>' +
                                            '<div class="col-xs-8"><div  class="rating-block">' + ratebtnstring + '</div></div>';
            }
            AvgRating = Math.round(TotalRating / attributecount); //total rating by attribute count
            var Avgratebtnstring = '';

            for (var count = 0; count < 5; count++) {
                if (count < AvgRating) {
                    Avgratebtnstring = Avgratebtnstring + '<span class="glyphicon glyphicon-star"></span>'
                }
                else {
                    Avgratebtnstring = Avgratebtnstring + '<span class="glyphicon glyphicon-star-empty"></span>'
                }
            }

            var ratingdiv = $('<div class="row">' +
                                                  '<div class="col-xs-12 col-md-6 text-center"><h1 class="rating-num">' + AvgRating + '</h1>' +
                                                      '<div class="rating">' + Avgratebtnstring + '</div>' +
                                                      '<div>' +
                                                      '<span class="glyphicon glyphicon-user"></span>' + thisRatingSummary[0].RatingCount + ' total' +
                                                      '</div>' +
                                                  '</div>' +
                                                  '<div class="col-xs-12 col-md-6">' +
                                                      '<div id ="RatingAttributes" class="row rating-desc">' + ratinglists +
                                                      '</div></div></div>'); 
            $("#RatingDisplay").append(ratingdiv);

        }
        else
        {
            debugger;
            var ratingdiv = $(' <div class="row"><div class="col-xs-12 col-md-6 text-center"><h1 class="rating-num">0.0</h1><div class="rating">'+
                              '<span class="glyphicon glyphicon-star-empty"></span><span class="glyphicon glyphicon-star-empty"></span>'+
                              '<span class="glyphicon glyphicon-star-empty"></span><span class="glyphicon glyphicon-star-empty"></span>' +
                              '<span class="glyphicon glyphicon-star-empty"></span>' +
                             '</div><div><span class="glyphicon glyphicon-user"></span>0 total</div></div></div>');
            $("#RatingDisplay").append(ratingdiv);
        }
        //Reviews
        var thisReviewList = GetProductReviews(id);
        $("#ReviewsDisplay").empty();
        if (thisReviewList.length>0) {
            
            for (var i = 0; i < thisReviewList.length; i++) {
                var resultdate =thisReviewList[i].ReviewCreatedDate;
                var imageurl;
                if (thisReviewList[i].ImageUrl)
                    imageurl = thisReviewList[i].ImageUrl
                else
                    imageurl = 'Content/images/NoImage60x60.png';

                var cnt = $('<div class="review-block"><div class="row">' +
                            '<div class="col-sm-3">' +
                            '<img src="' + imageurl + '" class="img-rounded">' +
                            '<div class="review-block-name"><a href="#">' + thisReviewList[i].CustomerName + '</a></div>' +
                            '<div class="review-block-date">' + resultdate + '<br />' + thisReviewList[i].DaysCount + ' days ago</div>' +
                            '</div>' +
                            '<div class="col-sm-9">' +
                            '<div id="ReviewBlockRate' + [i] + '" class="review-block-rate"></div>' +
                            '<div id=ReviewDesc' + i + 'class="review-block-description">' + thisReviewList[i].Review + '</div>' +
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
        else
        {
            var cnt = $('<div class="row"><div id="ReviewsDisplay" class="col-sm-7"><h2>No Reviews Yet</h2></div></div>');
            $("#ReviewsDisplay").append(cnt);
        }
}
   
}

function GetRatingSummary(id, attributesetId) {
    try {
        var data = {
            "id": id, "attributesetId": attributesetId
        };
        var ds = {};
        ds = GetDataFromServer("Products/GetRatingSummary/", data);

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

function ModelProductsRating(currentObj) {
    //popsup the model
   
    var rowData = DataTables.productTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null) && (rowData.AttributeSetID != null))
    {
        $("#titleProductRating").text(rowData.Name);
        var thisRatingSummary = GetRatingSummary(rowData.ID, rowData.AttributeSetID);
        if (thisRatingSummary.length > 0)
        {
            
        
            $("#RatingPopupDisplay").empty();
            var attributecount = thisRatingSummary[0].ProductRatingAttributes.length
            var ratinglists = ""  

            for (var i = 0; i < attributecount; i++)
            {
                var ratingstar = parseFloat(thisRatingSummary[0].ProductRatingAttributes[i].Value); 
                ratingstar = Math.round(ratingstar); //count of Rating to display as star
                var ratebtnstring = '';
                for (var count = 0; count < 5; count++)
                {
                    if (count < ratingstar) {
                        ratebtnstring = ratebtnstring + '<button type="button" class="btn btn-warning btn-xs" aria-label="Left Align"><span class="glyphicon glyphicon-star" aria-hidden="true"></span></button>'
                    }
                    else {
                        ratebtnstring = ratebtnstring + '<button type="button" class="btn btn-default btn-xs" aria-label="Left Align"><span class="glyphicon glyphicon-star" aria-hidden="true"></span></button>'
                    }
                }//for
                ratinglists = ratinglists + '<div class="col-xs-5 ">' + thisRatingSummary[0].ProductRatingAttributes[i].Caption + '</div>' +
                                            '<div class="col-xs-7 "><div  class="rating-block">' + ratebtnstring + '</div></div>';
            }//for
            $("#RatingPopupDisplay").append(ratinglists);
        }//if
        else
        {
            $("#RatingPopupDisplay").empty();
            var ratinglists = '<div class="col-xs-12 text-center"><h3>No Ratings Yet.. </h3></div>'
            $("#RatingPopupDisplay").append(ratinglists);
        }
    }//if

    $('#btnmodelproductrating').trigger('click');
}


function GetOtherAttributeValues()
{
    try {
        var data = { "id": id };
        var ds = {};
        ds = GetDataFromServer("Products/GetAttributeValuesByProduct/", data);

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