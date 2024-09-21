using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using HH.Domain.Models;

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

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }

    public virtual DbSet<FuelImport> FuelImports { get; set; }

    public virtual DbSet<Lubricant> Lubricants { get; set; }

    public virtual DbSet<ProductPrice> ProductPrices { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<SessionDetail> SessionDetails { get; set; }

    public virtual DbSet<Tank> Tanks { get; set; }

    public virtual DbSet<WeighingHistory> WeighingHistories { get; set; }

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
                .HasMaxLength(255)
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

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("expense_pkey");

            entity.ToTable("expense");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(13, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Debtor)
                .HasMaxLength(255)
                .HasColumnName("debtor");
            entity.Property(e => e.ExpenseDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expense_date");
            entity.Property(e => e.ExpenseTypeId).HasColumnName("expense_type_id");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.ExpenseType).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.ExpenseTypeId)
                .HasConstraintName("expense_expense_type_id_fkey");

            entity.HasOne(d => d.Session).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("expense_session_id_fkey");
        });

        modelBuilder.Entity<ExpenseType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("expense_type_pkey");

            entity.ToTable("expense_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<FuelImport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("fuel_import_pkey");

            entity.ToTable("fuel_import");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.ImportDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("import_date");
            entity.Property(e => e.ImportPrice)
                .HasPrecision(13, 2)
                .HasColumnName("import_price");
            entity.Property(e => e.ImportVolume)
                .HasPrecision(13, 2)
                .HasColumnName("import_volume");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.TankId).HasColumnName("tank_id");
            entity.Property(e => e.TotalCost)
                .HasPrecision(15, 2)
                .HasColumnName("total_cost");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Weight)
                .HasPrecision(13, 2)
                .HasColumnName("weight");

            entity.HasOne(d => d.Tank).WithMany(p => p.FuelImports)
                .HasForeignKey(d => d.TankId)
                .HasConstraintName("fuel_import_tank_id_fkey");
        });

        modelBuilder.Entity<Lubricant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lubricant_pkey");

            entity.ToTable("lubricant");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CurrentStock).HasColumnName("current_stock");
            entity.Property(e => e.ImportDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("import_date");
            entity.Property(e => e.ImportPrice)
                .HasPrecision(13, 2)
                .HasColumnName("import_price");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.SellPrice)
                .HasPrecision(13, 2)
                .HasColumnName("sell_price");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<ProductPrice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_price_pkey");

            entity.ToTable("product_price");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.ImportPrice)
                .HasPrecision(13, 2)
                .HasColumnName("import_price");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ProductTypeId).HasColumnName("product_type_id");
            entity.Property(e => e.SellingPrice)
                .HasPrecision(13, 2)
                .HasColumnName("selling_price");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.ProductType).WithMany(p => p.ProductPrices)
                .HasForeignKey(d => d.ProductTypeId)
                .HasConstraintName("product_price_product_type_id_fkey");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_type_pkey");

            entity.ToTable("product_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("session_pkey");

            entity.ToTable("session");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CashForChange)
                .HasPrecision(13, 2)
                .HasColumnName("cash_for_change");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_date");
            entity.Property(e => e.FuelPrice)
                .HasPrecision(13, 2)
                .HasColumnName("fuel_price");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_date");
            entity.Property(e => e.TotalRevenue)
                .HasPrecision(15, 2)
                .HasColumnName("total_revenue");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<SessionDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("session_detail_pkey");

            entity.ToTable("session_detail");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.EndVolume)
                .HasPrecision(13, 2)
                .HasColumnName("end_volume");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Revenue)
                .HasPrecision(13, 2)
                .HasColumnName("revenue");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.StartVolume)
                .HasPrecision(13, 2)
                .HasColumnName("start_volume");
            entity.Property(e => e.TankId).HasColumnName("tank_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Session).WithMany(p => p.SessionDetails)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("session_detail_session_id_fkey");

            entity.HasOne(d => d.Tank).WithMany(p => p.SessionDetails)
                .HasForeignKey(d => d.TankId)
                .HasConstraintName("session_detail_tank_id_fkey");
        });

        modelBuilder.Entity<Tank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tank_pkey");

            entity.ToTable("tank");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacity)
                .HasPrecision(13, 2)
                .HasColumnName("capacity");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CurrentVolume)
                .HasPrecision(13, 2)
                .HasColumnName("current_volume");
            entity.Property(e => e.Height)
                .HasPrecision(13, 2)
                .HasColumnName("height");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Type).WithMany(p => p.Tanks)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("tank_type_id_fkey");
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
