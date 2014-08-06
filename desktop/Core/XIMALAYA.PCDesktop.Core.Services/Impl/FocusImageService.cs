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
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Core.Models.FocusImage;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IFocusImageService))]
    public class FocusImageService : ServiceBase<FocusImageResult>, IFocusImageService
    {
        /// <summary>
        /// 
        /// </summary>
        [Import]
        protected IFocusImageResultResponsitory Responsitory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void GetData<T>(Action<object> act,T param)
        {
            Result<FocusImageResult> result = new Result<FocusImageResult>();

            new FocusImageResultDecorator<FocusImageResult>(result);
            new FocusImageDataDecorator<FocusImageResult>(result);
            this.Act = act;
            this.Decoder = Json.DecoderFor<FocusImageResult>(config => config.DeriveFrom(result.Config));

            this.Responsitory.Fetch(WellKnownUrl.FocsImage, param.ToString(), base.GetDataCallBack);
        }
    }
}
