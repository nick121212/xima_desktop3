using System;

namespace XIMALAYA.PCDesktop.Core.Services
{

    /// <summary>
    /// 声音详情
    /// </summary>
    public interface ISoundService
    {
        /// <summary>
        /// 获取声音详情数据接口
        /// </summary>
        void GetDetailData<T>(Action<object> act, T param);
    }
}
