using System;
using System.ComponentModel.Composition;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Core.Models.User;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IUserDetailService))]
    public class UserDetailService : ServiceBase<UserData>, IUserDetailService
    {
        /// <summary>
        /// 
        /// </summary>
        [Import]
        protected IUserResultResponsitory Responsitory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void GetData<T>(Action<object> act, T param)
        {
            Result<UserData> result = new Result<UserData>();

            new UserData3Decorator<UserData>(result);

            this.Act = act;
            this.Decoder = Json.DecoderFor<UserData>(config => config.DeriveFrom(result.Config));
            this.Responsitory.Fetch(WellKnownUrl.UserDetailInfo, param.ToString(), base.GetDataCallBack);
        }
    }
}
