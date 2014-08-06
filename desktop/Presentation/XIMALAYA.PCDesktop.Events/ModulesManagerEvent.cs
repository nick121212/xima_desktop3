using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;

namespace XIMALAYA.PCDesktop.Events
{
    /// <summary>
    /// 模块事件参数
    /// </summary>
    public class ModuleInfoArgument
    {
        public string ModuleName { get; set; }
        public Action Action { get; set; }
    }
    /// <summary>
    /// 加载模块请求事件
    /// </summary>
    public class ModulesManagerEvent : CompositePresentationEvent<ModuleInfoArgument> { }
}
