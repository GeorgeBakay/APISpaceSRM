using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISpaceSRM.Migrations
{
    /// <inheritdoc />
    public partial class init18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SendMessage",
                table: "records",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendMessage",
                table: "records");
        }
    }
}
