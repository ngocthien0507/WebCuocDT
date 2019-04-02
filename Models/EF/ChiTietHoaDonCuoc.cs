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

        [Key]
        [Column(Order = 2)]
        public DateTime TGBD { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime TGKT { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "money")]
        public decimal ThanhTien { get; set; }
    }
}
