using Microsoft.Practices.Prism.MefExtensions.Modularity;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Modules.Settings.Views;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Modules
{
    /// <summary>
    /// A module for the quickstart.
    /// </summary>
    [ModuleExport(WellKnownModuleNames.SettingsModule, typeof(SettingsModule))]
    public class SettingsModule : BaseModule
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            //if (this.RegionManager.Regions.ContainsRegionWithName(WellKnownRegionNames.SettingsModuleRegion))
            //{
            //    var view = this.Container.GetInstance<SettingsView>();
            //    var region = this.RegionManager.Regions[WellKnownRegionNames.SettingsModuleRegion];

            //    region.Add(view, WellKnownModuleNames.SettingsModule);
            //}
            this.EventAggregator.GetEvent<ContentChangeEvent>().Subscribe(s =>
            {
                if (s == WellKnownModuleNames.SettingsModule)
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
                var view = this.Container.GetInstance<SettingsView>();
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
