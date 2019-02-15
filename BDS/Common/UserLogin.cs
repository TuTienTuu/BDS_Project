using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDS.Common
{
    [Serializable]
    public class UserLogin
    {
        public string UserName { get; set; }
        public string  Password { get; set; }
    }
}