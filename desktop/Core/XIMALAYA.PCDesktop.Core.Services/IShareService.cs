using System;
using XIMALAYA.PCDesktop.Core.ParamsModel;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 分享接口
    /// </summary>
    public interface IShareService
    {
        /// <summary>
        /// 分类下的标签接口
        /// </summary>
        void GetData<T>(Action<object> act, T param, TagType tagType);
    }
}
