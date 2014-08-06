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

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(ISuperExploreIndex))]
    public class SuperExploreIndexService : ServiceBase<SuperExploreIndexResult>, ISuperExploreIndex
    {
        /// <summary>
        /// 
        /// </summary>
        [Import]
        protected ISubjectListResultResponsitory Responsitory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void GetData<T>(Action<object> act,T param)
        {
            Result<SuperExploreIndexResult> result = new Result<SuperExploreIndexResult>();

            new SuperExploreIndexResultDecorator<SuperExploreIndexResult>(result);

            new CategoryResultDecorator<SuperExploreIndexResult>(result);
            new CategoryDiscoverDataDecorator<SuperExploreIndexResult>(result);

            new FocusImageResultDecorator<SuperExploreIndexResult>(result);
            new FocusImageDataDecorator<SuperExploreIndexResult>(result);

            new UserDataDecorator<SuperExploreIndexResult>(result);

            new RecommendAlbumResultDecorator<SuperExploreIndexResult>(result);
            new AlbumDataDecorator<SuperExploreIndexResult>(result);

            new SubjectListResultDecorator<SuperExploreIndexResult>(result);
            new SubjectDataDecorator<SuperExploreIndexResult>(result);

            this.Act = act;
            this.Decoder = Json.DecoderFor<SuperExploreIndexResult>(config => config.DeriveFrom(result.Config));

            this.Responsitory.Fetch(WellKnownUrl.SuperExploreIndex, param.ToString(), GetDataCallBack);
        }
    }
}
