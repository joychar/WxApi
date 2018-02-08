using Common;
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
            bool trans = true;
            if (trans)
            {
                return content + " 是什么鬼？？";
            }
            else
            {
                string data = HttpHelper.HttpPostData(requestUrl, param, Encoding.GetEncoding("UTF8"));
                return "";
            }            
        }
    }
}
