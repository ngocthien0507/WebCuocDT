namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [Key]
        public int idKhachHang { get; set; }

        [StringLength(50)]
        public string TenKH { get; set; }

        [StringLength(9)]
        public string CMND { get; set; }

        [StringLength(50)]
        public string NgheNghiep { get; set; }

        [StringLength(50)]
        public string DiaChi { get; set; }

        [StringLength(11)]
        public string SoDT { get; set; }

        public virtual SIM SIM { get; set; }
    }
}
