using Microsoft.Practices.Prism.MefExtensions.Modularity;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Modules.AboutModule.Views;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Modules.AboutModule
{
    [ModuleExport(WellKnownModuleNames.AboutModule, typeof(AboutModule))]
    public class AboutModule : BaseModule
    {
        public override void Initialize()
        {
            this.EventAggregator.GetEvent<ContentChangeEvent>().Subscribe(s =>
            {
                if (s == WellKnownModuleNames.AboutModule)
                {
                    this.LoadModule();
                }
            });
        }
        /// <summary>
        /// 加载模块
        /// </summary>
        private void LoadModule()
        {
            if (this.RegionManager.Regions.ContainsRegionWithName(WellKnownRegionNames.DiscoverModuleRegion))
            {
                var view = this.Container.GetInstance<AboutView>();
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
