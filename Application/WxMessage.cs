using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Application
{
    public class WxMessage
    {

        public WxXmlModel InitMessage(string msg)
        {
            WxXmlModel model = new WxXmlModel();

            if (string.IsNullOrEmpty(msg)) return model;

            //封装请求类
            XmlDocument requestDocXml = new XmlDocument();
            requestDocXml.LoadXml(msg);
            XmlElement rootElement = requestDocXml.DocumentElement;
            WxXmlModel WxXmlModel = new WxXmlModel();
            WxXmlModel.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
            WxXmlModel.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
            WxXmlModel.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
            WxXmlModel.MsgType = rootElement.SelectSingleNode("MsgType").InnerText;
            switch (WxXmlModel.MsgType)
            {
                case "text"://文本
                    WxXmlModel.Content = rootElement.SelectSingleNode("Content").InnerText;
                    break;
                case "image"://图片
                    WxXmlModel.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
                    break;
                case "event"://事件
                    WxXmlModel.Event = rootElement.SelectSingleNode("Event").InnerText;
                    if (WxXmlModel.Event == "subscribe")//关注类型
                    {
                        WxXmlModel.EventKey = rootElement.SelectSingleNode("EventKey").InnerText;
                    }
                    break;
                default:
                    break;
            }

            return model;
        }
    }
}
