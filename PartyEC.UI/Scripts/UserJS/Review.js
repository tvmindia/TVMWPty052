var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function () {
    try { 
        DataTables.ReviewTable = $('#tblReview').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllReviews(),
             columns: [
               { "data": "ProductID" },
               { "data": "ProductName" },
               { "data": "CustomerName" },
               { "data": "Review" },
               { "data": "ReviewCreatedDate", "defaultContent": "<i>-</i>" },
               { "data": "Rating", "defaultContent": '<a href="#">Rating</a>' },
               { "data": "RatingDate", "defaultContent": "<i>-</i>" },
               { "data": "IsApproved", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{
                 'targets': 3, 'render': function (data, type, row) {
                     if (data)
                         if (data.length > 30)
                         {
                             var newdata = data.substring(0, 30);
                             return newdata + '.....';
                         }
                         else { return data; }
                 }
             }, {
                 'targets': 7, 'render': function (data, type, row) {
                     if (row.IsApproved == 'True') {
                         return 'Approved';
                     }
                     else { return 'Pending'; }
                 }
             }
          ]
         });
        $("#rdoreviewPending").prop('checked', true);
    }
    catch (e) { 
        notyAlert('error', e.message);
    } 
  
});

function GetAllReviews() {
    try {  
         var condition= $('input[name=REVIEW]:checked', '#MYFORM').val();
         var FromDate;
         var ToDate;
         if ($("#fromdate").val() != "") {
             FromDate = $("#fromdate").val();
         }
         else { FormDate = null; }              
         if ($("#todate").val() != "") {
             ToDate = $("#todate").val();
         }
         else { ToDate = null; }
      
       
         var data = { "Condition": condition, "FromDate": FromDate, "ToDate": ToDate };
        var ds = {};
        ds = GetDataFromServer("Reviews/GetAllReviews/", data);
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
        notyAlert('error', e.message);
    }
}

//---------------------------------------Bind All Reviews----------------------------------------------//

 
function BindAllReviews() {
    try {
        DataTables.ReviewTable.clear().rows.add(GetAllReviews()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function BindReviews()
{
    var res1 = countDays();
    var res = DateValidation();
    if (res && res1)
    {
        BindAllReviews();
    }
    else {
        return false;
    } 
}

function DateValidation() {
    var fromdate = $("#fromdate").val();
    var todate = $("#todate").val(); 
    if (fromdate == "" && todate != "") {
    notyAlert('error', 'Fill From Date');
    return false;
    }     
    else if (todate == "" &&  fromdate != "") {            
    notyAlert('error', 'Fill To Date');
    return false;
    } 
    return true;
}

function countDays() {
    var fromdate = $("#fromdate").val();
    var todate = $("#todate").val();

    if (fromdate != "" && todate != "") {
        var date1 = new Date(fromdate);
        var date2 = new Date(todate);
        var diff = date2.getTime() - date1.getTime();        
        if (diff > 0) {
            var ONE_DAY = 1000 * 60 * 60 * 24;
            $("#dayscount").text(Math.round(diff / ONE_DAY) + ' Days');
        }
        else {
            notyAlert('error', 'Please check the dates entered');
            $("#dayscount").text('');
            return false;
        }
    }
    else {
        $("#dayscount").text('');
    }
    return true;
}