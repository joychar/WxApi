using Common;
using Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace WxApi.Controllers.APIControllers
{
    public class ServiceIPAddressController : ApiController
    {
        string requestUrl = "https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}";
        public HttpResponseMessage Get(string passport)
        {
            string flushUrl = string.Format(requestUrl, Application.AccessToken.GetAccessToken());

            string data = HttpHelper.GetData(flushUrl);
            IPAddressModel ipAddressMessage = JsonConvert.DeserializeObject<IPAddressModel>(data);
            if (ipAddressMessage.ip_list == null || ipAddressMessage.ip_list.Length < 1)
            {
                FailedResultModel errorMessage = JsonConvert.DeserializeObject<FailedResultModel>(data);

                string error = Application.ErrorMessage.TranslateErrorCode(errorMessage.errcode);
                return new HttpResponseMessage { Content = new StringContent(error + System.Environment.NewLine + errorMessage.errmsg, Encoding.GetEncoding("UTF-8"), "text/plain") };
            }

            return new HttpResponseMessage { Content = new StringContent(string.Join(",", ipAddressMessage.ip_list), Encoding.GetEncoding("UTF-8"), "text/plain") };
        }
    }
}
