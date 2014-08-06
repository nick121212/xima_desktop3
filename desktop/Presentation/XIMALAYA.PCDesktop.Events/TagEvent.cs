using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;

namespace XIMALAYA.PCDesktop.Events
{
    /// <summary>
    /// 标签事件
    /// </summary>
    public class TagEventArgument
    {
        /// <summary>
        /// 分类名
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 标签的key
        /// </summary>
        public string TagKey { get; set; }
        /// <summary>
        /// 标签的名称
        /// </summary>
        public string TagName { get; set; }
    }
    /// <summary>
    /// 获取标签事件
    /// </summary>
    public class TagEvent : CompositePresentationEvent<TagEventArgument> { }
}
