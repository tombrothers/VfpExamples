using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using MvcODataExampleFreeTables.Dal;
using MvcODataExampleFreeTables.Models;

namespace MvcODataExampleFreeTables.Controllers {
    public class OrdersController : ODataController {
        private WebApiContext db = new WebApiContext();

        [Queryable]
        public IQueryable<Order> GetOrders() {
            return db.Orders;
        }

        public Order GetOrder([FromODataUri] string key) {
            return db.Orders.FirstOrDefault(p => p.SerialNo == key);
        }

        [Queryable]
        public IQueryable<OrderDetail> GetOrderDetails([FromODataUri]  string key) {
            return db.Orders.FirstOrDefault(p => p.SerialNo == key).OrderDetails.AsQueryable();
        }
    }
}