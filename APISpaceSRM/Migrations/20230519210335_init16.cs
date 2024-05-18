using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISpaceSRM.Migrations
{
    /// <inheritdoc />
    public partial class init16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photos_records_RecordId",
                table: "photos");

            migrationBuilder.AddForeignKey(
                name: "FK_photos_records_RecordId",
                table: "photos",
                column: "RecordId",
                principalTable: "records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photos_records_RecordId",
                table: "photos");

            migrationBuilder.AddForeignKey(
                name: "FK_photos_records_RecordId",
                table: "photos",
                column: "RecordId",
                principalTable: "records",
                principalColumn: "Id");
        }
    }
}
