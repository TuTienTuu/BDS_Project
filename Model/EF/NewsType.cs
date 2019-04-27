namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NewsType")]
    public partial class NewsType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NewsType()
        {
            News = new HashSet<News>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NewsTypeID { get; set; }

        [StringLength(255)]
        [DisplayName("Loại tin tức(VN)")]
        public string NewsTypeName_VN { get; set; }

        [StringLength(255)]
        [DisplayName("Loại tin tức(EN)")]
        public string NewsTypeName_EN { get; set; }

        [DisplayName("Trạng thái")]
        public bool? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News> News { get; set; }
    }
}
