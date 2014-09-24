using System;
using System.ComponentModel.Composition;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.Share;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Untils;

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

            switch (tagType)
            {
                case TagType.sound:
                    this.Responsitory.Fetch(WellKnownUrl.ShareSoundInfo, param.ToString(), base.GetDataCallBack);
                    break;
                case TagType.album:
                    this.Responsitory.Fetch(WellKnownUrl.ShareAlbumInfo, param.ToString(), base.GetDataCallBack);
                    break;
                case TagType.user:
                    this.Responsitory.Fetch(WellKnownUrl.ShareUserInfo, param.ToString(), base.GetDataCallBack);
                    break;
            }
        }
    }
}
