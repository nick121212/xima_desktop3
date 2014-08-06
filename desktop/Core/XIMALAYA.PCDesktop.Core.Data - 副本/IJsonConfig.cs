using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentJson.Configuration;

namespace XIMALAYA.PCDesktop.Core.Data
{
    /// <summary>
    /// 装饰者模式接口
    /// </summary>
    public interface IJsonConfig
    {
        /// <summary>
        /// 添加配置
        /// </summary>
        void AddConfig();
    }
}
