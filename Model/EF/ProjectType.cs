namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProjectType")]
    public partial class ProjectType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID{ get; set; }

        [StringLength(255)]
        public string Name_VN { get; set; }

        [StringLength(255)]
        public string Name_EN { get; set; }

        public DateTime? CreatedTime { get; set; }

        [StringLength(255)]
        public string Avatar { get; set; }

        [StringLength(255)]
        public string CreatedBy { get; set; }

        public bool? Status { get; set; }

    }
}
