using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BDS.Areas.Admin.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Vui lòng nhập UserName!")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Password!")]
        public string password { get; set; }

        public bool remember { get; set; }

        //public string salt = "BDS";
    }
}