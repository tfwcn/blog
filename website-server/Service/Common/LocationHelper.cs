using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class LocationHelper
    {
        /// <summary>
        /// 获取本地IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIp()
        {
            string ip = "";
            string result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }


            if (null == result || result == String.Empty)
            {
                result = System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }
        public static bool HasChinese(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }
        /// <summary>
        /// 获取公网IP地址
        /// "https://www.ip.cn", "utf-8"
        /// </summary>
        /// <returns></returns>
        public static string GetNetIp(string url, string encoding)
        {
            string result = string.Empty;
            try
            {
                Encoding encode = System.Text.Encoding.GetEncoding(encoding); //设置编码
                                                                              //获取HttpWebRequest对象
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Method = "GET";
                //设置用户代理
                wbRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.84 Safari/537.36";
                //获取返回流
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    //读取返回流
                    using (StreamReader sReader = new StreamReader(responseStream, encode))
                    {
                        result = parseHtmlIp(sReader.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error");
            }
            return result;
        }
        /// <summary>
        /// 获取HTML内ip
        /// </summary>
        /// <param name="pageHtml"></param>
        /// <returns></returns>
        public static string parseHtmlIp(String pageHtml)
        {
            string ip = "";
            Match m = Regex.Match(pageHtml, @"\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}");
            if (m.Success)
            {
                ip = m.Value;
            }
            return ip;
        }
        /// <summary>
        /// 百度api
        /// </summary>
        /// <returns></returns>
        public static string GetBaiduCS(string ip)
        {
            try
            {
                string cs = "";
                string url = "http://api.map.baidu.com/location/ip?ak=GlfVlFKSc6Y7aSr73IHM3lQI&ip=" + ip;
                WebClient client = new WebClient();
                var buffer = client.DownloadData(url);
                string jsonText = Encoding.UTF8.GetString(buffer);
                JObject jo = JObject.Parse(jsonText);
                var txt = jo["content"]["address_detail"]["city"];
                JToken st = txt;
                string str = st.ToString();
                if (str == "")
                {
                    cs = GetSinaCS(ip);
                    return cs;

                }
                int s = str.IndexOf('市');
                string css = str.Substring(0, s);
                bool bl = HasChinese(css);

                if (bl)
                {
                    cs = css;
                }
                else
                {
                    cs = GetSinaCS(ip);
                }

                return cs;
            }
            catch
            {
                return GetTaoBaoCS(ip);
            }

        }
        /// <summary>
        /// 新浪api
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetSinaCS(string ip)
        {
            try
            {
                string url = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=" + ip;
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                Byte[] pageData = MyWebClient.DownloadData(url); //从指定网站下载数据
                string stt = Encoding.GetEncoding("GBK").GetString(pageData).Trim();
                return stt.Substring(stt.Length - 2, 2);
            }
            catch
            {
                return "未知";
            }

        }
        /// <summary>
        /// 淘宝api
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns></returns>
        public static string GetTaoBaoCS(string strIP)
        {
            try
            {
                string Url = "http://ip.taobao.com/service/getIpInfo.php?ip=" + strIP + "";

                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                wReq.Timeout = 2000;
                System.Net.WebResponse wResp = wReq.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream))
                {
                    string jsonText = reader.ReadToEnd();
                    JObject ja = (JObject)JsonConvert.DeserializeObject(jsonText);
                    if (ja["code"].ToString() == "0")
                    {
                        string c = ja["data"]["city"].ToString();
                        int ci = c.IndexOf('市');
                        if (ci != -1)
                        {
                            c = c.Remove(ci, 1);
                        }
                        return c;
                    }
                    else
                    {
                        return "未知";
                    }
                }
            }
            catch (Exception)
            {
                return ("未知");
            }
        }
    }
}
