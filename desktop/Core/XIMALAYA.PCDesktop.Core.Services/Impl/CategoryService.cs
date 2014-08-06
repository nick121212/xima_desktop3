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
using XIMALAYA.PCDesktop.Core.ParamsModel;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(ICategoryService))]
    public class CategoryService : ServiceBase<CategoryResult>, ICategoryService
    {
        /// <summary>
        /// 
        /// </summary>
        [Import]
        protected ICategoryResultResponsitory Responsitory { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public void GetData<T>(Action<object> act,T param)
        {
            Result<CategoryResult> result = new Result<CategoryResult>();

            new CategoryResultDecorator<CategoryResult>(result);
            new CategoryDataDecorator<CategoryResult>(result);

            this.Act = act;
            this.Decoder = Json.DecoderFor<CategoryResult>(config => config.DeriveFrom(result.Config));

            this.Responsitory.Fetch(WellKnownUrl.CategoryList, param.ToString(),  GetDataCallBack);
        }
    }
}
