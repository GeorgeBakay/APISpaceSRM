using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISpaceSRM.Migrations
{
    /// <inheritdoc />
    public partial class init22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_records_setServices_SetServiceId",
                table: "records");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceSetService_setServices_SetServicesId",
                table: "ServiceSetService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_setServices",
                table: "setServices");

            migrationBuilder.RenameTable(
                name: "setServices",
                newName: "SetService");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SetService",
                table: "SetService",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_records_SetService_SetServiceId",
                table: "records",
                column: "SetServiceId",
                principalTable: "SetService",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceSetService_SetService_SetServicesId",
                table: "ServiceSetService",
                column: "SetServicesId",
                principalTable: "SetService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_records_SetService_SetServiceId",
                table: "records");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceSetService_SetService_SetServicesId",
                table: "ServiceSetService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SetService",
                table: "SetService");

            migrationBuilder.RenameTable(
                name: "SetService",
                newName: "setServices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_setServices",
                table: "setServices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_records_setServices_SetServiceId",
                table: "records",
                column: "SetServiceId",
                principalTable: "setServices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceSetService_setServices_SetServicesId",
                table: "ServiceSetService",
                column: "SetServicesId",
                principalTable: "setServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
