using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nhom7_webTourdulich.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable666655 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_KhachHang_MaKhachHangNavigationMaKhachHang",
                table: "HoaDon");

            migrationBuilder.AlterColumn<int>(
                name: "MaKhachHangNavigationMaKhachHang",
                table: "HoaDon",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_KhachHang_MaKhachHangNavigationMaKhachHang",
                table: "HoaDon",
                column: "MaKhachHangNavigationMaKhachHang",
                principalTable: "KhachHang",
                principalColumn: "Ma_Khach_Hang");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_KhachHang_MaKhachHangNavigationMaKhachHang",
                table: "HoaDon");

            migrationBuilder.AlterColumn<int>(
                name: "MaKhachHangNavigationMaKhachHang",
                table: "HoaDon",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_KhachHang_MaKhachHangNavigationMaKhachHang",
                table: "HoaDon",
                column: "MaKhachHangNavigationMaKhachHang",
                principalTable: "KhachHang",
                principalColumn: "Ma_Khach_Hang",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
