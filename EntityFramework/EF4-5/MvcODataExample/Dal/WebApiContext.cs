using System.Data.Entity;
using MvcODataExample.Dal.Maps;
using MvcODataExample.Models;

namespace MvcODataExample.Dal {
    public class WebApiContext : DbContext {
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<OrderDetail> OrderDetails { get; set; }

        static WebApiContext() {
            Database.SetInitializer(new WebApiDbContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new OrderDetailMap());
        }
    }
}