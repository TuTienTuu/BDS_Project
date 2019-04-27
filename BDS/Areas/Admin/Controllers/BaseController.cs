using BDS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BDS.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
       protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Lấy dữ liệu từ Session ra để kiểm tra
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            //Lấy link cần tra về trang trước khi đăng nhập đưa vào Session
            Session[CommonConstants.BACK_LINK] = Request.Url.ToString();

            //Kiểm tra thông tin Session
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action="Index",area = "Admin"}));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}