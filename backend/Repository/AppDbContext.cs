using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext
    : IdentityDbContext<AppUser>
{

    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductEntry> ProductEntries => Set<ProductEntry>();
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(product => product.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<Product>()
            .OwnsOne(product => product.NutritionFacts);

        modelBuilder.Entity<ProductEntry>()
            .Property(entry => entry.CreatedAt)
            .HasDefaultValueSql("NOW()");

        modelBuilder.Entity<ProductEntry>()
            .Property(entry => entry.UserId)
            .IsRequired();
            
        modelBuilder.Entity<ProductEntry>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(entry => entry.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductEntry>()
            .HasOne(entry => entry.Product)
            .WithMany()
            .HasForeignKey(entry => entry.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}