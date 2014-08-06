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
using XIMALAYA.PCDesktop.Core.Models.Subject;
using XIMALAYA.PCDesktop.Core.ParamsModel;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(ISubjectsService))]
    public class SubjectsService : ServiceBase<SubjectListResult>, ISubjectsService
    {
        /// <summary>
        /// 
        /// </summary>
        [Import]
        protected ISubjectListResultResponsitory Responsitory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void GetData<T>(Action<object> act, T param)
        {
            Result<SubjectListResult> result = new Result<SubjectListResult>();

            new SubjectListResultDecorator<SubjectListResult>(result);
            new SubjectDataDecorator<SubjectListResult>(result);

            this.Act = act;
            this.Decoder = Json.DecoderFor<SubjectListResult>(config => config.DeriveFrom(result.Config));

            this.Responsitory.Fetch(WellKnownUrl.SubjectList, param.ToString(), GetDataCallBack);
        }
    }
}
