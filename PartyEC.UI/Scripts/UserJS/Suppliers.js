var DataTables = {};
//---------------------------------------Docuement Ready--------------------------------------------------//
$(document).ready(function () {
    try {
        debugger;
        //var AttributesViewModel = new Object();
        DataTables.supplierTable = $('#tblsuppliers').DataTable(
         {
             dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllSuppliers(),
             columns: [
               { "data": "ID" },
               { "data": "Name" },
               { "data": "CreatedDate", "defaultContent": "<i>-</i>" }, 
               { "data": null, "orderable": false, "defaultContent": '<a onclick="Edit(this)"<i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{
                 "render": function (data, type, row) {
                     debugger;
                     var str = Date.parse(data);
                     var res = ConvertJsonToDate('' + str + '');
                     return res;
                 },
                 "targets": 2
             }]
         });
    }
    catch (e) {
        notyAlert('error', e.message);

    }
});

function GetAllSuppliers() {
    try {
        debugger;
        var data = { };
        var ds = {};
        ds = GetDataFromServer("Supplier/GetAllSuppliers/", data);
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
        notyAlert('error', e.message);
    }
}



//---------------------------------------Edit Attributes--------------------------------------------------//
function Edit(currentObj) {
    //Tab Change on edit click
    debugger;
    $('#tabSupplierDetails').trigger('click');
 
    var rowData = DataTables.supplierTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        fillSupplier(rowData.ID);
    }
}
//---------------------------------------Fill Attributes--------------------------------------------------//
function fillSupplier(ID) {
    debugger;
    var thissupplier = GetSupplierByID(ID); //Binding Data
    //Hidden
    $("#ID").val(thissupplier.ID)
    $("#deleteId").val(thissupplier.ID)//for delete action    
    //Textboxes
    $("#Name").val(thissupplier.Name);
    $("#Name").attr('disabled', true);
 
}
//---------------------------------------Clear Fields-----------------------------------------------------//
function clearfields() {
    $("#Name").attr('disabled', false);
    $("#ID").val("0")//ID is zero for New
    $("#deleteId").val("0")
    $("#Name").val("")
  
}
//---------------------------------------Button Patch Click Events------------------------------------------------//
function btnreset() {
    if ($("#ID").val() == 0) {
        clearfields();
    }
    else { 
        fillSupplier($("#ID").val())
    }
}
//---------------------------------------Save-------------------------------------------------------//
function clicksave() {
    var res = Validation();
    if (res) {
        $("#btnFormSave").click();
    }
}
//---------------------------------------Back-------------------------------------------------------//
function goback() {
    $('#tabSupplierList').trigger('click');
}
//---------------------------------------Delete-------------------------------------------------------//
function clickdelete() {
    var id = $("#ID").val();
    if (id != 0) {
        $("#btnFormDelete").click();
    }
    else {
        notyAlert('error', 'Please Select Attributes');
    }
}

function Validation() { 
    return true;
}

//---------------------------------------Add New Click----------------------------------------------------//
function btnAddNew() {
    $('#tabattributeDetails').trigger('click');
    clearfields();
}


//---------------------------------------Get Attributes Details By ID-------------------------------------//
function GetSupplierByID(id) {
    try {
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Supplier/GetSupplier/", data);
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
        notyAlert('error', e.message);
    }
}