using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Models
{
    //Should be DbName?
    public class DataContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<PhysicalItem> PhysicalItems { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options) { }

        //Confirming this decimal column has default
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PhysicalItem>()
                .Property(p => p.Value)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Item>()
                .HasIndex(i => i.PartNumber)
                .IsUnique();
        }
    }
}