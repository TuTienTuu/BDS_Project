using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;
using PagedList.Mvc;

namespace Model.Dao
{
    public class UserDao
    {
        DBModel db = null;
        public UserDao()
        {
            db = new DBModel();
        }
        //Account
        public IEnumerable<Account> ListAccount_Paging(int page = 1, int pageSize = 1)
        {

            return db.Accounts.OrderByDescending(x => x.CreatedTime).ToPagedList(page, pageSize);
        }
        public string Insert(Account entity)
        {
            db.Accounts.Add(entity);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return entity.UserName;
        }

        public Account GetAccountByUserName(string userName)
        {
            return db.Accounts.FirstOrDefault(x => x.UserName == userName);
        }

        public bool DeleteAccount(string userName)
        {
            try
            {
                var model = db.Accounts.Find(userName);
                db.Accounts.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public int Login(string userName, string password)
        {
            var result = db.Accounts.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
                return 0; //Tài khoản không tồn tại
            else
            {
                if (result.AccountStatus == false)
                    return -1; //Tài khoản đang bị khóa
                else if (result.Password != password)
                    return -2; //Mật khẩu không đúng
                else
                    return 1; //Đăng nhập thành công
            }
        }
    }
}
