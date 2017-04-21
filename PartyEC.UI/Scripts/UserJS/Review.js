var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function () {
    try {
        ChangeButtonPatchView("Reviews", "btnPatchReviewtab2", "Edit");
        DataTables.ReviewTable = $('#tblReview').DataTable(
         {

             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllReviews(),
             columns: [
               { "data": "ID" },
               { "data": "ProductID" },
               { "data": "ProductName" }, 
               { "data": "CustomerName" },
               { "data": "Review" },
               { "data": "ReviewCreatedDate", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="ModelProductsRating(this)">Rating</a>' }, 
               { "data": "RatingDate", "defaultContent": "<i>-</i>" },
               { "data": "IsApproved", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [ {//hiding hidden column 
                 "targets": [0],
                 "visible": false,
                 "searchable": false
             },
              {
                  'targets': [6],
                  "visible": false,
                  "searchable": false
              },
                {
                 'targets': 4, 'render': function (data, type, row) {
                     if (data)
                         if (data.length > 30)
                         {
                             var newdata = data.substring(0, 30);
                             return newdata + '.....';
                         }
                         else { return data; }
                 }
             }, {
                 'targets': 8, 'render': function (data, type, row) {
                     if (row.IsApproved == 'True') {
                         return 'Approved';
                     }
                     else { return 'Pending'; }
                 }
             },
 {
     'targets': 7, 'render': function (data, type, row) {
      
         if (row.RatingDate == null) { 
             return row.RatingCreatedDate;
         }
         else { 
             return row.RatingDate
         }
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

function GetReviewByID(id) {
    try {
        debugger;
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Reviews/GetReview/", data);
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

//---------------------------------------Edit Suppliers--------------------------------------------------//
function Edit(currentObj) {
    //Tab Change on edit click
    debugger;
    $('#tabReviewDetails').trigger('click');
    ChangeButtonPatchView("Reviews", "btnPatchReviewtab2", "Edit");//ControllerName,id of the container div,Name of the action

    var rowData = DataTables.ReviewTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        fillReview(rowData.ID);
        var thisRatingSummary = GetProductRatingByCustomer(rowData.ProductID, rowData.CustomerID,rowData.AttributeSetID);
        if (thisRatingSummary.length > 0) {
            $("#CustomerRating").empty();
            var ratinglists=ratingArea(thisRatingSummary)
            $("#CustomerRating").append(ratinglists);
        }//if
        else {
            $("#CustomerRating").empty();
            var ratinglists = '<div class="col-xs-12 text-center"><h3>No Ratings Yet.. </h3></div>'
            $("#CustomerRating").append(ratinglists);
        }


    }
}
//---------------------------------------Fill Suppliers--------------------------------------------------//
function fillReview(ID) {
    debugger;
    ChangeButtonPatchView("Supplier", "btnPatchSupplierstab2", "Edit");
    var thisReview = GetReviewByID(ID); //Binding Data  
    $("#ID").val(thisReview.ID);
    $("#ProductID").val(thisReview.ProductID);
    $("#ProductName").val(thisReview.ProductName);
    $("#CustomerName").val(thisReview.CustomerName);
    $("#Review").val(thisReview.Review);
    $("#ReviewCreatedDate").val(thisReview.ReviewCreatedDate);
    $("#RatingDate").val(thisReview.RatingDate);
    if (thisReview.IsApproved=='False')
    $("#IsApproved").val('Not Approved');
    else
        $("#IsApproved").val('Approved');

 
    

}


//---------------------------------------Back-------------------------------------------------------//
function goback() {
    $('#tabReviewList').trigger('click');
}


function clickapprove() {
    debugger;
    
    $('#btnFormUpdate').trigger('click');

}


function SaveSuccess(data, status, xhr) {
    BindAllReviews();
    var i = JSON.parse(data)
    debugger;

    switch (i.Result) {

        case "OK":
            notyAlert('success', i.Record.StatusMessage);
            var returnId = i.Record.ReturnValues
            fillReview(returnId);
            break;
        case "Error":
            notyAlert('error', i.Record.StatusMessage);
            break;
        case "ERROR":
            notyAlert('error', i.Message);
            break;
        default:
            break;
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
        fromdate = ConvertDateFormats(fromdate);
        todate = ConvertDateFormats(todate);
        var date1 = new Date(fromdate);
        var date2 = new Date(todate);
        var diff = date2.getTime() - date1.getTime();        
        if (diff >= 0) {
            var ONE_DAY = 1000 * 60 * 60 * 24;
            $("#dayscount").text((Math.round(diff / ONE_DAY)+1) + ' Days');
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

//----------------------------------------------------ModelProductsRating--------------------------------------------------------------//

function ModelProductsRating(currentObj) {
    //popsup the model
    debugger;
    var rowData = DataTables.ReviewTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null) && (rowData.CustomerID != null)) {
        $("#titleProductRating").text(rowData.ProductName);
        var thisRatingSummary = GetProductRatingByCustomer(rowData.ProductID, rowData.CustomerID,rowData.AttributeSetID);
        if (thisRatingSummary.length > 0) {
            $("#RatingPopupDisplay").empty();
            var ratinglists=ratingArea(thisRatingSummary)
            //var attributecount = thisRatingSummary[0].ProductRatingAttributes.length
            //var ratinglists = ""
            //for (var i = 0; i < attributecount; i++) {
            //    var ratingstar = parseFloat(thisRatingSummary[0].ProductRatingAttributes[i].Value);
            //    ratingstar = Math.round(ratingstar); //count of Rating to display as star
            //    var ratebtnstring = '';
            //    for (var count = 0; count < 5; count++) {
            //        if (count < ratingstar) {
            //            ratebtnstring = ratebtnstring + '<button type="button" class="btn btn-warning btn-xs" aria-label="Left Align"><span class="glyphicon glyphicon-star" aria-hidden="true"></span></button>'
            //        }
            //        else {
            //            ratebtnstring = ratebtnstring + '<button type="button" class="btn btn-default btn-xs" aria-label="Left Align"><span class="glyphicon glyphicon-star" aria-hidden="true"></span></button>'
            //        }
            //    }//for
            //    ratinglists = ratinglists + '<div class="col-xs-5 ">' + thisRatingSummary[0].ProductRatingAttributes[i].Caption + '</div>' +
            //                                '<div class="col-xs-7 "><div  class="rating-block">' + ratebtnstring + '</div></div>';
            //}//for
            $("#RatingPopupDisplay").append(ratinglists);
        }//if
        else {
            $("#RatingPopupDisplay").empty();
            var ratinglists = '<div class="col-xs-12 text-center"><h3>No Ratings Yet.. </h3></div>'
            $("#RatingPopupDisplay").append(ratinglists);
        }
    }//if
    $('#btnmodelproductrating').trigger('click');
}


function ratingArea(thisRatingSummary)
{
    var attributecount = thisRatingSummary[0].ProductRatingAttributes.length
    var ratinglists = ""
    for (var i = 0; i < attributecount; i++) {
        var ratingstar = parseFloat(thisRatingSummary[0].ProductRatingAttributes[i].Value);
        ratingstar = Math.round(ratingstar); //count of Rating to display as star
        var ratebtnstring = '';
        for (var count = 0; count < 5; count++) {
            if (count < ratingstar) {
                ratebtnstring = ratebtnstring + '<button type="button" class="btn btn-warning btn-xs" aria-label="Left Align"><span class="glyphicon glyphicon-star" aria-hidden="true"></span></button>'
            }
            else {
                ratebtnstring = ratebtnstring + '<button type="button" class="btn btn-default btn-xs" aria-label="Left Align"><span class="glyphicon glyphicon-star" aria-hidden="true"></span></button>'
            }
        }//for
        ratinglists = ratinglists + '<div class="col-xs-5 ">' + thisRatingSummary[0].ProductRatingAttributes[i].Caption + '</div>' +
                                    '<div class="col-xs-7 "><div  class="rating-block" style="padding-bottom: 5px;">' + ratebtnstring + '</div></div>';
    }//for
return ratinglists
}





function GetProductRatingByCustomer(productid, customerid, AttributesetID) {
    try {
        var data = { "productid": productid, "customerid": customerid, "AttributesetID": AttributesetID };
        var ds = {};
        ds = GetDataFromServer("Reviews/GetProductRatingByCustomer/", data);

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

//------------------------------------------------------------------------------------------------------------------//