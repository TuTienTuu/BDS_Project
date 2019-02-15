using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
   public class UserDao
    {
        DBModel db = null;
        public UserDao()
        {
            db = new DBModel();
        }
        public string Insert(Account entity )
        {
            db.Accounts.Add(entity);
            db.SaveChanges();
            return entity.UserName;
        }

        public Account GetById(string userName)
        {
            return db.Accounts.SingleOrDefault(x=>x.UserName ==  userName);
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
