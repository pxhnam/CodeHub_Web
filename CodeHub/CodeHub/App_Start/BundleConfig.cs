using System.Web.Optimization;

namespace CodeHub
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/core").Include(
                "~/Content/vendors/js/core.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/addons").Include(
                "~/Content/vendors/js/vendor.addons.js"
            ));

            //assets/vendors/js/vendor.addons.js"


            bundles.Add(new ScriptBundle("~/bundles/charts").Include(
                "~/Content/vendors/apexcharts/apexcharts.min.js",
                "~/Content/vendors/chartjs/Chart.min.js",
                "~/Content/js/charts/chartjs.addon.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                "~/Content/js/template.js",
                "~/Content/js/dashboard.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/shared/style.css",
                      "~/Content/css/demo_1/style.css",
                      "~/Content/vendors/iconfonts/mdi/css/materialdesignicons.css",
                      "~/Content/site.css"));
        }
    }
}
