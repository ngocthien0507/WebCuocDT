namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string Text { get; set; }

        [StringLength(50)]
        public string Link { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(50)]
        public string Target { get; set; }

        [Column(TypeName = "ntext")]
        public string Status { get; set; }

        public int? TypeID { get; set; }

        public virtual MenuType MenuType { get; set; }
    }
}
