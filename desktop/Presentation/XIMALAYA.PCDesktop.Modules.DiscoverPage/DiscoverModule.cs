using Microsoft.Practices.Prism.MefExtensions.Modularity;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Modules.DiscoverPage.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Modules.DiscoverPage
{
    [ModuleExport(WellKnownModuleNames.DiscoverModule, typeof(DiscoverModule))]
    public class DiscoverModule : BaseModule
    {
        public override void Initialize()
        {
            var detailModel = this.Container.GetInstance<CategoryDetailViewModel>();

            detailModel.Initialize();
            this.EventAggregator.GetEvent<ContentChangeEvent>().Subscribe(s =>
            {
                if (s == WellKnownModuleNames.DiscoverModule)
                {
                    this.LoadModule();
                }
            });
            this.LoadModule();
        }
        /// <summary>
        /// 加载模块
        /// </summary>
        private void LoadModule()
        {
            if (this.RegionManager.Regions.ContainsRegionWithName(WellKnownRegionNames.DiscoverModuleRegion))
            {
                var view = this.Container.GetInstance<DiscoverView>();
                var region = this.RegionManager.Regions[WellKnownRegionNames.DiscoverModuleRegion];

                if (!region.ActiveViews.Contains(view))
                {
                    foreach (object activeView in region.ActiveViews)
                    {
                        region.Remove(activeView);
                    }
                    view.DiscoverViewModel.Initialize();
                    region.Add(view);
                }
            }
        }
    }
}