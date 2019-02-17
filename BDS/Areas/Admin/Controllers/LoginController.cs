using BDS.Areas.Admin.Models;
using BDS.Common;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model, Salt salt)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.userName, Encryptor.SHA256(model.password + salt).ToString());
                if (result == 1)
                {
                    var user = dao.GetById(model.userName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.Password = user.Password;
                    Session.Add(Common.CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                    ModelState.AddModelError("", "Tài khoản không tồn tại, vui lòng đăng ký hoặc liên hệ Admin!");
                else if (result == -1)
                    ModelState.AddModelError("", "Tài khoản đang bị khóa, vui lòng liên hệ Admin!");
                else if (result == -2)
                    ModelState.AddModelError("", "Mật khẩu không đúng, vui lòng đăng nhập lại!");
            }

            return View("Index");
        }
    }
}