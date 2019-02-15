namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MenuLv3
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MenuLv3ID { get; set; }

        [StringLength(255)]
        public string MenuName_VN { get; set; }

        [StringLength(255)]
        public string MenuName_EN { get; set; }

        public bool? Status { get; set; }

        public int? MenuLv2ID { get; set; }

        public virtual MenuLv2 MenuLv2 { get; set; }
    }
}
