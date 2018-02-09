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
            string requestStr = "<xml><ToUserName><![CDATA[gh_d8f902cb0782]]></ToUserName><FromUserName><![CDATA[o20vNs15Uj_bXSVluqRl3GTH8lNw]]></FromUserName><CreateTime>1517840692</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + content + "]]></Content></xml>";
            string responseStr = new WxMessage().Response(requestStr);
            var data = new { state = true, content = responseStr };
            return Content(JsonConvert.SerializeObject(data));
        }
    }
}