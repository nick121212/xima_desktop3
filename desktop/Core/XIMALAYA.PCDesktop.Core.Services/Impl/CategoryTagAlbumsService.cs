using System;
using System.ComponentModel.Composition;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.Tags;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 分类下的标签下的专辑
    /// </summary>
    [Export(typeof(ICategoryTagAlbumsService))]
    public class CategoryTagAlbumsService : ServiceBase<TagAlbumsResult>, ICategoryTagAlbumsService
    {
        /// <summary>
        /// 
        /// </summary>
        [Import]
        protected ITagAlbumsResultResponsitory Responsitory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void GetData<T>(Action<object> act, T param)
        {
            Result<TagAlbumsResult> result = new Result<TagAlbumsResult>();

            new TagAlbumsResultDecorator<TagAlbumsResult>(result);
            new AlbumData1Decorator<TagAlbumsResult>(result);

            this.Act = act;
            this.Decoder = Json.DecoderFor<TagAlbumsResult>(config => config.DeriveFrom(result.Config));

            this.Responsitory.Fetch(WellKnownUrl.CategoryTagAlbums, param.ToString(), GetDataCallBack);
        }
    }
}
