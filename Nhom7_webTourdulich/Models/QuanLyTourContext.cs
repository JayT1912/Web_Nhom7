using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Nhom7_webTourdulich.Models;

public partial class QuanLyTourContext : DbContext
{
    public QuanLyTourContext()
    {
    }

    public QuanLyTourContext(DbContextOptions<QuanLyTourContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public virtual DbSet<ChucVu> ChucVus { get; set; }

    public virtual DbSet<DiemDen> DiemDens { get; set; }

    public virtual DbSet<GiaTour> GiaTours { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<LoaiTour> LoaiTours { get; set; }
    
    public virtual DbSet<DanhGia>  DanhGias { get; set; }


    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<NhomTour> NhomTours { get; set; }

    public virtual DbSet<PhuongTien> PhuongTiens { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<TrangThai> TrangThais { get; set; }

    public virtual DbSet<Register> Registers { get; set; }

    public virtual DbSet<UserImage> UserImages {get; set;}

    public virtual DbSet<User> Users {get; set;}

    public virtual DbSet<Login> Logins { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    // Map entity Tour đến bảng "tour"
    modelBuilder.Entity<Tour>().ToTable("tour");
        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => new { e.MaHoaDon, e.MaKhachHang }).HasName("PK__ChiTietH__A294B5FBC6D9C49E");

        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => new { e.MaHoaDon, e.MaKhachHang }).HasName("PK__ChiTietH__A294B5FB13EB6400");

            entity.ToTable("ChiTietHoaDon");

            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Hoa_Don");
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Khach_Hang");
            entity.Property(e => e.GiaTour)
                .HasColumnType("money")
                .HasColumnName("Gia_Tour");
            entity.Property(e => e.SoLuong)
                .HasDefaultValue(1)
                .HasColumnName("So_Luong");
            entity.Property(e => e.ThanhTien)
                .HasComputedColumnSql("([Gia_Tour]*[So_Luong])", true)
                .HasColumnType("money")
                .HasColumnName("Thanh_Tien");

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaHoaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHo__Ma_Ho__151B244E")

                .HasConstraintName("FK__ChiTietHo__Ma_Ho__5629CD9C");

           
        });

        modelBuilder.Entity<ChucVu>(entity =>
        {
            entity.HasKey(e => e.MaChucVu).HasName("PK__ChucVu__EFE449720DAF3C2C");

            entity.HasKey(e => e.MaChucVu).HasName("PK__ChucVu__EFE449723DD79615");

            entity.ToTable("ChucVu");

            entity.Property(e => e.MaChucVu)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Chuc_Vu");
            entity.Property(e => e.TenChucVu)
                .HasMaxLength(255)
                .HasColumnName("Ten_Chuc_Vu");
        });

        modelBuilder.Entity<DiemDen>(entity =>
        {
            entity.HasKey(e => e.MaDiemDen).HasName("PK__DiemDen__34267B5EDBE1CB85");

            entity.HasKey(e => e.MaDiemDen).HasName("PK__DiemDen__34267B5E86B79B2E");

            entity.ToTable("DiemDen");

            entity.Property(e => e.MaDiemDen)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Diem_Den");
            entity.Property(e => e.Ten).HasMaxLength(255);
            entity.Property(e => e.ThanhPho)
                .HasMaxLength(255)
                .HasColumnName("Thanh_Pho");
        });

