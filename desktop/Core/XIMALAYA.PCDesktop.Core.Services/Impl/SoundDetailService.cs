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
        protected ISoundsResultResponsitory Responsitory { get; set; }
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
    }
}
