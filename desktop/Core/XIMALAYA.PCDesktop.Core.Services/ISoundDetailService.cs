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
        void GetData<T>(Action<object> act,long trackId);
    }
}
