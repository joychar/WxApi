using Common;
using Model.ViewModel;
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

        public static TuringResponseModel AskTuring(string user, string content)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("key", turingApiKey);
            param.Add("info", content);
            param.Add("userid", user);

            string data = HttpHelper.HttpPostData(requestUrl, param, Encoding.GetEncoding("UTF-8"));

            return JsonConvert.DeserializeObject<TuringResponseModel>(data);
        }

    }
}
