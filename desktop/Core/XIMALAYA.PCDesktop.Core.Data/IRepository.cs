using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;


namespace XIMALAYA.PCDesktop.Core.Data
{
    /// <summary>
    /// 数据操作接口
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        void Fetch(string url, string datas,AsyncCallback async, bool IsPost = false);
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        void Add(string url, string datas, AsyncCallback async, bool IsPost = false);
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <returns></returns>
        void Edit(string url, string datas, AsyncCallback async, bool IsPost = false);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        void Del(string url, string datas, AsyncCallback async, bool IsPost = false);
    }
}
