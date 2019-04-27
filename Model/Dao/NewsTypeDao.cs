using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class NewsTypeDao
    {
        DBModel db = null;
        public NewsTypeDao()
        {
            db = new DBModel();
        }

        public NewsType GetNewsById(int id)
        {
            return db.NewsTypes.Find(id);
        }

        public int Insert(NewsType entity)
        {
            try
            {
                db.NewsTypes.Add(entity);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return entity.NewsTypeID;
        }

        public IEnumerable<NewsType> ListAllPaging(int page, int pageSize)
        {
            return db.NewsTypes.Where(x => x.Status == true).OrderBy(x => x.NewsTypeID).ToPagedList(page, pageSize);
        }

        public bool Update(NewsType entity)
        {
            try
            {
                var model = db.NewsTypes.FirstOrDefault(x => x.NewsTypeID == entity.NewsTypeID);
                model.NewsTypeName_EN = entity.NewsTypeName_EN;
                model.NewsTypeName_VN = entity.NewsTypeName_VN;
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
                var model = db.NewsTypes.FirstOrDefault(x=>x.NewsTypeID == id);
                db.NewsTypes.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
