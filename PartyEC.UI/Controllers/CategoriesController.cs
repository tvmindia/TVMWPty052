using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyEC.BusinessServices.Contracts;
using PartyEC.UI.Models;
using PartyEC.DataAccessObject.DTO;
using Newtonsoft.Json;
using AutoMapper;

namespace PartyEC.UI.Controllers
{
    public class CategoriesController : Controller
    {

        #region Constructor_Injection

        ICategoriesBusiness _categoryBusiness;
        ICommonBusiness _commonBusiness;
        IProductBusiness _productBusiness;

        public CategoriesController(ICategoriesBusiness categoryBusiness, ICommonBusiness commonBusiness,IProductBusiness productBusiness)
        {
            _categoryBusiness = categoryBusiness;
            _commonBusiness = commonBusiness;
            _productBusiness = productBusiness;
        }
        #endregion Constructor_Injection
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public string GetCategoryDetailsByID(int ID)
        {
            try
            {
                CategoriesViewModel CategoryList = Mapper.Map<Categories, CategoriesViewModel>(_categoryBusiness.GetCategory(ID));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = CategoryList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "Edit":
                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Title = "Delete";


                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "MainClick()";
                    ToolboxViewModelObj.savebtn.Title = "Save";

                    ToolboxViewModelObj.addsubbtn.Visible = true;
                    ToolboxViewModelObj.addsubbtn.Event = "AddNewSubCategory()";

                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Title = "Add";
                    ToolboxViewModelObj.addbtn.Event = "AddCategory()";

                    break;
                case "Add":
                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Disable = true;

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "MainClick()";
                    ToolboxViewModelObj.savebtn.Title = "Save";

                    ToolboxViewModelObj.addsubbtn.Visible = true;
                    ToolboxViewModelObj.addsubbtn.Disable = true;

                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Title = "Add";
                    ToolboxViewModelObj.addbtn.Event = "AddCategory()";
                    break;
                case "AddSub":
                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Disable = true;

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "MainClick()";
                    ToolboxViewModelObj.savebtn.Title = "Save";

                    ToolboxViewModelObj.addsubbtn.Visible = true;
                    ToolboxViewModelObj.addsubbtn.Event = "AddNewSubCategory()";

                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Title = "Add";
                    ToolboxViewModelObj.addbtn.Event = "AddCategory()";
                    break;
                case "tab1":
                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Disable = true;

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "MainClick()";
                    ToolboxViewModelObj.savebtn.Title = "Save";

                    ToolboxViewModelObj.addsubbtn.Visible = true;
                    ToolboxViewModelObj.addsubbtn.Event = "AddNewSubCategory()";

                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Title = "Add";
                    ToolboxViewModelObj.addbtn.Event = "AddCategory()";
                    break;
                case "tab2":
                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "SaveAddorRemove()";
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    break;
                default:
                    return Content("Nochange");
            }
            return PartialView("_ToolboxView", ToolboxViewModelObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string AddorRemoveLinks(CategoriesViewModel categoriesViewModelObj)
        {
            try
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = new OperationsStatusViewModel();
                categoriesViewModelObj.commonObj = new LogDetailsViewModel();
                categoriesViewModelObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                categoriesViewModelObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                //Deserialize the string to object
                List<ProductCategoryLinkViewModel> ProductList = JsonConvert.DeserializeObject<List<ProductCategoryLinkViewModel>>(categoriesViewModelObj.TableDataAdd);
                List<ProductCategoryLinkViewModel> ProductListDelete = JsonConvert.DeserializeObject<List<ProductCategoryLinkViewModel>>(categoriesViewModelObj.TableDataDelete);
                //Adding Created date and Createdby 
                foreach (var i in ProductList)
                {
                    i.commonObj = categoriesViewModelObj.commonObj;
                }

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_productBusiness.AddOrRemoveProductCategoryLink((Mapper.Map<List<ProductCategoryLinkViewModel>, List<ProductCategoryLink>>(ProductList)), (Mapper.Map<List<ProductCategoryLinkViewModel>, List<ProductCategoryLink>>(ProductListDelete))));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}