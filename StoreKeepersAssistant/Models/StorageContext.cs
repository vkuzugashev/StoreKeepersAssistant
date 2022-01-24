using Microsoft.EntityFrameworkCore;

namespace StoreKeepersAssistant.Models
{
    public class StorageContext : DbContext
    {
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        public StorageContext(DbContextOptions<StorageContext> options)
           : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            StorageData.Initialize(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasMany(o => o.InvoiceItems)
                    .WithOne(x => x.Invoice)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}
