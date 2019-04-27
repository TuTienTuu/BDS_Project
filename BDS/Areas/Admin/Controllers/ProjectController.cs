using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class ProjectController : BaseController
    {
        // GET: Admin/Project
        public ActionResult Index()
        {
            return View();
        }
    }
}