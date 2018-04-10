using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AppModel
{
    public class FunctionModel
    {
        public string functionCode { get; set; }
        public bool IsFinish { get; set; }
    }

    public class LogAccount : FunctionModel
    {
        public string tag { get; set; }
        public string account { get; set; }
        public string pwd { get; set; }
    }
}
