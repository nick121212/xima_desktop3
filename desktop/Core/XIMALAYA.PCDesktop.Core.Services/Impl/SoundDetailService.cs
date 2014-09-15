using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
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
    [Export(typeof(ISoundDetailService))]
    public class SoundDetailService : ServiceBase<SoundData>, ISoundDetailService
    {
        /// <summary>
        /// 
        /// </summary>
        [Import]
        protected ISoundsResultResponsitory Responsitory { get; set; }

        private JsonDecoder<LikedSoundUserResult> UserDecoder { get; set; }
        /// <summary>
        /// 数据返回后的回调
        /// </summary>
        protected Action<object> UserAct { get; set; }

        #region ISoundDetailService 成员

        /// <summary>
        /// 
        /// </summary>
        public void GetData<T>(Action<object> act, T param)
        {
            Result<SoundData> result = new Result<SoundData>();

            new SoundDetailResultDecorator<SoundData>(result);
            new UserData2Decorator<SoundData>(result);

            this.Act = act;
            this.Decoder = Json.DecoderFor<SoundData>(config => config.DeriveFrom(result.Config));
            this.Responsitory.Fetch(WellKnownUrl.SoundInfoNew, param.ToString(), base.GetDataCallBack);
        }

        public void GetLikedUsers<T>(Action<object> act, T param)
        {
            Result<LikedSoundUserResult> result = new Result<LikedSoundUserResult>();

            new LikedSoundUserResultDecorator<LikedSoundUserResult>(result);
            new UserData4Decorator<LikedSoundUserResult>(result);

            this.UserAct = act;
            this.UserDecoder = Json.DecoderFor<LikedSoundUserResult>(config => config.DeriveFrom(result.Config));
            this.Responsitory.Fetch(WellKnownUrl.LikedSoundUsers, param.ToString(), this.GetUserDataCallBack);
        }

        protected void GetUserDataCallBack(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            HttpWebResponse response = request.EndGetResponse(result) as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string responseString = reader.ReadToEnd();
            object fr = this.UserDecoder.Decode(responseString);

            if (this.UserAct != null)
            {
                this.UserAct.BeginInvoke(fr, null, null);
            }

            response.Dispose();
            responseStream.Dispose();
            reader.Dispose();
            result = null;
            request = null;
            response = null;
            responseStream = null;
            reader = null;
        }

        #endregion
    }
}
