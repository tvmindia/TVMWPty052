using System.Web.Optimization;

namespace PartyEC.UI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {   //css bundles
            
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.min.css", "~/Content/bootstrap-theme.min.css", "~/Content/font-awesome.min.css","~/Content/custom.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrapdatepicker").Include("~/Content/bootstrap-datepicker3.min.css"));
            bundles.Add(new StyleBundle("~/Content/jstree/default/jstreecss").Include("~/Content/jstree/default/jstreestyle.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.min.css", "~/Content/bootstrap-theme.min.css", "~/Content/font-awesome.min.css", "~/Content/custom.css"));
            bundles.Add(new StyleBundle("~/Content/lightbox").Include("~/Content/lightbox.min.css"));
            bundles.Add(new StyleBundle("~/Content/DataTables/css/datatable").Include("~/Content/DataTables/css/dataTables.bootstrap.min.css", "~/Content/DataTables/css/responsive.bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/Content/DataTables/css/datatablecheckbox").Include("~/Content/DataTables/css/dataTables.checkboxes.css"));
            bundles.Add(new StyleBundle("~/Content/DataTables/css/datatableSelect").Include("~/Content/DataTables/css/select.dataTables.min.css"));
            bundles.Add(new StyleBundle("~/Content/UserCSS/product").Include("~/Content/UserCSS/Product.css"));
            bundles.Add(new StyleBundle("~/Content/UserCSS/customer").Include("~/Content/UserCSS/Customer.css"));
            bundles.Add(new StyleBundle("~/Content/Uplodify").Include("~/Content/uploadify.css"));
            bundles.Add(new StyleBundle("~/Content/UserCSS/EventRequest").Include("~/Content/UserCSS/EventRequests.css"));
            bundles.Add(new StyleBundle("~/Content/UserCSS/Notifications").Include("~/Content/UserCSS/Notifications.css"));

            //jquery bundles
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-3.1.1.min.js"));
            //jquery unobtrusive ajax
            bundles.Add(new ScriptBundle("~/bundles/jqueryunobtrusiveajaxvalidate").Include("~/Scripts/jquery.validate.min.js", "~/Scripts/jquery.validate.unobtrusive.min.js", "~/Scripts/jquery.unobtrusive-ajax.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryform").Include("~/Scripts/jquery.form.js"));
            //bootstrap js bundles
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.min.js"));
            //jquery Datatable bundles
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include("~/Scripts/DataTables/jquery.dataTables.min.js","~/Scripts/DataTables/dataTables.bootstrap.min.js","~/Scripts/DataTables/dataTables.responsive.min.js","~/Scripts/DataTables/responsive.bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/datatableSelect").Include("~/Scripts/DataTables/dataTables.select.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/datatablecheckbox").Include("~/Scripts/DataTables/dataTables.checkboxes.js"));
            bundles.Add(new ScriptBundle("~/bundles/jstreeScript").Include("~/Scripts/jstree/dist/jstree.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrapdatepicker").Include("~/Scripts/bootstrap-datepicker.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/userpluginjs").Include("~/Scripts/jquery.noty.packaged.min.js","~/Scripts/custom.js"));
            bundles.Add(new ScriptBundle("~/bundles/Uploadify").Include("~/Scripts/jquery.uploadify.js"));
            bundles.Add(new ScriptBundle("~/bundles/Lightbox").Include("~/Scripts/lightbox.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/product").Include("~/Scripts/UserJS/Products.js"));
            bundles.Add(new ScriptBundle("~/bundles/attributes").Include("~/Scripts/UserJS/Attributes.js"));
            bundles.Add(new ScriptBundle("~/bundles/AttributeSet").Include("~/Scripts/UserJS/AttributeSet.js"));
            bundles.Add(new ScriptBundle("~/bundles/Event").Include("~/Scripts/UserJS/Event.js"));
            bundles.Add(new ScriptBundle("~/bundles/Categories").Include("~/Scripts/UserJS/Categories.js"));
            bundles.Add(new ScriptBundle("~/bundles/EventRequests").Include("~/Scripts/UserJS/EventRequests.js"));
            bundles.Add(new ScriptBundle("~/bundles/Order").Include("~/Scripts/UserJS/Order.js"));
            bundles.Add(new ScriptBundle("~/bundles/AssoProduct").Include("~/Scripts/UserJS/AssociatedProduct.js"));
            bundles.Add(new ScriptBundle("~/bundles/CartWishlist").Include("~/Scripts/UserJS/Cart_Wishlist.js"));
            bundles.Add(new ScriptBundle("~/bundles/Suppliers").Include("~/Scripts/UserJS/Suppliers.js"));
            bundles.Add(new ScriptBundle("~/bundles/customer").Include("~/Scripts/UserJS/Customer.js"));
            bundles.Add(new ScriptBundle("~/bundles/ShippingLocations").Include("~/Scripts/UserJS/ShippingLocation.js"));
            bundles.Add(new ScriptBundle("~/bundles/SupplierLocations").Include("~/Scripts/UserJS/SupplierLocations.js"));
            bundles.Add(new ScriptBundle("~/bundles/Countries").Include("~/Scripts/UserJS/Countries.js"));
            bundles.Add(new ScriptBundle("~/bundles/Manufacturers").Include("~/Scripts/UserJS/Manufacturers.js"));
            bundles.Add(new ScriptBundle("~/bundles/Review").Include("~/Scripts/UserJS/Review.js"));
            bundles.Add(new ScriptBundle("~/bundles/Notifications").Include("~/Scripts/UserJS/Notifiations.js"));
        }
    }
}



