$(document).ready(function () {
    debugger;
    //Tree bind into right side container
    $('#jstree_Drag').jstree({
        "core": {
            "themes": {
                "responsive": false
            },
            // so that create works
            "check_callback": true,
            'data': GetTreeDataLeft()
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
        "plugins": ["contextmenu", "dnd", "state", "types"]
    });
    
});

function GetTreeDataRight(id) {
    debugger;
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
function GetTreeDataLeft()
{
    debugger;
    try {

        var ds = {};
        ds = GetDataFromServer("DynamicUI/GetTreeListAttributes/", "");
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
    debugger;
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
function EditAttibuteSet()
{
    debugger;
    try
    {
        var id = $('#Name').val();
        //Tree bind into the right side container

        $('#jstree_DragUpdate').jstree({
            "core": {
                "themes": {
                    "responsive": false
                },
                // so that create works
                "check_callback": true,
                'data': GetTreeDataRight(id)
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
            "plugins": ["contextmenu", "dnd", "state", "types"]
        });
    }
    catch(e)
    {

    }
}
function SaveOrder()
{
    debugger;
    var TreeOrderFormatted = [];
    var TreeOrder = $("#jstree_DragUpdate").jstree(true).get_json('#', { 'flat': true });
    var j = 0;
    for (var i = 0; i < TreeOrder.length; i++) {

        var AttributeSetLinkViewModel = new Object();
        AttributeSetLinkViewModel.AttributeID = $.trim(TreeOrder[i].id.replace('C', ''));
        AttributeSetLinkViewModel.AttributeSetID = TreeOrder[i].parent;
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
    PostSaveOrder(TreeOrderFormatted,id=1);
}