using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PI.WebApi.Models;

public partial class HhDatabaseContext : DbContext
{
    public HhDatabaseContext()
    {
    }

    public HhDatabaseContext(DbContextOptions<HhDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<WeighingHistory> WeighingHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=huynhhanh.com;Port=5432;Database=HH.Database;Username=sa;Password=qweRTY789@;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("weighing_status", new[] { "new", "processing", "done" });

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
