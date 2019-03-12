using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{

    public class NewsDao
    {
        DBModel db = null;
        public NewsDao()
        {
            db = new DBModel();
        }

        public News GetNewsById(int id)
        {
            return db.News.Find(id);
        }
    }
}
