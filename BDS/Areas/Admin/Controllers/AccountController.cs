using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using BDS.Common;
using BDS.Areas.Admin.Models;
using System.Text.RegularExpressions;

namespace BDS.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {

        // GET: Admin/ccount
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
        public ActionResult Create(Account account, Salt salt)
        {
            if (ModelState.IsValid)
            {


                //Tạo đối tượng để create 1 Account mới
                var dao = new UserDao();

                //Mã hóa Password
                var encryptPassword = Encryptor.SHA256(account.Password + salt);

                //Format DateOfBirth
                var dateOfBirth = Convert.ToDateTime(account.DateOfBirth);

                //Kiểm tra hợp lệ Email
                var email = account.Email;
                Regex regEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regEmail.Match(email);
                if (!match.Success)
                    ModelState.AddModelError("", "Email không đúng định dạng");

                //Kiểm tra hợp lệ Avatar
                var avatar = account.Avatar;
                Regex regAvatar = new Regex(@"(.*?)\.(jpg|jpeg|png|gif)$");
                match = regAvatar.Match(avatar);

                //Gán các giá trị đã xử lý
                account.Password = encryptPassword;
                account.DateOfBirth = dateOfBirth;
                account.CreatedTime = DateTime.Now;
                account.Email = email;
                account.Avatar = avatar;
                account.AccountStatus = true;
                try
                {
                    dao.Insert(account);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Account", "Create"));
                }
                string userName = account.UserName;
                if (userName == null)
                {
                    ModelState.AddModelError("", "Thêm mới thành công");
                    return RedirectToAction("Index", "Account");
                }
                else
                    ModelState.AddModelError("", "Thêm tài khoản thành công");
            }
            var errors = ModelState
           .Where(x => x.Value.Errors.Count > 0)
           .Select(x => new { x.Key, x.Value.Errors })
           .ToArray();
            ModelState.AddModelError("", errors.ToString());
            return View("Index");
        }
    }
}