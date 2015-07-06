using System.Web;
using System.Web.Optimization;

namespace Soundy.Web
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Vendor/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Vendor/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Vendor/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/Vendor/bootstrap.js",
                      "~/Scripts/Vendor/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            //Angular Bundle

            bundles.Add(new ScriptBundle("~/bundles/angularJs").Include(
                      "~/Scripts/Vendor/Angular/angular.js",
                      "~/Scripts/Vendor/Angular/angular-mocks.js",
                      "~/Scripts/Vendor/Angular/angular-animate.js",
                      "~/Scripts/Vendor/Angular/angular-route.js",
                      "~/Scripts/Vendor/Angular/angular-sanitize.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                       "~/Scripts/App/app.js",
                       "~/Scripts/App/controllers/shellController.js"));
        }
    }
}
