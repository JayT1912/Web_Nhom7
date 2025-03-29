using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nhom7_webTourdulich.Migrations
{
    /// <inheritdoc />
    public partial class eee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourImages_Tour_TourMaTour",
                table: "TourImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourImages",
                table: "TourImages");

            migrationBuilder.RenameTable(
                name: "TourImages",
                newName: "Tour_Images");

            migrationBuilder.RenameIndex(
                name: "IX_TourImages_TourMaTour",
                table: "Tour_Images",
                newName: "IX_Tour_Images_TourMaTour");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tour_Images",
                table: "Tour_Images",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_Images_Tour_TourMaTour",
                table: "Tour_Images",
                column: "TourMaTour",
                principalTable: "Tour",
                principalColumn: "Ma_Tour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tour_Images_Tour_TourMaTour",
                table: "Tour_Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tour_Images",
                table: "Tour_Images");

            migrationBuilder.RenameTable(
                name: "Tour_Images",
                newName: "TourImages");

            migrationBuilder.RenameIndex(
                name: "IX_Tour_Images_TourMaTour",
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
    }
}
