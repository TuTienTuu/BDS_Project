using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class MenuLevel1Controller : Controller
    {
        // GET: Admin/MenuLevel1
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MenuLv1 menulv1)
        {
            if (ModelState.IsValid)
            {
                int id;
                UserDao dao = new UserDao();
                try
                {
                    id = dao.InsertMenuLv1(menulv1);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MenuLv1", "Create"));
                }
                if (id > 0)
                {
                    ModelState.AddModelError("", "Thêm tài khoản thành công");
                    return RedirectToAction("Index", "MenuLevel1");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                    return View("Create");
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin");
                return View("Create");
            }
        }
    }
}