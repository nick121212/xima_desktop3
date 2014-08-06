using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentJson.Configuration;
using FluentJson;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 发现页热门声音接口
    /// </summary>
    public interface IHotSoundsService
    {
        /// <summary>
        /// 获取数据接口
        /// </summary>
        void GetData<T>(Action<object> act, T param);
    }
}
