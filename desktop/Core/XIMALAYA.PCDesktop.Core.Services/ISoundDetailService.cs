using System;

namespace XIMALAYA.PCDesktop.Core.Services
{

    /// <summary>
    /// 声音详情
    /// </summary>
    public interface ISoundDetailService
    {
        /// <summary>
        /// 获取数据接口
        /// </summary>
        void GetData<T>(Action<object> act, T param);
        /// <summary>
        /// 获取喜欢声音的用户列表
        /// </summary>
        /// <param name="act"></param>
        /// <param name="param"></param>
        void GetLikedUsers<T>(Action<object> act, T param);
    }
}
