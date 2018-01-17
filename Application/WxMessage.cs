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

        public WxXmlModel InitMessageModel(string Msg)
        {
            WxXmlModel model = new WxXmlModel();

            if (string.IsNullOrEmpty(Msg)) return model;

            //封装请求类
            XmlDocument requestDocXml = new XmlDocument();
            requestDocXml.LoadXml(Msg);
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

        public WxXmlModel ResponseModel(WxXmlModel ReciveModel)
        {
            WxXmlModel responseModel = new WxXmlModel();

            responseModel.ToUserName = ReciveModel.FromUserName;
            responseModel.FromUserName = ReciveModel.ToUserName;
            responseModel.CreateTime = TimeHelper.GetTimeStamp(DateTime.Now);
            switch (ReciveModel.MsgType)
            {
                default:
                    responseModel.MsgType = "text";
                    responseModel.Content = "收到消息，内容：" + System.Environment.NewLine + ReciveModel.Content;
                    break;
            }
            

            return responseModel;
        }

        public string Response(WxXmlModel ReciveModel)
        {
            string response = string.Empty;
            StringBuilder sb = new StringBuilder();

            WxXmlModel ResponseModel = this.ResponseModel(ReciveModel);

            switch (ResponseModel.MsgType)
            {
                default:

                    break;
            }

            return response;
        }

        public string GetResponseStr(WxXmlModel ResponseModel)
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
            WxXmlModel reciveModel = InitMessageModel(msg);
            return Response(reciveModel);
        }
    }
}
