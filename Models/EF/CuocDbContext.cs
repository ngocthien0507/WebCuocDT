namespace Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CuocDbContext : DbContext
    {
        public CuocDbContext()
            : base("name=CuocDbContext")
        {
        }

        public virtual DbSet<HoaDonCuoc> HoaDonCuocs { get; set; }
        public virtual DbSet<HoaDonDK> HoaDonDKs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuType> MenuTypes { get; set; }
        public virtual DbSet<SIM> SIMs { get; set; }
        public virtual DbSet<SystemConfig> SystemConfigs { get; set; }
        public virtual DbSet<ChiTietHoaDonCuoc> ChiTietHoaDonCuocs { get; set; }
        public virtual DbSet<HDThanhToan> HDThanhToans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HoaDonCuoc>()
                .Property(e => e.SoDT)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonCuoc>()
                .Property(e => e.TongTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HoaDonDK>()
                .Property(e => e.CMND)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonDK>()
                .Property(e => e.PhiHoaMang)
                .HasPrecision(19, 4);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.CMND)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SoDT)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Target)
                .IsUnicode(false);

            modelBuilder.Entity<MenuType>()
                .HasMany(e => e.Menus)
                .WithOptional(e => e.MenuType)
                .HasForeignKey(e => e.TypeID);

            modelBuilder.Entity<SIM>()
                .Property(e => e.idSim)
                .IsUnicode(false);

            modelBuilder.Entity<SIM>()
                .HasMany(e => e.HoaDonCuocs)
                .WithOptional(e => e.SIM)
                .HasForeignKey(e => e.SoDT);

            modelBuilder.Entity<SIM>()
                .HasMany(e => e.KhachHangs)
                .WithOptional(e => e.SIM)
                .HasForeignKey(e => e.SoDT);

            modelBuilder.Entity<SystemConfig>()
                .Property(e => e.idSys)
                .IsFixedLength();

            modelBuilder.Entity<ChiTietHoaDonCuoc>()
                .Property(e => e.SoDT)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDonCuoc>()
                .Property(e => e.ThanhTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HDThanhToan>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<HDThanhToan>()
                .Property(e => e.TongTien)
                .HasPrecision(19, 4);
        }
    }
}
