﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using XIMALAYA.PCDesktop.Core.Models;
using XIMALAYA.PCDesktop.Tools;


namespace XIMALAYA.PCDesktop.Core.Data
{
    /// <summary>
    /// 仓储类基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> : IRepository
         where T : IBaseResult
    {
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        public void Fetch(string url, string datas, AsyncCallback async, bool IsPost = false)
        {
            if (IsPost)
            {
                HttpWebRequestOpt.Instance.SendDataByPostAsyn(url, datas,  async);
            }
            else
            {
                HttpWebRequestOpt.Instance.SendDataByGETAsyn(url, datas,  async);
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
                HttpWebRequestOpt.Instance.SendDataByPostAsyn(url, datas, async);
            }
            else
            {
                HttpWebRequestOpt.Instance.SendDataByGETAsyn(url, datas,  async);
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
                HttpWebRequestOpt.Instance.SendDataByPostAsyn(url, datas, async);
            }
            else
            {
                HttpWebRequestOpt.Instance.SendDataByGETAsyn(url, datas, async);
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public void Del(string url, string datas,  AsyncCallback async, bool IsPost = false)
        {
            if (IsPost)
            {
                HttpWebRequestOpt.Instance.SendDataByPostAsyn(url, datas,async);
            }
            else
            {
                HttpWebRequestOpt.Instance.SendDataByGETAsyn(url, datas,  async);
            }
        }
    }
}
