using System;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 专辑数据接口详情
    /// </summary>
    public interface IAlbumService
    {
        /// <summary>
        /// 获取专辑详情数据
        /// </summary>
        void GetDetailData<T>(Action<object> act, T param);
        /// <summary>
        /// 获取标签下的专辑数据
        /// </summary>
        /// <param name="act"></param>
        /// <param name="param"></param>
        void GetTagAlbums<T>(Action<object> act, T param);
        /// <summary>
        /// 获取他人的专辑列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        void GetUserAlbums<T>(Action<object> act,T param);
    }
}
