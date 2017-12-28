using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;

namespace WxApi.Controllers
{
    public class WxController : ApiController
    {
        private readonly string TOKEN = "pandahouse8844";
        // GET: Wx
        public HttpResponseMessage Get(string signature, string timestamp, string nonce, string echostr)
        {
            string[] ArrTmp = { TOKEN, timestamp, nonce };
            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            var result = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1").ToLower();

            
            return new HttpResponseMessage()
            {
                Content = new StringContent(echostr, Encoding.GetEncoding("UTF-8"), "application/x-www-form-urlencoded")
                
            };
            
        }
    }
}