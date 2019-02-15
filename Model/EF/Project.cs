namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Project")]
    public partial class Project
    {
        public int ProjectId { get; set; }

        [StringLength(255)]
        public string ProjectName_VN { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        public decimal? Price { get; set; }

        public DateTime? CreatedTime { get; set; }

        [StringLength(255)]
        public string Description_VN { get; set; }

        [Column(TypeName = "ntext")]
        public string Content_VN { get; set; }

        [StringLength(255)]
        public string Avatar { get; set; }

        [StringLength(255)]
        public string CreatedBy { get; set; }

        public bool? ProjectStatus { get; set; }

        [StringLength(255)]
        public string ProjectName_EN { get; set; }

        [StringLength(255)]
        public string Description_EN { get; set; }

        [StringLength(255)]
        public string Content_EN { get; set; }

        public int? ViewTimes { get; set; }

        public virtual Account Account { get; set; }
    }
}
