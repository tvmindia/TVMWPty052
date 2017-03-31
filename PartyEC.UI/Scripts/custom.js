var appAddress = window.location.protocol + "//" + window.location.host + "/";   //Retrieving browser Url 

$(document).ready(function () {
  
    $('input[type="date"]').datepicker({
        format: "yyyy-mm-dd",//dd-M-yyyy",
        maxViewMode: 0,
        todayBtn: "linked",
        clearBtn: true,
        autoclose: true,
        todayHighlight: true
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

    $('input').keydown(function (e) {
        var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
        if (key == 13) {
            e.preventDefault();
            var inputs = $(this).closest('form').find(':input:visible');
            inputs.eq(inputs.index(this) + 1).focus();
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
function PostDataToServer(page, formData, callback)
{
   $.ajax({
        type: "POST",
        url: appAddress+page,
        async: true,
        data: formData,
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            callback(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            notyAlert('error', errorThrown + ',' + textStatus + ',' + jqXHR.statusText);
        },
        complete:function()
        {
           
        }

    });
    
}


function GetDataFromServer(page, formData) {
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

//Common function for clearing input fields
function ClearFields() {
    $(':input').each(function () {

        if (this.type == 'text' || this.type == 'textarea' || this.type == 'file'|| this.type == 'search') {
            this.value = '';
        }
        else if (this.type == 'checkbox') {
            this.checked = false;
        }
        else if (this.type == 'select-one' || this.type == 'select-multiple') {
            this.value = '-1';
        }
    });

}

//------Date Formating :Return Result Eg: 01-Jan-2017--------------------//
//Passing value
//before Parse Eg:"2017-03-30T00:00:00"
//after Parse Date Eg:1490832000000  Passing as jsonDate
function ConvertJsonToDate(jsonDate) {
    if (jsonDate != null) {
       // var dateString = jsonDate.substr(6);
        var currentTime = new Date(parseInt(jsonDate));
        var month = currentTime.getMonth();
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        var monthNames = [
                      "Jan", "Feb", "Mar",
                      "Apr", "May", "Jun", "Jul",
                      "Aug", "Sep", "Oct",
                      "Nov", "Dec"
        ];
        var result = day + '-' + monthNames[month] + '-' + year;
        return result;
    }
}

