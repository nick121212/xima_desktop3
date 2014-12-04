using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Core.Models.Share;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Untils;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Modules.Share
{
    [ModuleExport(WellKnownModuleNames.ShareModule, typeof(ShareModule))]
    class ShareModule : BaseModule
    {
        private ShareEvent<ShareEventArgument> ShareEvent { get; set; }
        /// <summary>
        /// 分享服务
        /// </summary>
        [Import]
        private IShareService ShareService { get; set; }

        public override void Initialize()
        {
            this.ShareEvent = this.EventAggregator.GetEvent<ShareEvent<ShareEventArgument>>();
            this.ShareEvent.Subscribe(this.OnShareEvent);
        }

        private void OnShareEvent(ShareEventArgument args)
        {
            switch (args.ShareType)
            {
                case ShareType.Album:
                    this.GetShareAlbumLink(args.Site, args.ID);
                    break;
                case ShareType.Track:
                    this.GetShareSoundLink(args.Site, args.ID);
                    break;
                case ShareType.User:
                    this.GetShareUserLink(args.Site, args.ID);
                    break;
            }
        }

        public override void Dispose()
        {
            this.ShareEvent.Unsubscribe(this.OnShareEvent);
            this.ShareEvent = null;
            base.Dispose();
        }

        /// <summary>
        /// 分享用户
        /// </summary>
        /// <param name="Site"></param>
        /// <param name="uid"></param>
        private void GetShareUserLink(Sites Site, long uid)
        {
            if (this.ShareService == null) return;
            if (uid <= 0) return;

            this.ShareService.GetData(res =>
            {
                var result = res as ShareResult;

                if (result.Ret == 0)
                {
                    this.DoShare(Site, result.PicUrl, result.Content, result.Url);
                }
            }, new ShareParam
            {
                ShareUid = uid,
                tpName = "qq"
            }, TagType.user);
        }
        /// <summary>
        /// 分享专辑
        /// </summary>
        /// <param name="Site"></param>
        /// <param name="AlbumID"></param>
        private void GetShareAlbumLink(Sites Site, long AlbumID)
        {
            if (this.ShareService == null) return;
            if (AlbumID <= 0) return;

            this.ShareService.GetData(res =>
            {
                var result = res as ShareResult;

                if (result.Ret == 0)
                {
                    this.DoShare(Site, result.PicUrl, result.Content, result.Url);
                }
            }, new ShareParam
            {
                AlbumId = AlbumID,
                tpName = "qq"
            }, TagType.album);

        }
        /// <summary>
        /// 分享声音
        /// </summary>
        /// <param name="Site"></param>
        /// <param name="SoundID"></param>
        private void GetShareSoundLink(Sites Site, long SoundID)
        {
            if (this.ShareService == null) return;
            if (SoundID <= 0) return;

            this.ShareService.GetData(res =>
            {
                var result = res as ShareResult;

                if (result.Ret == 0)
                {
                    this.DoShare(Site, result.PicUrl, result.Content, result.Url);
                }
            }, new ShareParam
            {
                TrackId = SoundID,
                tpName = "qq"
            }, TagType.sound);
        }
        /// <summary>
        /// 跳转分享链接
        /// </summary>
        /// <param name="Site"></param>
        /// <param name="PicUrl"></param>
        /// <param name="Content"></param>
        /// <param name="url"></param>
        private void DoShare(Sites Site, string PicUrl, string Content, string url)
        {
            Parameters parameters = new Parameters(true);

            switch (Site)
            {
                case Sites.Douban:
                    parameters["name"] = Content;
                    parameters["href"] = url;
                    parameters["image"] = PicUrl;
                    parameters["text"] = string.Empty;
                    parameters["desc"] = string.Empty;
                    parameters["apikey"] = "0c2e1df44f97c4eb248a59dceec74ec1";
                    url = string.Format("http://shuo.douban.com/!service/share?{0}", parameters);
                    break;
                case Sites.Weibo:
                    parameters["appkey"] = "1075899032";
                    parameters["url"] = url;
                    parameters["title"] = Content;
                    parameters["content"] = "utf-8";
                    parameters["pic"] = PicUrl;
                    url = string.Format("http://service.t.sina.com.cn/share/share.php?{0}", parameters);
                    break;
                case Sites.Kaixin:
                    parameters["rurl"] = url;
                    parameters["rcontent"] = "";
                    parameters["rtitle"] = Content;
                    url = string.Format("http://www.kaixin001.com/repaste/bshare.php?{0}", parameters);
                    break;
                case Sites.Renren:
                    parameters["resourceUrl"] = url;
                    parameters["title"] = Content;
                    parameters["pic"] = PicUrl;
                    parameters["description"] = string.Empty;
                    parameters["charset"] = "utf-8";
                    url = string.Format("http://widget.renren.com/dialog/share?{0}", parameters);
                    break;
                case Sites.TencentWeibo:
                    parameters["url"] = url;
                    parameters["title"] = Content;
                    parameters["site"] = "http://www.kfstorm.com/doubanfm";
                    parameters["pic"] = PicUrl;
                    parameters["appkey"] = "801098586";
                    url = string.Format("http://v.t.qq.com/share/share.php?{0}", parameters);
                    break;
                case Sites.Facebook:
                    parameters["u"] = url;
                    parameters["t"] = Content;
                    url = string.Format("http://www.facebook.com/sharer.php?{0}", parameters);
                    break;
                case Sites.Twitter:
                    parameters["status"] = Content + " " + url;
                    url = string.Format("http://twitter.com/home?{0}", parameters);
                    break;
                case Sites.Qzone:
                    parameters["url"] = url;
                    parameters["title"] = Content;
                    url = string.Format("http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_onekey?{0}", parameters);
                    break;
                default:
                    break;
            }

            System.Diagnostics.Process.Start(url);
        }
    }
}
