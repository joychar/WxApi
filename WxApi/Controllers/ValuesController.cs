using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using Application;
using System.Text;

namespace WxApi.Controllers
{

    [Authorize]
    public class ValuesController : ApiController
    {
        private static ILog log = LogManager.GetLogger("loginfo");
        private readonly string token = "pandahouse8844";

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        public HttpResponseMessage ConnWechat(string signature, string timestamp, string nonce, string echostr)
        {
            try
            {
                log.Debug("signature: " + signature);
                log.Debug("timestamp: " + timestamp);
                log.Debug("nonce: " + nonce);
                log.Debug("echostr: " + echostr);

                string EchoStr = Common.Valid(signature, timestamp, nonce, echostr);

                if (!string.IsNullOrEmpty(EchoStr))
                {
                    log.Debug("验证成功！");
                    return Common.ToHttpMsgForWeChat(echostr);
                }
                else
                {
                    log.Debug("验证失败！");
                    return Common.ToHttpMsgForWeChat("验证失败！");
                }
            }


            catch (Exception ex)
            {
                log.Debug("参数错误！");
                return Common.ToHttpMsgForWeChat(ex.ToString());

            }

        }


    }
}
