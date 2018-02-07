using Common;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class UploadResource
    {
        public bool UploadPicture()
        {
            string uploadUrl = "";
            string token = "";

            //{
            //    "articles": [{
            //        "title": TITLE,
            //        "thumb_media_id": THUMB_MEDIA_ID,
            //        "author": AUTHOR,
            //        "digest": DIGEST,
            //        "show_cover_pic": SHOW_COVER_PIC(0 / 1),
            //        "content": CONTENT,
            //        "content_source_url": CONTENT_SOURCE_URL
            //          },
            //    //若新增的是多图文素材，则此处应还有几段articles结构
            //    ]
            //}

            Dictionary<string, string> param = new Dictionary<string, string>();

            string picContent = "";
            param.Add("articles", picContent);
            string data = HttpHelper.HttpPostData(string.Format(uploadUrl, token), param, Encoding.GetEncoding("UTF-8"));


            return true;
        }

    }
}
