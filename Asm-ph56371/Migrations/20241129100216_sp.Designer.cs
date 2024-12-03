﻿// <auto-generated />
using System;
using Asm_ph56371.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Asm_ph56371.Migrations
{
    [DbContext(typeof(AsmDbContext))]
    [Migration("20241129100216_sp")]
    partial class sp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Asm_ph56371.Models.ChiTietGioHang", b =>
                {
                    b.Property<Guid>("CTGHId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GioHangId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SanPhamId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("CTGHId");

                    b.HasIndex("GioHangId");

                    b.HasIndex("SanPhamId");

                    b.ToTable("ChiTietGioHangs");
                });

            modelBuilder.Entity("Asm_ph56371.Models.ChiTietHoaDon", b =>
                {
                    b.Property<Guid>("CTHDId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Gia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("HoaDonId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SanPhamId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("CTHDId");

                    b.HasIndex("HoaDonId");

                    b.HasIndex("SanPhamId");

                    b.ToTable("ChiTietHoaDons");
                });

            modelBuilder.Entity("Asm_ph56371.Models.GioHang", b =>
                {
                    b.Property<Guid>("gioHangId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("NguoiDungId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("gioHangId");

                    b.HasIndex("NguoiDungId")
                        .IsUnique();

                    b.ToTable("GioHangs");
                });

            modelBuilder.Entity("Asm_ph56371.Models.HoaDon", b =>
                {
                    b.Property<Guid>("hoaDonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("NguoiDungId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TongTien")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("hoaDonId");

                    b.HasIndex("NguoiDungId");

                    b.ToTable("HoaDons");
                });

            modelBuilder.Entity("Asm_ph56371.Models.NguoiDung", b =>
                {
                    b.Property<Guid>("nguoiDungId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VaiTro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("nguoiDungId");

                    b.ToTable("NguoiDung");
                });

            modelBuilder.Entity("Asm_ph56371.Models.SanPham", b =>
                {
                    b.Property<Guid>("SPId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Gia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("IMGURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoLuongTonKho")
                        .HasColumnType("int");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SPId");

                    b.ToTable("SanPhams");
                });

            modelBuilder.Entity("Asm_ph56371.Models.ChiTietGioHang", b =>
                {
                    b.HasOne("Asm_ph56371.Models.GioHang", "GioHang")
                        .WithMany("ChiTietGioHangs")
                        .HasForeignKey("GioHangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Asm_ph56371.Models.SanPham", "SanPham")
                        .WithMany("ChiTietGioHangs")
                        .HasForeignKey("SanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GioHang");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("Asm_ph56371.Models.ChiTietHoaDon", b =>
                {
                    b.HasOne("Asm_ph56371.Models.HoaDon", "HoaDon")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("HoaDonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Asm_ph56371.Models.SanPham", "SanPham")
                        .WithMany("chiTietHoaDons")
                        .HasForeignKey("SanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HoaDon");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("Asm_ph56371.Models.GioHang", b =>
                {
                    b.HasOne("Asm_ph56371.Models.NguoiDung", "NguoiDung")
                        .WithOne("GioHang")
                        .HasForeignKey("Asm_ph56371.Models.GioHang", "NguoiDungId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("Asm_ph56371.Models.HoaDon", b =>
                {
                    b.HasOne("Asm_ph56371.Models.NguoiDung", "NguoiDung")
                        .WithMany("HoaDon")
                        .HasForeignKey("NguoiDungId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("Asm_ph56371.Models.GioHang", b =>
                {
                    b.Navigation("ChiTietGioHangs");
                });

            modelBuilder.Entity("Asm_ph56371.Models.HoaDon", b =>
                {
                    b.Navigation("ChiTietHoaDons");
                });

            modelBuilder.Entity("Asm_ph56371.Models.NguoiDung", b =>
                {
                    b.Navigation("GioHang");

                    b.Navigation("HoaDon");
                });

            modelBuilder.Entity("Asm_ph56371.Models.SanPham", b =>
                {
                    b.Navigation("ChiTietGioHangs");

                    b.Navigation("chiTietHoaDons");
                });
#pragma warning restore 612, 618
        }
    }
}
