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
    public class ManufacturersController : Controller
    {
        #region Constructor_Injection 

        IMasterBusiness _masterBusiness;
        ICommonBusiness _commonBusiness;

        public ManufacturersController(IMasterBusiness masterBusiness, ICommonBusiness commonBusiness)
        {
            _commonBusiness = commonBusiness;
            _masterBusiness = masterBusiness;
        }
        #endregion Constructor_Injection 

        // GET: Manufacturers
        public ActionResult Index()
        {
            ManufacturerViewModel Country_obj = new ManufacturerViewModel();
            List<SelectListItem> selectListItem = new List<SelectListItem>();

            List<CountryViewModel> orderstatusListVM = Mapper.Map<List<Country>, List<CountryViewModel>>(_masterBusiness.GetAllCountries());
            foreach (CountryViewModel ovm in orderstatusListVM)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = ovm.Name,
                    Value = ovm.Code,
                    Selected = false
                });
            }
            Country_obj.CountryList = selectListItem;
            return View(Country_obj);
        }


        #region GetAllManufacturers
        [HttpGet]
        public string GetAllManufacturers()
        {
            try
            {
                List<ManufacturerViewModel> ManufacturerList = Mapper.Map<List<Manufacturer>, List<ManufacturerViewModel>>(_masterBusiness.GetAllManufacturers());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = ManufacturerList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }

        #endregion GetAllManufacturers

        //#region GetManufacturerByID

        //[HttpGet]
        //public string GetManufacturers(string ID)
        //{
        //    try
        //    {
        //        OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
        //        ManufacturerViewModel ManufacturerLoc = Mapper.Map<Manufacturer, ManufacturerViewModel>(_masterBusiness.GetManufacturers(Int32.Parse(ID), Mapper.Map<OperationsStatusViewModel, OperationsStatus>(operationsStatus)));
        //        return JsonConvert.SerializeObject(new { Result = "OK", Records = ManufacturerLoc });
        //    }
        //    catch (Exception ex)
        //    {
        //        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}


        //#endregion GetManufacturersByID
        

        //#region InsertUpdateManufacturer

        //[HttpPost]
        //public string InsertUpdateManufacturer(ManufacturerViewModel manufacturerObj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        OperationsStatusViewModel OperationsStatusViewModelObj = null;
        //        if (manufacturerObj.ID == 0) //Create Supplier
        //        {
        //            try
        //            {
        //                manufacturerObj.commonObj = new LogDetailsViewModel();
        //                manufacturerObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
        //                manufacturerObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
        //                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_masterBusiness.InsertManufacturers(Mapper.Map<ManufacturerViewModel, Manufacturer>(manufacturerObj)));
        //            }
        //            catch (Exception ex)
        //            {
        //                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
        //            }
        //        }
        //        else //Update Supplier
        //        {
        //            try
        //            {
        //                manufacturerObj.commonObj = new LogDetailsViewModel();
        //                manufacturerObj.commonObj.UpdatedBy = _commonBusiness.GetUA().UserName;
        //                manufacturerObj.commonObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();
        //                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_masterBusiness.UpdateManufacturers(Mapper.Map<ManufacturerViewModel, Manufacturer>(manufacturerObj)));
        //            }
        //            catch (Exception ex)
        //            {
        //                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
        //            }
        //        }
        //        if (OperationsStatusViewModelObj.StatusCode == 1)
        //        {
        //            return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
        //        }
        //        else
        //        {
        //            return JsonConvert.SerializeObject(new { Result = "Error", Record = OperationsStatusViewModelObj });
        //        }
        //    }
        //    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        //}

        //#endregion InsertUpdateManufacturer


        //#region DeleteManufacturer


        //[HttpPost]
        //public string DeleteManufacturer([Bind(Exclude = "Name")] ManufacturerViewModel ManufacturerObj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (ManufacturerObj.ID != 0)
        //        {
        //            try
        //            {
        //                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
        //                OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_masterBusiness.DeleteManufacturer(ManufacturerObj.ID));
        //                if (OperationsStatusViewModelObj.StatusCode == 1)
        //                {
        //                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
        //                }
        //                else
        //                {
        //                    return JsonConvert.SerializeObject(new { Result = "Error", Record = OperationsStatusViewModelObj });
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
        //            }
        //        }
        //    }
        //    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Select Manufacturer" });
        //}

        //#endregion DeleteAttributes

        #region ChangeButtonStyle
        [HttpGet]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
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