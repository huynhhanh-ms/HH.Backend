using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HH.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "note",
                table: "session",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "note",
                table: "fuel_import",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "note",
                table: "session");

            migrationBuilder.DropColumn(
                name: "note",
                table: "fuel_import");
        }
    }
}
