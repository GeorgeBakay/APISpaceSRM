using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISpaceSRM.Migrations
{
    /// <inheritdoc />
    public partial class init20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameCost",
                table: "works");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameCost",
                table: "works",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
