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
                  'targets': 3,
                  'render': function (data, type, row) {                   
                      if (data)
                          if (data.length > 30)
                          {   var newdata = data.substring(0, 30);
                              return newdata +'.....';
                          }
                          else  {  return data ; }                  
                  }},
                  {
                      'targets': 7,
                 'render': function (data, type, row) { 
                     if (row.IsApproved =='True') {
                         return 'Approved';   }
                     else {
                         return 'Pending';   } }
             }]
         }); 
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
    debugger;
    var res = DateValidation();
    if (res)
    {
        BindAllReviews();
    }
    else {
        return false;
    } 
}

function DateValidation() {
    debugger;
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