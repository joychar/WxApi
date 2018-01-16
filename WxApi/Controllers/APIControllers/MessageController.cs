using Application;
using log4net;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace WxApi.Controllers.APIControllers
{
    public class MessageController : ApiController
    {
        private ILog log = LogManager.GetLogger(typeof(MessageController).ToString());

        public HttpResponseMessage Post()
        {
            Stream requestStream = System.Web.HttpContext.Current.Request.InputStream;
            byte[] requestByte = new byte[requestStream.Length];
            requestStream.Read(requestByte, 0, (int)requestStream.Length);
            string requestStr = Encoding.UTF8.GetString(requestByte);

            WxXmlModel model = new WxMessage().InitMessage(requestStr);

            return new HttpResponseMessage { Content = new StringContent("ToUserName:"+ ToUserName, Encoding.GetEncoding("UTF-8"), "text/plain") };
        }
    }
}
