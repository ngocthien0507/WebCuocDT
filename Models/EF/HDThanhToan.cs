namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HDThanhToan")]
    public partial class HDThanhToan
    {
        [Key]
        public int idHDTT { get; set; }

        [Required]
        [StringLength(50)]
        public string TenKH { get; set; }

        [StringLength(11)]
        public string SDT { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongTien { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }

        public DateTime? NgayThanhToan { get; set; }

        public int? idHD { get; set; }

        public virtual HoaDonCuoc HoaDonCuoc { get; set; }
    }
}
