using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;

namespace BDS.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/ccount
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                string userName = dao.Insert(account);
                if (userName != null)
                    return RedirectToAction("Index", "Account");
                else
                    ModelState.AddModelError("","Thêm tài khoản không thành công");
            }
            return View("Index");
        }
    }
}