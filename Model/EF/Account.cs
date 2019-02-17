namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            News = new HashSet<News>();
            Projects = new HashSet<Project>();
        }

        [Key]
        [StringLength(255)]
        [Display(Name ="Tên tài khoản")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(255)]
        public string Password { get; set; }

        [Display(Name = "Họ và tên")]
        [StringLength(255)]
        public string FullName { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Giới tính")]
        public bool Gender { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedTime { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [StringLength(20)]
        public string Mobi { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(500)]
        public string Address { get; set; }

        [Display(Name = "Trạng thái")]
        public bool AccountStatus { get; set; }

        [Display(Name = "Ảnh đại diện")]
        [StringLength(255)]
        public string Avatar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News> News { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Projects { get; set; }
    }
}
