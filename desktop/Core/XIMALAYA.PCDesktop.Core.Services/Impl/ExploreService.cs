using System;
using System.ComponentModel.Composition;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.Discover;
using XIMALAYA.PCDesktop.Core.Models.Tags;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 发现页接口数据
    /// </summary>
    [Export(typeof(IExploreService))]
    class ExploreService : ServiceBase<SuperExploreIndexResult>, IExploreService
    {
        #region 属性

        /// <summary>
        /// 标签下的专辑decoder
        /// </summary>
        protected JsonDecoder<CategoryTagResult> CategoryTagResultDecoder { get; set; }
        /// <summary>
        /// 标签下的专辑action
        /// </summary>
        protected Action<object> CategoryTagResultAct { get; set; }

        #endregion

        #region IExploreService 成员

        /// <summary>
        /// 获取发现首页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetData<T>(Action<object> act, T param)
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

            try
            {
                this.Responsitory.Fetch(WellKnownUrl.SuperExploreIndex, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<SuperExploreIndexResult>(this.GetDataCallBack(asyncResult), this.Decoder, this.Act);
                });
            }
            catch (Exception ex)
            {
                this.Act.BeginInvoke(new SuperExploreIndexResult
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
        }
        /// <summary>
        /// 获取分类下的标签数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetTagsData<T>(Action<object> act, T param)
        {
            Result<CategoryTagResult> result = new Result<CategoryTagResult>();

            new CategoryTagResultDecorator<CategoryTagResult>(result);
            new TagDataDecorator<CategoryTagResult>(result);

            this.CategoryTagResultAct = act;
            this.CategoryTagResultDecoder = Json.DecoderFor<CategoryTagResult>(config => config.DeriveFrom(result.Config));

            try
            {
                this.Responsitory.Fetch(WellKnownUrl.CategoryTags, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<CategoryTagResult>(this.GetDataCallBack(asyncResult), this.CategoryTagResultDecoder, this.CategoryTagResultAct);
                });
            }
            catch (Exception ex)
            {
                this.Act.BeginInvoke(new CategoryTagResult
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }

            //this.Responsitory.Fetch(WellKnownUrl.CategoryTags, param.ToString(), GetDataCallBack);
        }

        #endregion
    }
}
