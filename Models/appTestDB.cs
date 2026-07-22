using Microsoft.EntityFrameworkCore;

namespace appTest.Models
{
    public class appTestDB : DbContext
    {
        public appTestDB(DbContextOptions<appTestDB> options)
            : base(options)
        {
        }

        public DbSet<Medicament> Medicament { get; set; }
        public DbSet<Fournisseurs> Fournisseurs { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Commande> Commandes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicament>().ToTable("Medicament");
            modelBuilder.Entity<Fournisseurs>().ToTable("Fournisseur");
            modelBuilder.Entity<Categorie>().ToTable("Categorie");
            modelBuilder.Entity<Stock>().ToTable("Stock");
            modelBuilder.Entity<Commande>().ToTable("Commande");
        }
    }
}
