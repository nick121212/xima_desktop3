using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;

namespace XIMALAYA.PCDesktop.Common.Events
{
    /// <summary>
    /// win7跳转列表参数
    /// </summary>
    public class JumpListEventArgs
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Arguments { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }
    }
    /// <summary>
    /// win7跳转列表事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JumplistEvent<T> : CompositePresentationEvent<T> { }
}
