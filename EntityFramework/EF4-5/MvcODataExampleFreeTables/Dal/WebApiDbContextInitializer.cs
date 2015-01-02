using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using MvcODataExampleFreeTables.Models;

namespace MvcODataExampleFreeTables.Dal {
    public class WebApiDbContextInitializer : DropCreateDatabaseAlways<WebApiContext> {
        protected override void Seed(WebApiContext context) {
            AddOrders(context);

            try {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex) {
                foreach (var error in ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors)) {
                    Debug.WriteLine(error.ErrorMessage);
                }

                throw;
            }
        }

        private static void AddOrders(WebApiContext context) {
            Enumerable.Range(1, 10).ToList().ForEach(x =>
                context.Orders.Add(new Order {
                    OrderNo = "O" + x,
                    SerialNo = "S" + x,
                    OrderDetails = Enumerable.Range(1, 5).Select(d => new OrderDetail { SerialNo = "S" + x, UniqueId = "U" + x.ToString() + d.ToString()}).ToArray()
                }));
        }
    }
}