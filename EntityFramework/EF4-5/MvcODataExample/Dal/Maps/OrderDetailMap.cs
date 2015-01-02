using System.Data.Entity.ModelConfiguration;
using MvcODataExample.Models;

namespace MvcODataExample.Dal.Maps {
    public class OrderDetailMap : EntityTypeConfiguration<OrderDetail> {
        public OrderDetailMap() {
            ToTable("OrderDetail");

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