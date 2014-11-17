using System;
using System.ComponentModel.Composition;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.MutiData;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(ISoundService))]
    class SoundService : ServiceBase<SoundData>, ISoundService
    {
        #region 属性

        private ServiceParams<SoundData> SoundDataResult { get; set; }
        private ServiceParams<MutiSoundResult> MutiSoundResult { get; set; }

        #endregion

        #region ISoundDetailService 成员

        /// <summary>
        /// 获取声音详情数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetDetailData<T>(Action<object> act, T param)
        {
            Result<SoundData> result = new Result<SoundData>();

            new SoundDetailResultDecorator<SoundData>(result);
            new UserData2Decorator<SoundData>(result);

            this.SoundDataResult = new ServiceParams<SoundData>(Json.DecoderFor<SoundData>(config => config.DeriveFrom(result.Config)),act);
            //this.Act = act;
            //this.Decoder = Json.DecoderFor<SoundData>(config => config.DeriveFrom(result.Config));
            try
            {
                this.Responsitory.Fetch(WellKnownUrl.SoundInfoNew, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<SoundData>(this.GetDataCallBack(asyncResult), this.SoundDataResult);
                });
            }
            catch (Exception ex)
            {
                this.SoundDataResult.Act.BeginInvoke(new SoundData
                {
                    
                }, null, null);
            }
            //this.Responsitory.Fetch(WellKnownUrl.SoundInfoNew, param.ToString(), base.GetDataCallBack);
        }
        /// <summary>
        /// 多声音数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetMutiSounds<T>(Action<object> act, T param)
        {
            Result<MutiSoundResult> result = new Result<MutiSoundResult>();

            new MutiSoundResultDecorator<MutiSoundResult>(result);
            new SoundData6Decorator<MutiSoundResult>(result);

            this.MutiSoundResult = new ServiceParams<MutiSoundResult>(Json.DecoderFor<MutiSoundResult>(config => config.DeriveFrom(result.Config)),act);
            //this.MutiSoundResultAct = act;
            //this.MutiSoundResultDecoder = Json.DecoderFor<MutiSoundResult>(config => config.DeriveFrom(result.Config));
            try
            {
                this.Responsitory.Fetch(WellKnownUrl.MutiData, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<MutiSoundResult>(this.GetDataCallBack(asyncResult), this.MutiSoundResult);
                });
            }
            catch (Exception ex)
            {
                this.MutiSoundResult.Act.BeginInvoke(new MutiUserResult
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
        }

        #endregion
    }
}
