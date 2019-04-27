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
            //Kiểm tra đăng nhập
            if (ModelState.IsValid)
            {
                //Đăng nhập và mã hóa mật khẩu
                var dao = new UserDao();
                var result = dao.Login(model.userName, Encryptor.SHA256(model.password + salt).ToString());
                if (result == 1)
                {
                    //Thêm thông tin vào Session
                    var user = dao.GetAccountByUserName(model.userName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.Password = user.Password;
                    Session.Add(Common.CommonConstants.USER_SESSION, userSession);

                    //Lấy thông tin của backlink
                    string backLink = Session[CommonConstants.BACK_LINK].ToString();

                    //Xóa dữ liệu back link
                    Session.Remove(CommonConstants.BACK_LINK);

                    //Trả về trang trước khi đăng nhập
                    return Redirect(backLink);
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