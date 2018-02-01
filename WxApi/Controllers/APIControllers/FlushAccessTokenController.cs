using Application;
using Common;
using Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace WxApi.Controllers.APIControllers
{
    public class FlushAccessTokenController : ApiController
    {

        public HttpResponseMessage Get(string passport)
        {
            return new HttpResponseMessage { Content = new StringContent(AccessToken.RecordAccessToken().ToString(), Encoding.GetEncoding("UTF-8"), "text/plain") };
        }
    }
}
