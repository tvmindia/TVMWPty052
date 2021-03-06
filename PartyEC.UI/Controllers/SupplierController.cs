﻿using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.CustomAttributes;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    [CustomAuthenticationFilter]
    public class SupplierController : Controller
    {

        #region Constructor_Injection 

        IMasterBusiness _masterBusiness;
        ICommonBusiness _commonBusiness;

        public SupplierController(IMasterBusiness masterBusiness, ICommonBusiness commonBusiness)
        {
            _commonBusiness = commonBusiness;
            _masterBusiness = masterBusiness;
        }
        #endregion Constructor_Injection 

        // GET: Supplier
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole,RoleContants.ManagerRole)]
        public ActionResult Index()
        {
            return View();
        }

        #region GetAllSuppliers
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetAllSuppliers()
        {
            try
            {               
                List<SupplierViewModel> supplierList = Mapper.Map<List<Supplier>, List<SupplierViewModel>>(_masterBusiness.GetAllSuppliers());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = supplierList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }

        #endregion GetAllSuppliers

        #region GetSupplierByID

        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetSupplier(string ID)
        {
            try
            {
                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                SupplierViewModel attribute = Mapper.Map<Supplier, SupplierViewModel>(_masterBusiness.GetSupplier(Int32.Parse(ID), Mapper.Map<OperationsStatusViewModel, OperationsStatus>(operationsStatus)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = attribute });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }


        #endregion GetSupplierByID
        
        #region InsertUpdateSuppliers

        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string InsertUpdateSuppliers(SupplierViewModel supplierObj)
        {
            if (ModelState.IsValid)
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;
                if (supplierObj.ID == 0) //Create Supplier
                {
                    try
                    {
                        supplierObj.commonObj = new LogDetailsViewModel();
                        supplierObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                        supplierObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                        OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_masterBusiness.InsertSupplier(Mapper.Map<SupplierViewModel, Supplier>(supplierObj)));
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
                        supplierObj.commonObj = new LogDetailsViewModel();
                        supplierObj.commonObj.UpdatedBy = _commonBusiness.GetUA().UserName;
                        supplierObj.commonObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                        OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_masterBusiness.UpdateSupplier(Mapper.Map<SupplierViewModel, Supplier>(supplierObj)));
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

        #endregion InsertUpdateAttributes
                
        #region DeleteSupplier


        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string DeleteSupplier(SupplierViewModel  supplierObj)
        {
            if (!ModelState.IsValid)
            {
                if (supplierObj.ID != 0)
                {
                    try
                    {
                        OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                        OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_masterBusiness.DeleteSupplier(supplierObj.ID));
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
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Select Supplier" });
        }

        #endregion DeleteSupplier

        #region ChangeButtonStyle
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "SuppliersList":
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
                    ToolboxViewModelObj.resetbtn.Title = "Reset";

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