using System;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 发现页接口
    /// </summary>
    public interface ISuperExploreIndexService
    {
        /// <summary>
        /// 获取数据接口
        /// </summary>
        void GetData<T>(Action<object> act, T param);
    }
}
