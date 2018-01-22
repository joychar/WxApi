using Common;
using Model;
using Newtonsoft.Json;
using System;

namespace Application
{
    public class AccessToken
    {
        private string FlushUrl = "https://hk.api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        string appid = "wx788c08387403462f";
        string secret = "79e31794ccd81cfe188195827b6c22f5";

        public bool RecordAccessToken()
        {
            string flushUrl = string.Format(FlushUrl, appid, secret);

            string data = HttpHelper.HttpGetData(flushUrl);
            AccessTokenModel tokenMessage = JsonConvert.DeserializeObject<AccessTokenModel>(data);

            RedisHelper redis = new RedisHelper();
            redis.Set("WeChatToken", tokenMessage, 300);
            if (string.IsNullOrEmpty(tokenMessage.access_token))
            {
                FailedResultModel errorMessage = JsonConvert.DeserializeObject<FailedResultModel>(data);

                string error = Application.ErrorMessage.TranslateErrorCode(errorMessage.errcode);
                return false;
            }
            return true;
        }

        public string GetAccessToken()
        {
            RedisHelper redis = new RedisHelper();
            if (!redis.IsSet("WeChatToken"))
            {
                if (!RecordAccessToken())
                {
                    throw new Exception("token获取失败");
                }
            }

            var result = redis.Get<AccessTokenModel>("WeChatToken");
            return result.access_token;
        }

    }
}
