using System;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 发现页接口
    /// </summary>
    public interface IExploreService
    {
        /// <summary>
        /// 获取数据接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        void GetData<T>(Action<object> act, T param);
        /// <summary>
        /// 获取分类下的标签数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        void GetTagsData<T>(Action<object> act, T param);
    }
}
