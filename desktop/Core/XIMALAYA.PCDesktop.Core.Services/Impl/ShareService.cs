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
        public void GetData<T>(Action<object> act, T param, TagType tagType)
        {
            Result<ShareResult> result = new Result<ShareResult>();
            string url = string.Empty;
            new ShareResultDecorator<ShareResult>(result);

            this.Act = act;
            this.Decoder = Json.DecoderFor<ShareResult>(config => config.DeriveFrom(result.Config));

            switch (tagType)
            {
                case TagType.sound:
                    url = WellKnownUrl.ShareSoundInfo;
                    //this.Responsitory.Fetch(WellKnownUrl.ShareSoundInfo, param.ToString(), base.GetDataCallBack);
                    break;
                case TagType.album:
                    url = WellKnownUrl.ShareAlbumInfo;
                    //this.Responsitory.Fetch(WellKnownUrl.ShareAlbumInfo, param.ToString(), base.GetDataCallBack);
                    break;
                case TagType.user:
                    url = WellKnownUrl.ShareUserInfo;
                    //this.Responsitory.Fetch(WellKnownUrl.ShareUserInfo, param.ToString(), base.GetDataCallBack);
                    break;
            }

            try
            {
                this.Responsitory.Fetch(url, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<ShareResult>(this.GetDataCallBack(asyncResult), this.Decoder, this.Act);
                });
            }
            catch (Exception ex)
            {
                this.Act.BeginInvoke(new ShareResult
                {
                    Ret = 0,
                    Message = ex.Message
                }, null, null);
            }

        }
    }
}
