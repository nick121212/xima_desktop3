using Microsoft.Practices.Prism.Events;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Common.Events
{
    /// <summary>
    /// 获取专辑列表数据
    /// </summary>
    public class ShareEvent<T> : CompositePresentationEvent<T> { }

    public class ShareEventArgument
    {
        public Sites Site { get; set; }
        public long ID { get; set; }
        public ShareType ShareType { get; set; }
    }
}
