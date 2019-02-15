namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MenuLv2
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuLv2()
        {
            MenuLv3 = new HashSet<MenuLv3>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MenuLv2ID { get; set; }

        [StringLength(255)]
        public string MenuName_VN { get; set; }

        [StringLength(255)]
        public string MenuName_EN { get; set; }

        public bool? Status { get; set; }

        public int? MenuLv1ID { get; set; }

        public virtual MenuLv1 MenuLv1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuLv3> MenuLv3 { get; set; }
    }
}
