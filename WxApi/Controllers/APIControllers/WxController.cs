using System.Net.Http;
using System.Text;
using System.Web.Http;
using Application;
using System.IO;
using log4net;
using System.Web;
using System;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace WxApi.Controllers.APIControllers
{
    public class WxController : ApiController
    {
        private ILog log = LogManager.GetLogger(typeof(MessageController).ToString());
        private readonly string TOKEN = "pandahouse";
        // GET: Wx
        public HttpResponseMessage Get(string signature, string timestamp, string nonce, string echostr)
        {
            if (!CheckWeChartSignature.CheckSignature(TOKEN, signature, timestamp, nonce)) echostr = "验证不正确";
            HttpResponseMessage responseMessage = new HttpResponseMessage { Content = new StringContent(echostr, Encoding.GetEncoding("UTF-8"), "text/plain") };

            return responseMessage;
        }


        public HttpResponseMessage Post()
        {
            string signature = HttpContext.Current.Request.QueryString["signature"];
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];
            string echostr = HttpContext.Current.Request.QueryString["echostr"];

            HttpResponseMessage responseMessage;

            try
            {
                if (!CheckWeChartSignature.CheckSignature(TOKEN, signature, timestamp, nonce))
                {
                    echostr = "验证不正确";
                    responseMessage = new HttpResponseMessage { Content = new StringContent(echostr, Encoding.GetEncoding("UTF-8"), "text/plain") };
                }
                else
                {
                    Stream requestStream = System.Web.HttpContext.Current.Request.InputStream;
                    byte[] requestByte = new byte[requestStream.Length];
                    requestStream.Read(requestByte, 0, (int)requestStream.Length);
                    string requestStr = Encoding.UTF8.GetString(requestByte);

                    string responseStr = new WxMessage().Response(requestStr);
                    log.Info("Controller回复：" + responseStr);

                    responseMessage = new HttpResponseMessage { Content = new StringContent(responseStr, Encoding.GetEncoding("UTF-8"), "text/plain") };
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                responseMessage = new HttpResponseMessage { Content = new StringContent("Success", Encoding.GetEncoding("UTF-8"), "text/plain") };
            }

            return responseMessage;
        }
    }
}