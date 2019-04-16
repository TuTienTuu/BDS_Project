namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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
        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }

        [StringLength(255)]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        [StringLength(255)]
        [DisplayName("Họ tên")]
        public string FullName { get; set; }

        [DisplayName("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Giới tính")]
        public bool? Gender { get; set; }

        [DisplayName("Thời gian tạo")]
        public DateTime? CreatedTime { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(20)]
        [DisplayName("Số điện thoại")]
        public string Mobi { get; set; }

        [StringLength(500)]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Trạng thái")]
        public bool? AccountStatus { get; set; }

        [StringLength(255)]
        [DisplayName("Ảnh đại diện")]
        public string Avatar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News> News { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Projects { get; set; }
    }
}
