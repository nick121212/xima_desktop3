using System;
using System.ComponentModel.Composition;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.MutiData;
using XIMALAYA.PCDesktop.Core.Models.User;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IUserService))]
    class UserService : ServiceBase<UserData>, IUserService
    {
        #region 属性

        private ServiceParams<UserData> UserDataResult { get; set; }
        private ServiceParams<LikedSoundUserResult> LikedSoundUserResult { get; set; }
        private ServiceParams<MutiUserResult> MutiUserResult { get; set; }
        private ServiceParams<UserData> LoginUserDataResult { get; set; }

        #endregion

        #region IUserService 成员

        /// <summary>
        /// 用户详情数据
        /// </summary>
        public void GetDetailData<T>(Action<object> act, T param)
        {
            Result<UserData> result = new Result<UserData>();

            new UserData3Decorator<UserData>(result);

            this.UserDataResult = new ServiceParams<UserData>(Json.DecoderFor<UserData>(config => config.DeriveFrom(result.Config)), act);
            //this.Act = act;
            //this.Decoder = Json.DecoderFor<UserData>(config => config.DeriveFrom(result.Config));
            try
            {
                this.Responsitory.Fetch(WellKnownUrl.UserDetailInfo, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<UserData>(this.GetDataCallBack(asyncResult), this.UserDataResult);
                });
            }
            catch (Exception ex)
            {
                this.UserDataResult.Act.BeginInvoke(new UserData
                {

                }, null, null);
            }
        }
        /// <summary>
        /// 获取喜欢声音的用户列表
        /// </summary>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetSoundLikedUsers<T>(Action<object> act, T param)
        {
            Result<LikedSoundUserResult> result = new Result<LikedSoundUserResult>();

            new LikedSoundUserResultDecorator<LikedSoundUserResult>(result);
            new UserData4Decorator<LikedSoundUserResult>(result);

            this.LikedSoundUserResult = new ServiceParams<LikedSoundUserResult>(Json.DecoderFor<LikedSoundUserResult>(config => config.DeriveFrom(result.Config)),act);
            //this.LikedSoundUserResultAct = act;
            //this.LikedSoundUserResultDecoder = Json.DecoderFor<LikedSoundUserResult>(config => config.DeriveFrom(result.Config));
            try
            {
                this.Responsitory.Fetch(WellKnownUrl.LikedSoundUsers, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<LikedSoundUserResult>(this.GetDataCallBack(asyncResult), this.LikedSoundUserResult);
                });
            }
            catch (Exception ex)
            {
                this.LikedSoundUserResult.Act.BeginInvoke(new LikedSoundUserResult
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
        }
        /// <summary>
        /// 多用户列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetMutiUsers<T>(Action<object> act, T param)
        {
            Result<MutiUserResult> result = new Result<MutiUserResult>();

            new MutiUserResultDecorator<MutiUserResult>(result);
            new UserData5Decorator<MutiUserResult>(result);

            this.MutiUserResult = new ServiceParams<MutiUserResult>(Json.DecoderFor<MutiUserResult>(config => config.DeriveFrom(result.Config)),act);
            //this.MutiUserResultAct = act;
            //this.MutiUserResultDecoder = Json.DecoderFor<MutiUserResult>(config => config.DeriveFrom(result.Config));
            try
            {
                this.Responsitory.Fetch(WellKnownUrl.MutiData, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<MutiUserResult>(this.GetDataCallBack(asyncResult), this.MutiUserResult);
                });
            }
            catch (Exception ex)
            {
                this.MutiUserResult.Act.BeginInvoke(new MutiUserResult
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
        }
        /// <summary>
        /// 获取登录用户的信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetLoginUserInfo<T>(Action<object> act, T param)
        {
            Result<UserData> result = new Result<UserData>();

            new UserData6Decorator<UserData>(result);

            this.LoginUserDataResult = new ServiceParams<UserData>(Json.DecoderFor<UserData>(config => config.DeriveFrom(result.Config)), act);
            try
            {
                this.Responsitory.Fetch(WellKnownUrl.LoginUserInfo, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData(this.GetDataCallBack(asyncResult), this.LoginUserDataResult);
                });
            }
            catch (Exception ex)
            {
                this.LoginUserDataResult.Act.BeginInvoke(new UserData
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
        }
        /// <summary>
        /// 本站登陆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void DoLogin<T>(Action<object> act, T param)
        {
            try
            {
                this.Responsitory.Fetch(WellKnownUrl.LoginPath, param.ToString(), asyncResult =>
                {
                    string result = this.GetDataCallBack(asyncResult);

                    act.BeginInvoke(result, null, null);
                },true);
            }
            catch (Exception ex)
            {
                act.BeginInvoke(new UserData
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
        }

        #endregion
    }
}
