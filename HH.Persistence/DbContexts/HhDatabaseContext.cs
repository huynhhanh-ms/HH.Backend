using System;
using System.Collections.Generic;
using HH.Domain.Common;
using HH.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HH.Persistence.DbContexts;

public partial class HhDatabaseContext : DbContext
{
    public HhDatabaseContext()
    {
    }

    public HhDatabaseContext(DbContextOptions<HhDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<WeighingHistory> WeighingHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        //.UseLazyLoadingProxies(useLazyLoadingProxies: false)
        .UseNpgsql(AppConfig.ConnectionStrings.DefaultConnection)
        .LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("account_status", new[] { "active", "inactive", "suspended" })
            .HasPostgresEnum("user_role", new[] { "admin", "user", "guest", "staff", "manager" })
            .HasPostgresEnum("weighing_status", new[] { "new", "processing", "done" });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("account_pkey");

            entity.ToTable("account");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .HasColumnName("fullname");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<WeighingHistory>(entity =>
        {
            entity.HasKey(e => e.WhId).HasName("weighing_history_pkey");

            entity.ToTable("weighing_history");

            entity.Property(e => e.WhId).HasColumnName("wh_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValue(0)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .HasDefaultValueSql("'Vô Danh'::character varying")
                .HasColumnName("customer_name");
            entity.Property(e => e.GoodsType)
                .HasMaxLength(200)
                .HasColumnName("goods_type");
            entity.Property(e => e.GoodsWeight).HasColumnName("goods_weight");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(20)
                .HasColumnName("license_plate");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.TotalWeighingDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("total_weighing_date");
            entity.Property(e => e.TotalWeight).HasColumnName("total_weight");
            entity.Property(e => e.UpdatedBy)
                .HasDefaultValue(0)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_date");
            entity.Property(e => e.VehicleImages).HasColumnName("vehicle_images");
            entity.Property(e => e.VehicleWeighingDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("vehicle_weighing_date");
            entity.Property(e => e.VehicleWeight).HasColumnName("vehicle_weight");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
