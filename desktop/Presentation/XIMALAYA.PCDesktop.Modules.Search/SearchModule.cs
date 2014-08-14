using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Modules.Search.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Untils;

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
                    this.LoadModule();
                }
            });
        }
        /// <summary>
        /// 加载搜索模块
        /// </summary>
        private void LoadModule()
        {
            if (this.RegionManager.Regions.ContainsRegionWithName(WellKnownRegionNames.DiscoverModuleRegion))
            {
                var view = this.Container.GetInstance<SearchMainView>();
                var region = this.RegionManager.Regions[WellKnownRegionNames.DiscoverModuleRegion];

                if (!region.ActiveViews.Contains(view))
                {
                    foreach (object activeView in region.ActiveViews)
                    {
                        region.Remove(activeView);
                    }
                    region.Add(view);
                }
            }
        }
    }
}
