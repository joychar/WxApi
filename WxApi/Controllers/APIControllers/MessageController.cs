using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace WxApi.Controllers.APIControllers
{
    public class MessageController : ApiController
    {
        public HttpResponseMessage Post(string ToUserName, string FromUserName, int CreateTime, string MsgType, string Content, Int64 MsgId)
        {


            return new HttpResponseMessage { Content = new StringContent("ToUserName:"+ ToUserName, Encoding.GetEncoding("UTF-8"), "text/plain") };
        }
    }
}
