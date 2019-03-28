namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonDK")]
    public partial class HoaDonDK
    {
        [Key]
        public int idHD { get; set; }

        [StringLength(50)]
        public string TenKH { get; set; }

        [StringLength(9)]
        public string CMND { get; set; }

        [StringLength(50)]
        public string DiaChi { get; set; }

        [Column(TypeName = "money")]
        public decimal? PhiHoaMang { get; set; }
    }
}
