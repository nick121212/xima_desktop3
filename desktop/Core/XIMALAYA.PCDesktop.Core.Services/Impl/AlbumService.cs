using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Core.Models.Tags;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Core.Services.Impl
{
    /// <summary>
    /// 专辑相关服务
    /// </summary>
    [Export(typeof(IAlbumService))]
    class AlbumService : ServiceBase<AlbumInfoResult>, IAlbumService
    {
        #region 属性

        /// <summary>
        /// json格式转换类
        /// </summary>
        protected JsonDecoder<TagAlbumsResult> TagAlbumsResultDecoder { get; set; }
        /// <summary>
        /// 标签下的专辑
        /// </summary>
        protected Action<object> TagAlbumsResultAct { get; set; }
        /// <summary>
        /// 他人的专辑decoder
        /// </summary>
        private JsonDecoder<AlbumInfoResult1> AlbumInfoResult1Decoder { get; set; }
        /// <summary>
        /// 他人的专辑回调
        /// </summary>
        protected Action<object> AlbumInfoResult1Act { get; set; }

        #endregion

        #region IAlbumService 成员

        /// <summary>
        /// 获取专辑详情页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetDetailData<T>(Action<object> act, T param)
        {
            Result<AlbumInfoResult> result = new Result<AlbumInfoResult>();

            new AlbumInfoResultDecorator<AlbumInfoResult>(result);
            new SoundsResultDecorator<AlbumInfoResult>(result);
            new AlbumData3Decorator<AlbumInfoResult>(result);
            new SoundData2Decorator<AlbumInfoResult>(result);
            this.Act = act;
            this.Decoder = Json.DecoderFor<AlbumInfoResult>(config => config.DeriveFrom(result.Config));

            try
            {
                this.Responsitory.Fetch(WellKnownUrl.AlbumInfo, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<AlbumInfoResult>(this.GetDataCallBack(asyncResult), this.Decoder, this.Act);
                });
            }
            catch (Exception ex)
            {
                this.Act.BeginInvoke(new AlbumInfoResult
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
        }

        /// <summary>
        /// 标签下的专辑
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetTagAlbums<T>(Action<object> act, T param)
        {
            Result<TagAlbumsResult> result = new Result<TagAlbumsResult>();

            new TagAlbumsResultDecorator<TagAlbumsResult>(result);
            new AlbumData1Decorator<TagAlbumsResult>(result);

            this.TagAlbumsResultAct = act;
            this.TagAlbumsResultDecoder = Json.DecoderFor<TagAlbumsResult>(config => config.DeriveFrom(result.Config));

            try
            {
                this.Responsitory.Fetch(WellKnownUrl.CategoryTagAlbums, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<TagAlbumsResult>(this.GetDataCallBack(asyncResult), this.TagAlbumsResultDecoder, this.TagAlbumsResultAct);
                });
            }
            catch (Exception ex)
            {
                this.Act.BeginInvoke(new TagAlbumsResult
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
        }
        /// <summary>
        /// 获取他人的专辑列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetUserAlbums<T>(Action<object> act, T param)
        {
            Result<AlbumInfoResult1> result = new Result<AlbumInfoResult1>();

            new AlbumInfoResult1Decorator<AlbumInfoResult1>(result);
            new AlbumData3Decorator<AlbumInfoResult1>(result);

            this.AlbumInfoResult1Act = act;
            this.AlbumInfoResult1Decoder = Json.DecoderFor<AlbumInfoResult1>(config => config.DeriveFrom(result.Config));

            try
            {
                this.Responsitory.Fetch(WellKnownUrl.UserAlbums, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<AlbumInfoResult1>(this.GetDataCallBack(asyncResult), this.AlbumInfoResult1Decoder, this.AlbumInfoResult1Act);
                });
            }
            catch (Exception ex)
            {
                this.Act.BeginInvoke(new AlbumInfoResult1
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
            //this.Responsitory.Fetch(WellKnownUrl.UserAlbums, param.ToString(), this.GetAlbumsDataCallBack);
        }

        #endregion
    }
}
