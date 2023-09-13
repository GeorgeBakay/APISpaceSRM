using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISpaceSRM.Migrations
{
    /// <inheritdoc />
    public partial class init13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photos_records_RecordAfterId",
                table: "photos");

            migrationBuilder.DropForeignKey(
                name: "FK_photos_records_RecordBeforeId",
                table: "photos");

            migrationBuilder.DropIndex(
                name: "IX_photos_RecordBeforeId",
                table: "photos");

            migrationBuilder.RenameColumn(
                name: "RecordBeforeId",
                table: "photos",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "RecordAfterId",
                table: "photos",
                newName: "RecordId");

            migrationBuilder.RenameIndex(
                name: "IX_photos_RecordAfterId",
                table: "photos",
                newName: "IX_photos_RecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_photos_records_RecordId",
                table: "photos",
                column: "RecordId",
                principalTable: "records",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photos_records_RecordId",
                table: "photos");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "photos",
                newName: "RecordBeforeId");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "photos",
                newName: "RecordAfterId");

            migrationBuilder.RenameIndex(
                name: "IX_photos_RecordId",
                table: "photos",
                newName: "IX_photos_RecordAfterId");

            migrationBuilder.CreateIndex(
                name: "IX_photos_RecordBeforeId",
                table: "photos",
                column: "RecordBeforeId");

            migrationBuilder.AddForeignKey(
                name: "FK_photos_records_RecordAfterId",
                table: "photos",
                column: "RecordAfterId",
                principalTable: "records",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_photos_records_RecordBeforeId",
                table: "photos",
                column: "RecordBeforeId",
                principalTable: "records",
                principalColumn: "Id");
        }
    }
}
