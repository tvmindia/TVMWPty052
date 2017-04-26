using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    public class UsersController : Controller
    {
        private IAuthenticationBusiness _authenticationBusiness;
        private ICommonBusiness _commonBusiness;
        public UsersController(IAuthenticationBusiness authenticationBusiness,ICommonBusiness commonBusiness)
        {
            _authenticationBusiness = authenticationBusiness;
            _commonBusiness = commonBusiness;
        }
        // GET: Users
        public ActionResult Index()
        {
           
           
           return View();
        }

        [HttpGet]
        public string GetAllUsersOfSystem()
        {
            try
            {
                List<UserViewModel> userList = Mapper.Map<List<User>, List<UserViewModel>>(_authenticationBusiness.GetAllUsers());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = userList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }

        [HttpGet]
        public string GetUserDetailsByUser(string ID)
        {
            List<UserViewModel> userList = null;
          try
            {
                if((!string.IsNullOrEmpty(ID)))
                {
                    userList = Mapper.Map<List<User>, List<UserViewModel>>(_authenticationBusiness.GetUserDetailByUser(int.Parse(ID)));
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "ID's are Empty!" });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            return JsonConvert.SerializeObject(new { Result = "OK", Records = userList });
        }


        #region UserInsertUpdate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string UserInsertUpdate(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    OperationsStatusViewModel OperationsStatusViewModelObj = null;
                    //INSERT
                    user.logDetails = new LogDetailsViewModel();
                    //Getting UA
                    user.logDetails.CreatedBy = _commonBusiness.GetUA().UserName;
                    user.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();
                    user.logDetails.UpdatedBy = user.logDetails.CreatedBy;
                    user.logDetails.UpdatedDate = user.logDetails.CreatedDate;
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_authenticationBusiness.InsertUpdateUser(Mapper.Map<UserViewModel, User>(user)));
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }
            }
            //Model state errror
            else
            {
                List<string> modelErrors = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        modelErrors.Add(modelError.ErrorMessage);
                    }
                }
                return JsonConvert.SerializeObject(new { Result = "VALIDATION", Message = string.Join(",", modelErrors) });
            }
        }
        #endregion UserInsertUpdate



        #region ChangeButtonStyle
        [HttpGet]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
               

                case "Add":
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Title = "Add";
                    ToolboxViewModelObj.addbtn.Event = "AddUser()";
                    break;

                case "Save":
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Title = "Add";
                    ToolboxViewModelObj.addbtn.Event = "AddUser()";

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    ToolboxViewModelObj.savebtn.Event = "SaveUser()";

                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    break;
                case "Edit":
                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    ToolboxViewModelObj.savebtn.Event = "SaveUser()";

                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Title = "Delete";
                    ToolboxViewModelObj.deletebtn.Event = "DeleteUser()";

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