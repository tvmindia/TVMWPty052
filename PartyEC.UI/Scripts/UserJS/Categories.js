var DataTables = {};
$(document).ready(function () {
    debugger;
    //Tree bind into right side container
    $('#jstree_Categories').jstree({
        "core": {
            "themes": {
                "responsive": false
            },
            // so that create works
            "check_callback": true,
            'data': GetCategoriesTree()
        },
        "types": {
            "default": {
                "icon": "fa fa-folder icon-state-warning icon-lg"
            },
            "file": {
                "icon": "fa fa-file icon-state-warning icon-lg"
            }
        },
        "state": { "key": "demo2" },
        "plugins": ["dnd", "state", "types"]
    });
    $('#jstree_Categories').on("changed.jstree", function (e, data) {
        debugger
        myFunction(e, data);

    });
    try {

        DataTables.productTable = $('#tblCategoryProduct').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllProducts(),
             columns: [
               { "data": "ID" },
               { "data": "Name" },
               { "data": "EnableYN", "defaultContent": "<i>-</i>" },
               { "data": "SupplierID", "defaultContent": "<i>-</i>" },
               { "data": "SKU", "defaultContent": "<i>-</i>" },
               { "data": "BaseSellingPrice", "defaultContent": "<i>-</i>" },
               { "data": "Qty", "defaultContent": "<i>-</i>" },
               { "data": "StockAvailableYN", "defaultContent": "<i>-</i>" },
               { "data": null, "defaultContent": "<input type='text'></input>" }
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
        alert(e.message);
    }
    //DataTables.attributeSetTable = $('#tblAttributeSet').DataTable(
    //{
    //    dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
    //    order: [],
    //    searching: true,
    //    paging: true,
    //    data: GetAllAttributeSet(),
    //    columns: [
    //      { "data": "ID" },
    //      { "data": "Name" },
    //      { "data": null, "orderable": false, "defaultContent": '<a onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
    //    ],
    //    columnDefs: [
    //     {
    //         "targets": [0],
    //         "visible": true,
    //         "searchable": true
    //     }
    //    ]
    //});

    $('#tabattributeSetDetails').click(function (e) {
        //ChangeButtonPatchView(//ControllerName,//Name of the container, //Name of the action);
        ChangeButtonPatchView("AttributeSet", "btnPatchAttributeSettab2", "Add");
        $('#jstree_Drag').jstree(true).settings.core.data = GetTreeDataLeft();
        $('#jstree_Drag').jstree(true).refresh(true);

        $('#jstree_DragUpdate').jstree(true).settings.core.data = GetTreeDataRight(0);
        $('#jstree_DragUpdate').jstree(true).refresh(true);

        $('#Name').val('');
        $('#ID').val(0);
        $('#TreeList').val('');
    });


});
function myFunction(e,data)
{
    debugger;
    if (data.node.parent == "#")
    {
        $('#ParentID').val(0);
    }
    else
    {
        $('#ParentID').val(data.node.parent);
    }
    $('#ID').val(data.node.id);
    var results = GetCategoryDetails(data.node.id);
    $('#Name').val(results.Name);
    $('#Description').val(results.Description);
    $("#Navigation").prop('checked', results.Navigation);
    $("#Filter").prop('checked', results.Filter);
    $("#Enable").prop('checked', results.Enable);
    $("#ImageID").val(results.ImageID);
    $("#imgCategory").attr('src', (results.URL != "" ? results.URL : "/Content/images/NoImageFound.png"));
    ChangeButtonPatchView("Categories", "btnPatchAttributeSettab", "AddSub");
}
function GetCategoryDetails(id)
{
    try {

        var ds = {};
        data = { "ID": id };
        ds = GetDataFromServer("Categories/GetCategoryDetailsByID/", data);
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
function GetAllProducts() {

    try {
        debugger;
        var data = "";
        var ds = {};
        ds = GetDataFromServer("Products/GetAllProducts/", data);
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

    }

}
function GetCategoriesTree() {
    try {

        var ds = {};
        ds = GetDataFromServer("DynamicUI/GetTreeListCategories/", "");
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