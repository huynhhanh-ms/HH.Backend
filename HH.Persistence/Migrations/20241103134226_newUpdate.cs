using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HH.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "weighing_history",
                newName: "created_at"
                );

            migrationBuilder.RenameColumn(
                name: "updated_date",
                table: "weighing_history",
                newName: "updated_at"
                );

            //migrationBuilder.RenameColumn(
            //    name: "UpdatedAt",
            //    table: "weighing_history",
            //    newName: "updated_at");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "weighing_history",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.RenameColumn(
            //    name: "CreatedAt",
            //    table: "weighing_history",
            //    newName: "created_at");

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "updated_at",
            //    table: "weighing_history",
            //    type: "timestamp without time zone",
            //    nullable: true,
            //    defaultValueSql: "CURRENT_TIMESTAMP",
            //    oldClrType: typeof(DateTime),
            //    oldType: "timestamp with time zone",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "created_at",
            //    table: "weighing_history",
            //    type: "timestamp without time zone",
            //    nullable: true,
            //    defaultValueSql: "CURRENT_TIMESTAMP",
            //    oldClrType: typeof(DateTime),
            //    oldType: "timestamp with time zone",
            //    oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "weighing_history",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "total_cost",
                table: "weighing_history",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "weighing_history");

            migrationBuilder.DropColumn(
                name: "total_cost",
                table: "weighing_history");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "weighing_history",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "weighing_history",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "weighing_history",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "weighing_history",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "weighing_history",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "weighing_history",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_date",
                table: "weighing_history",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP");
        }
    }
}
