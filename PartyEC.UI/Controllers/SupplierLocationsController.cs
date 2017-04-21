using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    public class SupplierLocationsController : Controller
    {
        #region Constructor_Injection 

        IMasterBusiness _masterBusiness;
        ICommonBusiness _commonBusiness;

        public SupplierLocationsController(IMasterBusiness masterBusiness, ICommonBusiness commonBusiness)
        {
            _commonBusiness = commonBusiness;
            _masterBusiness = masterBusiness;
        }
        #endregion Constructor_Injection 
        // GET: Sup_Delivery
        public ActionResult Index()
        {
            SupplierLocationsViewModel ordrsat_obj = new SupplierLocationsViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();

            List<ShippingLocationViewModel> orderstatusListVM = Mapper.Map<List<ShippingLocations>, List<ShippingLocationViewModel>>(_masterBusiness.GetAllShippingLocation());
            foreach (ShippingLocationViewModel ovm in orderstatusListVM)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = ovm.Name,
                    Value = ovm.ID.ToString(),
                    Selected = false
                });
            }
            ordrsat_obj.LocationList = selectListItem;

            List<SelectListItem> selectListsupplier = new List<SelectListItem>();

            List<SupplierViewModel> supplierListVM = Mapper.Map<List<Supplier>, List<SupplierViewModel>>(_masterBusiness.GetAllSuppliers());
            foreach (SupplierViewModel ovm in supplierListVM)
            {
                selectListsupplier.Add(new SelectListItem
                {
                    Text = ovm.Name,
                    Value = ovm.ID.ToString(),
                    Selected = false
                });
            }
            ordrsat_obj.supplierList = selectListsupplier;

            return View(ordrsat_obj);
        }



        #region GetAllSupplierLocations
        [HttpGet]
        public string GetAllSupplierLocations()
        {
            try
            {
                List<SupplierLocationsViewModel> supplierLocList = Mapper.Map<List<SupplierLocations>, List<SupplierLocationsViewModel>>(_masterBusiness.GetAllSupplierLocations());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = supplierLocList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }

        #endregion GetAllSupplierLocations

        #region GetSupplierLocationsByID

        [HttpGet]
        public string GetSupplierLocations(string ID)
        {
            try
            {
                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                SupplierLocationsViewModel supplierLoc = Mapper.Map<SupplierLocations, SupplierLocationsViewModel>(_masterBusiness.GetSupplierLocations(Int32.Parse(ID), Mapper.Map<OperationsStatusViewModel, OperationsStatus>(operationsStatus)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = supplierLoc });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }


        #endregion GetSupplierLocationsByID


        #region InsertUpdateSupplierLocations

        [HttpPost]
        public string InsertUpdateSupplierLocations(SupplierLocationsViewModel supplierLocObj)
        {
            if (ModelState.IsValid)
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;
                if (supplierLocObj.ID == 0) //Create Supplier
                {
                    try
                    {
                        supplierLocObj.commonObj = new LogDetailsViewModel();
                        supplierLocObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                        supplierLocObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                        OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_masterBusiness.InsertSupplierLocations(Mapper.Map<SupplierLocationsViewModel, SupplierLocations>(supplierLocObj)));
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                    }
                }
                else //Update Supplier
                {
                    try
                    {
                        supplierLocObj.commonObj = new LogDetailsViewModel();
                        supplierLocObj.commonObj.UpdatedBy = _commonBusiness.GetUA().UserName;
                        supplierLocObj.commonObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                        OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_masterBusiness.UpdateSupplierLocations(Mapper.Map<SupplierLocationsViewModel, SupplierLocations>(supplierLocObj)));
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                    }
                }
                if (OperationsStatusViewModelObj.StatusCode == 1)
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "Error", Record = OperationsStatusViewModelObj });
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }

        #endregion InsertUpdateSupplierLocations


        #region DeleteSupplierLocations


        [HttpPost]
        public string DeleteSupplierLocations(SupplierLocationsViewModel supplierLocObj)
        {
            if (ModelState.IsValid)
            {
                if (supplierLocObj.ID != 0)
                {
                    try
                    {
                        OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                        OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_masterBusiness.DeleteSupplierLocations(supplierLocObj.ID));
                        if (OperationsStatusViewModelObj.StatusCode == 1)
                        {
                            return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                        }
                        else
                        {
                            return JsonConvert.SerializeObject(new { Result = "Error", Record = OperationsStatusViewModelObj });
                        }
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                    }
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Select Supplier Location" });
        }

        #endregion DeleteAttributes

        #region ChangeButtonStyle
        [HttpGet]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "SupplierLocationsList":
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Event = "btnAddNew()";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    break;
                case "Edit":
                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Event = "clickdelete()";
                    ToolboxViewModelObj.deletebtn.Title = "Delete";

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "clicksave()";
                    ToolboxViewModelObj.savebtn.Title = "Save";

                    ToolboxViewModelObj.resetbtn.Visible = true;
                    ToolboxViewModelObj.resetbtn.Event = "btnreset()";

                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";

                    break;
                case "Add":
                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Disable = true;
                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "clicksave()";
                    ToolboxViewModelObj.savebtn.Title = "Save";

                    ToolboxViewModelObj.resetbtn.Visible = true;
                    ToolboxViewModelObj.resetbtn.Event = "btnreset()";
                    ToolboxViewModelObj.resetbtn.Title = "Reset";

                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    break;
                default:
                    return Content("Nochange");
            }
            return PartialView("_ToolboxView", ToolboxViewModelObj);
        }
        #endregion ChangeButtonStyle




    }
}