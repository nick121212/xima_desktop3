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
using XIMALAYA.PCDesktop.Core.Models.MutiData;
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

        private ServiceParams<TagAlbumsResult> TagAlbumsResult { get; set; }
        private ServiceParams<AlbumInfoResult> AlbumInfoResult { get; set; }
        private ServiceParams<AlbumInfoResult1> AlbumInfoResult1 { get; set; }
        private ServiceParams<MutiAlbumResult> MutiAlbumResult { get; set; }

        ///// <summary>
        ///// json格式转换类
        ///// </summary>
        //private JsonDecoder<TagAlbumsResult> TagAlbumsResultDecoder { get; set; }
        ///// <summary>
        ///// 标签下的专辑
        ///// </summary>
        //private Action<object> TagAlbumsResultAct { get; set; }
        ///// <summary>
        ///// 他人的专辑decoder
        ///// </summary>
        //private JsonDecoder<AlbumInfoResult1> AlbumInfoResult1Decoder { get; set; }
        ///// <summary>
        ///// 他人的专辑回调
        ///// </summary>
        //private Action<object> AlbumInfoResult1Act { get; set; }
        ///// <summary>
        ///// 多专辑decoder
        ///// </summary>
        //private JsonDecoder<MutiAlbumResult> MutiAlbumDecoder { get; set; }
        ///// <summary>
        ///// 多专辑回调
        ///// </summary>
        //private Action<object> MutiAlbumAct { get; set; }

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

            this.AlbumInfoResult = new ServiceParams<AlbumInfoResult>(Json.DecoderFor<AlbumInfoResult>(config => config.DeriveFrom(result.Config)), act);
            //this.Act = act;
            //this.Decoder = Json.DecoderFor<AlbumInfoResult>(config => config.DeriveFrom(result.Config));

            try
            {
                this.Responsitory.Fetch(WellKnownUrl.AlbumInfo, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<AlbumInfoResult>(this.GetDataCallBack(asyncResult), this.AlbumInfoResult);
                });
            }
            catch (Exception ex)
            {
                this.AlbumInfoResult.Act.BeginInvoke(new AlbumInfoResult
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

            this.TagAlbumsResult = new ServiceParams<TagAlbumsResult>(Json.DecoderFor<TagAlbumsResult>(config => config.DeriveFrom(result.Config)), act);

            try
            {
                this.Responsitory.Fetch(WellKnownUrl.CategoryTagAlbums, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<TagAlbumsResult>(this.GetDataCallBack(asyncResult), this.TagAlbumsResult);
                });
            }
            catch (Exception ex)
            {
                this.TagAlbumsResult.Act.BeginInvoke(new TagAlbumsResult
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
            this.TagAlbumsResult.Dispose();
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

            this.AlbumInfoResult1 = new ServiceParams<AlbumInfoResult1>(Json.DecoderFor<AlbumInfoResult1>(config => config.DeriveFrom(result.Config)), act);

            //this.AlbumInfoResult1Act = act;
            //this.AlbumInfoResult1Decoder = Json.DecoderFor<AlbumInfoResult1>(config => config.DeriveFrom(result.Config));

            try
            {
                this.Responsitory.Fetch(WellKnownUrl.UserAlbums, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<AlbumInfoResult1>(this.GetDataCallBack(asyncResult), this.AlbumInfoResult1);
                });
            }
            catch (Exception ex)
            {
                this.AlbumInfoResult1.Act.BeginInvoke(new AlbumInfoResult1
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
            //this.Responsitory.Fetch(WellKnownUrl.UserAlbums, param.ToString(), this.GetAlbumsDataCallBack);
        }
        /// <summary>
        /// 多专辑
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetMutiAlbums<T>(Action<object> act, T param)
        {
            Result<MutiAlbumResult> result = new Result<MutiAlbumResult>();

            new MutiAlbumResultDecorator<MutiAlbumResult>(result);
            new AlbumData5Decorator<MutiAlbumResult>(result);

            this.MutiAlbumResult = new ServiceParams<MutiAlbumResult>(Json.DecoderFor<MutiAlbumResult>(config => config.DeriveFrom(result.Config)), act);

            //this.MutiAlbumAct = act;
            //this.MutiAlbumDecoder = Json.DecoderFor<MutiAlbumResult>(config => config.DeriveFrom(result.Config));
            try
            {
                this.Responsitory.Fetch(WellKnownUrl.MutiData, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<MutiAlbumResult>(this.GetDataCallBack(asyncResult), this.MutiAlbumResult);
                });
            }
            catch (Exception ex)
            {
                this.MutiAlbumResult.Act.BeginInvoke(new MutiAlbumResult
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
        }

        #endregion
    }
}
