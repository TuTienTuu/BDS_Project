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
        public ActionResult Create(AccountModel account, Salt salt, string rePassword)
        {
            if (ModelState.IsValid)
            {
                //Tạo đối tượng để create 1 Account mới
                var dao = new UserDao();

                //Kiểm tra dữ liệu nhập vào
                if (CheckValue(account,rePassword) == false)
                    return View("Create");

                //Xử lý dữ liệu
                //Mã hóa Password
                var encryptPassword = Encryptor.SHA256(account.Password + salt);

                //Format DateOfBirth
                var dateOfBirth = Convert.ToDateTime(account.DateOfBirth);

                //Kiểm tra hợp lệ Email
                string validEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                if (IsValidData(account.Mobi, validEmail) == false)
                    IsValidData(account.Mobi, validEmail);

                //Kiểm tra hợp lệ Avatar\\\\\\\\\
                string validAvatar = @"(.*?)\.(jpg|jpeg|png|gif)$";

                //Use Namespace called :  System.IO  
                string FileName = Path.GetFileNameWithoutExtension(account.ImageFile.FileName);

                //To Get File Extension  
                string FileExtension = Path.GetExtension(account.ImageFile.FileName);

                //Add Current Date To Attached File Name  
                FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

                //Get Upload path from Web.Config file AppSettings.  
                string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();

                //Its Create complete path to store in server.  
                string upload = UploadPath + FileName;

                //To copy and save file into server.  
                 account.ImageFile.SaveAs(upload);

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


                //Chuyển sang Account để lưu vào DB
                Account acc = new Account();
                acc.UserName = account.UserName;
                acc.Password = account.Password;
                acc.FullName = account.FullName;
                acc.Gender = account.Gender;
                acc.Address = account.Address;
                acc.DateOfBirth = account.DateOfBirth;
                acc.Email = account.Email;
                acc.Mobi = account.Mobi;
                acc.Avatar = account.Avatar;
                acc.AccountStatus = account.AccountStatus;
                acc.CreatedTime = account.CreatedTime;

                //Lưu dữ liệu
                try
                {

                    dao.Insert(acc);
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

        public bool CheckValue(AccountModel account,string rePassword)
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
                return true;
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
                account.FullName = string.Empty;
                return true;
            }

            //Kiểm tra Address
            if (string.IsNullOrEmpty(account.Address))
            {
                account.FullName = string.Empty;
                return true;
            }

            //Kiểm tra Gender
            if (account.Gender == false && account.Gender == false)
            {
                ModelState.AddModelError("", "Vui lòng chọn giới tính");
                return false;
            }

            return true;
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

    }
}