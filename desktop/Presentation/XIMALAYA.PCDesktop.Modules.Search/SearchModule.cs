using Microsoft.Practices.Prism.MefExtensions.Modularity;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Modules.Search.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Modules.Search
{
    /// <summary>
    /// 加载菜单
    /// </summary>
    [ModuleExport(WellKnownModuleNames.SearchModule, typeof(SearchModule))]
    public class SearchModule : BaseModule
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            this.EventAggregator.GetEvent<ContentChangeEvent>().Subscribe(s =>
            {
                if (s == WellKnownModuleNames.SearchModule)
                {
                    this.LoadModule(this.Container.GetInstance<SearchMainView>());
                }
            });
        }
    }
}
