namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MenuLv1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuLv1()
        {
            MenuLv2 = new HashSet<MenuLv2>();
        }

        [Display(Name ="Mã Menu")]
        public int MenuLv1ID { get; set; }

        [StringLength(255)]
        [Display(Name = "Tên Menu Tiếng Việt")]
        public string MenuName_VN { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [StringLength(255)]
        [Display(Name = "Tên Menu Tiếng Anh")]
        public string MenuName_EN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuLv2> MenuLv2 { get; set; }
    }
}
