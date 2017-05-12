var DataTables = {};
var this_ObjOrder = null;
$(document).ready(function () {
    DataTables.orderHeadertable = $('#tblOrderList').DataTable(
    {
        dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
        order: [],
        searching: true,
        paging: true,
        data: GetOrderHeader(),
        columns: [
          { "data": "ID" },
          { "data": "ParentOrderID" },
          { "data": null },
          { "data": "OrderDate" },
          { "data": "CustomerName" },
          { "data": "ContactNo" },
          { "data": null },
          {"data":"TotalShippingAmt"},
          { "data": "OrderStatus" },
          { "data": null, "orderable": false, "defaultContent": '<a onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
        ],
        columnDefs: [
         {
             "targets": [0,1],
             "visible": false,
             "searchable": false
         },
         {
             'targets': 2,
             'render': function (data, type, full, meta) {
                 if (data.OrderRev != 0 && data.OrderRev != "")
                 {
                     var Order = data.OrderNo + " / " + data.OrderRev;
                     return Order;
                 }
                 else
                 {
                     var Order = data.OrderNo;
                     return Order;
                 }
             }
         },
         {
             'targets': 6,
             'render': function (data, type, full, meta) {
                 debugger;
                 return (data.TotalOrderAmt - data.TotalDiscountAmt);
                 }
         }
        ]
    });
    DataTables.orderDetailstable = $('#tblOrderDetailList').DataTable(
    {
        dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
        order: [],
        searching: true,
        paging: true,
        data: null,
        columns: [
          { "data": "OrderDetailID" },
          {"data": "ProductSpecXML"},
          { "data": "ItemStatus" },
          { "data": "Qty" },
          { "data": "Price" },
          { "data": "Total" },
          { "data": "ShippingAmt" },
          { "data": "TaxAmt" },
          { "data": "DiscountAmt" },
          { "data": "TotalDiscountAmt" },
          { "data": "SubTotal" }
        ],
        columnDefs: [
         {
             "targets": [0],
             "visible": true,
             "searchable": true
         },
         {//hiding hidden column 
             "targets": [1],
             "visible": true,
             "searchable": true,
             "render": function (data, type, full, meta) {
                 debugger;
                 var Name="<b>"+data.split("||")[0]+"</b>";
                 var Spec = (data.split("||")[1]).split("><");
                 for (var i = 0; i < Spec.length-1; i++)
                 {
                     if (i > 0)
                     {
                         var html = Spec[i].replace(">"," : ");
                         Name=Name +"</br>"+ (html.split("</")[0]);
                     }
                    
                 }
                 return Name;
             }
         }
        ]
    });
    DataTables.orderDetailstableInvoiceRegion = $('#tblOrderDetailsInvoiceRegion').DataTable(
    {
        dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
        order: [],
        searching: true,
        paging: true,
        data: null,
        columns: [
          { "data": "OrderDetailID" },
          { "data": "ProductSpecXML" },
          { "data": "ItemStatus" },
          { "data": "Qty" },
          { "data": "Price" },
          { "data": "Total" },
          { "data": "ShippingAmt" },
          { "data": "TaxAmt" },
          { "data": "DiscountAmt" },
          { "data": "TotalDiscountAmt" },
          { "data": "SubTotal" }
        ],
        columnDefs: [
         {
             "targets": [0],
             "visible": true,
             "searchable": true
         },
         {//hiding hidden column 
             "targets": [1],
             "visible": true,
             "searchable": true,
             "render": function (data, type, full, meta) {
                 debugger;
                 var Name = "<b>" + data.split("||")[0] + "</b>";
                 var Spec = (data.split("||")[1]).split("><");
                 for (var i = 0; i < Spec.length - 1; i++) {
                     if (i > 0) {
                         var html = Spec[i].replace(">", " : ");
                         Name = Name + "</br>" + (html.split("</")[0]);
                     }

                 }
                 return Name;
             }
         }
        ]
    });
    DataTables.orderModificationtable = $('#tblOrderModification').DataTable(
    {
        dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
        order: [],
        searching: true,
        paging: false,
        data: null,
        bInfo: false,
        columns: [
          { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="DeleteDemoOrderData(this)"><i class="fa fa-trash-o" aria-hidden="true"></i></a>' },
          { "data": "OrderDetailID" },
          { "data": "ProductSpecXML" },
          { "data": "ItemStatus" },
          { "data": "Qty" },
          { "data": "Price" },
          { "data": "Total" },
          { "data": "ShippingAmt" },
          { "data": "TaxAmt" },
          { "data": "DiscountAmt" },
          { "data": "TotalDiscountAmt" },
          { "data": "SubTotal" }
        ],
        columnDefs: [
         {//hiding hidden column 
             "targets": [2],
             "visible": true,
             "searchable": false,
             "render": function (data, type, full, meta) {
                 debugger;
                 if (data != "" && data != null)
                  {
                 var Name = "<b>" + data.split("||")[0] + "</b>";
                 var Spec = (data.split("||")[1]).split("><");
                 for (var i = 0; i < Spec.length - 1; i++) {
                     if (i > 0) {
                         var html = Spec[i].replace(">", " : ");
                         Name = Name + "</br>" + (html.split("</")[0]);
                     }

                 }
                 }
                 return Name;
             }
         },
         {
             'targets': 0
         },
                     {
                         'targets': 4,
                         'render': function (data, type, full, meta) {
                             debugger;
                             if (data == 0) {
                                 var txtbox = '<input class="form-control" style="width:100%;text-align: center;font-weight:900;" type="text" value="1" onfocusout="Calculatesum(this)"></input> '
                             }
                             else {
                                 var txtbox = '<input class="form-control" style="width:100%;text-align: center;font-weight:900;" type="text" value="' + (data) + '" onfocusout="Calculatesum(this)"></input> '
                             }

                             return txtbox
                         }
                     }
        ]
    });
    DataTables.tblProductList = $('#tblProductList').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: null,
             columns: [
               { "data": null, "defaultContent": '' },
               { "data": "ProductID" },
               { "data": "ProductName" },
               { "data": "BaseSellingPrice", "defaultContent": "<i>-</i>" },
               { "data": "PriceDifference", "defaultContent": "<i>-</i>" },
               { "data": "ActualPrice", "defaultContent": "<i>-</i>" },
               { "data": "ShippingCharge", "defaultContent": "<i>-</i>" },
               { "data": "DiscountAmount", "defaultContent": "<i>-</i>" }
             ],
             columnDefs: [
              {
                  orderable: false,
                  className: 'select-checkbox',
                  targets: 0
              },
             {//hiding hidden column 
                 "targets": [2],
                 "visible": true,
                 "searchable": true,
                 "render": function (data, type, full, meta) {
                     debugger;
                     var Name = "<b>" + data.split("||")[0] + "</b>";
                     var Spec = (data.split("||")[1]).split("><");
                     for (var i = 0; i < Spec.length - 1; i++) {
                         if (i > 0) {
                             var html = Spec[i].replace(">", " : ");
                             Name = Name + "</br>" + (html.split("</")[0]);
                         }

                     }
                     return Name;
                 }
             }],
             select: {
                 style: 'multi',
                 selector: 'tr'
             },
             order: [[1, 'asc']]
         });
    DataTables.tblProductListRevised = $('#tblProductListRevised').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: false,
             data: null,
             columns: [
               { "data": "OrderDetailID" },
               { "data": "ProductSpecXML" },
               { "data": "ItemStatus" },
               { "data": "Qty" },
               { "data": "Price" },
               { "data": "Total" },
               { "data": "ShippingAmt" },
               { "data": "TaxAmt" },
               { "data": "DiscountAmt" },
               { "data": "SubTotal" }
             ],
             columnDefs: [
              {
                  "targets": [0],
                  "visible": true,
                  "searchable": true
              },
              {//hiding hidden column 
                  "targets": [1],
                  "visible": true,
                  "searchable": true,
                  "render": function (data, type, full, meta) {
                      debugger;
                      var Name = "<b>" + data.split("||")[0] + "</b>";
                      var Spec = (data.split("||")[1]).split("><");
                      for (var i = 0; i < Spec.length - 1; i++) {
                          if (i > 0) {
                              var html = Spec[i].replace(">", " : ");
                              Name = Name + "</br>" + (html.split("</")[0]);
                          }

                      }
                      return Name;
                  }
              },
                 
             ]
         });

    DataTables.orderDetailstableShipmentRegion = $('#tblOrderForShipmentRegion').DataTable(
    {
        dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
        order: [],
        searching: true,
        paging: true,
        data: null,
        columns: [
          { "data": null ,"defaultContent": ''},
          { "data": "OrderDetailID" },
          { "data": "ProductSpecXML" },
          { "data": "Qty" },
          { "data": "ShippedQty" },
          { "data": "QtyShipped" }
        ],
        columnDefs: [
            {
                orderable: false,
                className: 'select-checkbox',
                targets: 0
            },
                     {//hiding hidden column 
                         "targets": [2],
                         "visible": true,
                         "searchable": false,
                         "render": function (data, type, full, meta) {
                             debugger;
                             if (data != "" && data != null) {
                                 var Name = "<b>" + data.split("||")[0] + "</b>";
                                 var Spec = (data.split("||")[1]).split("><");
                                 for (var i = 0; i < Spec.length - 1; i++) {
                                     if (i > 0) {
                                         var html = Spec[i].replace(">", " : ");
                                         Name = Name + "</br>" + (html.split("</")[0]);
                                     }

                                 }
                             }
                             return Name;
                         }
                     },
                     {
                         'targets': 4,
                         'render': function (data, type, full, meta) {
                             debugger;
                             if (data)
                             {
                                 var txtbox = '<input class="form-control" style="width:100%;text-align: center;font-weight:900;" type="text" value="' + (data) + '" onkeyup="ChangeQtyShipment(this)"></input>'
                             }
                             else
                             {
                                 var txtbox = '<input class="form-control" style="width:100%;text-align: center;font-weight:900;" type="text" value="' + 1 + '" onkeyup="ChangeQtyShipment(this)"></input> '
                             }                         

                             return txtbox
                         }
                     }
        ],
        select: {
            style: 'multi',
            selector: 'tr'
        }
    });
    DataTables.orderShippedShipmentRegion = $('#tblShippedItems').DataTable(
   {
       dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
       order: [],
       searching: true,
       paging: true,
       data: null,
       columns: [
         { "data": "OrderID" },
         { "data": "ShipmentNo" },
         { "data": "ShipmentDateString" },
         { "data": "DeliveredDate" },
         { "data": "DeliveredBy" },
         { "data": null, "orderable": false, "defaultContent": '<a onclick="ShowShipment(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
       ],
       columnDefs: [
       ],
       select: {
           selector: 'tr'
       }
   });
    DataTables.OrderOldShipmentShipmentRegion = $('#tblOrderShippedinShipmentRegion').DataTable(
    {
        dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
        order: [],
        searching: true,
        paging: true,
        data: null,
        columns: [
          { "data": "ID" },
          { "data": "ShipmentID" },
          { "data": "OrderDetailObj" },
          { "data": "OrderDetailObj" },
          { "data": "ShippedQty" }
        ],
        columnDefs: [
                     {//hiding hidden column 

                         "targets": [2],
                         "visible": true,
                         "searchable": false,
                         "render": function (data, type, full, meta) {
                             debugger;
                             data1 = data.ProductSpecXML;
                             if (data1 != "" && data1 != null) {
                                 var Name = "<b>" + data1.split("||")[0] + "</b>";
                                 var Spec = (data1.split("||")[1]).split("><");
                                 for (var i = 0; i < Spec.length - 1; i++) {
                                     if (i > 0) {
                                         var html = Spec[i].replace(">", " : ");
                                         Name = Name + "</br>" + (html.split("</")[0]);
                                     }

                                 }
                             }
                             return Name;
                         }
                     },
                     {//hiding hidden column 

                         "targets": [3],
                         "visible": true,
                         "searchable": false,
                         "render": function (data, type, full, meta) {                             
                             return data.Qty;
                         }
                     }
        ],
        select: {
            selector: 'tr'
        }
    });
    $('#tabOrderList').click(function (e) {
        ChangeButtonPatchView("Order", "btnPatchOrders", "List");
    });
    //$('#tblProductList tbody').on('click', 'tr', function () {  
    //    var tabledata = DataTables.tblProductList.rows('.selected').data();
    //    var Total = 0;
    //    for (i = 0; i < tabledata.length; i++) {
    //        Total=Total + tabledata[i].ActualPrice
    //    }
    //    $('#h4Total').text("Total Excluding Shipping Charges: " + Total+" QAR");
    //});
    ChangeButtonPatchView("Order", "btnPatchOrders", "List");
});
//Table Data Bind for Order Header
function GetOrderHeader()
{
    try {
        var data = "";
        var ds = {};
        ds = GetDataFromServer("Order/GetOrderHeader/", data);
        if (ds != '') {ds = JSON.parse(ds);}
        if (ds.Result == "OK") {return ds.Records;}
        if (ds.Result == "ERROR") {alert(ds.Message);}
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
function GetAllOrdersList(ID) {
    debugger;
    try {
        data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Order/GetAllOrdersList/", data);
        if (ds != '') { ds = JSON.parse(ds); }
        if (ds.Result == "OK") { return ds.Records; }
        if (ds.Result == "ERROR") { alert(ds.Message); }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
function GetAllOrderComments(ID) {
    try {
        data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Order/GetEventsLog/", data);
        if (ds != '') { ds = JSON.parse(ds); }
        if (ds.Result == "OK") { return ds.Records; }
        if (ds.Result == "ERROR") { alert(ds.Message); }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
function GetOrderSummery(ID) {
    try {
        data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Order/GetOrderSummery/", data);
        if (ds != '') { ds = JSON.parse(ds); }
        if (ds.Result == "OK") { return ds.Records; }
        if (ds.Result == "ERROR") { alert(ds.Message); }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
function GetOrderDetails(ID) {
    try {

        var ds = {};
        data = { "ID": ID };
        ds = GetDataFromServer("Order/GetOrderDetails/", data);
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
function GetProductsListtoAdd(ID)
{
    try {
        data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Order/GetProductsListtoAdd/", data);
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
function GetShipmentHeader(ID) {
    debugger;
    try {
        data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Order/GetShipmentHeader/", data);
        if (ds != '') { ds = JSON.parse(ds); }
        if (ds.Result == "OK") { return ds.Records; }
        if (ds.Result == "ERROR") { alert(ds.Message); }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
function GetShipmentDetails(ID) {
    debugger;
    try {
        data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Order/GetAllShipmentDetail/", data);
        if (ds != '') { ds = JSON.parse(ds); }
        if (ds.Result == "OK") { return ds.Records; }
        if (ds.Result == "ERROR") { alert(ds.Message); }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
function GetOrderExcludesShip(ID) {
    debugger;
    try {
        data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Order/GetOrderExcludesShip/", data);
        if (ds != '') { ds = JSON.parse(ds); }
        if (ds.Result == "OK") { return ds.Records; }
        if (ds.Result == "ERROR") { alert(ds.Message); }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
function GetMailTemplateData(ID) {
    debugger;
    try {
        data = {"ID":ID};
        var ds = {};
        ds = GetDataFromServer("Order/GetMailTemplate/", data);
        if (ds != '') { ds = JSON.parse(ds); }
        if (ds.Result == "OK") { return ds.Records; }
        if (ds.Result == "ERROR") { alert(ds.Message); }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
//function InsertNewOrderRevision(tabledata, OrderID)
//{
//    try {
//        debugger;
//        var data = { "OrderDetailList": tabledata};
//        var ds = {};
//        ds = GetDataFromServer("Order/InsertNewOrderRevision/", data);
//        if (ds != '') {
//            ds = JSON.parse(ds);
//        }
//        if (ds.Result == "OK") {
//            return ds.Records;
//        }
//        if (ds.Result == "ERROR") {
//            notyAlert('error', ds.Message);
//        }
//    }
//    catch (e) {
//        notyAlert('error', e.message);
//    }
//}
//Edit For Order Header
function Edit(this_obj)
{
    debugger;
    this_ObjOrder = this_obj;
    $('#tabOrderDetails').trigger('click');
    ChangeButtonPatchView("Order", "btnPatchOrders", "Edit_List");
    var rowData = DataTables.orderHeadertable.row($(this_obj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        if (rowData.OrderStatus == "Cancelled" || rowData.OrderStatus == "Invoiced" || rowData.OrderStatus == "Delivered")
        {
            ChangeButtonPatchView("Order", "btnPatchOrders", "Cancelled");

        }
        debugger;
        BindAllDetails(rowData.ID, rowData.ParentOrderID)
    }
    $('#tabOrderRegion').click();
}
function BindAllDetails(ID, ParentOrderID)
{
    $("#ID").val(ID);
    $("#ParentOrderID").val(ParentOrderID);
    var Result = GetOrderDetails(ID);
    BindGeneralSection(Result);
    BindAccountSection(Result);
    BindBillDetails(Result);
    BindShippingDetails(Result);
    BindPaymentInformation(Result);
    BindShippingHandlingSection(Result);
    BindTableOrderDetailList(Result.ID);
    BindOrderComments(Result);
    BindOrderSummery(Result);
    BindNewRevisionGeneral(Result);
    BindRevisionLinkGeneral(Result);

    //******************************** INVOICE AREA

    BindGeneralSectionInvoiceRegion(Result);
    BindAccountSectionInvoiceRegion(Result);
    BindPaymentInformationInvoiceRegion(Result);
    BindShippingHandlingSectionInvoiceRegion(Result);
    BindOrderSummeryInvoiceRegion(Result);

    //****************************** SHIPPING AREA

    BindGeneralSectionShipmentRegion(Result);
    BindAccountSectionShipmentRegion(Result);
    BindGeneralSectionShipmentRegionOld(Result);
    BindAccountSectionShipmentRegionOld(Result);
    $('#tabOrderRegion').click();
}
function CancelIssue()
{
    debugger;
    var r = confirm("Are You Sure ?, This will cancel your Current Order..");
    if (r == true)
    {
        debugger;
        var GrandTotal = 0;
        $('#tabCreateRevision').click();
        DataTables.orderModificationtable.clear().rows.add(GetAllOrdersList($('#hdnOrderHID').val())).draw(false);
        var ordertabledata = DataTables.orderModificationtable.rows().data();
        for (var i = 0; i < ordertabledata.length; i++)
        {
            GrandTotal = GrandTotal + ordertabledata[i].SubTotal;
        }
        ChangeButtonPatchView("Order", "btnPatchOrders", "Revise");
        $('#tblGrandTotal .strGrandTotal').text(GrandTotal + " QAR");
    }
    
}
function CancelOrder()
{
    debugger;
    var r = confirm("Are You Sure ?, This will cancel your Current Order..");
    if (r == true) {
        var ID = $('#hdnOrderHID').val();
        var OrderViewModel = new Object();
        OrderViewModel.ID = ID;
        var data = "{'orderObj':" + JSON.stringify(OrderViewModel) + "}";
        PostDataToServer('Order/CancelOrder/', data, function (JsonResult) {
            if (JsonResult != '') {
                switch (JsonResult.Result) {
                    case "OK":
                        notyAlert('success', JsonResult.Records.StatusMessage);
                        goback();
                        break;
                    case "ERROR":
                        notyAlert('error', JsonResult.Records.StatusMessage);
                        break;
                    default:
                        break;
                }
            }
        })
        
    }
}
function AddReviseOrder()
{
    debugger;
    var GrandTotal = 0;
    var tabledata = DataTables.tblProductList.rows('.selected').data();
    var ordertabledata = DataTables.orderModificationtable.rows().data();
    for (var i = 0; i < ordertabledata.length; i++)
    {
        GrandTotal=GrandTotal + ordertabledata[i].SubTotal;
    }
    var OrderID = $("#ID").val();
    var OrderDetailList = [];
    for (i = 0; i < tabledata.length; i++)
    {
        var Order = new Object();
        Order.OrderDetailID = null;
        Order.Qty = 1;
        Order.Total = tabledata[i].ActualPrice;
        Order.ShippingAmt = tabledata[i].ShippingCharge;
        Order.TaxAmt = 0;
        Order.ItemID = tabledata[i].ID;
        Order.DiscountAmt = tabledata[i].DiscountAmount;
        Order.TotalDiscountAmt = tabledata[i].DiscountAmount;
        Order.SubTotal = ((tabledata[i].ActualPrice + tabledata[i].ShippingCharge) - tabledata[i].DiscountAmount);
        Order.ItemStatus = "Pending";
        Order.Price = tabledata[i].ActualPrice;
        Order.ProductSpecXML = tabledata[i].ProductName;
        Order.ProductID = tabledata[i].ProductID;
        ordertabledata.push(Order);
        GrandTotal = GrandTotal + Order.SubTotal;
    }
    $('#tblGrandTotal .strGrandTotal').text(GrandTotal+" QAR");
    DataTables.orderModificationtable.clear().rows.add(ordertabledata).draw(false);
}

function DeleteDemoOrderData(this_Obj)
{
    debugger;
    var GrandTotal = 0;
    var newDataTable=null;
    var tabledata = DataTables.orderModificationtable.row($(this_Obj).parents('tr')).data();;
    var ordertabledata = DataTables.orderModificationtable.rows().data();
    for (var i = 0; i < ordertabledata.length; i++) {
        GrandTotal = GrandTotal + ordertabledata[i].SubTotal;
    }
    if(tabledata.length==0)
    {
        notyAlert('warning', "Please select items to delete");
    }
    else
    {
        for (var j = 0; j < ordertabledata.length; j++)
        {
            //for (var i = 0; i < tabledata.length; i++) {
                if (tabledata.ProductSpecXML === ordertabledata[j].ProductSpecXML) {
                    ordertabledata.splice(j, 1);
                    GrandTotal = GrandTotal - tabledata.SubTotal;
                }
           // }
        }
        $('#tblGrandTotal .strGrandTotal').text(GrandTotal + " QAR");
        DataTables.orderModificationtable.clear().rows.add(ordertabledata).draw(false);
    }
}
function InsertNewOrder()
{
    debugger;
    var newdata = [];
    var r = confirm("Are You Sure ?, This will cancel your Current Order..");
    if (r == true) {
        var newOrderData = DataTables.orderModificationtable.rows().data();
        var OrderDetailList = [];
        for(var i=0;i<newOrderData.length;i++)
        {
            if ((parseInt(newOrderData[i].Qty)) > newOrderData[i].ProductQty)
            {
                notyAlert('warning', "We're sorry! Only " + newOrderData.ProductQty + " units for " + ProductSpecXML.split('||')[0]);
                return false;
            }
            else {
                var OrderDetailViewModelObj = new Object();
                OrderDetailViewModelObj.ItemID = newOrderData[i].ItemID;
                OrderDetailViewModelObj.ProductID = newOrderData[i].ProductID;
                OrderDetailViewModelObj.ProductSpecXML = newOrderData[i].ProductSpecXML.split('||')[1];
                OrderDetailViewModelObj.ItemStatus = 1;
                OrderDetailViewModelObj.Qty = newOrderData[i].Qty;
                OrderDetailViewModelObj.Price = newOrderData[i].Price;
                OrderDetailViewModelObj.ShippingAmt = newOrderData[i].ShippingAmt;
                OrderDetailViewModelObj.TaxAmt = newOrderData[i].TaxAmt;
                OrderDetailViewModelObj.DiscountAmt = newOrderData[i].DiscountAmt;

                OrderDetailList.push(OrderDetailViewModelObj);
            }
            
        }
        var OrderDetailViewModel = new Object();
        if ($("#ParentOrderID").val() != "0")
        {
            OrderDetailViewModel.OrderID = $("#ParentOrderID").val();
        }
        else {
            OrderDetailViewModel.OrderID = $("#hdnOrderHID").val();
        }
        OrderDetailViewModel.OrderDetailsList = OrderDetailList;
        var data = "{'OrderDetailViewModelObj':" + JSON.stringify(OrderDetailViewModel) + "}";
        PostDataToServer('Order/InsertReviseOrder/', data, function (JsonResult) {
            debugger;
            if (JsonResult != '') {
                switch (JsonResult.Result) {
                    case "OK":
                        notyAlert('success', JsonResult.Record.StatusMessage);                        
                        BindAllDetails(JsonResult.Record.ReturnValues, OrderDetailViewModel.OrderID)
                        
                        //goback();
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
function Calculatesum(this_obj)
{
    var GrandTotal = 0;
    var Qty = this_obj.value;    
    var rowData = DataTables.orderModificationtable.row($(this_obj).parents('tr')).data();
    if (Qty > rowData.ProductQty)
    {
        notyAlert('warning', "We're sorry! Only " + rowData.ProductQty + " units");
        $(this_obj).val(1);
        $(this_obj).focus();
    }
    else
    {
        var TotalDiscount = (rowData.DiscountAmt * Qty);
        var SubTotal = ((rowData.Price * Qty) - TotalDiscount);
        var rowsData = DataTables.orderModificationtable.rows().data();
        for (i = 0; i < rowsData.length; i++) {
            if (rowsData[i].ProductSpecXML == rowData.ProductSpecXML) {
                rowsData[i].SubTotal = SubTotal + rowData.ShippingAmt;
                rowsData[i].TotalDiscountAmt = TotalDiscount;
                rowsData[i].Total = rowData.Price * Qty;
                rowsData[i].Qty = Qty;
            }
            GrandTotal = GrandTotal + rowsData[i].SubTotal;
        }
        $('#tblGrandTotal .strGrandTotal').text(GrandTotal + " QAR");
        DataTables.orderModificationtable.clear().rows.add(rowsData).draw(false);
    }
    
}
function AddNewRevision()
{
    var ID = $('#hdnLocationID').val();
    $('#ModelCreateNewOrder').modal('show');
    DataTables.tblProductList.clear().rows.add(GetProductsListtoAdd(ID)).draw(false);
}
function gobackDetails()
{
    $('#taborderDetails').trigger('click');
}
function goback()
{
    $('#tabOrderList').trigger('click');
    DataTables.orderHeadertable.clear().rows.add(GetOrderHeader()).draw(false);
    ChangeButtonPatchView("Order", "btnPatchOrders", "List");
}
function gobackDetails()
{
    $('#tabOrderDetails').trigger('click');
    ChangeButtonPatchView("Order", "btnPatchOrders", "Edit_List");
}
function BindGeneralSection(Result)
{
    debugger;
    $("#lblOrderNo").text(Result.OrderNo);
    $("#lblOrderDate").text(Result.OrderDate);
    $("#lblOrderStatus").text(Result.OrderStatus);
    $("#lblSourceIP").text(Result.SourceIP);
    $("#lblRevNo").text(Result.OrderRev);
    if (Result.RevisionIDs != "")
    {
        $('#lblRevisionIDs').show();
        $("#RevisionLinkBind").empty();
        var IDs = Result.RevisionIDs.split(',');
        for (var i = 0; i < IDs.length; i++)
        {
            $("#RevisionLinkBind").append('<a class="col-md-6" onclick="ShowRevision(' + IDs[i] + ')">Revision ' + (i) + ' </a>');
        }
        
    }
    else
    {
        $('#lblRevisionIDs').hide();
        $("#RevisionLinkBind").empty();
    }
    
}
function ShowRevision(id)
{
    debugger;
    $('#ModelRevisionDetails').modal('show');
    //$("#lblRevNo_RevLink").text(id);
    DataTables.tblProductListRevised.clear().rows.add(GetAllOrdersList(id)).draw(false);
}
function BindAccountSection(Result)
{
    $('#imgPreviewCustomer').attr('src', Result.CustomerURL);
    $("#lblCustomerName").text(Result.CustomerName);
    $("#lblContactNo").text(Result.ContactNo);
    $("#lblCustomerEmail").text(Result.CustomerEmail);
    $("#hdnAccountEmailID").val(Result.CustomerEmail);
    $("#hdnAccountCustomerName").val(Result.CustomerName);
}
function BindBillDetails(Result)
{
    $('#lblName').text(Result.BillFirstName + "." + Result.BillMidName + "." + Result.BillLastName)
    $('#lblAddress').text(Result.BillAddress)
    $('#lblAddress').append('<br/>' + Result.BillCity)
    $('#lblAddress').append('<br/>' + Result.BillStateProvince)
    $('#lblAddress').append('<br/>' + Result.BillCountryCode)

    $('#lblContact').text(Result.BillContactNo)
    $('#BillFirstName').val(Result.BillFirstName);
    $('#BillMidName').val(Result.BillMidName);
    $('#BillLastName').val(Result.BillLastName);
    $('#BillAddress').val(Result.BillAddress);
    $('#BillCity').val(Result.BillCity);
    $('#BillContactNo').val(Result.BillContactNo);
    $('#BillStateProvince').val(Result.BillStateProvince);
    $('#CustomerID').val(Result.CustomerID);
    $('#hdnOrderHID').val(Result.ID);
    $('#BillCountryCode').val(Result.BillCountryCode);
}
function BindShippingDetails(Result)
{
    $('#lblShipDisName').text(Result.ShipFirstName + "." + Result.ShipMidName + "." + Result.ShipLastName)
    $('#lblShipDisAddress').text(Result.ShipAddress)
    $('#lblShipDisAddress').append('<br/>' + Result.ShipCity)
    $('#lblShipDisAddress').append('<br/>' + Result.ShipStateProvince)
    $('#lblShipDisAddress').append('<br/>' + Result.ShipCountryCode)
    $('#lblShipDisContact').text(Result.ShipContactNo)

    $('#ShipFirstName').val(Result.ShipFirstName);
    $('#ShipMidName').val(Result.ShipMidName);
    $('#ShipLastName').val(Result.ShipLastName);
    $('#ShipAddress').val(Result.ShipAddress);
    $('#ShipCity').val(Result.ShipCity);
    $('#ShipContactNo').val(Result.ShipContactNo);
    $('#ShipStateProvince').val(Result.ShipStateProvince);
    $('#hdnOrderHIDShip').val(Result.ID);
    $('#ShipCountryCode').val(Result.ShipCountryCode);
}
function BindPaymentInformation(Result) {
    $('#lblPaymentType').text(Result.PaymentType)
    $('#lblCurrencyCode').text(Result.CurrencyCode)
    $('#lblPaymentStatus').text(Result.PaymentStatus)
}
function BindShippingHandlingSection(Result)
{
    debugger;
    $('#lblShippingLocation').text(Result.ShippingLocationName)
    $('#lblShippingAmt').text(Result.TotalShippingAmt)
    $('#hdnLocationID').val(Result.shippingLocationID)
}
function BindTableOrderDetailList(ID)
{
    DataTables.orderDetailstable.clear().rows.add(GetAllOrdersList(ID)).draw(false);
}
function BindOrderComments(Result)
{
    debugger;
    $("#CommentsDisplay").empty();
    $("#hdnEventsLogParentID").val(Result.ID);
    var CommentList = GetAllOrderComments(Result.ID);
    //$("#CustomerNotifiedYN").prop('checked', Result.CustomerNotifiedYN);
    if (CommentList != null) {

        for (var i = 0; i < CommentList.length; i++) {
            var cnt = $('<li id="Comment' + i + '" class="list-group-item col-md-12" style="background-color:#f4f0f0;">' + CommentList[i].Comment + '<span class="badge">' + CommentList[i].CommentDate + '</span></li>');
            $("#CommentsDisplay").append(cnt);
        }
    }
}
function BindOrderSummery(Result)
{
    debugger;
    var OrderSummeryList=GetOrderSummery(Result.ID);
    $('#tdSubTotal').text(OrderSummeryList.SubTotalOrderSummery+ Result.CurrencyCode)
    $('#tdTaxTotal').text(OrderSummeryList.TaxAmtOrderSummery+ Result.CurrencyCode)
    $('#tdDeliveryCosts').text(OrderSummeryList.ShippingCostOrderSummery+ Result.CurrencyCode)
    $('#tdOrderDiscount').text(OrderSummeryList.DiscountAmtOrderSummery+ Result.CurrencyCode)
    $('.strGrandTotal').text(OrderSummeryList.GrandTotalOrderSummery+ Result.CurrencyCode)
}
function BindNewRevisionGeneral(Result)
{
    $("#lblOrderNo_NewRev").text(Result.OrderNo);
    $("#lblOrderDate_NewRev").text(Result.OrderDate);
    $("#lblOrderStatus_NewRev").text(Result.OrderStatus);
    $("#lblSourceIP_NewRev").text(Result.SourceIP);
    $("#lblRevNo_NewRev").text("--");
    $("#lblCustomerName_NewRev").text(Result.CustomerName);
    $("#lblContactNo_NewRev").text(Result.ContactNo);
}
function BindRevisionLinkGeneral(Result) {
    $("#lblOrderNo_RevLink").text(Result.OrderNo);
    $("#lblOrderDate_RevLink").text(Result.OrderDate);
    $("#lblOrderStatus_RevLink").text("Cancelled");
    $("#lblSourceIP_RevLink").text(Result.SourceIP);    
    $("#lblCustomerName_RevLink").text(Result.CustomerName);
    $("#lblContactNo_RevLink").text(Result.ContactNo);
}
function CheckinBillingChanges(data)
{
    var i = JSON.parse(data.responseText)
    switch (i.Result) {
        case "OK":
            notyAlert('success', i.Records.StatusMessage);
            var Result = GetOrderDetails($("#hdnOrderHID").val());
            BindBillDetails(Result);
            $('.close').click();
            break;
        case "ERROR":
            notyAlert('success', i.Records.StatusMessage);
            break;

    }
}
function CheckinShipingChanges(data) {
    var i = JSON.parse(data.responseText)
    switch (i.Result) {
        case "OK":
            notyAlert('success', i.Records.StatusMessage);
            var Result = GetOrderDetails($("#hdnOrderHIDShip").val());
            BindShippingDetails(Result);
            $('.close').click();
            break;
        case "ERROR":
            notyAlert('success', i.Records.StatusMessage);
            break;

    }
}
function CheckinCommentInsert(data) {
    debugger;
    var i = JSON.parse(data.responseText)
    switch (i.Result) {
        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            var Result = GetOrderDetails($("#hdnEventsLogParentID").val());
            BindOrderComments(Result);
            break;
        case "ERROR":
            notyAlert('success', i.Record.StatusMessage);
            break;

    }
}

//###########################################################################################################################################################################################################//
//#############################################******************************* INVOICE SECTION **********************************************###############################################################//


function BindGeneralSectionInvoiceRegion(Result) {
    debugger;
    $("#lblOrderNoInvoiceRegion").text(Result.OrderNo);
    $("#lblOrderDateInvoiceRegion").text(Result.OrderDate);
    $("#lblOrderStatusInvoiceRegion").text(Result.OrderStatus);
    $("#lblSourceIPInvoiceRegion").text(Result.SourceIP);
    $("#lblRevNoInvoiceRegion").text(Result.OrderRev);

}
function BindAccountSectionInvoiceRegion(Result) {
    $('#imgPreviewCustomerInvoiceRegion').attr('src', Result.CustomerURL);
    $("#lblCustomerNameInvoiceRegion").text(Result.CustomerName);
    $("#lblContactNoInvoiceRegion").text(Result.ContactNo);
    $("#lblCustomerEmailInvoiceRegion").text(Result.CustomerEmail);
    $("#hdnAccountEmailIDInvoiceRegion").val(Result.CustomerEmail);
    $("#hdnAccountCustomerNameInvoiceRegion").val(Result.CustomerName);
}
function BindPaymentInformationInvoiceRegion(Result) {
    debugger;
    $('#lblPaymentTypeInvoiceRegion').text(Result.PaymentType)
    $('#lblCurrencyCodeInvoiceRegion').text(Result.CurrencyCode)
    $('#PaymentStatusList').val(Result.PayStatusCode)
}
function BindShippingHandlingSectionInvoiceRegion(Result) {
    $('#lblShippingLocationInvoiceRegion').text(Result.ShippingLocationName)
    $('#lblShippingAmtInvoiceRegion').text(Result.TotalShippingAmt)
}
function BindTableOrderDetailListInvoiceRegion(ID) {
    DataTables.orderDetailstableInvoiceRegion.clear().rows.add(GetAllOrdersList(ID)).draw(false);
}
function BindOrderSummeryInvoiceRegion(Result) {
    debugger;
    var OrderSummeryList = GetOrderSummery(Result.ID);
    $('#tdSubTotalInvoiceRegion').text(OrderSummeryList.SubTotalOrderSummery + Result.CurrencyCode)
    $('#tdTaxTotalInvoiceRegion').text(OrderSummeryList.TaxAmtOrderSummery + Result.CurrencyCode)
    $('#tdDeliveryCostsInvoiceRegion').text(OrderSummeryList.ShippingCostOrderSummery + Result.CurrencyCode)
    $('#tdOrderDiscountInvoiceRegion').text(OrderSummeryList.DiscountAmtOrderSummery + Result.CurrencyCode)
    $('.strGrandTotalInvoiceRegion').text(OrderSummeryList.GrandTotalOrderSummery + Result.CurrencyCode)
}
function TabActionInvoiceRegion()
{
    debugger;
    BindTableOrderDetailListInvoiceRegion($('#hdnOrderHID').val());
    if ($("#lblOrderStatusInvoiceRegion").text() == "Invoiced")
    {
        ChangeButtonPatchView("Order", "btnPatchOrders", "Cancelled");
    }
    else
    {
        ChangeButtonPatchView("Order", "btnPatchOrders", "InvoiceRegion");
    }
    
}
function PaymentStatusOnChange(this_Obj)
{
    debugger;
    var ID = $('#hdnOrderHID').val();
    var Order = new Object();
    Order.ID = ID;
    Order.PayStatusCode = $("#PaymentStatusList").val();
    var data = "{'OrderObj':" + JSON.stringify(Order) + "}";
    PostDataToServer('Order/UpdateOrderPaymentStatus/', data, function (JsonResult) {
        if (JsonResult != '') {
            switch (JsonResult.Result) {
                case "OK":
                    notyAlert('success', JsonResult.Records.StatusMessage);
                    goback();
                    break;
                case "ERROR":
                    notyAlert('error', JsonResult.Records.StatusMessage);
                    break;
                default:
                    break;
            }
        }
    })
}
function SubmitInvoice()
{
    debugger;
    var ID = $('#hdnOrderHID').val();
    var DetailList = [];
    var TableDetail = DataTables.orderDetailstableInvoiceRegion.rows().data();
    var InvoiceViewModel = new Object();
    InvoiceViewModel.ID = null;
    InvoiceViewModel.InvoiceNo = null;
    InvoiceViewModel.ParentID = ID;
    InvoiceViewModel.ParentType = "Order";
    InvoiceViewModel.InvoiceDate = null;
    InvoiceViewModel.PaymentStatus = $("#PaymentStatusList").val();
    for (var i = 0; i < TableDetail.length; i++)
    {
        var InvoiceDetailViewModel = new Object();
        InvoiceDetailViewModel.ID = null;
        InvoiceDetailViewModel.InvoiceID = ID;
        InvoiceDetailViewModel.OrderItemID = TableDetail[i].ItemID;
        InvoiceDetailViewModel.InvoiceAmt = ((((TableDetail[i].Price + TableDetail[i].ShippingAmt + TableDetail[i].TaxAmt) - (TableDetail[i].DiscountAmt))) * TableDetail[i].Qty);
        DetailList.push(InvoiceDetailViewModel);
    }
    InvoiceViewModel.DetailList = DetailList;
    var data = "{'InvoiceViewObj':" + JSON.stringify(InvoiceViewModel) + "}";
    PostDataToServer('Order/InsertInvoice/', data, function (JsonResult) {
        if (JsonResult != '') {
            switch (JsonResult.Result) {
                case "OK":
                    notyAlert('success', JsonResult.Records.StatusMessage);
                    break;
                case "ERROR":
                    notyAlert('error', JsonResult.Records.StatusMessage);
                    break;
                default:
                    break;
            }
        }
    })
}

//##################################################################################################################################################################################################################
//##############************************************************************************** SHIPPING AREA ******************************************

function BindGeneralSectionShipmentRegion(Result) {
    debugger;
    $("#lblOrderNoShippingRegion").text(Result.OrderNo);
    $("#lblOrderDateShippingRegion").text(Result.OrderDate);
    $("#lblOrderStatusShippingRegion").text(Result.OrderStatus);
    $("#lblSourceIPShippingRegion").text(Result.SourceIP);
    $("#lblRevNoShippingRegion").text(Result.OrderRev);

}
function BindAccountSectionShipmentRegion(Result) {
    $('#imgPreviewCustomerShippingRegion').attr('src', Result.CustomerURL);
    $("#lblCustomerNameShippingRegion").text(Result.CustomerName);
    $("#lblContactNoShippingRegion").text(Result.ContactNo);
    $("#lblCustomerEmailShippingRegion").text(Result.CustomerEmail);
    $("#hdnAccountEmailIDShippingRegion").val(Result.CustomerEmail);
    $("#hdnAccountCustomerNameShippingRegion").val(Result.CustomerName);
}
function TabActionShipmentRegion()
{
    debugger;
    ChangeButtonPatchView("Order", "btnPatchOrders", "ShipmentRegion");
    var RowData = GetShipmentHeader($('#hdnOrderHID').val());
    //if (RowData.length > 0)
    //{
        DataTables.orderShippedShipmentRegion.clear().rows.add(RowData).draw(false);
    //}
    //else
    //{
    //    $("#tabAddShippingRegion").click();
    //}
}
function TabActionAddShipmentRegion()
{
    BindTableOrderDetailListShipmentRegion($('#hdnOrderHID').val());
    ChangeButtonPatchView("Order", "btnPatchOrders", "AddShipmentRegion");
}
function BindTableOrderDetailListShipmentRegion(ID) {
    DataTables.orderDetailstableShipmentRegion.clear().rows.add(GetOrderExcludesShip(ID)).draw(false);
}
function ChangeQtyShipment(this_Obj)
{
    debugger;
    var Qty = this_Obj.value;
    var rowData = DataTables.orderDetailstableShipmentRegion.row($(this_Obj).parents('tr')).data();
    if (Qty > rowData.Qty) {
        notyAlert('warning', "We're sorry! Only " + rowData.Qty + " units");
        $(this_obj).val(1);
    }
    else {
        var rowsData = DataTables.orderDetailstableShipmentRegion.rows().data();
        for (i = 0; i < rowsData.length; i++) {
            if (rowsData[i].ProductSpecXML == rowData.ProductSpecXML) {
                if (Qty == 0)
                {
                    rowsData[i].QtyShipped = rowData.Qty;
                }
                else
                {
                    rowsData[i].QtyShipped = rowsData[i].QtyShipped - Qty;
                }               
                rowsData[i].ShippedQty = Qty;
            }
        }
        //$('#tblGrandTotal .strGrandTotal').text(GrandTotal + " QAR");
        DataTables.orderDetailstableShipmentRegion.clear().rows.add(rowsData).draw(false);
    }
}
function AddShipment()
{
    $("#tabAddShippingRegion").click();
}
function ShowShipment(this_Obj)
{
    debugger;
    var rowData = DataTables.orderShippedShipmentRegion.row($(this_Obj).parents('tr')).data();
    DataTables.OrderOldShipmentShipmentRegion.clear().rows.add(GetShipmentDetails(rowData.ID)).draw(false);
    $('#hdnShipmentID').val(rowData.ID);
    $('#tabOldShippingRegion').click();
    $('#txtDeliveredByShippingRegion').val(rowData.DeliveredBy);
    $('#txtDeliveredDateShippingRegion').val(rowData.DeliveredDate);
}
function TabActionOldShipmentRegion()
{
    ChangeButtonPatchView("Order", "btnPatchOrders", "OldShipmentDetail");
}
function BindGeneralSectionShipmentRegionOld(Result) {
    debugger;
    $("#lblOrderNoShippingRegionOld").text(Result.OrderNo);
    $("#lblOrderDateShippingRegionOld").text(Result.OrderDate);
    $("#lblOrderStatusShippingRegionOld").text(Result.OrderStatus);
    $("#lblSourceIPShippingRegionOld").text(Result.SourceIP);
    $("#lblRevNoShippingRegionOld").text(Result.OrderRev);

}
function BindAccountSectionShipmentRegionOld(Result) {
    $('#imgPreviewCustomerShippingRegionOld').attr('src', Result.CustomerURL);
    $("#lblCustomerNameShippingRegionOld").text(Result.CustomerName);
    $("#lblContactNoShippingRegionOld").text(Result.ContactNo);
    $("#lblCustomerEmailShippingRegionOld").text(Result.CustomerEmail);
    $("#hdnAccountEmailIDShippingRegionOld").val(Result.CustomerEmail);
    $("#hdnAccountCustomerNameShippingRegionOld").val(Result.CustomerName);
}
function SaveShippingDetails()
{
    debugger;
    var DetailsList = [];
    var tabledata = DataTables.orderDetailstableShipmentRegion.rows('.selected').data();
    var ShipmentViewModel = new Object();
    ShipmentViewModel.OrderID = $('#hdnOrderHID').val();
    for(var i=0;i<tabledata.length;i++)
    {
        var ShipmentDetailViewModel = new Object();
        ShipmentDetailViewModel.ShipmentID = null;
        ShipmentDetailViewModel.OrderItemID = tabledata[i].ItemID;
        ShipmentDetailViewModel.ShippedQty = tabledata[i].ShippedQty;
        DetailsList.push(ShipmentDetailViewModel);
    }
    ShipmentViewModel.DetailsList = DetailsList;
    var data = "{'shipmentViewModelObj':" + JSON.stringify(ShipmentViewModel) + "}";
    PostDataToServer('Order/InsertShipment/', data, function (JsonResult) {
        if (JsonResult != '') {
            switch (JsonResult.Result) {
                case "OK":
                    notyAlert('success', JsonResult.Record.StatusMessage);
                    goBackShipping();
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
function UpdateDeliveryStatus()
{
    debugger;
    var ShipmentViewModel = new Object();
    ShipmentViewModel.OrderID = $('#hdnOrderHID').val();
    ShipmentViewModel.ID = $('#hdnShipmentID').val();
    ShipmentViewModel.DeliveredDate = $('#txtDeliveredDateShippingRegion').val();
    ShipmentViewModel.DeliveredBy = $('#txtDeliveredByShippingRegion').val();
    var data = "{'shipmentViewModelObj':" + JSON.stringify(ShipmentViewModel) + "}";
    PostDataToServer('Order/UpdateDeliveryStatus/', data, function (JsonResult) {
        if (JsonResult != '') {
            switch (JsonResult.Result) {
                case "OK":
                    notyAlert('success', JsonResult.Record.StatusMessage);
                    goback();
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
function goBackShipping()
{
    $('#tabShippingRegion').click();
}
function TabActionOrderRegion()
{
    ChangeButtonPatchView("Order", "btnPatchOrders", "Edit_List");
}

function ShowTemplatePreview()
{
    debugger;
    var ID = $('#hdnOrderHID').val();
    var Result = GetMailTemplateData(ID)
    $('#ModelTemplatePreview').modal('show');
    $('#PreviewTemplateArea').empty();
    $('#PreviewTemplateArea').append(Result);
}