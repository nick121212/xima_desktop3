using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;

namespace XIMALAYA.PCDesktop.Common.Events
{
    /// <summary>
    /// 获取声音列表数据
    /// </summary>
    public class SoundListEvent<T> : CompositePresentationEvent<T> { }
    /// <summary>
    /// 声音详情页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SoundDetailEvent<T> : CompositePresentationEvent<T> { }
}
