using System;
using System.ComponentModel.Composition;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.Discover;
using XIMALAYA.PCDesktop.Tools;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(ISuperExploreIndexService))]
    public class SuperExploreIndexService : ServiceBase<SuperExploreIndexResult>, ISuperExploreIndexService
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
            new AlbumData1Decorator<SuperExploreIndexResult>(result);
            //专题列表
            //new SubjectListResultDecorator<SuperExploreIndexResult>(result);
            //new SubjectDataDecorator<SuperExploreIndexResult>(result);

            this.Act = act;
            this.Decoder = Json.DecoderFor<SuperExploreIndexResult>(config => config.DeriveFrom(result.Config));

            this.Responsitory.Fetch(WellKnownUrl.SuperExploreIndex, param.ToString(), GetDataCallBack);
        }
    }
}
