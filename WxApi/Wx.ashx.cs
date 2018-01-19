using Application;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WxApi
{
    /// <summary>
    /// Wx 的摘要说明
    /// </summary>
    public class Wx : IHttpHandler
    {
        private ILog log = LogManager.GetLogger(typeof(Wx).ToString());
        private readonly string TOKEN = "pandahouse";

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.HttpMethod.ToLower() == "post")
            {
                Stream requestStream = System.Web.HttpContext.Current.Request.InputStream;
                byte[] requestByte = new byte[requestStream.Length];
                requestStream.Read(requestByte, 0, (int)requestStream.Length);
                string requestStr = Encoding.UTF8.GetString(requestByte);
                

                string responseStr = new WxMessage().Response(requestStr);
                log.Info("Ashx回复：" + responseStr);
                HttpContext.Current.Response.Write(responseStr);
                HttpContext.Current.Response.End();
            }
            else
            {
                if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["echoStr"]))
                {
                    HttpContext.Current.Response.Write("消息并非来自微信");
                    HttpContext.Current.Response.End();
                }

                string echoStr = HttpContext.Current.Request.QueryString["echoStr"];
                string signature = HttpContext.Current.Request.QueryString["signature"].ToString();
                string timestamp = HttpContext.Current.Request.QueryString["timestamp"].ToString();
                string nonce = HttpContext.Current.Request.QueryString["nonce"].ToString();

                if (CheckWeChartSignature.CheckSignature(TOKEN, signature, timestamp, nonce))
                {
                    HttpContext.Current.Response.Write(echoStr);
                    HttpContext.Current.Response.End();
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}