var DataTables = {};
$(document).ready(function () {
    try {

        DataTables.shipmentTable = $('#tblShipments').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllShipments(),
             columns: [
               { "data": "ID", "defaultContent": "<i>-</i>" },
               { "data": "OrderID", "defaultContent": "<i>-</i>" },
               { "data": "ShipmentNo", "defaultContent": "<i>-</i>" },
               { "data": "ShipmentDateString", "defaultContent": "<i>-</i>" },
               { "data": "DeliveredDate", "defaultContent": "<i>-</i>" },
               { "data": "DeliveredBy", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="Edit(this)"><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }

             ],
             columnDefs: [
              
             ]

         });
    }
    catch (e) {
        notyAlert('error', e.message);
    }

    try
    {
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
    }
    catch(e)
    {
        notyAlert('error', e.message);
    }

});

function BindShipmentTable() {
    try {
        DataTables.shipmentTable.clear().rows.add(GetAllShipments()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function ShipmentDetailTabClick()
{
    try {
        ChangeButtonPatchView("Shipments", "ShipmentToolBox", "Detail");
        $("#txtDeliveredDateShippingRegion").val('');
        $("#txtDeliveredByShippingRegion").val('');

    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function ShipmentListTabClick()
{
    $('#ShipmentToolBox').html('');
}
function goback() {
    $('#tabShipmentList a').trigger('click');
    $('#ShipmentToolBox').html('');
}
function Edit(curobj)
{
    try
    {
        var rowData = DataTables.shipmentTable.row($(curobj).parents('tr')).data();
        if (rowData) {
            $('#tabShipmentDetails a').trigger('click');
            $('#hdnShipmentID').val(rowData.ID);
            $('#hdnOrderHID').val(rowData.OrderID);
            var Result = GetOrderDetails(rowData.OrderID);
            if (Result) {
                BindGeneralSection(Result);
                BindAccountSection(Result);
                BindShipmentDetail(rowData.ID)
               
               
            }

        }
    }
    catch(e)
    {
        notyAlert('error', e.message);
    }
}

function BindShipmentDetail(ID)
{
    DataTables.OrderOldShipmentShipmentRegion.clear().rows.add(GetShipmentDetails(ID)).draw(false);
}

function GetAllShipments() {
    try {

        var data = {};
        var ds = {};
        ds = GetDataFromServer("Shipments/GetAllShipmentHeaders/", data);
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
function GetShipmentDetails(ID) {
   
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
function BindGeneralSection(Result)
{
    try
    {
        $("#lblOrderNoShippingRegion").text(Result.OrderNo);
        $("#lblOrderDateShippingRegion").text(Result.OrderDate);
        $("#lblOrderStatusShippingRegion").text(Result.OrderStatus);
        $("#lblSourceIPShippingRegion").text(Result.SourceIP);
        $("#lblRevNoShippingRegion").text(Result.OrderRev);
    }
    catch(e)
    {
        notyAlert('error', e.message);
    }
}
function BindAccountSection(Result) {
    try
    {
        $('#imgPreviewCustomerShippingRegion').attr('src', Result.CustomerURL);
        $("#lblCustomerNameShippingRegion").text(Result.CustomerName);
        $("#lblContactNoShippingRegion").text(Result.ContactNo);
        $("#lblCustomerEmailShippingRegion").text(Result.CustomerEmail);
    }
    catch(e)
    {
        notyAlert('error', e.message);
    }
   

}

function UpdateDeliveryStatus() {
    try
    {
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
                        BindShipmentTable();
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
    catch(e)
    {
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
        notyAlert('error', e.message);
    }
}
