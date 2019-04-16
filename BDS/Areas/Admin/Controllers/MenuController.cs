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
    public class MenuController : Controller
    {
        // GET: Admin/Menu
        public ActionResult Index()
        {
            return View();
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