using System;
using System.ComponentModel.Composition;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.Share;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Tools;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IShareService))]
    public class ShareService : ServiceBase<ShareResult>, IShareService
    {
        /// <summary>
        /// 
        /// </summary>
        [Import]
        protected IShareResultResponsitory Responsitory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void GetData<T>(Action<object> act, T param, TagType tagType)
        {
            Result<ShareResult> result = new Result<ShareResult>();

            new ShareResultDecorator<ShareResult>(result);

            this.Act = act;
            this.Decoder = Json.DecoderFor<ShareResult>(config => config.DeriveFrom(result.Config));

            if (tagType == TagType.sound)
            {
                this.Responsitory.Fetch(WellKnownUrl.ShareSoundInfo, param.ToString(), base.GetDataCallBack);
            }
            else
            {
                this.Responsitory.Fetch(WellKnownUrl.ShareAlbumInfo, param.ToString(), base.GetDataCallBack);
            }

        }
    }
}
