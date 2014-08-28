using System;

namespace XIMALAYA.PCDesktop.Core.Services
{
  
    /// <summary>
    /// 用户详情
    /// </summary>
    public interface IUserDetailService
    {
        /// <summary>
        /// 获取数据接口
        /// </summary>
        void GetData<T>(Action<object> act,T param);
    }
}
