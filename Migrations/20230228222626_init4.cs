using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISpaceSRM.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "setServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deiscount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceSetService",
                columns: table => new
                {
                    ServicesId = table.Column<int>(type: "int", nullable: false),
                    SetServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceSetService", x => new { x.ServicesId, x.SetServicesId });
                    table.ForeignKey(
                        name: "FK_ServiceSetService_services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceSetService_setServices_SetServicesId",
                        column: x => x.SetServicesId,
                        principalTable: "setServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSetService_SetServicesId",
                table: "ServiceSetService",
                column: "SetServicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceSetService");

            migrationBuilder.DropTable(
                name: "setServices");
        }
    }
}
