namespace WebApplication1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<CHUDE> CHUDEs { get; set; }
        public virtual DbSet<DONHANG> DONHANGs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<NHAXUATBAN> NHAXUATBANs { get; set; }
        public virtual DbSet<SACH> SACHes { get; set; }
        public virtual DbSet<TACGIA> TACGIAs { get; set; }
        public virtual DbSet<THAMGIA> THAMGIAs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.MaDonHang)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.MaSach)
                .IsUnicode(false);

            modelBuilder.Entity<CHUDE>()
                .Property(e => e.MaChuDe)
                .IsUnicode(false);

            modelBuilder.Entity<CHUDE>()
                .HasMany(e => e.SACHes)
                .WithRequired(e => e.CHUDE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DONHANG>()
                .Property(e => e.MaDonHang)
                .IsUnicode(false);

            modelBuilder.Entity<DONHANG>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<DONHANG>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.DONHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.DienThoai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .HasMany(e => e.DONHANGs)
                .WithRequired(e => e.KHACHHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NHAXUATBAN>()
                .Property(e => e.MaNXB)
                .IsUnicode(false);

            modelBuilder.Entity<NHAXUATBAN>()
                .Property(e => e.DienThoai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NHAXUATBAN>()
                .HasMany(e => e.SACHes)
                .WithRequired(e => e.NHAXUATBAN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SACH>()
                .Property(e => e.MaSach)
                .IsUnicode(false);

            modelBuilder.Entity<SACH>()
                .Property(e => e.AnhBia)
                .IsUnicode(false);

            modelBuilder.Entity<SACH>()
                .Property(e => e.MaChuDe)
                .IsUnicode(false);

            modelBuilder.Entity<SACH>()
                .Property(e => e.MaNXB)
                .IsUnicode(false);

            modelBuilder.Entity<SACH>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.SACH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SACH>()
                .HasMany(e => e.THAMGIAs)
                .WithRequired(e => e.SACH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TACGIA>()
                .Property(e => e.MaTacGia)
                .IsUnicode(false);

            modelBuilder.Entity<TACGIA>()
                .Property(e => e.DienThoai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TACGIA>()
                .HasMany(e => e.THAMGIAs)
                .WithRequired(e => e.TACGIA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<THAMGIA>()
                .Property(e => e.MaSach)
                .IsUnicode(false);

            modelBuilder.Entity<THAMGIA>()
                .Property(e => e.MaTacGia)
                .IsUnicode(false);
        }
    }
}
