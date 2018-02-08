using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class TuringRebotRequest
    {
        public static string requestUrl = "http://www.tuling123.com/openapi/api";
        public static string turingApiKey = "83be7cf594624bba9f3e306a548642ee";

        public static string AskTuring(string user, string content)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("key", turingApiKey);
            param.Add("info", content);
            param.Add("userid", user);

            bool trans = true;
            if (trans)
            {
                return content + " 是什么鬼？？";
            }
            else
            {
                string data = HttpHelper.HttpPostData(requestUrl, param, Encoding.GetEncoding("UTF8"));

                var turingMsg = JsonConvert.DeserializeObject(data) as JObject;
                int msgCode = (int)turingMsg["code"];

                string turingSay = string.Empty;
                switch (msgCode)
                {
                    case 100000://文本消息
                        turingSay = "文本消息";
                        break;
                    case 200000://链接消息
                        turingSay = "链接消息";
                        break;
                    case 302000://新闻消息
                        turingSay = "新闻消息";
                        break;
                    case 308000://菜谱消息
                        turingSay = "菜谱消息";
                        break;
                    default:
                        turingSay = "胖胖就是看着你，不想说话。";
                        break;
                }

                return turingSay;
            }            
        }
    }
}
