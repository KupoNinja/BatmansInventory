using BatmansInventory.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Models
{
    //Should be DbName?
    public class BatmansInventoryContext : DbContext, IBatmansInventoryContext
    {
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<PhysicalItem> PhysicalItems { get; set; }
        public DbSet<Location> Locations { get; set; }
        public BatmansInventoryContext(DbContextOptions<BatmansInventoryContext> options) 
            : base(options) { }

        //Confirming this decimal column has default
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InventoryItem>()
                .HasIndex(i => i.PartNumber)
                .IsUnique();

            modelBuilder.Entity<PhysicalItem>()
                .Property(p => p.Value)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PhysicalItem>()
                .HasIndex(p => p.SerialNumber)
                .IsUnique();
        }
    }
}