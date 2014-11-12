using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 服务基础类
    /// </summary>
    public class ServiceBase<T> : IDisposable
    {
        #region 属性

        /// <summary>
        /// 调用http请求类
        /// </summary>
        [Import]
        protected IRepository Responsitory { get; set; }
        /// <summary>
        /// json格式转换类
        /// </summary>
        protected JsonDecoder<T> Decoder { get; set; }
        /// <summary>
        /// 数据返回后的回调
        /// </summary>
        protected Action<object> Act { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        protected string GetDataCallBack(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            HttpWebResponse response = request.EndGetResponse(result) as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string responseString = reader.ReadToEnd();

            response.Dispose();
            responseStream.Dispose();
            reader.Dispose();
            result = null;
            request = null;
            response = null;
            responseStream = null;
            reader = null;

            return responseString;
        }

        protected void GetDecodeData<T1>(string responseString, JsonDecoder<T1> decoder, Action<object> act)
        {
            object fr = decoder.Decode(responseString);

            if (act != null)
            {
                IAsyncResult result = act.BeginInvoke((T1)fr, null, null);
                act.EndInvoke(result);
            }

            act = null;
            decoder = null;
        }

        #endregion

        #region IDisposable 成员

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            //this.Act.EndInvoke(null);
            //this.Decoder = null;
            //this.Act = null;
            GC.Collect();
        }

        #endregion
    }
}
