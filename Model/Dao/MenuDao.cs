using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MenuDao
    {
        DBModel db = null;

        public MenuDao()
        {
            db = new DBModel();
        }

        public int Insert(Menu entity)
        {
            try
            {
                db.Menus.Add(entity);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return entity.ID;
        }

        public IEnumerable<Menu> LoadParentID(int id)
        {
            return db.Menus.Where(x => x.ID == id - 1).ToList();
        }

        public IEnumerable<Menu> ListAllPaging(int page,int pageSize)
        {
            return db.Menus.OrderBy(x => x.DisplayOrder).ToPagedList(page, pageSize);
        }

        public Menu GetByID(int id)
        {
            return db.Menus.FirstOrDefault(x => x.ID == id);
        }

        public bool Update(Menu entity)
        {
            try
            {
                var model = db.Menus.FirstOrDefault(x => x.ID == entity.ID);
                model.LevelMenu = entity.LevelMenu;
                model.MenuName_EN = entity.MenuName_EN;
                model.MenuName_VN = entity.MenuName_VN;
                model.ParentID = entity.ParentID;
                model.DisplayOrder = entity.DisplayOrder;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var model = db.Menus.FirstOrDefault(x => x.ID == id);
                db.Menus.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChangeStatus(int id)
        {
            var model = db.Menus.FirstOrDefault(x => x.ID == id);
            model.Status = !model.Status;
            db.SaveChanges();
            return bool.Parse(model.Status.ToString());
        }
    }
}
