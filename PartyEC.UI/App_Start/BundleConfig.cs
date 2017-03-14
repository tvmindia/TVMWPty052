using System.Web.Optimization;

namespace PartyEC.UI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {   //css bundles
            bundles.Add(new StyleBundle("~/Content/jstree").Include("~/Content/jstree/dist/themes/default/style.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.min.css", "~/Content/bootstrap-theme.min.css", "~/Content/font-awesome.min.css", "~/Content/custom.css"));
            bundles.Add(new StyleBundle("~/Content/datatable").Include("~/Content/DataTables/css/dataTables.bootstrap.min.css","~/Content/DataTables/css/responsive.bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/Content/product").Include("~/Content/UserCSS/Product.css"));
            //jquery bundles
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-3.1.1.min.js"));
            //jquery unobtrusive ajax
            bundles.Add(new ScriptBundle("~/bundles/jqueryunobtrusiveajaxvalidate").Include("~/Scripts/jquery.validate.min.js", "~/Scripts/jquery.validate.unobtrusive.min.js", "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            //bootstrap js bundles
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.min.js"));
            //jquery Datatable bundles
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include("~/Scripts/DataTables/jquery.dataTables.min.js","~/Scripts/DataTables/dataTables.bootstrap.min.js","~/Scripts/DataTables/dataTables.responsive.min.js","~/Scripts/DataTables/responsive.bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jstreeScript").Include("~/Scripts/jstree/dist/jstree.js"));
            bundles.Add(new ScriptBundle("~/bundles/userpluginjs").Include("~/Scripts/jquery.noty.packaged.min.js","~/Scripts/custom.js"));
            bundles.Add(new ScriptBundle("~/bundles/product").Include("~/Scripts/UserJS/Products.js"));
            bundles.Add(new ScriptBundle("~/bundles/attributes").Include("~/Scripts/UserJS/Attributes.js"));
            bundles.Add(new ScriptBundle("~/bundles/AttributeSet").Include("~/Scripts/UserJS/AttributeSet.js"));
            bundles.Add(new ScriptBundle("~/bundles/Categories").Include("~/Scripts/UserJS/Categories.js"));
        }
    }
}



