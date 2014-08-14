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

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 搜索服务
    /// </summary>
    [Export(typeof(ISearchService))]
    public class SearchService : ServiceBase<SearchResult>, ISearchService
    {
        [Import]
        protected ISearchResultResponsitory Responsitory { get; set; }

        private JsonDecoder<SearchSoundResult> SoundDecoder { get; set; }
        private JsonDecoder<SearchAlbumResult> AlbumDecoder { get; set; }
        private JsonDecoder<SearchUserResult> UserDecoder { get; set; }

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
            this.Responsitory.Fetch(WellKnownUrl.SearchPath, param.ToString(), GetDataCallBackAll);
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
            this.SoundDecoder = Json.DecoderFor<SearchSoundResult>(config => config.DeriveFrom(result.Config));
            this.Responsitory.Fetch(WellKnownUrl.SearchPath, param.ToString(), SoundDataCallBack);
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
            this.AlbumDecoder = Json.DecoderFor<SearchAlbumResult>(config => config.DeriveFrom(result.Config));
            this.Responsitory.Fetch(WellKnownUrl.SearchPath, param.ToString(), AlbumDataCallBack);
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
            this.UserDecoder = Json.DecoderFor<SearchUserResult>(config => config.DeriveFrom(result.Config));
            this.Responsitory.Fetch(WellKnownUrl.SearchPath, param.ToString(), UserDataCallBack);
        }

        private void GetDataCallBackAll(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            HttpWebResponse response = request.EndGetResponse(result) as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string responseString = reader.ReadToEnd();

            object fr = this.Decoder.Decode(responseString);
            if (this.Act != null)
            {
                this.Act.BeginInvoke(fr, null, null);
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
        private void AlbumDataCallBack(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            HttpWebResponse response = request.EndGetResponse(result) as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string responseString = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<string, object> dic = js.Deserialize<Dictionary<string, object>>(responseString);

            if (dic.ContainsKey("response"))
            {
                responseString = js.Serialize(dic["response"]);
                object fr = this.AlbumDecoder.Decode(responseString);
                if (this.Act != null)
                {
                    this.Act.BeginInvoke(fr, null, null);
                }
            }
            js = null;
            dic = null;
            response.Dispose();
            responseStream.Dispose();
            reader.Dispose();
            result = null;
            request = null;
            response = null;
            responseStream = null;
            reader = null;
        }
        private void SoundDataCallBack(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            HttpWebResponse response = request.EndGetResponse(result) as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string responseString = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<string, object> dic = js.Deserialize<Dictionary<string, object>>(responseString);

            if (dic.ContainsKey("response"))
            {
                responseString = js.Serialize(dic["response"]);
                object fr = this.SoundDecoder.Decode(responseString);
                if (this.Act != null)
                {
                    this.Act.BeginInvoke(fr, null, null);
                }
            }
            js = null;
            dic = null;
            response.Dispose();
            responseStream.Dispose();
            reader.Dispose();
            result = null;
            request = null;
            response = null;
            responseStream = null;
            reader = null;
        }
        private void UserDataCallBack(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            HttpWebResponse response = request.EndGetResponse(result) as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string responseString = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<string, object> dic = js.Deserialize<Dictionary<string, object>>(responseString);

            if (dic.ContainsKey("response"))
            {
                responseString = js.Serialize(dic["response"]);
                object fr = this.UserDecoder.Decode(responseString);
                if (this.Act != null)
                {
                    this.Act.BeginInvoke(fr, null, null);
                }
            }
            js = null;
            dic = null;
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
