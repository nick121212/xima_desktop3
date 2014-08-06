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
using XIMALAYA.PCDesktop.Core.Models.Category;
using XIMALAYA.PCDesktop.Core.Models.Discover;
using XIMALAYA.PCDesktop.Core.Models.Recommend;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Models.Tags;

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
