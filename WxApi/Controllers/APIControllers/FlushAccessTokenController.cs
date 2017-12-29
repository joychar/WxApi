using Application;
using Common;
using Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace WxApi.Controllers
{
    public class FlushAccessTokenController : ApiController
    {
        string FlushUrl = "https://hk.api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        string appid = "wx788c08387403462f";
        string secret = "79e31794ccd81cfe188195827b6c22f5";//

        public HttpResponseMessage Get(string passport)
        {
            string flushUrl = string.Format(FlushUrl, appid, secret);

            string data = HttpHelper.HttpGetData(flushUrl);
            AccessTokenModel tokenMessage = JsonConvert.DeserializeObject<AccessTokenModel>(data);
            if (string.IsNullOrEmpty(tokenMessage.access_token))
            {
                FailedResultModel errorMessage = JsonConvert.DeserializeObject<FailedResultModel>(data);

                string error = Application.ErrorMessage.TranslateErrorCode(errorMessage.errcode);
                return new HttpResponseMessage { Content = new StringContent(error + System.Environment.NewLine + errorMessage.errmsg, Encoding.GetEncoding("UTF-8"), "text/plain") };
            }
            AccessToken.RecordAccessToken(tokenMessage.access_token);

            return new HttpResponseMessage { Content = new StringContent(tokenMessage.access_token, Encoding.GetEncoding("UTF-8"), "text/plain") };
        }
    }
}
