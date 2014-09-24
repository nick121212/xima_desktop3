using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Core.Models.User;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IUserDetailService))]
    public class UserDetailService : ServiceBase<UserData>, IUserDetailService
    {
        /// <summary>
        /// 
        /// </summary>
        [Import]
        protected IUserResultResponsitory Responsitory { get; set; }

        private JsonDecoder<AlbumInfoResult1> AlbumsDecoder { get; set; }
        /// <summary>
        /// 数据返回后的回调
        /// </summary>
        protected Action<object> AlbumsAct { get; set; }

        /// <summary>
        /// 用户详情数据
        /// </summary>
        public void GetData<T>(Action<object> act, T param)
        {
            Result<UserData> result = new Result<UserData>();

            new UserData3Decorator<UserData>(result);

            this.Act = act;
            this.Decoder = Json.DecoderFor<UserData>(config => config.DeriveFrom(result.Config));
            this.Responsitory.Fetch(WellKnownUrl.UserDetailInfo, param.ToString(), base.GetDataCallBack);
        }
        /// <summary>
        /// 用户专辑列表数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetAlbumsData<T>(Action<object> act, T param)
        {
            Result<AlbumInfoResult1> result = new Result<AlbumInfoResult1>();

            new AlbumInfoResult1Decorator<AlbumInfoResult1>(result);
            new AlbumData3Decorator<AlbumInfoResult1>(result);

            this.AlbumsAct = act;
            this.AlbumsDecoder = Json.DecoderFor<AlbumInfoResult1>(config => config.DeriveFrom(result.Config));
            this.Responsitory.Fetch(WellKnownUrl.UserAlbums, param.ToString(), this.GetAlbumsDataCallBack);
        }

        private void GetAlbumsDataCallBack(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            HttpWebResponse response = request.EndGetResponse(result) as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string responseString = reader.ReadToEnd();
            object fr = this.AlbumsDecoder.Decode(responseString);

            if (this.AlbumsAct != null)
            {
                this.AlbumsAct.BeginInvoke(fr, null, null);
            }

            response.Dispose();
            responseStream.Dispose();
            reader.Dispose();
            result = null;
            request = null;
            response = null;
            responseStream = null;
            reader = null;
        }
    }
}
