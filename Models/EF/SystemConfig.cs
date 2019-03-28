namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemConfig")]
    public partial class SystemConfig
    {
        [Key]
        [StringLength(20)]
        public string idSys { get; set; }

        public long? GiaTri { get; set; }

        public int? TinhTrang { get; set; }
    }
}
