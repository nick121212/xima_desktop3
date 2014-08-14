using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;

namespace XIMALAYA.PCDesktop.Events
{
    /// <summary>
    /// 内容切换事件
    /// </summary>
    public class ContentChangeEvent: CompositePresentationEvent<string> { }
}
