using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace WxApi.Controllers
{
    public class FlushAccessTokenController : ApiController
    {
        string FlushUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        string appid = "wx788c08387403462f";
        string secret = "79e31794ccd81cfe188195827b6c22f5";

        public HttpResponseMessage Get(string passport)
        {
            string flushUrl = string.Format(FlushUrl, appid, secret);

            HttpWebResponse response = HttpHelper.CreateGetHttpResponse(flushUrl, null, null, null);



            HttpResponseMessage responseMessage = new HttpResponseMessage { Content = new StringContent(appid, Encoding.GetEncoding("UTF-8"), "text/plain") };

            return responseMessage;
        }
    }
}
