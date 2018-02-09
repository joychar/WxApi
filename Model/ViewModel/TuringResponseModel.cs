using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class TuringResponseModel
    {
        public int code { get; set; }
        public string text { get; set; }
        public string url { get; set; }
        public List<TuringListInfo> list { get; set; }
    }

    public class TuringListInfo
    {
        public string detailurl { get; set; }
        public string icon { get; set; }
        public string article { get; set; }
        public string source { get; set; }
        public string name { get; set; }
        public string info { get; set; }
    }
}
