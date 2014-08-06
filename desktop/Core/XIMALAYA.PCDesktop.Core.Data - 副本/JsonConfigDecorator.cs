using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentJson.Configuration;

namespace XIMALAYA.PCDesktop.Core.Data
{
    /// <summary>
    /// 装饰者抽象类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T1"></typeparam>
    public abstract class JsonConfigDecorator<T, T1> : IJsonConfig
        where T : class
        where T1 : class
    {
        /// <summary>
        /// 
        /// </summary>
        public JsonConfiguration<T1> Config { get; set; }
        /// <summary>
        /// 装饰类
        /// </summary>
        private IJsonConfig Decorator { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public JsonConfigDecorator() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        public JsonConfigDecorator(IJsonConfig decorator,JsonConfiguration<T1> config)
        {
            this.Decorator = decorator;
            this.Config = config;
        }
        /// <summary>
        /// 添加配置项
        /// </summary>
        public virtual void AddConfig()
        {
            if (this.Decorator != null && this.Config != null)
            {
                this.Decorator.AddConfig();
            }
        }
    }
}
