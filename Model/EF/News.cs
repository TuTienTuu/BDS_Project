namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        public int NewsID { get; set; }

        [StringLength(255)]
        public string Title_VN { get; set; }

        public DateTime? CreatedTime { get; set; }

        [StringLength(255)]
        public string Description_VN { get; set; }

        [Column(TypeName = "ntext")]
        public string Content_VN { get; set; }

        [StringLength(255)]
        public string Avatar { get; set; }

        [StringLength(255)]
        public string CreatedBy { get; set; }

        public bool? NewsStatus { get; set; }

        [StringLength(255)]
        public string Tags_VN { get; set; }

        [StringLength(255)]
        public string Title_EN { get; set; }

        [StringLength(255)]
        public string Description_EN { get; set; }

        [StringLength(255)]
        public string Content_EN { get; set; }

        [StringLength(255)]
        public string Tags_EN { get; set; }

        [StringLength(255)]
        public string MetaTitle_VN { get; set; }

        [StringLength(255)]
        public string MetaTitle_EN { get; set; }

        public int? ViewTimes { get; set; }

        public int? NewsType { get; set; }

        public virtual Account Account { get; set; }

        public virtual NewsType NewsType1 { get; set; }
    }
}
