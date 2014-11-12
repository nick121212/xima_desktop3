using Microsoft.Practices.Prism.MefExtensions.Modularity;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Modules.DiscoverModule.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Modules.DiscoverModule
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
            //this.LoadModule();
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
                    view.ViewModel.Initialize();
                    region.Add(view);
                }
            }
        }
    }
}