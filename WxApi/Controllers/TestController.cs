using Application;
using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WxApi.Controllers
{
    public class TestController : Controller
    {
        private RedisHelper redis = new RedisHelper();

        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Set(string key,string value)
        {
            redis.Set(key, value, 2);

            var data = new { state = "success"  };
            return Content(JsonConvert.SerializeObject(data));
        }

        public ActionResult Get(string key)
        {
            var  data = new { result = redis.Get<string>(key) };

            return Content(JsonConvert.SerializeObject(data));
        }

        public ActionResult PostTuring(string content)
        {
            string say = TuringRebotRequest.AskTuring("123", content);


            var data = new { state = true, content = say };
            return Content(JsonConvert.SerializeObject(data));
        }
    }
}