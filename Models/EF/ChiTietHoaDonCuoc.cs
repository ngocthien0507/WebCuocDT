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
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idHD { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(11)]
        public string SoDT { get; set; }

        public DateTime TGBD { get; set; }

        public DateTime TGKT { get; set; }

        [Column(TypeName = "money")]
        public decimal? ThanhTien { get; set; }
    }
}
