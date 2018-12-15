using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication.Models
{
    public partial class CoffeeShopDotNetContext : DbContext
    {
        public CoffeeShopDotNetContext()
        {
        }

        public CoffeeShopDotNetContext(DbContextOptions<CoffeeShopDotNetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Components> Components { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<OrderItemsToProduct> OrderItemsToProduct { get; set; }
        public virtual DbSet<OrderItemToComponents> OrderItemToComponents { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderToOrderItems> OrderToOrderItems { get; set; }
        public virtual DbSet<ProductComponents> ProductComponents { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserToOrders> UserToOrders { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=coffeeShopDotNet;Username=postgres;Password=postgres");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Components>(entity =>
            {
                entity.ToTable("components");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.ToTable("order_items");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<OrderItemsToProduct>(entity =>
            {
                entity.ToTable("order_items_to_product");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.OrderItemsToProduct)
                    .HasForeignKey<OrderItemsToProduct>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkmqvipyo985l97toah1kj9e3xv");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItemsToProduct)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fkayd63wyhafffi1vqf29ueeol8");
            });

            modelBuilder.Entity<OrderItemToComponents>(entity =>
            {
                entity.HasKey(e => new { e.OrderItemId, e.ComponentsId });

                entity.ToTable("order_item_to_components");

                entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");

                entity.Property(e => e.ComponentsId).HasColumnName("components_id");

                entity.HasOne(d => d.Components)
                    .WithMany(p => p.OrderItemToComponents)
                    .HasForeignKey(d => d.ComponentsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkk0ge8jltnoome1fp8j2mltvgy");

                entity.HasOne(d => d.OrderItem)
                    .WithMany(p => p.OrderItemToComponents)
                    .HasForeignKey(d => d.OrderItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkp2nj6rvoenw15hleux784a8yi");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationTime).HasColumnName("creation_time");

                entity.Property(e => e.OrderStatus).HasColumnName("order_status");

                entity.Property(e => e.UpdateTime).HasColumnName("update_time");
            });

            modelBuilder.Entity<OrderToOrderItems>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.OrderItemsId });

                entity.ToTable("order_to_order_items");

                entity.HasIndex(e => e.OrderItemsId)
                    .HasName("uk_1f594ejdg9g5uvurlt1bd11lt")
                    .IsUnique();

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.OrderItemsId).HasColumnName("order_items_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderToOrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkti4cltax34aq0aacd8t3dxweu");

                entity.HasOne(d => d.OrderItems)
                    .WithOne(p => p.OrderToOrderItems)
                    .HasForeignKey<OrderToOrderItems>(d => d.OrderItemsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkc5mb0d9g6yrq3hdk3m14aoimn");
            });

            modelBuilder.Entity<ProductComponents>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.ProductComponentsId });

                entity.ToTable("product_components");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductComponentsId).HasColumnName("product_components_id");

                entity.HasOne(d => d.ProductComponentsNavigation)
                    .WithMany(p => p.ProductComponents)
                    .HasForeignKey(d => d.ProductComponentsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk14hc450r1usomd1rm8v93ma8i");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductComponents)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkrrg5w2hd9jchfufpivm8615ld");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.Type)
                    .HasName("uk_tm6owue934k6dfdbyimg1owfd")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Username)
                    .HasName("uk_r43af9ap4edm43mmtq01oddj6")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(255);

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(255);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<UserToOrders>(entity =>
            {
                entity.ToTable("user_to_orders");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.UserToOrders)
                    .HasForeignKey<UserToOrders>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fke1x9q6ktmtwc5um9g84f0p7br");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserToOrders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fkb44eudimb4csvoko7cnp0fm6g");
            });
        }
    }
}
