using System;

namespace XIMALAYA.PCDesktop.Core.Services
{

    /// <summary>
    /// 用户详情
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 获取数据接口
        /// </summary>
        void GetDetailData<T>(Action<object> act, T param);
        /// <summary>
        /// 获取喜欢声音的用户列表
        /// </summary>
        /// <param name="act"></param>
        /// <param name="param"></param>
        void GetSoundLikedUsers<T>(Action<object> act, T param);
        /// <summary>
        /// 多用户列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        void GetMutiUsers<T>(Action<object> act, T param);
        /// <summary>
        /// 获取登录用户的信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        void GetLoginUserInfo<T>(Action<object> act, T param);
        /// <summary>
        /// 本站登录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        void DoLogin<T>(Action<object> act, T param);
    }
}
