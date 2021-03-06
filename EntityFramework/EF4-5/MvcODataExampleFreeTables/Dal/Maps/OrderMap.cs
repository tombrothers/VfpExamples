﻿using System.Data.Entity.ModelConfiguration;
using MvcODataExampleFreeTables.Models;

namespace MvcODataExampleFreeTables.Dal.Maps {
    public class OrderMap : EntityTypeConfiguration<Order> {
        public OrderMap() {
            ToTable("Order");

            HasKey(t => t.SerialNo);

            Property(t => t.OrderNo)
                .IsFixedLength()
                .HasMaxLength(7);

            Property(t => t.SerialNo)
                .IsFixedLength()
                .HasMaxLength(10);

            HasMany(t => t.OrderDetails)
                .WithMany()
                .Map(x => { 
                    x.ToTable("O_OD"); 
                    x.MapLeftKey("S1");
                    x.MapRightKey("S2");
                });
        }
    }
}