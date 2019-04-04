namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonCuoc")]
    public partial class HoaDonCuoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDonCuoc()
        {
            ChiTietHoaDonCuocs = new HashSet<ChiTietHoaDonCuoc>();
            HDThanhToans = new HashSet<HDThanhToan>();
        }

        [Key]
        public int idHD { get; set; }

        [StringLength(11)]
        public string SoDT { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongTien { get; set; }

        public int? TinhTrang { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDonCuoc> ChiTietHoaDonCuocs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HDThanhToan> HDThanhToans { get; set; }

        public virtual SIM SIM { get; set; }
    }
}
