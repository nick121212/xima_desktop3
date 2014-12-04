using Microsoft.Practices.Prism.MefExtensions.Modularity;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Modules.Menus.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Modules.Menus
{
    /// <summary>
    /// 加载菜单
    /// </summary>
    [ModuleExport(WellKnownModuleNames.MenusModule, typeof(MenusModule))]
    public class MenusModule : BaseModule
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            if (this.RegionManager.Regions.ContainsRegionWithName(WellKnownRegionNames.MenusRegion))
            {
                var view = this.Container.GetInstance<MainMenuView>();
                var region = this.RegionManager.Regions[WellKnownRegionNames.MenusRegion];

                region.Add(view, WellKnownModuleNames.MenusModule);
            }
            this.EventAggregator.GetEvent<ContentChangeEvent>().Publish(WellKnownModuleNames.PassportModule);
        }
    }
}
