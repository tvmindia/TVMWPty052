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
            $('#tblCategoryProduct').on('change', 'input[type="checkbox"]', function () {
                $(this).closest('tr').toggleClass('selected');
            });
            $('#divOverlayimage').show();
    }
    catch(e)
    {

    }
   

});
//on-select function for treenodes
function onSelectNode(e, data)
{
    try
    {
        if (data.node.parent == "#")
        {
            $('#ParentID').val(0);
        }
        else
        {
            $('#ParentID').val(data.node.parent);
        }
        $('#ID').val(data.node.id);
        $('#hdnDeleteCatID').val(data.node.id);
        var results = GetCategoryDetails(data.node.id);
        $('#Name').val(results.Name);
        $('#Description').val(results.Description);
        $("#Navigation").prop('checked', results.Navigation);
        $("#Filter").prop('checked', results.Filter);
        $("#Enable").prop('checked', results.Enable);
        $("#ImageID").val(results.ImageID);
        $("#imgCategory").attr('src', (results.URL != "" ? (results.URL + '?' + new Date().getTime()) : "/Content/images/NoImageFound.png"));
        ChangeButtonPatchView("Categories", "btnPatchAttributeSettab", "Edit");
        $('#divOverlayimage').hide();
        //var TreeOrder = $("#jstree_Categories").jstree(true).get_json('#', { 'flat': true });
        var loMainSelected = data;
        uiGetParents(loMainSelected);
    }
    catch(e)
    {

    }
}
//Function for binding breadcrum
function uiGetParents(loSelectedNode, TreeOrder) {
    try {
        var lnLevel = loSelectedNode.node.parents.length;
        var lsSelectedID = loSelectedNode.node.id;
        var loParent = $('#jstree_Categories').jstree(true).get_node(lsSelectedID);;
        var lsParents = '<li class="active">'+loSelectedNode.node.text + ' </li>';
        var loParent = loParent.parents;
        for (var ln = 0; ln <= lnLevel - 1 ; ln++) {
            if(loParent[ln]!='#')
            {
                lsParents = '<li><a href="#">' + ($('#jstree_Categories').jstree(true).get_node(loParent[ln])).text + "</a></li> " + lsParents;
            }
            if(loParent[ln]=='#')
            {
                lsParents = '<li><a href="#"><i class="fa fa fa-list"></i></a></li>' + lsParents;
            }
        }
        if (lsParents.length > 0) {
            lsParents = lsParents.substring(0, lsParents.length - 1);
        }
        $('#olCategory').empty();
        $('#olCategory').append(lsParents);
    }
    catch (err) {
        alert('Error in uiGetParents');
    }
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
function GetAllProducts(id) {

    try {
        debugger;
        var data = {"CategoryID":id};
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
    try
    {
        $('#divOverlayimage').show();
        ClearFields();
        $('#ParentID').val($('#ID').val());
        $('#imgCategory').attr('src', '/Content/images/NoImageFound.png');
        ChangeButtonPatchView("Categories", "btnPatchAttributeSettab", "AddSub");
        
        $('#ID').val(0);
        var fluCategory = document.getElementById('CategoryImageUpload');
        fluCategory.value = "";
        fluCategory.innerHTML = "No file chosen";
        

    }
    catch(e)
    {

    }
    
}
function AddCategory() {
    try
    {
        debugger;
        $('#divOverlayimage').show();
        ClearFields();
        $('#ID').val(0);
        $('#ParentID').val(0);
        $('#imgCategory').attr('src', '/Content/images/NoImageFound.png');
        ChangeButtonPatchView("Categories", "btnPatchAttributeSettab", "Add");
        $('#jstree_Categories').jstree("deselect_all");
        $('#olCategory').empty();
        $('#olCategory').append('<li><a href="#"><i class="fa fa fa-list"></i></a></li>');
        var fluCategory = document.getElementById('CategoryImageUpload');
        fluCategory.value = "";
        fluCategory.innerHTML = "No file chosen";
        

    }
    catch(e)
    {

    }
   
}
function MainClick()
{
    $('#btnFormSave').click();
}
function SaveAddorRemove()
{
    $('#btnFormTableData').click();
}
function AddProductLink()
{
    try
    {
        debugger;
        var AddList = [];
        var DeleteList = [];
        var tabledata = DataTables.productTable.rows('.selected').data();
        for (var i = 0; i < tabledata.length; i++) {
            if (tabledata[i].LinkID == 0) {
                var ProductCategoryLinkViewModel = new Object();
                ProductCategoryLinkViewModel.ProductID = tabledata[i].ID;
                ProductCategoryLinkViewModel.CategoryID = $('#ID').val();
                AddList.push(ProductCategoryLinkViewModel);
            }
            else if (tabledata[i].LinkID != 0) {
                var ProductCategoryLinkViewModel = new Object();
                ProductCategoryLinkViewModel.ID = tabledata[i].LinkID;
                ProductCategoryLinkViewModel.ProductID = tabledata[i].ID;
                ProductCategoryLinkViewModel.CategoryID = tabledata[i].CategoryID;
                DeleteList.push(ProductCategoryLinkViewModel);
            }
        }
        $('#hdnTableDataAdd').val(JSON.stringify(AddList));
        $('#hdnTableDataDelete').val(JSON.stringify(DeleteList));
    }
    catch(e)
    {

    }
    
}
////////////////////////////////////////Onclick for Radio button
function GetAssignedPro()
{
    debugger;
    var id=$('#ID').val()!="0"?$('#ID').val():"";
    DataTables.productTable.clear().rows.add(GetAssignedProWithID(id)).draw(false);
    $("#rdoproductAssigned").prop("checked", true);
    ChangeButtonPatchView("Categories", "btnPatchAttributeSettab", "tab2");
    $('#divOverlay').show();
}
function GetUnAssignedPro()
{
    debugger;
    var id = $('#ID').val() != "0" ? $('#ID').val() : "";
    DataTables.productTable.clear().rows.add(GetUnAssignedProWithID(id)).draw(false);
}
function GetAllPro()
{
    debugger;
    var id = $('#ID').val();
    DataTables.productTable.clear().rows.add(GetAllProducts(id)).draw(false);
}
function TabRedirect()
{
    ChangeButtonPatchView("Categories", "btnPatchAttributeSettab", "tab1");
    $('#divOverlay').hide();
}
function DeleteCategory()
{
    $('#btnFormDeleteCategory').click();
}

function CheckSubmittedDelete(data) { //function CouponSubmitted(data) in the question
    debugger;
    var i = JSON.parse(data.responseText)
    switch (i.Result) {
        case "OK":
            notyAlert('success', i.Records.StatusMessage);
            DataTables.attributeSetTable.clear().rows.add(GetAllAttributeSet()).draw(false);
            //ChangeButtonPatchView(//ControllerName,//Name of the container, //Name of the action);
            ChangeButtonPatchView("AttributeSet", "btnPatchAttributeSettab2", "Add");
            $('#tabattributeSetDetails').trigger('click');
            break;
        case "ERROR":
            notyAlert('success', i.Records.StatusMessage);
            break;

    }

}
function CheckSubmittedInsertCategory(data) { //function CouponSubmitted(data) in the question
    debugger;
    var i = JSON.parse(data.responseText)
    switch (i.Result) {
        case "OK":
            notyAlert('success', i.Records.StatusMessage);
            $('#jstree_Categories').jstree(true).settings.core.data = GetCategoriesTree();
            $('#jstree_Categories').jstree(true).refresh(true);
            //ChangeButtonPatchView(//ControllerName,//Name of the container, //Name of the action);
            ChangeButtonPatchView("Categories", "btnPatchAttributeSettab", "Edit");
            if (i.Records.ReturnValues != null)
            {
                $('#ID').val(i.Records.ReturnValues);
            }
            $('#divOverlayimage').hide();
            break;
        case "ERROR":
            notyAlert('error', i.Records.StatusMessage);
            break;

    }

}
function CheckSubmittedDeleteCategory(data) {
    var i = JSON.parse(data.responseText)
    switch (i.Result) {
        case "OK":
            notyAlert('success', i.Records.StatusMessage);
            $('#jstree_Categories').jstree(true).settings.core.data = GetCategoriesTree();
            $('#jstree_Categories').jstree(true).refresh(true);
            AddCategory();
            break;
        case "ERROR":
            notyAlert('error', i.Records.StatusMessage);
            break;
    }
}

