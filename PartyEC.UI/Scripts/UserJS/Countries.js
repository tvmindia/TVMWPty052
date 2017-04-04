var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function () {
  
    try {
        DataTables.CountriesTable = $('#tblCountries').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllCountries(),
             columns: [ 
               { "data": "Name" },
               { "data": "Code" }//, 
               //{ "data": null, "orderable": false, "defaultContent": '<a href="#" onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ]
         });
    }
    catch (e) {
        notyAlert('error', e.message);

    }
});

function GetAllCountries() {
    try {
        debugger;
        var data = {};
        var ds = {};
        ds = GetDataFromServer("Countries/GetAllCountries/", data);
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
