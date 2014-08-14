using System;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 分类下的标签下的专辑接口
    /// </summary>
    public interface ICategoryTagAlbumsService
    {
        /// <summary>
        /// 分类下的标签下的专辑接口
        /// </summary>
        void GetData<T>(Action<object> act, T param);
    }
}
