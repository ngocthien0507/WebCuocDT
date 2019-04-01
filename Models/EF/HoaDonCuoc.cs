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
        [Key]
        public int idHD { get; set; }

        [StringLength(11)]
        public string SoDT { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongTien { get; set; }

        public int? TinhTrang { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }

        public virtual SIM SIM { get; set; }
    }
}
