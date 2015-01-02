using System.Data.Entity;
using MvcODataExampleFreeTables.Dal.Maps;
using MvcODataExampleFreeTables.Models;

namespace MvcODataExampleFreeTables.Dal {
    public class WebApiContext : DbContext {
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<OrderDetail> OrderDetails { get; set; }

        static WebApiContext() {
            Database.SetInitializer<WebApiContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new OrderDetailMap());
        }
    }
}