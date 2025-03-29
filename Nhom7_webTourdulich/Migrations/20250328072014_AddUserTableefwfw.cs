using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nhom7_webTourdulich.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTableefwfw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourImage_Tour_TourMaTour",
                table: "TourImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourImage",
                table: "TourImage");

            migrationBuilder.DropColumn(
                name: "QRScanned",
                table: "HoaDon");

            migrationBuilder.RenameTable(
                name: "TourImage",
                newName: "TourImages");

            migrationBuilder.RenameIndex(
                name: "IX_TourImage_TourMaTour",
                table: "TourImages",
                newName: "IX_TourImages_TourMaTour");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourImages",
                table: "TourImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourImages_Tour_TourMaTour",
                table: "TourImages",
                column: "TourMaTour",
                principalTable: "Tour",
                principalColumn: "Ma_Tour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourImages_Tour_TourMaTour",
                table: "TourImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourImages",
                table: "TourImages");

            migrationBuilder.RenameTable(
                name: "TourImages",
                newName: "TourImage");

            migrationBuilder.RenameIndex(
                name: "IX_TourImages_TourMaTour",
                table: "TourImage",
                newName: "IX_TourImage_TourMaTour");

            migrationBuilder.AddColumn<bool>(
                name: "QRScanned",
                table: "HoaDon",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourImage",
                table: "TourImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourImage_Tour_TourMaTour",
                table: "TourImage",
                column: "TourMaTour",
                principalTable: "Tour",
                principalColumn: "Ma_Tour");
        }
    }
}
