using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Application
{
    public static class Common
    {
        public static string Valid(string signature, string timestamp, string nonce, string echostr)
        {
            if (CheckSignature(signature, timestamp, nonce))
            {
                if (!string.IsNullOrEmpty(echostr))
                {
                    return echostr;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private static bool CheckSignature(string signature, string timestamp, string nonce)
        {
            string Token = string.Empty;
            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp); //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static HttpResponseMessage ToHttpMsgForWeChat(string strMsg)
        {
            HttpResponseMessage result = new HttpResponseMessage
            {
                Content = new StringContent(strMsg, Encoding.GetEncoding("UTF-8"), "application/x-www-form-urlencoded")
            };
            return result;
        }
    }
}
