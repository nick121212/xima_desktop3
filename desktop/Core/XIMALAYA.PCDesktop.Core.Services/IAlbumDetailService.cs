using System;

namespace XIMALAYA.PCDesktop.Core.Services
{
  
    /// <summary>
    /// 专辑详情
    /// </summary>
    public interface IAlbumDetailService
    {
        /// <summary>
        /// 获取数据接口
        /// </summary>
        void GetData<T>(Action<object> act, T param);
    }
}
