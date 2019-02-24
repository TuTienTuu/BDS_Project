using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class MenuLevel2Controller : Controller
    {
        // GET: Admin/MenuLevel2
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(MenuLv2 menuLv2 )
        {
            return View();
        }
    }
}