using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nhom7_webTourdulich.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChucVu",
                columns: table => new
                {
                    Ma_Chuc_Vu = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_Chuc_Vu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChucVu__EFE449723DD79615", x => x.Ma_Chuc_Vu);
                });

            migrationBuilder.CreateTable(
                name: "DiemDen",
                columns: table => new
                {
                    Ma_Diem_Den = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Thanh_Pho = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DiemDen__34267B5E86B79B2E", x => x.Ma_Diem_Den);
                });

            migrationBuilder.CreateTable(
                name: "GiaTour",
                columns: table => new
                {
                    Ma_Gia_Tour = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gia = table.Column<decimal>(type: "money", nullable: false),
                    Ngay_Bat_Dau = table.Column<DateOnly>(type: "date", nullable: false),
                    Ngay_Ket_Thuc = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GiaTour__6B20AB467AE194FF", x => x.Ma_Gia_Tour);
                });

            migrationBuilder.CreateTable(
                name: "LoaiTour",
                columns: table => new
                {
                    Ma_Loai_Tour = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_Loai_Tour = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LoaiTour__ACE9E7DAC83A2409", x => x.Ma_Loai_Tour);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RememberMe = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PhuongTien",
                columns: table => new
                {
                    Ma_Phuong_Tien = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Trang_Thai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhuongTi__09E91370EB43D8E5", x => x.Ma_Phuong_Tien);
                });

            migrationBuilder.CreateTable(
                name: "Registers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepeatPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsTermsAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrangThai",
                columns: table => new
                {
                    Ma_Trang_Thai = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_Trang_Thai = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrangTha__B9CD9D6BDB4AF9BE", x => x.Ma_Trang_Thai);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    Ma_Nhan_Vien = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ma_Chuc_Vu = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false),
                    Hinh_Anh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LinkFB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkZL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkIG = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhanVien__7AB896891713CABD", x => x.Ma_Nhan_Vien);
                    table.ForeignKey(
                        name: "FK__NhanVien__Ma_Chu__4316F928",
                        column: x => x.Ma_Chuc_Vu,
                        principalTable: "ChucVu",
                        principalColumn: "Ma_Chuc_Vu");
                });

            migrationBuilder.CreateTable(
                name: "KhuyenMai",
                columns: table => new
                {
                    Ma_Khuyen_Mai = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_Khuyen_Mai = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ma_Gia_Tour = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false),
                    Phan_Tram_Giam = table.Column<double>(type: "float", nullable: true),
                    Gia_Giam = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Ngay_Bat_Dau = table.Column<DateOnly>(type: "date", nullable: false),
                    Ngay_Ket_Thuc = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KhuyenMa__19F298C5BD513EEA", x => x.Ma_Khuyen_Mai);
                    table.ForeignKey(
                        name: "FK__KhuyenMai__Ma_Gi__59FA5E80",
                        column: x => x.Ma_Gia_Tour,
                        principalTable: "GiaTour",
                        principalColumn: "Ma_Gia_Tour");
                });

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    Ma_Tour = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ma_Loai_Tour = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false),
                    Ma_Gia_Tour = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false),
                    Ma_Diem_Den = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false),
                    So_Ngay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    So_Luong_Nguoi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tour__1C19FFEADA1259D4", x => x.Ma_Tour);
                    table.ForeignKey(
                        name: "FK__Tour__Ma_Diem_De__49C3F6B7",
                        column: x => x.Ma_Diem_Den,
                        principalTable: "DiemDen",
                        principalColumn: "Ma_Diem_Den");
                    table.ForeignKey(
                        name: "FK__Tour__Ma_Gia_Tou__48CFD27E",
                        column: x => x.Ma_Gia_Tour,
                        principalTable: "GiaTour",
                        principalColumn: "Ma_Gia_Tour");
                    table.ForeignKey(
                        name: "FK__Tour__Ma_Loai_To__47DBAE45",
                        column: x => x.Ma_Loai_Tour,
                        principalTable: "LoaiTour",
                        principalColumn: "Ma_Loai_Tour");
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhGias",
                columns: table => new
                {
                    MaDanhGia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoSao = table.Column<int>(type: "int", nullable: false),
                    NgayDanhGia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaTour = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGias", x => x.MaDanhGia);
                    table.ForeignKey(
                        name: "FK_DanhGias_Tour_MaTour",
                        column: x => x.MaTour,
                        principalTable: "Tour",
                        principalColumn: "Ma_Tour",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    Ma_Khach_Hang = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Sdt = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Dia_Chi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gioi_Tinh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Quoc_Tich = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Hinh_Anh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaTour = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KhachHan__37BB3C420E78E4C3", x => x.Ma_Khach_Hang);
                    table.ForeignKey(
                        name: "FK_KhachHang_Tour_MaTour",
                        column: x => x.MaTour,
                        principalTable: "Tour",
                        principalColumn: "Ma_Tour");
                });

            migrationBuilder.CreateTable(
                name: "NhomTour",
                columns: table => new
                {
                    Ma_Nhom_Tour = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_Tour = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false),
                    Ngay_Khoi_Hanh = table.Column<DateOnly>(type: "date", nullable: false),
                    Ngay_Ket_Thuc = table.Column<DateOnly>(type: "date", nullable: false),
                    Diem_Xuat_Phat = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ma_Trang_Thai = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false),
                    So_Luong_Nguoi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Noi_Dung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhomTour__3D75DE0FA98AB25B", x => x.Ma_Nhom_Tour);
                    table.ForeignKey(
                        name: "FK__NhomTour__Ma_Tou__4CA06362",
                        column: x => x.Ma_Tour,
                        principalTable: "Tour",
                        principalColumn: "Ma_Tour");
                    table.ForeignKey(
                        name: "FK__NhomTour__Ma_Tra__4D94879B",
                        column: x => x.Ma_Trang_Thai,
                        principalTable: "TrangThai",
                        principalColumn: "Ma_Trang_Thai");
                });

            migrationBuilder.CreateTable(
                name: "TourImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTour = table.Column<int>(type: "int", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourMaTour = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourImage_Tour_TourMaTour",
                        column: x => x.TourMaTour,
                        principalTable: "Tour",
                        principalColumn: "Ma_Tour",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    Ma_Hoa_Don = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ma_Khach_Hang = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false),
                    Ma_Nhom_Tour = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false),
                    Ngay_Lap = table.Column<DateOnly>(type: "date", nullable: false),
                    Tong_Tien = table.Column<decimal>(type: "money", nullable: false),
                    Diem_Don = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ngay_Di = table.Column<DateOnly>(type: "date", nullable: false),
                    Gio_Di = table.Column<TimeOnly>(type: "time", nullable: false),
                    Thanh_Toan = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    MaKhachHangNavigationMaKhachHang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HoaDon__91EF063FFF61E16D", x => x.Ma_Hoa_Don);
                    table.ForeignKey(
                        name: "FK_HoaDon_KhachHang_MaKhachHangNavigationMaKhachHang",
                        column: x => x.MaKhachHangNavigationMaKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "Ma_Khach_Hang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__HoaDon__Ma_Nhom___52593CB8",
                        column: x => x.Ma_Nhom_Tour,
                        principalTable: "NhomTour",
                        principalColumn: "Ma_Nhom_Tour");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDon",
                columns: table => new
                {
                    Ma_Hoa_Don = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Ma_Khach_Hang = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false),
                    Gia_Tour = table.Column<decimal>(type: "money", nullable: false),
                    So_Luong = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Thanh_Tien = table.Column<decimal>(type: "money", nullable: true, computedColumnSql: "([Gia_Tour]*[So_Luong])", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietH__A294B5FB13EB6400", x => new { x.Ma_Hoa_Don, x.Ma_Khach_Hang });
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDon_KhachHang_Ma_Khach_Hang",
                        column: x => x.Ma_Khach_Hang,
                        principalTable: "KhachHang",
                        principalColumn: "Ma_Khach_Hang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ChiTietHo__Ma_Ho__5629CD9C",
                        column: x => x.Ma_Hoa_Don,
                        principalTable: "HoaDon",
                        principalColumn: "Ma_Hoa_Don");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_Ma_Khach_Hang",
                table: "ChiTietHoaDon",
                column: "Ma_Khach_Hang");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGias_MaTour",
                table: "DanhGias",
                column: "MaTour");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_Ma_Nhom_Tour",
                table: "HoaDon",
                column: "Ma_Nhom_Tour");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaKhachHangNavigationMaKhachHang",
                table: "HoaDon",
                column: "MaKhachHangNavigationMaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_MaTour",
                table: "KhachHang",
                column: "MaTour");

            migrationBuilder.CreateIndex(
                name: "IX_KhuyenMai_Ma_Gia_Tour",
                table: "KhuyenMai",
                column: "Ma_Gia_Tour");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_Ma_Chuc_Vu",
                table: "NhanVien",
                column: "Ma_Chuc_Vu");

            migrationBuilder.CreateIndex(
                name: "IX_NhomTour_Ma_Tour",
                table: "NhomTour",
                column: "Ma_Tour");

            migrationBuilder.CreateIndex(
                name: "IX_NhomTour_Ma_Trang_Thai",
                table: "NhomTour",
                column: "Ma_Trang_Thai");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_Ma_Diem_Den",
                table: "Tour",
                column: "Ma_Diem_Den");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_Ma_Gia_Tour",
                table: "Tour",
                column: "Ma_Gia_Tour");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_Ma_Loai_Tour",
                table: "Tour",
                column: "Ma_Loai_Tour");

            migrationBuilder.CreateIndex(
                name: "IX_TourImage_TourMaTour",
                table: "TourImage",
                column: "TourMaTour");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietHoaDon");

            migrationBuilder.DropTable(
                name: "DanhGias");

            migrationBuilder.DropTable(
                name: "KhuyenMai");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "PhuongTien");

            migrationBuilder.DropTable(
                name: "Registers");

            migrationBuilder.DropTable(
                name: "TourImage");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "ChucVu");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NhomTour");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "TrangThai");

            migrationBuilder.DropTable(
                name: "DiemDen");

            migrationBuilder.DropTable(
                name: "GiaTour");

            migrationBuilder.DropTable(
                name: "LoaiTour");
        }
    }
}
