using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HH.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "expense");

            migrationBuilder.DropTable(
                name: "fuel_import_session");

            migrationBuilder.DropTable(
                name: "fuel_price");

            migrationBuilder.DropTable(
                name: "lubricant");

            migrationBuilder.DropTable(
                name: "petrol_pump");

            migrationBuilder.DropTable(
                name: "trigger_log");

            migrationBuilder.DropTable(
                name: "weighing_history");

            migrationBuilder.DropTable(
                name: "expense_type");

            migrationBuilder.DropTable(
                name: "fuel_import");

            migrationBuilder.DropTable(
                name: "session");

            migrationBuilder.DropTable(
                name: "tank");

            migrationBuilder.DropTable(
                name: "fuel_type");
        }
    }
}
