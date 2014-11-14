using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;

namespace XIMALAYA.PCDesktop.Common.Events
{
    /// <summary>
    /// 登录相关事件
    /// </summary>
    public class PassportEvent<T> : CompositePresentationEvent<T> { }
}
