using MaleFashion.Server.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MaleFashion.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #region DbSet
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrdersItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region FluentAPI

            // User
            modelBuilder.Entity<User>(entity =>
            {
                // One-to-many relationship with Order
                entity.HasMany(u => u.Orders)
                    .WithOne(o => o.User)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                // One-to-many relationship with Favorite
                entity.HasMany(u => u.Favorites)
                    .WithOne(f => f.User)
                    .HasForeignKey(f => f.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product").HasKey(p => p.Id);

                // One-to-many relationship with SubCategory
                entity.HasOne(p => p.SubCategory)
                    .WithMany(sc => sc.Products)
                    .HasForeignKey(p => p.SubCategoryId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // ProductVariant
            modelBuilder.Entity<ProductVariant>(entity =>
            {
                entity.ToTable("ProductVariant").HasKey(pv => pv.Id);

                // Many-to-one relationship with Product
                entity.HasOne(pv => pv.Product)
                    .WithMany(p => p.ProductVariants)
                    .HasForeignKey(pv => pv.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                // One-to-many relationship with CartItem
                entity.HasMany(pv => pv.CartItems)
                    .WithOne(ci => ci.ProductVariant)
                    .HasForeignKey(ci => ci.ProductVariantId)
                    .OnDelete(DeleteBehavior.Cascade);

                // One-to-many relationship with OrderItem
                entity.HasMany(pv => pv.OrderItems)
                    .WithOne(oi => oi.ProductVariant)
                    .HasForeignKey(oi => oi.ProductVariantId)
                    .OnDelete(DeleteBehavior.Cascade);

                // One-to-many relationship with Favorite
                entity.HasMany(pv => pv.Favorites)
                    .WithOne(f => f.ProductVariant)
                    .HasForeignKey(f => f.ProductVariantId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Color
            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("Color").HasKey(c => c.Id);
            });

            // Size
            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size").HasKey(s => s.Id);
            });

            // MainCategory
            modelBuilder.Entity<MainCategory>(entity =>
            {
                entity.ToTable("MainCategory").HasKey(mc => mc.Id);

                // One-to-many relationship with SubCategory
                entity.HasMany(mc => mc.SubCategories)
                    .WithOne(sc => sc.MainCategory)
                    .HasForeignKey(sc => sc.MainCategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // SubCategory
            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.ToTable("SubCategory").HasKey(sc => sc.Id);
            });

            // Cart
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart").HasKey(c => c.Id);

                // One-to-many relationship with CartItem
                entity.HasMany(c => c.CartItems)
                    .WithOne(ci => ci.Cart)
                    .HasForeignKey(ci => ci.CartId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // CartItem
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.ToTable("CartItem").HasKey(ci => ci.Id);
            });

            // Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order").HasKey(o => o.Id);

                // One-to-many relationship with OrderItem
                entity.HasMany(o => o.OrderItems)
                    .WithOne(oi => oi.Order)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // OrderItem
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem").HasKey(oi => oi.Id);
            });

            // OrderStatus
            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus").HasKey(os => os.Id);

                // One-to-many relationship with Order
                entity.HasMany(os => os.Orders)
                    .WithOne(o => o.OrderStatus)
                    .HasForeignKey(o => o.OrderStatusId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Favorite (User - ProductVariant n-n)
            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.ToTable("Favorite").HasKey(f => f.Id);

                entity.HasIndex(f => new { f.UserId, f.ProductVariantId }).IsUnique();
            });
            #endregion
        }
    }
}
