﻿using System;
using System.ComponentModel.Composition;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class HttpWebRequestSingleton : Singleton<HttpWebRequestOpt> { }
    /// <summary>
    /// 仓储类基类
    /// </summary>
    [Export(typeof(IRepository))]
    public class HttpRepository : IRepository
    {
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        public void Fetch(string url, string datas, AsyncCallback async, bool IsPost = false)
        {
            if (IsPost)
            {
                HttpWebRequestSingleton.Instance.SendDataByPostAsyn(url, datas, async);
            }
            else
            {
                HttpWebRequestSingleton.Instance.SendDataByGETAsyn(url, datas, async);
            }
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        public void Add(string url, string datas, AsyncCallback async, bool IsPost = false)
        {
            if (IsPost)
            {
                HttpWebRequestSingleton.Instance.SendDataByPostAsyn(url, datas, async);
            }
            else
            {
                HttpWebRequestSingleton.Instance.SendDataByGETAsyn(url, datas, async);
            }
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <returns></returns>
        public void Edit(string url, string datas, AsyncCallback async, bool IsPost = false)
        {
            if (IsPost)
            {
                HttpWebRequestSingleton.Instance.SendDataByPostAsyn(url, datas, async);
            }
            else
            {
                HttpWebRequestSingleton.Instance.SendDataByGETAsyn(url, datas, async);
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public void Del(string url, string datas, AsyncCallback async, bool IsPost = false)
        {
            if (IsPost)
            {
                HttpWebRequestSingleton.Instance.SendDataByPostAsyn(url, datas, async);
            }
            else
            {
                HttpWebRequestSingleton.Instance.SendDataByGETAsyn(url, datas, async);
            }
        }
    }
}
