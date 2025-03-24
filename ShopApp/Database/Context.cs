using Microsoft.EntityFrameworkCore;
using ShopApp.Utils;

namespace ShopApp.Database;

public class AppDbContext : DbContext {
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Cart> Carts { get; set; }

    // Makarov: since we use Sqlite on WINDOWS, we need to use Task.Run for async operations. Sqlite on Windows does not support parallel operations.
    private AppDbContext() { }
    public static async Task<AppDbContext> InitializeAsync() {
        var context = await Task.Run(() => {
            var context = new AppDbContext();
            context.Database.EnsureCreated();
            return context;
        });
        return context;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        // Aleksei: путь к файлу базы данных SQLite в проекте
        Paths.EnsureExists();
        optionsBuilder.UseSqlite($"Data Source={Paths.AppDatabase}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // Aleksei: Настройка связей и ограничений
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId);
        modelBuilder.Entity<Cart>()
            .HasOne(c => c.User)
            .WithMany(u => u.Carts)
            .HasForeignKey(c => c.UserId);
        modelBuilder.Entity<Cart>()
            .HasOne(c => c.Product)
            .WithMany(p => p.Carts)
            .HasForeignKey(c => c.ProductId);
        TestDataGenerator.Seed(modelBuilder);
    }
}
