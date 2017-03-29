using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Models
{
  public class ProductViewModel
    {
        #region General
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter product name")]
        [Display(Name = "Name")]
        [StringLength(250)]
        public string Name { get; set; }
        
        
        [Required(ErrorMessage = "Please enter SKU")]
        [Display(Name = "SKU")]
        [StringLength(250)]
        public string SKU { get; set; }

        [Required(ErrorMessage = "Please enter Enabled")]
        [Display(Name = "Active(Yes/No)")]
        public Boolean Enabled { get; set; }

        [Required(ErrorMessage = "Please enter Unit")]
        [Display(Name = "Unit")]
        [StringLength(10)]
        public string Unit { get; set; }

             
        [Display(Name = "URL")]
        [StringLength(int.MaxValue)]
        public string URL { get; set; }

        [Required(ErrorMessage = "Please enter Action Type")]
        [Display(Name = "Action Type")]
        public char ActionType { get; set; }//book //buy //Quote

        [Required(ErrorMessage = "Please enter Supplier")]
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        public List<SelectListItem> suppliers { get; set; }
        //  public List<SupplierViewModel> suppliers { get; set; }

        [Display(Name = "Manufacturer")]
        //making it nullable because its not mandatory field
        public int? ManufacturerID { get; set; }
        public List<SelectListItem> manufacturers { get; set; }
        [Required(ErrorMessage = "Please enter Product Type")]
        [Display(Name = "Product Type")]
        public char? ProductType { get; set; }//simple //configurable


        [Display(Name = "Attribute Set")]
        public int AttributeSetID { get; set; }

        public List<SelectListItem> AttributeSets { get; set; }
        
        public Boolean FreeDelivery { get; set; }

        public string productDetailhdf { get; set; }

        #endregion General
        #region Prices
        [Required(ErrorMessage = "Please enter Cost Price")]
        [Display(Name = "Cost Price")]
        // [RegularExpression("^[0-9]*$", ErrorMessage = "Must be number")]
        // [RegularExpression(@"[0-9]{0,}\.[0-9]{2}", ErrorMessage = "Must be a Decimal ie:(85.50)")]
        [Range(0, 9999999999999999.99)]
        public decimal? CostPrice { get; set; }

        [Required(ErrorMessage = "Please enter Selling Price ")]
        [Display(Name = "Selling Price")]
        //[RegularExpression(@"[0-9]{0,}\.[0-9]{2}", ErrorMessage = "Must be a Decimal ie:(85.50)")]
        [Range(0, 9999999999999999.99)]
        public decimal? BaseSellingPrice { get; set; }

        [Required(ErrorMessage = "Please enter Show Price  ")]
        [Display(Name = "Show Price")]
        public Boolean ShowPrice { get; set; }
        
        [Display(Name = "Tax Class")]
        public string TaxClass { get; set; }
       
        [Display(Name = "Discount Price")]
        //[RegularExpression(@"[0-9]{0,}\.[0-9]{2}", ErrorMessage = "Must be a Decimal ie:(85.50)")]
        [Range(0, 9999999999999999.99)]
        public decimal? DiscountAmount { get; set; }

        [DataType(DataType.Date,ErrorMessage ="Must be a Date")]
        //[DisplayFormat(DataFormatString = "{0:dd-M-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Discount Start Date")]
        public DateTime? DiscountStartDate { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Must be a Date")]
        //[DisplayFormat(DataFormatString = "{0:dd-M-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Discount End Date")]
        public DateTime? DiscountEndDate { get; set; }

        #endregion Prices


        #region Images
        [DataType(DataType.Upload)]
        [Display(Name = "Choose Product Main Image")]
        public HttpPostedFileBase ProdutMainImageUpload { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Choose Product Other Image")]
        public HttpPostedFileBase OtherImagesUpload { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Choose Product Sticker")]
        public HttpPostedFileBase ProductStickerUpload { get; set; }
        public Guid? StickerID { get; set; }
        public int ImageID { get; set; }
        public string ImageURL { get; set; }
        public int ProductDetID { get; set; }
        public bool MainImage { get; set; }
        public string[] IDSet { get; set; }
        #endregion Images
        #region Description
        [Required(ErrorMessage = "Please enter Short Description")]
        [Display(Name = "Short Description")]
        [MaxLength(250)]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Please enter Long Description")]
        [Display(Name = "Long Description")]
        [MaxLength(250)]
        [DataType(DataType.MultilineText)]
        public string LongDescription { get; set; }
        #endregion Description

        #region Inventory
        [Required(ErrorMessage = "Please enter Stock Status")]
        [Display(Name = "Stock Status")]
        public Boolean StockAvailable { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Must be number")]
        [Display(Name = "Qty")]
        public int? Qty { get; set; }

        

       
        [RegularExpression("^[0-9]*$", ErrorMessage = "Must be number")]
        [Display(Name = "Reorder Alert Qty")]
        public int? OutOfStockAlertQty { get; set; }


        #endregion Inventory

        #region ProductTags
     
        [Display(Name = "Key Words")]
        [MaxLength(250)]
        public string HeaderTags { get; set; }
        #endregion ProductTags


        public int LinkID { get; set; }
        public int CategoryID { get; set; }
        public float PositionNo { get; set; }
        public LogDetailsViewModel logDetails { get; set; }
        public List<ProductDetailViewModel> ProductDetails { get;set;}
        public ProductDetailViewModel ProductDetailObj { get; set; }
        //collection of related products ids
        public string IDList { get; set; }
        public List<AttributeValuesViewModel> ProductOtherAttributes { get; set; }

    }
  public class ProductDetailViewModel
  {
        public int ID { get; set; }
        public int ProductID { get; set; }

      

        public int? Qty { get; set; }
        public int? OutOfStockAlertQty { get; set; }
        public decimal? PriceDifference { get; set; }
        public string DetailTags { get; set; }
        public Boolean Enabled { get; set; }
        public Boolean StockAvailable { get; set; }
        public Boolean DefaultOption { get; set; }
        public decimal? DiscountAmount { get; set; }
        public DateTime? DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public LogDetailsViewModel logDetails { get; set; }
        public List<AttributeValuesViewModel> ProductAttributes { get; set; }
        public List<ProductImagesViewModel> ProductDetailImages { get; set; }
        //For associated product
        public string ProductName { get; set; }
        public decimal BaseSellingPrice { get; set; }
    }
    public class ProductCategoryLinkViewModel
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int CategoryID {get;set;}
        public LogDetailsViewModel commonObj { get; set; }
    }

    public class ProductImagesViewModel
    {
        public int ID { get; set; }
        public string URL { get; set; }
        public bool isMain { get; set; }
    }

    public class ProductReviewViewModel
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public string Review { get; set; }
        public int DaysCount { get; set; }
        public DateTime ReviewCreatedDate { get; set; }
        public string CustomerName { get; set; }
        public string AvgRating { get; set; }
        public string ImageUrl { get; set; }
        public List<AttributeValues> ProductRatingAttributes { get; set; }

    }
}
