using System;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 搜索页接口
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        /// 搜索所有
        /// </summary>
        void GetAllData<T>(Action<object> act, T param);
        /// <summary>
        /// 搜索声音数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        void GetSoundData<T>(Action<object> act, T param);
        /// <summary>
        /// 搜索专辑数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        void GetAlbumData<T>(Action<object> act, T param);
        /// <summary>
        /// 搜索人的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        void GetUserData<T>(Action<object> act, T param);
    }
}
