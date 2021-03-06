﻿using Application;
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
            log.Info(requestStr);

            string responseStr = new WxMessage().Response(requestStr);
            log.Info(responseStr);
            return new HttpResponseMessage { Content = new StringContent(responseStr, Encoding.GetEncoding("UTF-8"), "application/xml") };
        }
    }
}
