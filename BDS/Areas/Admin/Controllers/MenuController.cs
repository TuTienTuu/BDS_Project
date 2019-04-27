using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace BDS.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(int page = 1, int pageSize = 20)
        {
            var model = new MenuDao().ListAllPaging(page, pageSize);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Model.EF.Menu entity)
        {
            if (ModelState.IsValid)
            {
                int id;
                try
                {
                    if (entity.ParentID == null)
                        entity.ParentID = 0;
                    entity.Status = true;
                    id = new MenuDao().Insert(entity);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Menu", "Index"));
                }
                if (id > 0)
                {
                    ModelState.AddModelError("", "Thêm mới thành công");
                    return RedirectToAction("Index", "Menu");
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
            var model = new MenuDao().GetByID(id);
            GetDropdown(int.Parse(model.ParentID.ToString()));
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(Model.EF.Menu entity)
        {
            bool result;
            if (ModelState.IsValid)
            {
                try
                {
                    result = new MenuDao().Update(entity);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Menu", "Update"));
                }
                if (result)
                {
                    ModelState.AddModelError("", "Cập nhật thành công");
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
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ");
                return View();
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            bool result;
            try
            {
                result = new MenuDao().Delete(id);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Menu", "Index"));
            }
            if (result)
            {
                ModelState.AddModelError("", "Xóa dữ liệu thành công");
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Xóa dữ liệu không thành công");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var model = new MenuDao().ChangeStatus(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDropdown(int id)
        {
            DBModel db = new DBModel();
            List<SelectListItem> l = new List<SelectListItem>();
            var model = db.Menus.Where(x => x.Status == true).OrderByDescending(x => x.ParentID == id).ToArray();
            for (int i = 0; i < model.Length; i++)
            {
                l.Add(new SelectListItem { Value = model[i].ID.ToString(), Text = model[i].MenuName_VN });
            }
            ViewData["danhsach"] = l;
            return View();

        }

        public JsonResult GetLevelMenu()
        {
            var Level = new List<ListItem>();
            Level.Add(new ListItem("Cấp Menu", "0"));
            Level.Add(new ListItem("Cấp 1", "1"));
            Level.Add(new ListItem("Cấp 2", "2"));
            Level.Add(new ListItem("Cấp 3", "3"));
            return Json(Level, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetParentID(string id)
        {
            ListItem item = new ListItem();
            int newID = int.Parse(id);
            var ParentID = new MenuDao().LoadParentID(newID);
            return Json(ParentID, JsonRequestBehavior.AllowGet);
        }
    }
}