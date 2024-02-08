using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintegrujPL.Models;

namespace ZintegrujPL.DAL.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        //"name=DefaultConnection"
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Price> Prices { get; set; }

        // Konfiguracja modelu i relacji
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja tabeli Products
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId); // Ustawia pole ProductId jako klucz główny

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.SKU)
                .IsUnique(); // Ustawia indeks na SKU i oznacza go jako unikatowy

            // Konfiguracja tabeli Inventories
            modelBuilder.Entity<Inventory>()
                .HasKey(i => i.InventoryId); // Ustawia pole InventoryId jako klucz główny

           

            // Konfiguracja tabeli Prices
            modelBuilder.Entity<Price>()
                .HasKey(p => p.PriceId); // Ustawia pole PriceId jako klucz główny

            //modelBuilder.Entity<Price>()
            //    .HasOne<Product>() // Określa relację jeden-do-wielu z tabelą Products
            //    .WithMany()
            //    .HasForeignKey(p => p.SKU) // Ustawia pole SKU jako klucz obcy
            //    .HasPrincipalKey(p => p.SKU); // Ustawia pole SKU w Product jako klucz główny dla tej relacji
        }
    }
}
