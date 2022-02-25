using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace prs_server_mvc.Models
{
    public partial class PrsDbContext : DbContext
    {
        public PrsDbContext()
        {
        }

        public PrsDbContext(DbContextOptions<PrsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestLine> RequestLines { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.PartNbr, "IX_Products_PartNbr")
                    .IsUnique();

                entity.HasIndex(e => e.VendorId, "IX_Products_VendorId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PartNbr)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PhotoPath).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.VendorId);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Requests_UserId");

                entity.Property(e => e.DeliveryMode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Justification)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.RejectionReason).HasMaxLength(80);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Total).HasColumnType("decimal(11, 2)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<RequestLine>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "IX_RequestLines_ProductId");

                entity.HasIndex(e => e.RequestId, "IX_RequestLines_RequestId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.RequestLines)
                    .HasForeignKey(d => d.ProductId);

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestLines)
                    .HasForeignKey(d => d.RequestId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username, "IX_Users_Username")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Phone).HasMaxLength(12);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasIndex(e => e.Code, "IX_Vendors_Code")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Phone).HasMaxLength(12);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(5);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
