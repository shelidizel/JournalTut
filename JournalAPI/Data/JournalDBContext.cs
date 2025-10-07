using JournalAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace JournalAPI.Data
{
    public class JournalDBContext : DbContext
    {
        public JournalDBContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Journal> Journals { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Unit> Units { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship for BaseCurrency
            // We'll leave this one to Cascade (the default for required relationships)
            modelBuilder.Entity<Journal>()
                .HasOne(j => j.Currency) // The BaseCurrency navigation property
                .WithMany() // Or WithMany(c => c.BaseJournals) if you add a collection to Currency
                .HasForeignKey(j => j.BaseCurrencyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); // Explicitly set to Cascade (Default)

            // Configure the relationship for PoCurrency
            // This is the one we *must* set to NO ACTION/Restrict to break the cycle/multiple path issue.
            modelBuilder.Entity<Journal>()
                .HasOne(j => j.POCurrency) // The PoCurrency navigation property
                .WithMany() // Or WithMany(c => c.PoJournals) if you add a collection to Currency
                .HasForeignKey(j => j.PoCurrencyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // *** KEY CHANGE: Set to Restrict ***

            // Note: DeleteBehavior.Restrict corresponds to ON DELETE NO ACTION in SQL Server.
        }

    }
}
