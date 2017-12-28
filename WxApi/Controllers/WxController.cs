using Application;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;

namespace WxApi.Controllers
{
    public class WxController : ApiController
    {
        private readonly string TOKEN = "pandahouse";
        // GET: Wx
        public HttpResponseMessage Get(string signature, string timestamp, string nonce, string echostr)
        {
            if (!CheckSignature(TOKEN, signature, timestamp, nonce))
                echostr = "验证不正确";
            HttpResponseMessage responseMessage = new HttpResponseMessage { Content = new StringContent(echostr, Encoding.GetEncoding("UTF-8"), "text/plain") };

            return responseMessage;
        }

        
        /// <summary>
        /// 验证微信签名
        /// </summary>
        private bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };

            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            var data = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(tmpStr));
            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString("X2"));
            }
            tmpStr = sb.ToString();
            tmpStr = tmpStr.ToLower();

            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}