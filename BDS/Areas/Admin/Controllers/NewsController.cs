using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDS.Areas.Admin.Controllers
{
    public class NewsController : BaseController
    {
        // GET: Admin/News
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var dao = new NewsDao();
            var item = dao.GetNewsById(id);
            SetViewBag(item.NewsType);
            return View();
        }

        [HttpPost]
        public ActionResult Create(News model)
        {

            if (ModelState.IsValid)
            {

            }
            SetViewBag();
            return View();
        }


        [HttpPost]
        public ActionResult Update(News model)
        {

            if (ModelState.IsValid)
            {

            }
            SetViewBag(model.NewsType);
            return View();
        }

        public void SetViewBag(int? selectedID = null)
        {
            var dao = new CategoryDao();
            ViewBag.NewsType = new SelectList(dao.ListNewsType(), "NewsTypeID", "NewsTypeName_VN", selectedID);
        }
    }
}