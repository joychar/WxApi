using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class WechatSecretReqMessage
    {
        public string Encrypt { get; set; }
        public string MsgSignature { get; set; }
        public string TimeStamp { get; set; }
        public string Nonce { get; set; }
    }
}
