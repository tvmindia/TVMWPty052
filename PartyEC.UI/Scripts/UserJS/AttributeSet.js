var DataTables = {};
$(document).ready(function () {
    
    //Tree bind into right side container
    $('#jstree_Drag').jstree({
        "core": {
            "themes": {
                "responsive": false
            },
            // so that create works
            "check_callback": true,
            'data': GetTreeDataLeft(0)
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

    //Tree bind into the right side container

    $('#jstree_DragUpdate').jstree({
        "core": {
            "themes": {
                "responsive": false
            },
            // so that create works
            //"check_callback": true,
            'check_callback': function (operation, node, node_parent, node_position, more) {
                // operation can be 'create_node', 'rename_node', 'delete_node', 'move_node' or 'copy_node'
                // in case of 'rename_node' node_position is filled with the new node name
                
                if (operation === "move_node") {
                    return node_parent.id === node.parent; //only allow dropping inside nodes of type 'Parent'
                }
                return true;  //allow all other operations
            },
            'data': GetTreeDataRight(0)
        },
        "types": {
            "default": {
                "icon": "fa fa-folder icon-state-warning icon-lg"
            },
            "file": {
                "icon": "fa fa-file icon-state-warning icon-lg"
            }
        },
        "contextmenu" : {
            "items" : function ($node) {
                return {
                    
                    "Delete" : {
                        "label" : "Delete",
                        "action": function (data) {
                            var inst = $.jstree.reference(data.reference),
                                obj = inst.get_node(data.reference);
                            if (inst.is_selected(obj)) {
                                inst.delete_node(inst.get_selected());
                            }
                            else {
                                inst.delete_node(obj);
                            }
                        }
                    }
                };
            }
        },
        "state": { "key": "demo2" },
        "plugins": ["dnd", "state", "types", "contextmenu"]
    });
    DataTables.attributeSetTable = $('#tblAttributeSet').DataTable(
    {
        dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
        order: [],
        searching: true,
        paging: true,
        data: GetAllAttributeSet(),
        columns: [
          { "data": "ID" },
          { "data": "Name" },
          { "data": null, "orderable": false, "defaultContent": '<a onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
        ],
        columnDefs: [
         {
             "targets": [0],
             "visible": true,
             "searchable": true
         }
        ]
    });
   
    $('#tabattributeSetDetails').click(function (e) {
        //ChangeButtonPatchView(//ControllerName,//Name of the container, //Name of the action);
        ChangeButtonPatchView("AttributeSet", "btnPatchAttributeSettab2", "Add");
        $('#jstree_Drag').jstree(true).settings.core.data = GetTreeDataLeft(0);
        $('#jstree_Drag').jstree(true).refresh(true);

        $('#jstree_DragUpdate').jstree(true).settings.core.data = GetTreeDataRight(0);
        $('#jstree_DragUpdate').jstree(true).refresh(true);

        $('#Name').val('');
        $('#ID').val(0);
        $('#hdnTreeList').val("0");
    });
   
    
});
function MainClick()
{
    
    $('#btnFormSave').click();
}
function AddNew()
{
    $('#tabattributeSetDetails').trigger('click');
}
function Edit(currentObj) {
    //Tab Change on edit click
    $('#tabattributeSetDetails').trigger('click');
    
    var rowData = DataTables.attributeSetTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {

        EditAttibuteSet(rowData.ID);
        $("#ID").val(rowData.ID);
        $("#hdnDelete").val(rowData.ID);
        $("#Name").val(rowData.Name);
    }
    //ChangeButtonPatchView(//ControllerName,//Name of the container, //Name of the action);
    ChangeButtonPatchView("AttributeSet", "btnPatchAttributeSettab2", "Edit");
}

function GetAllAttributeSet() {

    try {
        var data = "";
        var ds = {};
        ds = GetDataFromServer("AttributeSet/GetAllAttributeSet/", data);
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
function GetTreeDataRight(id) {
    try {
        var data = {"ID":id};
        var ds = {};
        ds = GetDataFromServer("DynamicUI/GetTreeListForAttributeSet/", data);
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
function GetTreeDataLeft(id)
{
    try {

        var ds = {};
        data = { "ID": id };
        ds = GetDataFromServer("DynamicUI/GetTreeListAttributes/", data);
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
function PostSaveOrder(AttributeSetLinkViewModel,id)
{
    try {
        
        var data = "{'Treeview':" + JSON.stringify(AttributeSetLinkViewModel) + ",'ID':"+id+"}";
        var ds = {};
        ds = PostDataToServer("AttributeSetLink/PostTreeOrder/", data);
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
function EditAttibuteSet(id)
{
    try
    {
        $('#jstree_Drag').jstree(true).settings.core.data = GetTreeDataLeft(id);
        $('#jstree_Drag').jstree(true).refresh(true);

        $('#jstree_DragUpdate').jstree(true).settings.core.data = GetTreeDataRight(id);
        $('#jstree_DragUpdate').jstree(true).refresh(true);
    }
    catch(e)
    {

    }
    
}
function SaveOrder()
{
    var TreeOrderFormatted = [];
    var TreeOrder = $("#jstree_DragUpdate").jstree(true).get_json('#', { 'flat': true });
    var j = 0;
    for (var i = 0; i < TreeOrder.length; i++) {

        var AttributeSetLinkViewModel = new Object();
        AttributeSetLinkViewModel.AttributeID = parseInt($.trim(TreeOrder[i].id.replace('C', '')));
        AttributeSetLinkViewModel.AttributeSetID = parseInt(TreeOrder[i].parent!='#'?TreeOrder[i].parent:0);
        if (TreeOrder[i].parent == "#")
        {
            AttributeSetLinkViewModel.DisplayOrder = TreeOrder[i].id;
            j = 0;
        }
        else
        {
            AttributeSetLinkViewModel.DisplayOrder = TreeOrder[i].parent + "." + (1 + j);
            j++;
        }
        //AttributeSetLinkViewModel.text = TreeOrder[i].text;
        TreeOrderFormatted.push(AttributeSetLinkViewModel);
    }
    $('#hdnTreeList').val(JSON.stringify(TreeOrderFormatted));
}
function CheckSubmitted(data) { //function CouponSubmitted(data) in the question
    debugger;
    var i = JSON.parse(data.responseText)
    switch(i.Result)
    {
        case "OK":
            debugger;
            notyAlert('success', i.Records.StatusMessage);
            if (i.Records.ReturnValues)
            {
                $('#ID').val(i.Records.ReturnValues);
            }
            //ChangeButtonPatchView(//ControllerName,//Name of the container, //Name of the action);
            ChangeButtonPatchView("AttributeSet", "btnPatchAttributeSettab2", "Edit");
            $('#jstree_Drag').jstree(true).settings.core.data = GetTreeDataLeft($("#ID").val());
            $('#jstree_Drag').jstree(true).refresh(true);

            $('#jstree_DragUpdate').jstree(true).settings.core.data = GetTreeDataRight($("#ID").val());
            $('#jstree_DragUpdate').jstree(true).refresh(true);

            DataTables.attributeSetTable.clear().rows.add(GetAllAttributeSet()).draw(false);
            break;
        case "ERROR":
            notyAlert('success', i.Records.StatusMessage);
            break;
            
    }
    
}
function CheckSubmittedDelete(data) { //function CouponSubmitted(data) in the question
    
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
function Delete()
{
    $('#btnFormDelete').click();
}