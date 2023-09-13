using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISpaceSRM.Migrations
{
    /// <inheritdoc />
    public partial class init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_records_RecordAfterId",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_records_RecordBeforeId",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_works_records_RecordId",
                table: "works");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "photos");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_RecordBeforeId",
                table: "photos",
                newName: "IX_photos_RecordBeforeId");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_RecordAfterId",
                table: "photos",
                newName: "IX_photos_RecordAfterId");

            migrationBuilder.AlterColumn<int>(
                name: "RecordId",
                table: "works",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "works",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "works",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BodySize",
                table: "records",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BodyType",
                table: "records",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "records",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_photos",
                table: "photos",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_works_records_RecordId",
                table: "works",
                column: "RecordId",
                principalTable: "records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photos_records_RecordAfterId",
                table: "photos");

            migrationBuilder.DropForeignKey(
                name: "FK_photos_records_RecordBeforeId",
                table: "photos");

            migrationBuilder.DropForeignKey(
                name: "FK_works_records_RecordId",
                table: "works");

            migrationBuilder.DropPrimaryKey(
                name: "PK_photos",
                table: "photos");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "works");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "works");

            migrationBuilder.DropColumn(
                name: "BodySize",
                table: "records");

            migrationBuilder.DropColumn(
                name: "BodyType",
                table: "records");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "records");

            migrationBuilder.RenameTable(
                name: "photos",
                newName: "Photo");

            migrationBuilder.RenameIndex(
                name: "IX_photos_RecordBeforeId",
                table: "Photo",
                newName: "IX_Photo_RecordBeforeId");

            migrationBuilder.RenameIndex(
                name: "IX_photos_RecordAfterId",
                table: "Photo",
                newName: "IX_Photo_RecordAfterId");

            migrationBuilder.AlterColumn<int>(
                name: "RecordId",
                table: "works",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_works_records_RecordId",
                table: "works",
                column: "RecordId",
                principalTable: "records",
                principalColumn: "Id");
        }
    }
}
