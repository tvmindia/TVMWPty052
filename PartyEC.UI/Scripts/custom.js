var appAddress = window.location.protocol + "//" + window.location.host + "/";   //Retrieving browser Url 
$(document).ready(function () {
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
});

function PostDataToServer(page,formData)
{
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
        error: function (xmlhttprequest, textstatus, message) {
            //message.code will be:-timeout", "error", "abort", and "parsererror"
            alert(errorThrown + ',' + textstatus + ',' + jqXHR.statusText);
        },
        complete:function()
        {
           
        }

    });
    return jsonResult;
}


function GetDataFromServer(page, formData) {
    var jsonResult = {};
    $.ajax({
        type: "GET",
        url: appAddress + page,
        data: formData,
        async: false,
        cache: false,
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        success: function (data) {
            jsonResult = data;
            
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //message.code will be:-timeout", "error", "abort", and "parsererror"
            alert(errorThrown + ',' + textstatus + ',' + jqXHR.statusText);
        },
        complete: function () {
          
        }

    });
    return jsonResult;
}



