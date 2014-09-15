using Microsoft.Practices.Prism.MefExtensions.Modularity;
using XIMALAYA.PCDesktop.Common;
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
            if (this.RegionManager.Regions.ContainsRegionWithName(WellKnownRegionNames.SettingsModuleRegion))
            {
                var view = this.Container.GetInstance<SettingsView>();
                var region = this.RegionManager.Regions[WellKnownRegionNames.SettingsModuleRegion];

                region.Add(view, WellKnownModuleNames.SettingsModule);
            }
        }

    }
}
