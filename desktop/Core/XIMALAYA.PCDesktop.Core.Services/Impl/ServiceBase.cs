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

        ///// <summary>
        ///// json格式转换类
        ///// </summary>
        //protected JsonDecoder<T> Decoder { get; set; }
        ///// <summary>
        ///// 数据返回后的回调
        ///// </summary>
        //protected Action<object> Act { get; set; }

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

        protected void GetDecodeData<T1>(string responseString, ServiceParams<T1> param)
        {
            object fr = param.Decoder.Decode(responseString);

            if (param.Act != null)
            {
                IAsyncResult result = param.Act.BeginInvoke((T1)fr, null, null);
                param.Act.EndInvoke(result);
            }
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

    public class ServiceParams<T> : IDisposable
    {
        /// <summary>
        /// json格式转换类
        /// </summary>
        public JsonDecoder<T> Decoder { get; set; }
        /// <summary>
        /// 数据返回后的回调
        /// </summary>
        public Action<object> Act { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="decoder"></param>
        /// <param name="act"></param>
        public ServiceParams(JsonDecoder<T> decoder, Action<object> act)
        {
            this.Decoder = decoder;
            this.Act = act;
        }
        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            this.Decoder = null;
            this.Act = null;
        }
    }
}
