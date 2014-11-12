using Microsoft.Practices.Prism.MefExtensions.Modularity;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Modules.Passport.Views;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Modules.Passport
{
    /// <summary>
    /// 登录模块
    /// </summary>
    [ModuleExport(WellKnownModuleNames.PassportModule, typeof(PassportModule))]
    public class PassportModule : BaseModule
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            this.EventAggregator.GetEvent<ContentChangeEvent>().Subscribe(s =>
            {
                if (s == WellKnownModuleNames.PassportModule)
                {
                    this.LoadModule();
                }
            });

            this.LoadModule();
        }
        /// <summary>
        /// 加载搜索模块
        /// </summary>
        private void LoadModule()
        {
            if (this.RegionManager.Regions.ContainsRegionWithName(WellKnownRegionNames.DiscoverModuleRegion))
            {
                var viewModel = this.Container.GetInstance<LoginView>();
                var region = this.RegionManager.Regions[WellKnownRegionNames.DiscoverModuleRegion];

                if (!region.ActiveViews.Contains(viewModel))
                {
                    foreach (object activeView in region.ActiveViews)
                    {
                        region.Remove(activeView);
                    }
                    region.Add(viewModel);
                }
            }
        }
    }
}
