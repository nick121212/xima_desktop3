using System;
using System.ComponentModel.Composition;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Tools;

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
        protected ISoundResultResponsitory Responsitory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void GetData<T>(Action<object> act, long trackId)
        {
            Result<SoundData> result = new Result<SoundData>();

            new SoundData5Decorator<SoundData>(result);

            this.Act = act;
            this.Decoder = Json.DecoderFor<SoundData>(config => config.DeriveFrom(result.Config));
            this.Responsitory.Fetch(string.Format(WellKnownUrl.SoundInfo, trackId), string.Empty, base.GetDataCallBack);
        }
    }
}
