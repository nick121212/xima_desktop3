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
        /// 
        [Import]
        protected ISuperExploreIndexResultResponsitory Responsitory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void GetData<T>(Action<object> act,T param)
        {
            Result<SuperExploreIndexResult> result = new Result<SuperExploreIndexResult>();

            new SuperExploreIndexResultDecorator<SuperExploreIndexResult>(result);
            //分类
            new CategoryResultDecorator<SuperExploreIndexResult>(result);
            new CategoryDataDecorator<SuperExploreIndexResult>(result);
            //焦点图
            new FocusImageResultDecorator<SuperExploreIndexResult>(result);
            new FocusImageDataDecorator<SuperExploreIndexResult>(result);
            //推荐用户
            //new UserDataDecorator<SuperExploreIndexResult>(result);
            //推荐专辑
            new AlbumInfoResult1Decorator<SuperExploreIndexResult>(result);
            new AlbumDataDecorator<SuperExploreIndexResult>(result);
            //专题列表
            //new SubjectListResultDecorator<SuperExploreIndexResult>(result);
            //new SubjectDataDecorator<SuperExploreIndexResult>(result);

            this.Act = act;
            this.Decoder = Json.DecoderFor<SuperExploreIndexResult>(config => config.DeriveFrom(result.Config));

            this.Responsitory.Fetch(WellKnownUrl.SuperExploreIndex, param.ToString(), GetDataCallBack);

        }
    }
}
