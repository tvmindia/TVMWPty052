var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function () {
    ChangeButtonPatchView("Review", "btnPatchReviewtab2", "Add"); //ControllerName,id of the container div,Name of the action
    try {
        debugger;
        DataTables.supplierTable = $('#tblReview').DataTable(
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
               { "data": "Rating","defaultContent": "<i>-</i>" },
               { "data": "RatingDate", "defaultContent": "<i>-</i>" },
               { "data": "IsApproved", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ]
         });
    }
    catch (e) {
        notyAlert('error', e.message);

    }
});

function GetAllReviews() {
    try {
        var data = {};
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