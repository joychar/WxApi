using Common;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        string secret = "79e31794ccd81cfe188195827b6c22f5";//

        public HttpResponseMessage Get(string passport)
        {
            string flushUrl = string.Format(FlushUrl, appid, secret);

            HttpWebResponse response = HttpHelper.CreateGetHttpResponse(flushUrl, null, null, null);

            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            string data = streamReader.ReadToEnd();
            AccessToken tokenMessage = JsonConvert.DeserializeObject<AccessToken>(data);
            if (string.IsNullOrEmpty(tokenMessage.access_token))
            {
                FailedResult errorMessage = JsonConvert.DeserializeObject<FailedResult>(data);

                string error = Application.ErrorMessage.TranslateErrorCode(errorMessage.errcode);
                return new HttpResponseMessage { Content = new StringContent(error + System.Environment.NewLine + errorMessage.errmsg, Encoding.GetEncoding("UTF-8"), "text/plain") };
            }

            return new HttpResponseMessage { Content = new StringContent(tokenMessage.access_token, Encoding.GetEncoding("UTF-8"), "text/plain") };
        }
    }
}
