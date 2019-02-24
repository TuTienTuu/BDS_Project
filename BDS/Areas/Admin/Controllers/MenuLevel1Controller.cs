using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace BDS.Areas.Admin.Controllers
{
    public class MenuLevel1Controller : Controller
    {
        // GET: Admin/MenuLevel1
        public ActionResult Index(int page = 1, int pageSize = 1)
        {
            UserDao dao = new UserDao();
            var Menulv1 = dao.ListMenuLv1_Paging(page, pageSize);
            return View(Menulv1);
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
                    return RedirectToAction("Create", "MenuLevel1");
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

        public ActionResult Update(int id)
        {
            var model = new UserDao().GetMenuLv1DetailById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(MenuLv1 menulv1, int id)
        {
            bool result;
            if (ModelState.IsValid)
            {
                UserDao dao = new UserDao();
                menulv1.MenuLv1ID = id;
                try
                {
                    result = dao.UpdateMenuLv1(menulv1);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "MenuLv1", "Update"));
                }
                if (result)
                {
                    ModelState.AddModelError("", "Cập nhật thông tin thành công");
                    return RedirectToAction("Index", "MenuLevel1");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thông tin không thành công");
                    return View("Update");
                }
            }
            return View("Update");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().DeleteMenuLv1(id);
            return RedirectToAction("Index");
        }
    }
}