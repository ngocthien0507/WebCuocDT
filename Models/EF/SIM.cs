namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SIM")]
    public partial class SIM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SIM()
        {
            ChiTietHoaDonCuocs = new HashSet<ChiTietHoaDonCuoc>();
            HoaDonCuocs = new HashSet<HoaDonCuoc>();
            KhachHangs = new HashSet<KhachHang>();
        }

        [Key]
        [StringLength(11)]
        public string idSim { get; set; }

        [StringLength(50)]
        public string ChuSoHuu { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKichHoat { get; set; }

        public int TinhTrang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDonCuoc> ChiTietHoaDonCuocs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonCuoc> HoaDonCuocs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachHang> KhachHangs { get; set; }
    }
}
