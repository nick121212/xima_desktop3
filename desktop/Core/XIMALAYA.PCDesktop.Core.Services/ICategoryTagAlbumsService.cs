using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentJson.Configuration;
using FluentJson;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 分类下的标签下的专辑接口
    /// </summary>
    public interface ICategoryTagAlbumsService
    {
        /// <summary>
        /// 分类下的标签下的专辑接口
        /// </summary>
        void GetData<T>(Action<object> act, T param);
    }
}
