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
        url: page,
        async: true,
        data: formData,
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            jsonResult = JSON.parse(data);
        },
        error: function (xmlhttprequest, textstatus, message) {
            //message.code will be:-timeout", "error", "abort", and "parsererror"
            alert({ text: message.code + ', ' + xmlhttprequest.statusText, type: 'error' });
        },
        complete:function()
        {
            alert("hoooory completd");
        }

    });
}






type: "post",
    url: page,
data: data,
delay: 1,
async: false,
contentType: "application/json; charset=utf-8",
dataType: "json",