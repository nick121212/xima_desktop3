using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.Search;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 搜索服务
    /// </summary>
    [Export(typeof(ISearchService))]
    public class SearchService : ServiceBase<SearchResult>, ISearchService
    {

        private JsonDecoder<SearchSoundResult> SearchSoundResultDecoder { get; set; }
        private JsonDecoder<SearchAlbumResult> SearchAlbumResultDecoder { get; set; }
        private JsonDecoder<SearchUserResult> SearchUserResultDecoder { get; set; }

        /// <summary>
        /// 搜索所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetAllData<T>(Action<object> act, T param)
        {
            Result<SearchResult> result = new Result<SearchResult>();

            new SearchResultDecorator<SearchResult>(result);
            new SearchAlbumResultDecorator<SearchResult>(result);
            new SearchSoundResultDecorator<SearchResult>(result);
            new SearchUserResultDecorator<SearchResult>(result);
            new SoundData4Decorator<SearchResult>(result);
            new AlbumData4Decorator<SearchResult>(result);
            new UserData1Decorator<SearchResult>(result);

            this.Act = act;
            this.Decoder = Json.DecoderFor<SearchResult>(config => config.DeriveFrom(result.Config));
            try
            {
                this.Responsitory.Fetch(WellKnownUrl.SearchPath, param.ToString(), asyncResult =>
                {
                    this.GetDecodeData<SearchResult>(this.GetDataCallBack(asyncResult), this.Decoder, this.Act);
                });
            }
            catch (Exception ex)
            {
                this.Act.BeginInvoke(new SearchSoundResult
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
            //this.Responsitory.Fetch(WellKnownUrl.SearchPath, param.ToString(), GetDataCallBackAll);
        }
        /// <summary>
        /// 搜索声音数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetSoundData<T>(Action<object> act, T param)
        {
            Result<SearchSoundResult> result = new Result<SearchSoundResult>();

            new SearchSoundResultDecorator<SearchSoundResult>(result);
            new SoundData4Decorator<SearchSoundResult>(result);

            this.Act = act;
            this.SearchSoundResultDecoder = Json.DecoderFor<SearchSoundResult>(config => config.DeriveFrom(result.Config));
            try
            {
                this.Responsitory.Fetch(WellKnownUrl.SearchPath, param.ToString(), asyncResult =>
                {
                    string responseString = this.GetDataCallBack(asyncResult);
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    Dictionary<string, object> dic = js.Deserialize<Dictionary<string, object>>(responseString);

                    if (dic.ContainsKey("response"))
                    {
                        responseString = js.Serialize(dic["response"]);
                        this.GetDecodeData<SearchSoundResult>(responseString, this.SearchSoundResultDecoder, this.Act);
                    }
                    //this.GetDecodeData<SearchSoundResult>(this.GetDataCallBack1(asyncResult), this.SearchSoundResultDecoder, this.Act);
                });
            }
            catch (Exception ex)
            {
                this.Act.BeginInvoke(new SearchSoundResult
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
            //this.Responsitory.Fetch(WellKnownUrl.SearchPath, param.ToString(), SoundDataCallBack);
        }
        /// <summary>
        /// 搜索专辑数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetAlbumData<T>(Action<object> act, T param)
        {
            Result<SearchAlbumResult> result = new Result<SearchAlbumResult>();

            new SearchAlbumResultDecorator<SearchAlbumResult>(result);
            new AlbumData4Decorator<SearchAlbumResult>(result);

            this.Act = act;
            this.SearchAlbumResultDecoder = Json.DecoderFor<SearchAlbumResult>(config => config.DeriveFrom(result.Config));
            try
            {
                this.Responsitory.Fetch(WellKnownUrl.SearchPath, param.ToString(), asyncResult =>
                {
                    string responseString = this.GetDataCallBack(asyncResult);
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    Dictionary<string, object> dic = js.Deserialize<Dictionary<string, object>>(responseString);

                    if (dic.ContainsKey("response"))
                    {
                        responseString = js.Serialize(dic["response"]);
                        this.GetDecodeData<SearchAlbumResult>(responseString, this.SearchAlbumResultDecoder, this.Act);
                    }
                    //this.GetDecodeData<SearchSoundResult>(this.GetDataCallBack1(asyncResult), this.SearchSoundResultDecoder, this.Act);
                });
            }
            catch (Exception ex)
            {
                this.Act.BeginInvoke(new SearchAlbumResult
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
            //this.Responsitory.Fetch(WellKnownUrl.SearchPath, param.ToString(), AlbumDataCallBack);
        }
        /// <summary>
        /// 搜索用户数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="param"></param>
        public void GetUserData<T>(Action<object> act, T param)
        {
            Result<SearchUserResult> result = new Result<SearchUserResult>();

            new SearchUserResultDecorator<SearchUserResult>(result);
            new UserData1Decorator<SearchUserResult>(result);

            this.Act = act;
            this.SearchUserResultDecoder = Json.DecoderFor<SearchUserResult>(config => config.DeriveFrom(result.Config));
            try
            {
                this.Responsitory.Fetch(WellKnownUrl.SearchPath, param.ToString(), asyncResult =>
                {
                    string responseString = this.GetDataCallBack(asyncResult);
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    Dictionary<string, object> dic = js.Deserialize<Dictionary<string, object>>(responseString);

                    if (dic.ContainsKey("response"))
                    {
                        responseString = js.Serialize(dic["response"]);
                        this.GetDecodeData<SearchUserResult>(responseString, this.SearchUserResultDecoder, this.Act);
                    }
                    //this.GetDecodeData<SearchSoundResult>(this.GetDataCallBack1(asyncResult), this.SearchSoundResultDecoder, this.Act);
                });
            }
            catch (Exception ex)
            {
                this.Act.BeginInvoke(new SearchUserResult
                {
                    Ret = 500,
                    Message = ex.Message
                }, null, null);
            }
            //this.Responsitory.Fetch(WellKnownUrl.SearchPath, param.ToString(), UserDataCallBack);
        }
    }
}
