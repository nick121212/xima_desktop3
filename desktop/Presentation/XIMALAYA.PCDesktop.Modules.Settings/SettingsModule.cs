using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using XIMALAYA.PCDesktop.Tools;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Modules.Settings;
using XIMALAYA.PCDesktop.Modules.Settings.Views;
using XIMALAYA.PCDesktop.Tools.Untils;

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
                var view = this.Container.GetInstance<AppearanceView>();
                var region = this.RegionManager.Regions[WellKnownRegionNames.SettingsModuleRegion];

                region.Add(view, WellKnownModuleNames.SettingsModule);
            }
        }

    }
}
