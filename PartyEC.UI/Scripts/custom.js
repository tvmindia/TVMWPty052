var appAddress = window.location.protocol + "//" + window.location.host + "/";   //Retrieving browser Url 
var status = false;
var defer = new $.deferred();
$(document).ready(function () {
    
    defer.progress(function (status) {
        status = status;
        if (status != undefined) {
            defer.resolver();
            if (!status) {
                return false;
            } else {
                return true;
            }
        }
    });
    //menu submenu popup on click 3rd level menus
    $('.navbar a.dropdown-toggle').on('click', function (e) {
        var $el = $(this);
        var $parent = $(this).offsetParent(".dropdown-menu");
        $(this).parent("li").toggleClass('open');

        if (!$parent.parent().hasClass('nav')) {
            $el.next().css({ "top": $el[0].offsetTop, "left": $parent.outerWidth() - 4 });
        }

        $('.nav li.open').not($(this).parents("li")).removeClass("open");

        return false;
    });
   
    $(".dropdown, .btn-group").hover(function () {
        var dropdownMenu = $(this).children(".dropdown-menu");
        if (dropdownMenu.is(":visible")) {
            dropdownMenu.parent().toggleClass("open");
        }
    });
   
});

function notyAlert(type,msgtxt) {
    var n = noty({
        text: msgtxt,
        type: type,//'alert','information','error','warning','notification','success'
        dismissQueue: true,
        timeout: 3000,
        layout: 'top',
        theme: 'defaultTheme',//closeWith: ['click'],
        maxVisible: 5
    });
   
}
function PostDataToServer(page,formData)
{
    debugger;
    var jsonResult={};
    $.ajax({
        type: "POST",
        url: appAddress+page,
        async: true,
        data: formData,
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            jsonResult = data;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            notyAlert('error', errorThrown + ',' + textStatus + ',' + jqXHR.statusText);
        },
        complete:function()
        {
           
        }

    });
    return jsonResult;
}


function GetDataFromServer(page, formData) {
    debugger;
    var jsonResult = {};
    $.ajax({
        
        type: "GET",
        url: appAddress + page,
        data:formData,
        async: false,
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
         jsonResult = data;
        },
        error: function (jqXHR, textStatus, errorThrown) {
          notyAlert('error',errorThrown + ',' + textStatus + ',' + jqXHR.statusText);
        },
        complete: function () {
          
        }

    });
    return jsonResult;
}
function ChangeButtonPatchView(Controller,Dom, Action) {
    var data = { ActionType: Action };
    var ds = {};
    ds = GetDataFromServer(Controller + "/ChangeButtonStyle/", data);
    if (ds == "Nochange")
    {
        return;
    }
    $("#" + Dom).empty();
    $("#" + Dom).html(ds);
}

function NetworkFailure(data, status, xhr) {
    var i = JSON.parse(data)
    notyAlert('error', status);
}

function showConfirm(defer)
{
    var _self = this;
    var status = undefined;
    var n = noty({
        text: 'Are you sure you want to delete?',
        type: 'confirm',
        dismissQueue: false,
        layout: 'topRight',
        theme: 'metroui',
        buttons: [{addClass: 'btn btn-primary', text: 'Ok', onClick: function($noty) {
            _self.status = true;
            $noty.close();
            // return true;
            defer.notify(_self.status);
    }
},
{addClass: 'btn btn-danger', text: 'Cancel', onClick: function($noty) {
    $noty.close();
    // return false
    defer.notify(_self.status);
}
}
        ]
    })
}


