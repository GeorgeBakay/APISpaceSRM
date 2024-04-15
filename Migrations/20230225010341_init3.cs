using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISpaceSRM.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_records_RecordId",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_records_RecordId1",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_RecordId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_RecordId1",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "RecordId1",
                table: "Photo");

            migrationBuilder.AlterColumn<long>(
                name: "Size",
                table: "Photo",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "RecordAfterId",
                table: "Photo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecordBeforeId",
                table: "Photo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_RecordAfterId",
                table: "Photo",
                column: "RecordAfterId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_RecordBeforeId",
                table: "Photo",
                column: "RecordBeforeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_records_RecordAfterId",
                table: "Photo",
                column: "RecordAfterId",
                principalTable: "records",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_records_RecordBeforeId",
                table: "Photo",
                column: "RecordBeforeId",
                principalTable: "records",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_records_RecordAfterId",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_records_RecordBeforeId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_RecordAfterId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_RecordBeforeId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "RecordAfterId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "RecordBeforeId",
                table: "Photo");

            migrationBuilder.AlterColumn<decimal>(
                name: "Size",
                table: "Photo",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "RecordId",
                table: "Photo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecordId1",
                table: "Photo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_RecordId",
                table: "Photo",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_RecordId1",
                table: "Photo",
                column: "RecordId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_records_RecordId",
                table: "Photo",
                column: "RecordId",
                principalTable: "records",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_records_RecordId1",
                table: "Photo",
                column: "RecordId1",
                principalTable: "records",
                principalColumn: "Id");
        }
    }
}
