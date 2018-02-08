using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    /// <summary>
    /// 对话关键字匹配
    /// </summary>
    public class CatchKeyWord
    {
        /// <summary>
        /// 功能关键字
        /// </summary>
        private static List<string> keyword = new List<string>() { "记密码" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public static bool IsKeyWord(string Msg)
        {
            return keyword.Contains(Msg);
        }

        //public void 
    }
}
