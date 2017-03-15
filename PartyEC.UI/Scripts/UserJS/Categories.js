var DataTables = {};
$(document).ready(function () {
    try
    {
        //Tree bind into LHS side container with category
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
        //function bind for onselect Tree Node
        $('#jstree_Categories').on("changed.jstree", function (e, data) {
            onSelectNode(e, data);

        });
        //Datatable  bind with products

            DataTables.productTable = $('#tblCategoryProduct').DataTable(
             {
                 dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
                 order: [],
                 searching: true,
                 paging: true,
                 data: GetAllProducts(),
                 columns: [
                   { "data": null },
                   {"data":"ID"},
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
                     {
                         'targets': 0,
                         'render': function (data, type, full, meta) {
                             var checkbox = $("<input/>", {
                                 "type": "checkbox"
                             });
                             if (data.CategoryID) {
                                 checkbox.attr("checked", "checked");
                                 checkbox.addClass("checkbox_checked");
                             } else {
                                 checkbox.addClass("checkbox_unchecked");
                             }
                             return checkbox.prop("outerHTML")
                         }
                     },
                  {//hiding hidden column 
                      "targets": [0],
                      "visible": true,
                      "searchable": true
                  }
                 ]
             });
            //table = $('#tblCategoryProduct').DataTable({
            //    'dom': '<"pull-left"f>rt<"bottom"ip><"clear">',
            //    'searching': true,
            //    'paging': true,
            //    'data': GetAllProducts(),
            //    'columns': [
            //       {"data":"ID"},
            //       { "data": "ID" },
            //       { "data": "Name" },
            //       { "data": "EnableYN", "defaultContent": "<i>-</i>" },
            //       { "data": "SupplierID", "defaultContent": "<i>-</i>" },
            //       { "data": "SKU", "defaultContent": "<i>-</i>" },
            //       { "data": "BaseSellingPrice", "defaultContent": "<i>-</i>" },
            //       { "data": "Qty", "defaultContent": "<i>-</i>" },
            //       { "data": "StockAvailableYN", "defaultContent": "<i>-</i>" },
            //       { "data": null, "defaultContent": "<input type='text' class='col-lg-3'></input>" }

            //    ],
            //    'columnDefs': [
            //       {

            //           'targets': 0,
            //           'checkboxes': {
            //               'selectRow': true
            //           }


            //       }

            //    ],
            //    'select': {
            //        'style': 'multi'
            //    },
            //    'order': []
            //});
    }
    catch(e)
    {

    }
   

});
//on-select function for treenodes
function onSelectNode(e, data)
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
    ChangeButtonPatchView("Categories", "btnPatchAttributeSettab", "Edit");
}
//Get category for Edit using ID
function GetCategoryDetails(id)
{
    try {
        var ds = {};
        data = { "ID": id };
        ds = GetDataFromServer("Categories/GetCategoryDetailsByID/", data);
        if (ds != '') {ds = JSON.parse(ds);}
        if (ds.Result == "OK") {return ds.Records;}
        if (ds.Result == "ERROR") { alert(ds.Message); }
    }
    catch (e) {

    }
}
//Get Products for Table binding fro Asigned Products
function GetAssignedProWithID(id) {

    try {
        debugger;
        var data = { "CategoryID": id };
        var ds = {};
        ds = GetDataFromServer("Products/GetAssignedPro/", data);
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
//Get Products for Table binding fro Asigned Products
function GetUnAssignedProWithID(id) {

    try {
        debugger;
        var data = { "CategoryID": id };
        var ds = {};
        ds = GetDataFromServer("Products/GetUnAssignedPro/", data);
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
//Get Products for Table binding fro tab2
function GetAllProducts() {

    try {
        debugger;
        var data = "";
        var ds = {};
        ds = GetDataFromServer("Products/GetAllProductswithCategory/", data);
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
//Get Category for treeview through DynamicUI
function GetCategoriesTree() {
    try {
        var ds = {};
        ds = GetDataFromServer("DynamicUI/GetTreeListCategories/", "");
        if (ds != '') {ds = JSON.parse(ds);}
        if (ds.Result == "OK") {return ds.Records;}
        if (ds.Result == "ERROR") {alert(ds.Message);}
    }
    catch (e) {

    }
}


/////////////////////////////Button Patch onclick functions
function AddNewSubCategory()
{
    ClearFields();
    $('#imgCategory').attr('src', '/Content/images/NoImageFound.png');
    var fluCategory = document.getElementById('CategoryImageUpload');
    fluCategory.value = "";
    fluCategory.innerHTML = "No file chosen";
    ChangeButtonPatchView("Categories", "btnPatchAttributeSettab", "AddSub");
}
function AddCategory() {
    debugger;
    ClearFields();
    $('#ID').val(0);
    $('#ParentID').val(0);
    $('#imgCategory').attr('src', '/Content/images/NoImageFound.png');
    var fluCategory = document.getElementById('CategoryImageUpload');
    fluCategory.value = "";
    fluCategory.innerHTML = "No file chosen";
    $('#jstree_Categories').jstree("deselect_all");
    ChangeButtonPatchView("Categories", "btnPatchAttributeSettab", "Add");
}
function MainClick()
{
    $('#btnFormSave').click();
}

////////////////////////////////////////Onclick for Radio button
function GetAssignedPro()
{
    debugger;
    var id=$('#ID').val()!="0"?$('#ID').val():"";
    DataTables.productTable.clear().rows.add(GetAssignedProWithID(id)).draw(false);
}
function GetUnAssignedPro()
{
    debugger;
    var id = $('#ID').val() != "0" ? $('#ID').val() : "";
    DataTables.productTable.clear().rows.add(GetUnAssignedProWithID(id)).draw(false);
    $(':input').each(function () {

        if (this.type == 'checkbox') {
            this.checked = false;
        }
    });
}
function GetAllPro()
{
    debugger;
    DataTables.productTable.clear().rows.add(GetAllProducts()).draw(false);
}
function findcheck()
{
    debugger;
    var checkIds = [];
    $('input[type="checkbox"]:checked', productTable.fnGetNodes()).each(function (i) {
        debugger;
        var tr = $(this).closest('tr');
        checkIds.push(tr);
    });
}