        modelBuilder.Entity<GiaTour>(entity =>
        {
            entity.HasKey(e => e.MaGiaTour).HasName("PK__GiaTour__6B20AB462CA1B26C");

            entity.HasKey(e => e.MaGiaTour).HasName("PK__GiaTour__6B20AB467AE194FF");

            entity.ToTable("GiaTour");

            entity.Property(e => e.MaGiaTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Gia_Tour");
            entity.Property(e => e.Gia).HasColumnType("money");
            entity.Property(e => e.NgayBatDau).HasColumnName("Ngay_Bat_Dau");
            entity.Property(e => e.NgayKetThuc).HasColumnName("Ngay_Ket_Thuc");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("PK__HoaDon__91EF063FC4157924");

            entity.HasKey(e => e.MaHoaDon).HasName("PK__HoaDon__91EF063FFF61E16D");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Hoa_Don");
            entity.Property(e => e.DiemDon)
                .HasMaxLength(255)
                .HasColumnName("Diem_Don");
            entity.Property(e => e.GioDi).HasColumnName("Gio_Di");
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Khach_Hang");
            entity.Property(e => e.MaNhomTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Nhom_Tour");
            entity.Property(e => e.NgayDi).HasColumnName("Ngay_Di");
            entity.Property(e => e.NgayLap).HasColumnName("Ngay_Lap");
            entity.Property(e => e.ThanhToan)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Thanh_Toan");
            entity.Property(e => e.TongTien)
                .HasColumnType("money")
                .HasColumnName("Tong_Tien");

        
            entity.HasOne(d => d.MaNhomTourNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaNhomTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__Ma_Nhom___114A936A")

                .HasConstraintName("FK__HoaDon__Ma_Nhom___52593CB8");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {

            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__37BB3C420E78E4C3");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Khach_Hang");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .HasColumnName("Dia_Chi");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(255)
                .HasColumnName("Gioi_Tinh");
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(255)
                .HasColumnName("Hinh_Anh");
            entity.Property(e => e.QuocTich)
                .HasMaxLength(255)
                .HasColumnName("Quoc_Tich");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Ten).HasMaxLength(255);
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKhuyenMai).HasName("PK__KhuyenMa__19F298C55EA7F7FF");

            entity.HasKey(e => e.MaKhuyenMai).HasName("PK__KhuyenMa__19F298C5BD513EEA");

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.MaKhuyenMai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Khuyen_Mai");
            entity.Property(e => e.GiaGiam)
                .HasMaxLength(255)
                .HasColumnName("Gia_Giam");
            entity.Property(e => e.MaGiaTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Gia_Tour");
            entity.Property(e => e.NgayBatDau).HasColumnName("Ngay_Bat_Dau");
            entity.Property(e => e.NgayKetThuc).HasColumnName("Ngay_Ket_Thuc");
            entity.Property(e => e.PhanTramGiam).HasColumnName("Phan_Tram_Giam");
            entity.Property(e => e.TenKhuyenMai)
                .HasMaxLength(255)
                .HasColumnName("Ten_Khuyen_Mai");

            entity.HasOne(d => d.MaGiaTourNavigation).WithMany(p => p.KhuyenMais)
                .HasForeignKey(d => d.MaGiaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KhuyenMai__Ma_Gi__6C190EBB")

                .HasConstraintName("FK__KhuyenMai__Ma_Gi__59FA5E80");
        });

        modelBuilder.Entity<LoaiTour>(entity =>
        {
            entity.HasKey(e => e.MaLoaiTour).HasName("PK__LoaiTour__ACE9E7DA26904217");

            entity.HasKey(e => e.MaLoaiTour).HasName("PK__LoaiTour__ACE9E7DAC83A2409");

            entity.ToTable("LoaiTour");

            entity.Property(e => e.MaLoaiTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Loai_Tour");
            entity.Property(e => e.TenLoaiTour)
                .HasMaxLength(255)
                .HasColumnName("Ten_Loai_Tour");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__NhanVien__7AB89689143E91C2");

            entity.HasKey(e => e.MaNhanVien).HasName("PK__NhanVien__7AB896891713CABD");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNhanVien)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Nhan_Vien");
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(255)
                .HasColumnName("Hinh_Anh");
            entity.Property(e => e.MaChucVu)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Chuc_Vu");
            entity.Property(e => e.Ten).HasMaxLength(255);

            entity.HasOne(d => d.MaChucVuNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.MaChucVu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVien__Ma_Chu__5812160E")

                .HasConstraintName("FK__NhanVien__Ma_Chu__4316F928");
        });

        modelBuilder.Entity<NhomTour>(entity =>
        {
            entity.HasKey(e => e.MaNhomTour).HasName("PK__NhomTour__3D75DE0FE9D64DA2");

            entity.HasKey(e => e.MaNhomTour).HasName("PK__NhomTour__3D75DE0FA98AB25B");

            entity.ToTable("NhomTour");

            entity.Property(e => e.MaNhomTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Nhom_Tour");
            entity.Property(e => e.DiemXuatPhat)
                .HasMaxLength(255)
                .HasColumnName("Diem_Xuat_Phat");
            entity.Property(e => e.MaTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Tour");
            entity.Property(e => e.MaTrangThai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Trang_Thai");
            entity.Property(e => e.NgayKetThuc).HasColumnName("Ngay_Ket_Thuc");
            entity.Property(e => e.NgayKhoiHanh).HasColumnName("Ngay_Khoi_Hanh");
            entity.Property(e => e.NoiDung).HasColumnName("Noi_Dung");
            entity.Property(e => e.SoLuongNguoi)
                .HasMaxLength(255)
                .HasColumnName("So_Luong_Nguoi");

            entity.HasOne(d => d.MaTourNavigation).WithMany(p => p.NhomTours)
                .HasForeignKey(d => d.MaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhomTour__Ma_Tou__0B91BA14")

                .HasConstraintName("FK__NhomTour__Ma_Tou__4CA06362");

            entity.HasOne(d => d.MaTrangThaiNavigation).WithMany(p => p.NhomTours)
                .HasForeignKey(d => d.MaTrangThai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhomTour__Ma_Tra__0C85DE4D")

                .HasConstraintName("FK__NhomTour__Ma_Tra__4D94879B");
        });

        modelBuilder.Entity<PhuongTien>(entity =>
        {
            entity.HasKey(e => e.MaPhuongTien).HasName("PK__PhuongTi__09E91370F7B68465");

            entity.HasKey(e => e.MaPhuongTien).HasName("PK__PhuongTi__09E91370EB43D8E5");

            entity.ToTable("PhuongTien");

            entity.Property(e => e.MaPhuongTien)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Phuong_Tien");
            entity.Property(e => e.Ten).HasMaxLength(255);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Trang_Thai");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.MaTour).HasName("PK__Tour__1C19FFEA992E3B6A");

            entity.HasKey(e => e.MaTour).HasName("PK__Tour__1C19FFEADA1259D4");

            entity.ToTable("Tour");

            entity.Property(e => e.MaTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Tour");
            entity.Property(e => e.MaDiemDen)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Diem_Den");
            entity.Property(e => e.MaGiaTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Gia_Tour");
            entity.Property(e => e.MaLoaiTour)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Loai_Tour");
            entity.Property(e => e.SoLuongNguoi)
                .HasMaxLength(255)
                .HasColumnName("So_Luong_Nguoi");
            entity.Property(e => e.SoNgay).HasColumnName("So_Ngay");
            entity.Property(e => e.Ten).HasMaxLength(255);

            entity.HasOne(d => d.MaDiemDenNavigation).WithMany(p => p.Tours)
                .HasForeignKey(d => d.MaDiemDen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour__Ma_Diem_De__76969D2E")

                .HasConstraintName("FK__Tour__Ma_Diem_De__49C3F6B7");

            entity.HasOne(d => d.MaGiaTourNavigation).WithMany(p => p.Tours)
                .HasForeignKey(d => d.MaGiaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour__Ma_Gia_Tou__75A278F5")
                .HasConstraintName("FK__Tour__Ma_Gia_Tou__48CFD27E");

            entity.HasOne(d => d.MaLoaiTourNavigation).WithMany(p => p.Tours)
                .HasForeignKey(d => d.MaLoaiTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour__Ma_Loai_To__74AE54BC")

                .HasConstraintName("FK__Tour__Ma_Loai_To__47DBAE45");
        });

        modelBuilder.Entity<TrangThai>(entity =>
        {
            entity.HasKey(e => e.MaTrangThai).HasName("PK__TrangTha__B9CD9D6BA20F14FE");

            entity.HasKey(e => e.MaTrangThai).HasName("PK__TrangTha__B9CD9D6BDB4AF9BE");

            entity.ToTable("TrangThai");

            entity.Property(e => e.MaTrangThai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Trang_Thai");
            entity.Property(e => e.TenTrangThai)
                .HasMaxLength(255)
                .HasColumnName("Ten_Trang_Thai");
        });

        OnModelCreatingPartial(modelBuilder);
       });
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}