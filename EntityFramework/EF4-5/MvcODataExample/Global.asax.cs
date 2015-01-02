using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using VfpEntityFrameworkProvider;

namespace MvcODataExample {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            VfpProviderFactory.Register();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}