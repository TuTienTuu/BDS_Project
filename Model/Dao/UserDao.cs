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

        //MenuLV1
        public IEnumerable<MenuLv1> ListMenuLv1_Paging (int page =1, int pageSize =1)
        {

            return db.MenuLv1.OrderBy(x=>x.MenuLv1ID).ToPagedList(page,pageSize);
        }

        public int InsertMenuLv1(MenuLv1 entity)
        {
            db.MenuLv1.Add(entity);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return entity.MenuLv1ID;
        }

        public bool UpdateMenuLv1(MenuLv1 entity)
        {
            try
            {
                var model = db.MenuLv1.Find(entity.MenuLv1ID);
                model.MenuName_EN = entity.MenuName_EN;
                model.MenuName_VN = entity.MenuName_VN;
                model.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public MenuLv1 GetMenuLv1ById(int id)
        {
            return db.MenuLv1.FirstOrDefault(x => x.MenuLv1ID == id);
        }

        public MenuLv1 GetMenuLv1DetailById(int id)
        {
           return db.MenuLv1.Find(id);

        }

        public bool DeleteMenuLv1(int id)
        {
            try
            {
                var model = db.MenuLv1.Find(id);
                db.MenuLv1.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false ;
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
