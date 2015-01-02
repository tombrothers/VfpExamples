using System.Web.Http;
using System.Web.Http.OData.Builder;
using MvcODataExampleFreeTables.Models;

namespace MvcODataExampleFreeTables {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            var modelBuilder = new ODataConventionModelBuilder();

            modelBuilder.EntitySet<Order>("Orders");
            modelBuilder.EntitySet<OrderDetail>("OrderDetails");

            var model = modelBuilder.GetEdmModel();

            config.Routes.MapODataRoute(routeName: "OData", routePrefix: "odata", model: model);
        }
    }
}