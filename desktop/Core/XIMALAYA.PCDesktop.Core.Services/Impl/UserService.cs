using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Core.Models.MutiData;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Core.Models.User;
using XIMALAYA.PCDesktop.Tools;
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

        /// <summary>
        /// 喜欢声音的用户decoder
        /// </summary>
        private JsonDecoder<LikedSoundUserResult> LikedSoundUserResultDecoder { get; set; }
        /// <summary>
        /// 喜欢声音的用户act
        /// </summary>
        private Action<object> LikedSoundUserResultAct { get; set; }
        /// <summary>
        /// 多用户decoder
        /// </summary>
        private JsonDecoder<MutiUserResult> MutiUserResultDecoder { get; set; }
        /// <summary>
        /// 多用户act
        /// </summary>
        private Action<object> MutiUserResultAct { get; set; }

        #endregion

        #region IUserService 成员

        /// <summary>
        /// 用户详情数据
        /// </summary>
        public void GetDetailData<T>(Action<object> act, T param)
        {
            Result<UserData> result = new Result<UserData>();

            new UserData3Decorator<UserData>(result);

            this.Act = act;
            this.Decoder = Json.DecoderFor<UserData>(config => config.DeriveFrom(result.Config));
            try
            {
                this.Responsitory.Fetch(WellKnownUrl.UserDetailInfo, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<UserData>(this.GetDataCallBack(asyncResult), this.Decoder, this.Act);
                });
            }
            catch (Exception ex)
            {
                this.Act.BeginInvoke(new UserData
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

            this.LikedSoundUserResultAct = act;
            this.LikedSoundUserResultDecoder = Json.DecoderFor<LikedSoundUserResult>(config => config.DeriveFrom(result.Config));
            try
            {
                this.Responsitory.Fetch(WellKnownUrl.LikedSoundUsers, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<LikedSoundUserResult>(this.GetDataCallBack(asyncResult), this.LikedSoundUserResultDecoder, this.LikedSoundUserResultAct);
                });
            }
            catch (Exception ex)
            {
                this.Act.BeginInvoke(new LikedSoundUserResult
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

            this.MutiUserResultAct = act;
            this.MutiUserResultDecoder = Json.DecoderFor<MutiUserResult>(config => config.DeriveFrom(result.Config));
            try
            {
                this.Responsitory.Fetch(WellKnownUrl.MutiData, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<MutiUserResult>(this.GetDataCallBack(asyncResult), this.MutiUserResultDecoder, this.MutiUserResultAct);
                });
            }
            catch (Exception ex)
            {
                this.Act.BeginInvoke(new MutiUserResult
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
        }

        #endregion
    }
}
