using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CategoryDao
    {
        DBModel db = null;
        public CategoryDao()
        {
            db = new DBModel();
        }

        public List<NewsType> ListNewsType()
        {
            return db.NewsTypes.Where(x => x.Status == true).ToList();
        }

    }
}
