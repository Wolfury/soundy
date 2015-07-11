using System.Web;
using System.Web.Optimization;

namespace Soundy.Web
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Vendor/jquery-{version}.js",
                        "~/Scripts/Vendor/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Vendor/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Vendor/modernizr-*"));

            //Angular Bundle

            bundles.Add(new ScriptBundle("~/bundles/angularJs").Include(
                      "~/Scripts/Vendor/Angular/angular.js",
                      "~/Scripts/Vendor/Angular/angular-mocks.js",
                      "~/Scripts/Vendor/Angular/angular-animate.js",
                      "~/Scripts/Vendor/Angular/angular-route.js",
                      "~/Scripts/Vendor/Angular/angular-sanitize.js",
                      "~/Scripts/Vendor/Angular/angular-datepicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                       "~/Scripts/App/app.js",
                       "~/Scripts/App/controllers/shellController.js",
                       "~/Scripts/App/services/songsService.js",
                       "~/Scripts/App/services/playlistsService.js",
                       "~/Scripts/App/services/authorsService.js",
                       "~/Scripts/App/controllers/masterController.js"));
        }
    }
}
