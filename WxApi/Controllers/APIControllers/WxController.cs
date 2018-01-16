using System.Net.Http;
using System.Text;
using System.Web.Http;
using Application;

namespace WxApi.Controllers.APIControllers
{
    public class WxController : ApiController
    {
        private readonly string TOKEN = "pandahouse";
        // GET: Wx
        public HttpResponseMessage Get(string signature, string timestamp, string nonce, string echostr)
        {
            if (!CheckWeChartSignature.CheckSignature(TOKEN, signature, timestamp, nonce))
                echostr = "验证不正确";
            HttpResponseMessage responseMessage = new HttpResponseMessage { Content = new StringContent(echostr, Encoding.GetEncoding("UTF-8"), "text/plain") };

            return responseMessage;
        }

        
        
    }
}