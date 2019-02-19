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

namespace BDS.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        public DBModel db = new DBModel();
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
        public ActionResult Create(Account account, Salt salt, string rePassword)
        {
            if (ModelState.IsValid)
            {
                //Tạo đối tượng để create 1 Account mới
                var dao = new UserDao();

                //Kiểm tra dữ liệu nhập vào
                if (CheckValue(account,rePassword) == false)
                    return View("Create");

                //Xử lý dữ liệu
                acc(account, salt);

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

        public bool CheckValue(Account account,string rePassword)
        {
            //Kiểm tra tài khoản tồn tại chưa?
            if (string.IsNullOrEmpty(account.UserName) || string.IsNullOrWhiteSpace(account.UserName))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin");
                return false;
            }
            else
            {
                if (db.Accounts.FirstOrDefault(x => x.UserName == account.UserName) != null)
                {
                    ModelState.AddModelError("", "Tài khoản đã tồn tại, vui lòng nhập tài khoản khác");
                    return false;
                }
            }

            //Kiểm tra Password và RePassword
            if (string.IsNullOrEmpty(account.Password) && string.IsNullOrEmpty(Request["txtrePassword"]))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin");
                return false;
            }
            else if (account.Password != rePassword)
            {
                ModelState.AddModelError("", "Mật khẩu không giống nhau, vui lòng nhập lại");
                return false;
            }

            //Kiểm tra FullName
            if (account.FullName == null)
            {
                account.FullName = string.Empty;
                return false;
            }

            //Kiểm tra DateOfBirth
            if (string.IsNullOrEmpty(account.DateOfBirth.ToString()))
            {
                ModelState.AddModelError("", "Vui lòng chọn ngày sinh");
                return false;
            }

            //Kiểm tra Email
            if (string.IsNullOrEmpty(account.Email))
            {
                ModelState.AddModelError("", "Vui lòng nhập Email");
                return false;
            }

            //Kiểm tra Mobi
            if (string.IsNullOrEmpty(account.Mobi))
            {
                ModelState.AddModelError("", "Vui lòng nhập Số điện thoại");
                return false;
            }

            //Kiểm tra Address
            if (string.IsNullOrEmpty(account.Address))
            {
                ModelState.AddModelError("", "Vui lòng nhập Địa chỉ");
                return false;
            }

            //Kiểm tra Gender
            if (account.Gender == false && account.Gender == false)
            {
                ModelState.AddModelError("", "Vui lòng chọn giới tính");
                return false;
            }

            return true;
        }

        public Account acc(Account account, Salt salt)
        {
            //Mã hóa Password
            var encryptPassword = Encryptor.SHA256(account.Password + salt);

            //Format DateOfBirth
            var dateOfBirth = Convert.ToDateTime(account.DateOfBirth);

            //Kiểm tra hợp lệ Email
            string validEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (IsValidData(account.Mobi, validEmail) == false)
                IsValidData(account.Mobi, validEmail);

            //Kiểm tra hợp lệ Avatar
            string validAvatar = @"(.*?)\.(jpg|jpeg|png|gif)$";
            //Use Namespace called :  System.IO  
            string FileName = Path.GetFileNameWithoutExtension(account.Avatar.FileName);

            //To Get File Extension  
            string FileExtension = Path.GetExtension(membervalues.ImageFile.FileName);

            //Add Current Date To Attached File Name  
            FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

            //Get Upload path from Web.Config file AppSettings.  
            string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();

            //Its Create complete path to store in server.  
            membervalues.ImagePath = UploadPath + FileName;

            //To copy and save file into server.  
            membervalues.ImageFile.SaveAs(membervalues.ImagePath);

            if (IsValidData(account.Mobi, validAvatar) == false)
                IsValidData(account.Mobi, validAvatar);

            //Kiểm tra hợp lệ Mobi
            string validMobi = @"(.*?)\.(jpg|jpeg|png|gif)$";
            if (IsValidData(account.Mobi, validMobi) == false)
                IsValidData(account.Mobi, validMobi);

            //Gán các giá trị đã xử lý
            account.Password = encryptPassword;
            account.DateOfBirth = dateOfBirth;
            account.CreatedTime = DateTime.Now;
            account.AccountStatus = true;

            return account;
        }

        public bool IsValidData(string inputValue, string validRegex)
        {
            Regex reg = new Regex(validRegex);
            Match match = reg.Match(inputValue);
            if (!match.Success)
            {
                ModelState.AddModelError("", "Dữ liệu không đúng định dạng, vui lòng nhập lại.");
                return false;
            }
            return true;
        }

        public int UpLoadAvatar(HttpPostedFileBase file)
        {
            string validAvatar = @"(.*?)\.(jpg|jpeg|png|gif)$";
            string path;
            try
            {
                if (file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    if (IsValidData(fileName, validAvatar))
                    {
                        path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        file.SaveAs(path);
                    }
                    IsValidData(fileName, validAvatar);
                    return 1;
                }
                return 0;
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Vui lòng chọn hình ảnh để lưu");
                return 0;
            }
        }
    }
}