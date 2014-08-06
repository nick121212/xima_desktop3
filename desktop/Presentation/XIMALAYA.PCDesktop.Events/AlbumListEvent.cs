using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;

namespace XIMALAYA.PCDesktop.Events
{
    /// <summary>
    /// 获取专辑列表数据
    /// </summary>
    public class AlbumListEvent<T> : CompositePresentationEvent<T> { }

}
