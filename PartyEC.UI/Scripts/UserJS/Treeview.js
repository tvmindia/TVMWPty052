$(document).ready(function () {
    debugger;
    $('#jstree_Drag').jstree({
        "core": {
            "themes": {
                "responsive": false
            },
            // so that create works
            "check_callback": true,
            'data': GetTreeData()
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
    $('#jstree_DragUpdate').jstree({
        "core": {
            "themes": {
                "responsive": false
            },
            // so that create works
            "check_callback": true,
            'data': [{
                "text": "Cake",
                "icon":"fa fa-folder-open",
                "state": {
                    "selected": true
                },
                "children": [{
                    "text": "Product",
                    "icon": "fa fa-product-hunt"
                }, {
                    "text": "Order",
                    "icon": "fa fa-first-order",
                }, {
                    "text": "Rating",
                    "icon": "fa fa-star-half-o"
                }]
                    
                }]
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

function GetTreeData() {
    debugger;
    try {
        
        var ds = {};
        ds = GetDataFromServer("DynamicUI/GetTreeListForAttributeSet/", "");
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