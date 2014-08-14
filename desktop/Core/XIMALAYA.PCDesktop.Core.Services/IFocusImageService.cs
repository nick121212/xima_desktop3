using System;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 焦点图接口
    /// </summary>
    public interface IFocusImageService
    {
        /// <summary>
        /// 获取数据接口
        /// </summary>
        void GetData<T>(Action<object> act, T param);
    }
}
