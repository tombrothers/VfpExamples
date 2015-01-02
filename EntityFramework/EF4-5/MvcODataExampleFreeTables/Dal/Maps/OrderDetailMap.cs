using System.Data.Entity.ModelConfiguration;
using MvcODataExampleFreeTables.Models;

namespace MvcODataExampleFreeTables.Dal.Maps {
    public class OrderDetailMap : EntityTypeConfiguration<OrderDetail> {
        public OrderDetailMap() {
            ToTable("OrderDet");

            HasKey(t => t.UniqueId);

            Property(t => t.UniqueId)
                .IsFixedLength()
                .HasMaxLength(7);

            Property(t => t.SerialNo)
                .IsFixedLength()
                .HasMaxLength(10);
        }
    }
}