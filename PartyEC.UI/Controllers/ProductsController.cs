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
    public class ProductsController : Controller
    {

        IProductBusiness _productBusiness;
        ICommonBusiness _commonBusiness;
        IMasterBusiness _masterBusiness;
        IAttributeSetBusiness _attributeSetBusiness;
        public ProductsController(IProductBusiness productBusiness,IMasterBusiness masterBusiness,ICommonBusiness commonBusiness, IAttributeSetBusiness attributeSetBusiness)
        {
            _productBusiness = productBusiness;
            _masterBusiness = masterBusiness;
            _commonBusiness = commonBusiness;
            _attributeSetBusiness = attributeSetBusiness;
           
        }

        // GET: Products
        public ActionResult Index()
        {
            //suv test
            //OperationsStatus myStatus = new OperationsStatus();
            //Product p = _productBusiness.GetProduct(1001, myStatus);
            //suv test


            //_attributeBusiness.GetAllAttributeBySet(1,true);

            //Drop BInd
            ProductViewModel product = null;
            try
            {
                product = new ProductViewModel();
                List<SelectListItem> selectListItem = new List<SelectListItem>();
                //Supplier Drop down bind
                List<SupplierViewModel> supplierListVM=Mapper.Map<List<Supplier>, List<SupplierViewModel>>(_masterBusiness.GetAllSuppliers());
                foreach(SupplierViewModel svm in supplierListVM)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = svm.Name,
                        Value = svm.ID.ToString(),
                        Selected = false
                    });
                }
                product.suppliers = selectListItem;
                //Manufacturer Drop down bind
                List<ManufacturerViewModel> manfactureListVM=Mapper.Map<List<Manufacturer>,List<ManufacturerViewModel>>(_masterBusiness.GetAllManufacturers());
                selectListItem = null;
                selectListItem = new List<SelectListItem>();
                foreach (ManufacturerViewModel mvm in manfactureListVM)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = mvm.Name,
                        Value = mvm.ID.ToString(),
                        Selected = false
                    });
                }
                product.manufacturers = selectListItem;

                //AttributeSet Drop down bind
                List<AttributeSetViewModel> AttributeSetListVM = Mapper.Map<List<AttributeSet>, List<AttributeSetViewModel>>(_attributeSetBusiness.GetAllAttributeSet());
                selectListItem = null;
                selectListItem = new List<SelectListItem>();
                foreach (AttributeSetViewModel avm in AttributeSetListVM)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = avm.Name,
                        Value = avm.ID.ToString(),
                        Selected = false
                    });
                }
                product.AttributeSets = selectListItem;
            }
            catch(Exception ex)
            {

            }
            

            return View(product);
        }
        [HttpGet]
        public string GetAllProducts(ProductViewModel productObj)
        {
            try
            {
                List<ProductViewModel> productList = Mapper.Map<List<Product>,List<ProductViewModel>>(_productBusiness.GetAllProducts(Mapper.Map<ProductViewModel, Product>(productObj)));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
         
        }

        [HttpGet]
        public string GetRelatedProducts(string id)
        {
            try
            {
                if(!string.IsNullOrEmpty(id))
                {
                    List<ProductViewModel> productList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetRelatedProducts(int.Parse(id)));

                    return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
                }
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "id is empty" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
           
        }

        [HttpGet]
        public string GetUNRelatedProducts(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    List<ProductViewModel> productList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetUNRelatedProducts(int.Parse(id)));

                    return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
                }
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "id is empty" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }


        [HttpGet]
        public string GetAllProductswithCategory(string CategoryID)
        {
            try
            {
                List<ProductViewModel> productList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetAllProductswithCategory(CategoryID));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            // return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
        }
        [HttpGet]
        public string GetAssignedPro(string CategoryID)
        {
            try
            {
                List<ProductViewModel> productList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetAssignedPro(CategoryID));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            // return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
        }
        [HttpGet]
        public string GetUnAssignedPro(string CategoryID)
        {
            try
            {
                List<ProductViewModel> productList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetUnAssignedPro(CategoryID));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            // return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
        }

        [HttpGet]
        public string GetProduct(string id)
        {
            try
            {
                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                ProductViewModel product = Mapper.Map<Product,ProductViewModel>(_productBusiness.GetProduct(Int32.Parse(id), Mapper.Map<OperationsStatusViewModel, OperationsStatus>(operationsStatus)));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = product });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpGet]
        public string GetRelatedImages(string id)
        {
            try
            {
                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                List<ProductViewModel> product = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetRelatedImages(Int32.Parse(id), Mapper.Map<OperationsStatusViewModel, OperationsStatus>(operationsStatus)));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = product });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string ProductInsertUpdate(ProductViewModel productObj)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    OperationsStatusViewModel OperationsStatusViewModelObj = null;
                    switch (productObj.ID)
                    {
                        //INSERT
                        case 0:
                            productObj.logDetails = new LogDetailsViewModel();
                            //Getting UA
                            productObj.logDetails.CreatedBy = _commonBusiness.GetUA().UserName;
                            productObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();
                            productObj.ProductDetails = JsonConvert.DeserializeObject<List<ProductDetailViewModel>>(productObj.productDetailhdf);
                            OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_productBusiness.InsertProduct(Mapper.Map<ProductViewModel, Product>(productObj)));
                            return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                        default:
                            productObj.logDetails = new LogDetailsViewModel();
                            //Getting UA
                            productObj.logDetails.UpdatedBy = _commonBusiness.GetUA().UserName;
                            productObj.logDetails.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                            productObj.ProductDetails = JsonConvert.DeserializeObject<List<ProductDetailViewModel>>(productObj.productDetailhdf);
                            OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_productBusiness.UpdateProduct(Mapper.Map<ProductViewModel, Product>(productObj)));
                            return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                    }
                   
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string RelatedProductsInsert(ProductViewModel productObj)
        {
            if ((!ModelState.IsValid)&&(!string.IsNullOrEmpty(productObj.IDList)))
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;
                try
                {
                    productObj.logDetails = new LogDetailsViewModel();
                    //Getting UA
                    productObj.logDetails.CreatedBy = _commonBusiness.GetUA().UserName;
                    productObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_productBusiness.InsertRelatedProducts(Mapper.Map<ProductViewModel, Product>(productObj), productObj.IDList));
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                catch(Exception ex)
                {
                  return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }

        [HttpPost]
        public string DeleteProductOtherImages(ProductViewModel productViewObj)
        {
            if ((!ModelState.IsValid))
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;
                try
                {
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_productBusiness.DeleteProductsImage(productViewObj.IDSet));
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                catch (Exception ex)
                {

                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string DeleteRelatedProducts(ProductViewModel productObj)
        {
            if ((!ModelState.IsValid) && (!string.IsNullOrEmpty(productObj.IDList)))
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;
                try
                {
                    productObj.logDetails = new LogDetailsViewModel();
                    //Getting UA
                    productObj.logDetails.CreatedBy = _commonBusiness.GetUA().UserName;
                    productObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_productBusiness.DeleteRelatedProducts(Mapper.Map<ProductViewModel, Product>(productObj), productObj.IDList));
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                catch (Exception ex)
                {

                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }

        #region UpdateProductHeaderOtherAttributes
        [HttpPost]
        public string UpdateProductHeaderOtherAttributes(ProductViewModel productObj)
        {
            try
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;
                productObj.logDetails = new LogDetailsViewModel();
                //Getting UA
                productObj.logDetails.CreatedBy = _commonBusiness.GetUA().UserName;
                productObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();
                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_productBusiness.UpdateProductHeaderOtherAttributes(Mapper.Map<ProductViewModel, Product>(productObj)));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion UpdateProductHeaderOtherAttributes

        #region InsertProductDetails
        [HttpPost]
        public string InsertUpdateProductDetails(ProductViewModel productObj)
        {
            try
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;

                //INSERT

                productObj.logDetails = new LogDetailsViewModel();
                //Getting UA
                productObj.logDetails.CreatedBy = _commonBusiness.GetUA().UserName;
                productObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();
                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_productBusiness.InsertUpdateProductDetails(Mapper.Map<ProductViewModel, Product>(productObj)));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            
        }
        #endregion InsertProductDetails

        #region GetProductDetailByProduct
        [HttpGet]
        public string GetProductDetailByProduct(string id)
        {
          try
            {
                List<ProductDetailViewModel> productDetail = null;
                if (!string.IsNullOrEmpty(id))
                {
                    OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                    productDetail = Mapper.Map<List<ProductDetail>,List<ProductDetailViewModel>>(_productBusiness.GetProductDetail(Int32.Parse(id)));
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = productDetail });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = productDetail });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion GetProductDetailByProduct


        #region GetProductDetailsByProductDetailID
        [HttpGet]
        public string GetProductDetailsByProductDetailID(string productID,string productDetailID )
        {
            try
            {
                ProductDetailViewModel productDetail = null;
                if ((!string.IsNullOrEmpty(productID))&&(!string.IsNullOrEmpty(productDetailID)))
                {
                    OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                    productDetail = Mapper.Map<ProductDetail, ProductDetailViewModel>(_productBusiness.GetProductDetailsByProduct(Int32.Parse(productID),Int32.Parse(productDetailID)));
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = productDetail });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = productDetail });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion GetProductDetailsByProductDetailID

        #region DeleteProductDetail
        [HttpPost]
        public string DeleteProductDetail(ProductDetailViewModel productDeails)
        {
            try
             {
               OperationsStatusViewModel OperationsStatusViewModelObj = null;
                if((productDeails.ID>0)&&(productDeails.ProductID>0))
                {
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_productBusiness.DeleteProductsDetails(productDeails.ID, productDeails.ProductID));
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                
                
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }

        }
        #endregion DeleteProductDetail


        #region ChangeButtonStyle
        [HttpGet]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "Edit":
                    //ToolboxViewModelObj.deletebtn.Visible = true;
                    //ToolboxViewModelObj.deletebtn.Event = "Productdelete()";
                    //ToolboxViewModelObj.deletebtn.Title = "Delete";

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "ProductSave()";
                    ToolboxViewModelObj.savebtn.Title = "Save";

                    //ToolboxViewModelObj.resetbtn.Visible = true;
                    //ToolboxViewModelObj.resetbtn.Event = "btnreset()";
                    //ToolboxViewModelObj.resetbtn.Title = "Reset";

                    //ToolboxViewModelObj.backbtn.Visible = true;
                    //ToolboxViewModelObj.backbtn.Event = "goback()";
                    //ToolboxViewModelObj.backbtn.Title = "Back";

                    break;
                case "Delete":
                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Event = "DeleteOtherImage()";
                    ToolboxViewModelObj.deletebtn.Title = "Delete";
                    break;
                case "CancelDelete":
                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Disable = true;
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

        public ActionResult UploadProductImage(ProductViewModel ProductViewObj)
        {
            OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();

            var file = Request.Files["Filedata"];
            var FileNameCustom = ProductViewObj.Name + ".png";
            string savePath = Server.MapPath(@"~\Content\ProductImages\" + FileNameCustom);
            file.SaveAs(savePath);
            ProductViewObj.ImageURL = "/Content/ProductImages/" + FileNameCustom;
            if (ProductViewObj.ImageID==0)
            {
                ProductViewObj.logDetails = new LogDetailsViewModel();
                ProductViewObj.logDetails.CreatedBy = _commonBusiness.GetUA().UserName;
                ProductViewObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();
                ProductViewObj.logDetails.UpdatedBy = _commonBusiness.GetUA().UserName;
                ProductViewObj.logDetails.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                ProductViewObj.MainImage = true;
                operationsStatus = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_productBusiness.InsertImageProduct(Mapper.Map<ProductViewModel, Product>(ProductViewObj)));

            }
            return Content(Url.Content(@"~\Content\ProductImages\" + FileNameCustom));
        }
        public ActionResult UploadOtherImages(ProductViewModel ProductViewObj)
        {
            OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
            Random rnd = new Random();
            var file = Request.Files["Filedata"];
            var FileNameCustom = ProductViewObj.Name+"Other"+rnd.Next(111,9999).ToString()+ ".png";
            string savePath = Server.MapPath(@"~\Content\ProductImages\" + FileNameCustom);
            file.SaveAs(savePath);
            ProductViewObj.ImageURL = "/Content/ProductImages/" + FileNameCustom;
                ProductViewObj.logDetails = new LogDetailsViewModel();
                ProductViewObj.logDetails.CreatedBy = _commonBusiness.GetUA().UserName;
                ProductViewObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();
                ProductViewObj.logDetails.UpdatedBy = _commonBusiness.GetUA().UserName;
                ProductViewObj.logDetails.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                operationsStatus = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_productBusiness.InsertImageProduct(Mapper.Map<ProductViewModel, Product>(ProductViewObj)));
                return Content(Url.Content(@"~\Content\ProductImages\" + FileNameCustom));
        }
        public ActionResult UploadStickerImages(ProductViewModel ProductViewObj)
        {
            OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();

            Random rnd = new Random();
            var file = Request.Files["Filedata"];
            var FileNameCustom = ProductViewObj.Name + "Sticker" + rnd.Next(111, 9999).ToString() + ".png";
            string savePath = Server.MapPath(@"~\Content\ProductImages\" + FileNameCustom);
            file.SaveAs(savePath);
            ProductViewObj.URL = "/Content/ProductImages/" + FileNameCustom;
            //if (ProductViewObj.i == null)
            //{
            //    categoryObj.commonObj = new LogDetailsViewModel();
            //    categoryObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
            //    categoryObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
            //    categoryObj.commonObj.UpdatedBy = _commonBusiness.GetUA().UserName;
            //    categoryObj.commonObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();
            //    operationsStatus = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_categoryBusiness.InsertImageCategory(Mapper.Map<CategoriesViewModel, Categories>(categoryObj)));

            //}
            return Content(Url.Content(@"~\Content\ProductImages\" + FileNameCustom));
        }








    }
}