using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WxApi.Controllers
{
    public class UploadResourceController : Controller
    {
        // GET: UploadResource
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadImg(HttpPostedFileBase file)
        {


            return Json(new { state = false, msg = "", fileName = "" });
        }

        public ActionResult UploadNews(HttpPostedFileBase file)
        {
            

            return Json(new { state = false, msg = "", fileName = "" });
        }
    }
}