using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISpaceSRM.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Procent = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "records",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Sum = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_records_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileExtention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecordId = table.Column<int>(type: "int", nullable: true),
                    RecordId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "records",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Photo_records_RecordId1",
                        column: x => x.RecordId1,
                        principalTable: "records",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    RecordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_works_records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "records",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_works_services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployerWork",
                columns: table => new
                {
                    EmployersId = table.Column<int>(type: "int", nullable: false),
                    WorksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerWork", x => new { x.EmployersId, x.WorksId });
                    table.ForeignKey(
                        name: "FK_EmployerWork_employers_EmployersId",
                        column: x => x.EmployersId,
                        principalTable: "employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployerWork_works_WorksId",
                        column: x => x.WorksId,
                        principalTable: "works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerWork_WorksId",
                table: "EmployerWork",
                column: "WorksId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_RecordId",
                table: "Photo",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_RecordId1",
                table: "Photo",
                column: "RecordId1");

            migrationBuilder.CreateIndex(
                name: "IX_records_ClientId",
                table: "records",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_works_RecordId",
                table: "works",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_works_ServiceId",
                table: "works",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerWork");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "employers");

            migrationBuilder.DropTable(
                name: "works");

            migrationBuilder.DropTable(
                name: "records");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "clients");
        }
    }
}
