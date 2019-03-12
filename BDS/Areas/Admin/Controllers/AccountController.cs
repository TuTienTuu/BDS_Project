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
using System.IO;
using System.Configuration;

namespace BDS.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        public DBModel db = new DBModel();
        // GET: Admin/ccount
        public ActionResult Index(int page =1, int pageSize = 1)
        {
            UserDao dao = new UserDao();
            var account = dao.ListAccount_Paging(page, pageSize);
            return View(account);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Update(string userName)
        {
            var model = new UserDao().GetAccountByUserName(userName);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Account account, Salt salt, string rePassword)
        {
            if (ModelState.IsValid)
            {
                //Tạo đối tượng để create 1 Account mới
                var dao = new UserDao();

                //Xử lý dữ liệu
                //Mã hóa Password
                var encryptPassword = Encryptor.SHA256(account.Password + salt);

                //Format DateOfBirth
                var dateOfBirth = Convert.ToDateTime(account.DateOfBirth);


                //Gán các giá trị đã xử lý
                account.Password = encryptPassword;
                account.DateOfBirth = dateOfBirth;
                account.CreatedTime = DateTime.Now;

                //Lưu dữ liệu
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
                    ModelState.AddModelError("", "Thêm mới không thành công");
                    return View("Create");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm tài khoản thành công");
                    //return RedirectToAction("Index", "Account");
                }
                    
            }
            else
            {
                var query = from state in ModelState.Values
                            from error in state.Errors
                            select error.ErrorMessage;

                var errorList = query.ToList();
                ModelState.AddModelError("", errorList.ToString());
                return View("Create");
            }
            return View("Index");
        }

    }
}