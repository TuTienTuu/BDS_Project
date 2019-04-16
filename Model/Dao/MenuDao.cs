using Model.EF;
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
    }
}
