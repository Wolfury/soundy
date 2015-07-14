using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Soundy.Web.Controllers;
using System.Web;

namespace Soundy.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }

        protected void Application_EndRequest()
        {
            if (Context.Response.StatusCode == 404)
            {
                Response.Clear();

                var rd = new RouteData();
                rd.Values["controller"] = "Home";
                rd.Values["action"] = "Index";

                IController c = new HomeController();
                c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
            }
        }
    }
}
