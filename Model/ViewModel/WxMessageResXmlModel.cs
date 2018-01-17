using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    /// <summary>
    /// 微信接口XmlModel
    /// XML解析
    /// </summary>
    public class WxMessageResXmlModel
    {
        /// <summary>
        /// 消息接收方微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 消息发送方微信号
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 信息类型 地理位置:location,文本消息:text,消息类型:image
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 通过素材管理中的接口上传多媒体文件，得到的id。
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MusicURL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HQMusicUrl { get; set; }
        public string ThumbMediaId { get; set; }
        public string ArticleCount { get; set; }
        public string Articles { get; set; }
        public string PicUrl { get; set; }
        public string Url { get; set; }
    }
}
