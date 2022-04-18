using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web.Security;
using System.Security.Cryptography;
using System.Web;
using System.Collections;
using System.Web.Script.Serialization;

namespace BaiDuFanYi
{
    
    public class FanYi
    {
        public  string LostTranslate(string content,string from,string to)
        {
            if (Translate(content, from, to).Error_code == null)
            {
                return Translate(content, from, to).Trans_result[0].Dst;
            }
            else
                return Translate(content, from, to).Error_msg;
        }
        public static PostResult Translate(string content, string languageFrom, string languageTo)
        {
            string appId = "20210514000827165", passWord = "VLZcrCyLan2T6qIuza80", randomNum = new Random().Next().ToString();
            string md5Sign = FormsAuthentication.HashPasswordForStoringInConfigFile(appId + content + randomNum + passWord, "MD5").ToLower();
            string FullRequest = "http://api.fanyi.baidu.com/api/trans/vip/translate?q=" + content + "&from=" + languageFrom + "&to=" + languageTo + "&appid=" + appId + "&salt=" + randomNum + "&sign=" + md5Sign;
            string m_Content = new WebClient().DownloadString(FullRequest);
            PostResult m_postResult = new JavaScriptSerializer().Deserialize<PostResult>(m_Content);
            return m_postResult;
        }
        public class PostResult
        {
            public string Error_code { set; get; }          //错误代码
            public string Error_msg { set; get; }
            public string From { set; get; }
            public string To { set; get; }
            public TranslateContent[] Trans_result { set; get; }
        }
        public class TranslateContent
        {
            public string Src { set; get; }
            public string Dst { set; get; }
        }
    }
}
