using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Modules.MusicPlayer.Views;
using XIMALAYA.PCDesktop.Untils;

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
