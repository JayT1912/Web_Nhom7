using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nhom7_webTourdulich.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable66665995 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ChiTietHo__Ma_Ho__5629CD9C",
                table: "ChiTietHoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_TourImage_Tour_TourMaTour",
                table: "TourImage");

            migrationBuilder.AlterColumn<int>(
                name: "TourMaTour",
                table: "TourImage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietHo__Ma_Ho__151B244E",
                table: "ChiTietHoaDon",
                column: "Ma_Hoa_Don",
                principalTable: "HoaDon",
                principalColumn: "Ma_Hoa_Don");

            migrationBuilder.AddForeignKey(
                name: "FK_TourImage_Tour_TourMaTour",
                table: "TourImage",
                column: "TourMaTour",
                principalTable: "Tour",
                principalColumn: "Ma_Tour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ChiTietHo__Ma_Ho__151B244E",
                table: "ChiTietHoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_TourImage_Tour_TourMaTour",
                table: "TourImage");

            migrationBuilder.AlterColumn<int>(
                name: "TourMaTour",
                table: "TourImage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietHo__Ma_Ho__5629CD9C",
                table: "ChiTietHoaDon",
                column: "Ma_Hoa_Don",
                principalTable: "HoaDon",
                principalColumn: "Ma_Hoa_Don");

            migrationBuilder.AddForeignKey(
                name: "FK_TourImage_Tour_TourMaTour",
                table: "TourImage",
                column: "TourMaTour",
                principalTable: "Tour",
                principalColumn: "Ma_Tour",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
