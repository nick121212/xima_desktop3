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
using XIMALAYA.PCDesktop.Modules.MusicPlayer.Views;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Modules.MusicPlayer
{
    /// <summary>
    /// 播放器模块
    /// </summary>
    [ModuleExport(WellKnownModuleNames.MusicPlayerModule, typeof(MusicPlayerModule), InitializationMode = InitializationMode.OnDemand)]
    public class MusicPlayerModule : BaseModule
    {
        /// <summary>
        /// 模块初始化
        /// </summary>
        public override void Initialize()
        {
            if (this.RegionManager.Regions.ContainsRegionWithName(WellKnownRegionNames.MusicPlayerModuleRegion))
            {
                var view = this.Container.GetInstance<MusicPlayerView>();
                var region = this.RegionManager.Regions[WellKnownRegionNames.MusicPlayerModuleRegion];

                view.ViewModel.Initialize();
                region.Add(view, WellKnownModuleNames.MusicPlayerModule);
            }
        }

    }
}
