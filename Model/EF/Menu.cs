namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int ID { get; set; }

        [StringLength(255)]
        [DisplayName("Tên hiển thị(VN)")]
        public string MenuName_VN { get; set; }

        [DisplayName("Trạng thái")]
        public bool? Status { get; set; }

        [StringLength(255)]
        [DisplayName("Tên hiển thị(EN)")]
        public string MenuName_EN { get; set; }

        [DisplayName("Cấp Menu")]
        public int? LevelMenu { get; set; }

        [DisplayName("Menu cha")]
        public int? ParentID { get; set; }
    }
}
