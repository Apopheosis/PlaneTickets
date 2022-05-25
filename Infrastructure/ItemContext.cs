using System;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using Models;

    public class ItemContext : DbContext
    {
        public ItemContext()
        {
            
        }

        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Item> Items { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
                entity.HasIndex(sc => new
                {
                    sc.TicketNumber,
                    sc.SerialNumber
                }).IsUnique());
        }
    }