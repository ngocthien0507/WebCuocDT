namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDonCuoc")]
    public partial class ChiTietHoaDonCuoc
    {
        [Key]
        public int idCTHD { get; set; }

        [Required]
        [StringLength(11)]
        public string SoDT { get; set; }

        public DateTime TGBD { get; set; }

        public DateTime TGKT { get; set; }

        [Column(TypeName = "money")]
        public decimal? ThanhTien { get; set; }

        public int? idHD { get; set; }

        public virtual HoaDonCuoc HoaDonCuoc { get; set; }
    }
}
