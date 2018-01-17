using Common;
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

        public WxMessageRecXmlModel InitMessageModel(string Msg)
        {
            WxMessageRecXmlModel model = new WxMessageRecXmlModel();

            if (string.IsNullOrEmpty(Msg)) return model;

            //封装请求类
            XmlDocument requestDocXml = new XmlDocument();
            requestDocXml.LoadXml(Msg);
            XmlElement rootElement = requestDocXml.DocumentElement;
            WxMessageRecXmlModel WxXmlModel = new WxMessageRecXmlModel();
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
                    WxXmlModel.Content = rootElement.SelectSingleNode("Content").InnerText;
                    break;
            }

            return model;
        }

        public WxMessageResXmlModel ResponseModel(WxMessageRecXmlModel ReciveModel)
        {
            WxMessageResXmlModel responseModel = new WxMessageResXmlModel();

            responseModel.ToUserName = ReciveModel.FromUserName;
            responseModel.FromUserName = ReciveModel.ToUserName;
            responseModel.CreateTime = TimeHelper.GetTimeStamp(DateTime.Now);
            responseModel.MsgType = ReciveModel.MsgType;
            switch (ReciveModel.MsgType)
            {
                case "text":
                    responseModel.Content = "收到文本消息，内容：" + System.Environment.NewLine + ReciveModel.Content;
                    break;
                case "image"://图片
                    responseModel.MediaId = "0";
                    break;
                default:
                    responseModel.MsgType = "text";
                    responseModel.Content = "收到文本消息，内容：" + System.Environment.NewLine + ReciveModel.Content;
                    break;
            }
            

            return responseModel;
        }

        public string GetResponse(WxMessageResXmlModel ResponseModel)
        {
            XmlDocument xml = new XmlDocument();

            XmlElement root = xml.CreateElement("xml");
            xml.AppendChild(root);

            XmlElement toUserName = xml.CreateElement("ToUserName");
            XmlCDataSection toName = xml.CreateCDataSection(ResponseModel.ToUserName);
            toUserName.AppendChild(toName);
            root.AppendChild(toUserName);

            XmlElement fromUserName = xml.CreateElement("FromUserName");
            XmlCDataSection fromName = xml.CreateCDataSection(ResponseModel.ToUserName);
            fromUserName.AppendChild(fromName);
            root.AppendChild(fromUserName);

            XmlElement createTime = xml.CreateElement("CreateTime");
            createTime.InnerText = ResponseModel.CreateTime;
            root.AppendChild(createTime);

            return xml.OuterXml;
        }

        public string Response(string msg)
        {
            WxMessageResXmlModel responseModel = ResponseModel(InitMessageModel(msg));
            return GetResponse(responseModel);
        }
    }
}
