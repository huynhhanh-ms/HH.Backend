using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PI.Domain.Models;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace PI.Persitence.DbContexts;

public partial class PharmacyInventoryContext : DbContext
{
    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategorySetting> CategorySettings { get; set; }

    public virtual DbSet<Distributor> Distributors { get; set; }

    public virtual DbSet<ExportRequest> ExportRequests { get; set; }

    public virtual DbSet<ExportRequestDetail> ExportRequestDetails { get; set; }

    public virtual DbSet<ImportRequest> ImportRequests { get; set; }

    public virtual DbSet<ImportRequestDetail> ImportRequestDetails { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Lot> Lots { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

    public virtual DbSet<ProductAttributeMapping> ProductAttributeMappings { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductStock> ProductStocks { get; set; }

    public virtual DbSet<ProductUnit> ProductUnits { get; set; }

    public virtual DbSet<Shipment> Shipments { get; set; }

    public virtual DbSet<ShipmentDetail> ShipmentDetails { get; set; }

    public virtual DbSet<StockCheck> StockChecks { get; set; }

    public virtual DbSet<StockCheckDetail> StockCheckDetails { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.Role).HasDefaultValueSql("'Staff'");
            entity.Property(e => e.Status).HasDefaultValueSql("'Active'");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("category_ibfk_1");
        });

        modelBuilder.Entity<CategorySetting>(entity =>
        {
            entity.HasKey(e => e.CategorySettingId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(d => d.Category).WithMany(p => p.CategorySettings).HasConstraintName("category_setting_ibfk_1");
        });

        modelBuilder.Entity<Distributor>(entity =>
        {
            entity.HasKey(e => e.DistributorId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        });

        modelBuilder.Entity<ExportRequest>(entity =>
        {
            entity.HasKey(e => e.ExportRequestId).HasName("PRIMARY");
        });

        modelBuilder.Entity<ExportRequestDetail>(entity =>
        {
            entity.HasKey(e => e.ExportRequestDetailId).HasName("PRIMARY");

            entity.HasOne(d => d.ExportRequest).WithMany(p => p.ExportRequestDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("export_request_detail_ibfk_2");

            entity.HasOne(d => d.ProductUnit).WithMany(p => p.ExportRequestDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("export_request_detail_ibfk_1");
        });

        modelBuilder.Entity<ImportRequest>(entity =>
        {
            entity.HasKey(e => e.ImportRequestId).HasName("PRIMARY");
        });

        modelBuilder.Entity<ImportRequestDetail>(entity =>
        {
            entity.HasKey(e => e.ImportRequestDetailId).HasName("PRIMARY");

            entity.HasOne(d => d.ImportRequest).WithMany(p => p.ImportRequestDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("import_request_detail_ibfk_2");

            entity.HasOne(d => d.ProductUnit).WithMany(p => p.ImportRequestDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("import_request_detail_ibfk_1");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        });

        modelBuilder.Entity<Lot>(entity =>
        {
            entity.HasKey(e => e.LotId).HasName("PRIMARY");

            entity.Property(e => e.LotStatus).HasDefaultValueSql("'ACTIVE'");

            entity.HasOne(d => d.ProductUnit).WithMany(p => p.Lots)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lot_ibfk_1");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Medicines).HasConstraintName("medicine_ibfk_2");

            entity.HasOne(d => d.Unit).WithMany(p => p.Medicines).HasConstraintName("medicine_unit_FK");

            entity.HasMany(d => d.Ingredients).WithMany(p => p.Medicines)
                .UsingEntity<Dictionary<string, object>>(
                    "MedicineIngredient",
                    r => r.HasOne<Ingredient>().WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("medicine_ingredient_ibfk_1"),
                    l => l.HasOne<Medicine>().WithMany()
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("medicine_ingredient_ibfk_2"),
                    j =>
                    {
                        j.HasKey("MedicineId", "IngredientId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("medicine_ingredient");
                        j.HasIndex(new[] { "IngredientId" }, "ingredient_id");
                        j.HasIndex(new[] { "MedicineId" }, "medicine_id");
                        j.IndexerProperty<int>("MedicineId").HasColumnName("medicine_id");
                        j.IndexerProperty<int>("IngredientId").HasColumnName("ingredient_id");
                    });
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PRIMARY");

            entity.HasOne(d => d.Receiver).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notification_account_FK");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ibfk_2");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductCreatedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ibfk_3");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products).HasConstraintName("product_ibfk_1");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Products).HasConstraintName("product_ibfk_5");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ProductUpdatedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ibfk_4");
        });

        modelBuilder.Entity<ProductAttribute>(entity =>
        {
            entity.HasKey(e => e.ProductAttributeId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        });

        modelBuilder.Entity<ProductAttributeMapping>(entity =>
        {
            entity.HasKey(e => e.ProductAtMpId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(d => d.ProductAttribute).WithMany(p => p.ProductAttributeMappings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_attribute_mapping_ibfk_1");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductAttributeMappings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_attribute_mapping_ibfk_2");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_image_ibfk_1");
        });

        modelBuilder.Entity<ProductStock>(entity =>
        {
            entity.HasKey(e => e.ProductStockId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsCurrent).HasDefaultValueSql("'1'");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.ProductUnit).WithMany(p => p.ProductStocks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_stock_ibfk_1");
        });

        modelBuilder.Entity<ProductUnit>(entity =>
        {
            entity.HasKey(e => e.ProductUnitId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("product_unit_product_unit_FK");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductUnits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_unit_ibfk_2");

            entity.HasOne(d => d.Unit).WithMany(p => p.ProductUnits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_unit_ibfk_1");
        });

        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasKey(e => e.ShipmentId).HasName("PRIMARY");

            entity.HasOne(d => d.Distributor).WithMany(p => p.Shipments).HasConstraintName("shipment_ibfk_3");

            entity.HasOne(d => d.ExportRequest).WithMany(p => p.Shipments).HasConstraintName("shipment_export_request_FK");
        });

        modelBuilder.Entity<ShipmentDetail>(entity =>
        {
            entity.HasKey(e => e.ShipmentDetailId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Lot).WithMany(p => p.ShipmentDetails).HasConstraintName("shipment_detail_ibfk_2");

            entity.HasOne(d => d.ProductUnit).WithMany(p => p.ShipmentDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shipment_detail_ibfk_1");

            entity.HasOne(d => d.Shipment).WithMany(p => p.ShipmentDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shipment_detail_ibfk_3");
        });

        modelBuilder.Entity<StockCheck>(entity =>
        {
            entity.HasKey(e => e.StockCheckId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.DueDate).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.Priority).HasDefaultValueSql("'Medium'");
            entity.Property(e => e.StartDate).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.Status).HasDefaultValueSql("'Todo'");
            entity.Property(e => e.StockCheckType).HasDefaultValueSql("'Regular'");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.StockCheckCreatedByNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_created_by_id");

            entity.HasOne(d => d.Staff).WithMany(p => p.StockCheckStaffs).HasConstraintName("stock_check_ibfk_1");

            entity.HasOne(d => d.Stockkeeper).WithMany(p => p.StockCheckStockkeepers).HasConstraintName("fk_stockkeeper_id");
        });

        modelBuilder.Entity<StockCheckDetail>(entity =>
        {
            entity.HasKey(e => e.StockCheckDetailId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.Status).HasDefaultValueSql("'Todo'");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(d => d.ProductUnit).WithMany(p => p.StockCheckDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stock_check_detail_ibfk_2");

            entity.HasOne(d => d.StockCheck).WithMany(p => p.StockCheckDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stock_check_detail_ibfk_1");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
