using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class NewsTypeController : BaseController
    {
        // GET: Admin/NewsType
        public ActionResult Index(int page =1,int pageSize =1)
        {
            var model = new NewsTypeDao().ListAllPaging(page, pageSize);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NewsType entity)
        {
            int id;
            if (ModelState.IsValid)
            {
                try
                {
                    entity.Status = true;
                    id = new NewsTypeDao().Insert(entity);
                }
                catch (Exception ex)
                {
                    return View("Erorr", new HandleErrorInfo(ex, "NewsType", "Create"));
                }

                if (id > 0)
                {
                    ModelState.AddModelError("","Thêm mới thành công");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var model = new NewsTypeDao().GetNewsById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(NewsType entity)
        {
            if (ModelState.IsValid)
            {
                bool result;
                try
                {
                    result = new NewsTypeDao().Update(entity);
                }
                catch (Exception ex)
                {
                    return View("Error",new HandleErrorInfo(ex, "NewsType", "Index"));
                }
                if (result)
                {
                    ModelState.AddModelError("","Cập nhật thành công");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin");
                return View();
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            bool result;
            try
            {
                result = new NewsTypeDao().Delete(id);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "NewsType", "Index"));
            }
            if (result)
            {
                ModelState.AddModelError("","Xóa thành công");
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Xóa không thành công");
                return View();
            }
           
        }
    }
}