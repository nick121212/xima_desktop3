using System;
using System.ComponentModel.Composition;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IAlbumDetailService))]
    public class AlbumDetailService : ServiceBase<AlbumInfoResult>, IAlbumDetailService
    {
        /// <summary>
        /// 
        /// </summary>
        [Import]
        protected IAlbumInfoResultResponsitory Responsitory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void GetData<T>(Action<object> act,T param)
        {
            Result<AlbumInfoResult> result = new Result<AlbumInfoResult>();

            new AlbumInfoResultDecorator<AlbumInfoResult>(result);
            new SoundsResultDecorator<AlbumInfoResult>(result);
            new AlbumData3Decorator<AlbumInfoResult>(result);
            new SoundData2Decorator<AlbumInfoResult>(result);
            this.Act = act;
            this.Decoder = Json.DecoderFor<AlbumInfoResult>(config => config.DeriveFrom(result.Config));

            this.Responsitory.Fetch(WellKnownUrl.AlbumInfo, param.ToString(), base.GetDataCallBack);
        }
    }
}
