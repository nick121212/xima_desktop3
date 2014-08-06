using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Net;
using System.IO;
using System.Diagnostics;

using FluentJson.Configuration;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;
using XIMALAYA.PCDesktop.Core.Models;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Core.Models.Discover;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IHotSoundsService))]
    public class HotSoundsService : ServiceBase<HotSoundsResult>, IHotSoundsService
    {
        /// <summary>
        /// 
        /// </summary>
        [Import]
        protected IHotSoundsResultResponsitory Responsitory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void GetData<T>(Action<object> act,T param)
        {
            Result<HotSoundsResult> result = new Result<HotSoundsResult>();

            new HotSoundsResultDecorator<HotSoundsResult>(result);
            new CategoryDataDecorator<HotSoundsResult>(result);
            new SoundData3Decorator<HotSoundsResult>(result);

            this.Act = act;
            this.Decoder = Json.DecoderFor<HotSoundsResult>(config => config.DeriveFrom(result.Config));

            this.Responsitory.Fetch(WellKnownUrl.HotSounds, param.ToString(), base.GetDataCallBack);
        }
    }
}
