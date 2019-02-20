using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.Areas.Admin.Models
{
    public class AccountModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Email { get; set; }
        public string Mobi { get; set; }
        public string Address { get; set; }
        public bool AccountStatus { get; set; }
        public string Avatar { get; set; }
        public HttpPostedFile ImageFile { get; set; }
    }
}