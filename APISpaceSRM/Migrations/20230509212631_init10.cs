using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISpaceSRM.Migrations
{
    /// <inheritdoc />
    public partial class init10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_records_setServices_SerServiceId",
                table: "records");

            migrationBuilder.DropIndex(
                name: "IX_records_SerServiceId",
                table: "records");

            migrationBuilder.DropColumn(
                name: "SerServiceId",
                table: "records");

            migrationBuilder.AddColumn<int>(
                name: "SetServiceId",
                table: "records",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_records_SetServiceId",
                table: "records",
                column: "SetServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_records_setServices_SetServiceId",
                table: "records",
                column: "SetServiceId",
                principalTable: "setServices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_records_setServices_SetServiceId",
                table: "records");

            migrationBuilder.DropIndex(
                name: "IX_records_SetServiceId",
                table: "records");

            migrationBuilder.DropColumn(
                name: "SetServiceId",
                table: "records");

            migrationBuilder.AddColumn<int>(
                name: "SerServiceId",
                table: "records",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_records_SerServiceId",
                table: "records",
                column: "SerServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_records_setServices_SerServiceId",
                table: "records",
                column: "SerServiceId",
                principalTable: "setServices",
                principalColumn: "Id");
        }
    }
}
