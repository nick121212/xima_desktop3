using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Tools
{
    /// <summary>
    /// 数据操作类
    /// </summary>
    public class HttpWebRequestOpt : Singleton<HttpWebRequestOpt>
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>
        /// cookie
        /// </summary>
        public CookieContainer Cookies { get; set; }

        private HttpWebRequestOpt()
        {
            FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo(Path.Combine(Directory.GetCurrentDirectory(), "XIMALAYA.PCDesktop.exe"));

            this.Cookies = new CookieContainer();
            this.UserAgent = string.Format("ting-ximalaya_v{0} name/ximalaya os/{1} osName/{2}", myFileVersion.FileVersion, OSInfo.Instance.OsInfo.VersionString, OSInfo.Instance.OsInfo.Platform.ToString());
            this.Cookies.Add(new Cookie("4&_token", "935&d63fef280403904a8c0a5ee0dbe228f2d064", "/", ".ximalaya.com"));
        }

        /// <summary>
        /// 通过POST方式发送数据
        /// </summary>
        /// <param name="Url">url</param>
        /// <param name="postDataStr">Post数据</param>
        /// <returns></returns>
        public string SendDataByPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

            request.CookieContainer = this.Cookies;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataStr.Length;
            request.UserAgent = this.UserAgent;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        /// <summary>
        /// 通过GET方式发送数据
        /// </summary>
        /// <param name="Url">url</param>
        /// <param name="postDataStr">GET数据</param>
        /// <returns></returns>
        public string SendDataByGET(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);

            request.CookieContainer = this.Cookies;
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = this.UserAgent;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        /// <summary>
        /// 异步通过POST方式发送数据
        /// </summary>
        /// <param name="Url">url</param>
        /// <param name="postDataStr">GET数据</param>
        /// <param name="async"></param>
        public void SendDataByPostAsyn(string Url, string postDataStr, AsyncCallback async)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

            request.CookieContainer = this.Cookies;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataStr.Length;
            request.UserAgent = this.UserAgent;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            request.BeginGetResponse(async, request);
        }
        /// <summary>
        /// 异步通过GET方式发送数据
        /// </summary>
        /// <param name="Url">url</param>
        /// <param name="postDataStr">GET数据</param>
        /// <param name="async"></param>
        /// <returns></returns>
        public void SendDataByGETAsyn(string Url, string postDataStr, AsyncCallback async)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);

            request.CookieContainer = this.Cookies;
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = this.UserAgent;

            request.BeginGetResponse(async, request);
        }
    }
}
