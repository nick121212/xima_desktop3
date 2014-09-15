using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Modules.UserModule.Views;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Modules.UserModule
{
    /// <summary>
    /// 用户相关
    /// </summary>
    [ModuleExport(WellKnownModuleNames.UserModule, typeof(UserModule),
        InitializationMode = InitializationMode.WhenAvailable)]
    public class UserModule : BaseModule
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            //this.AddMinUserViewToRegion();
            this.EventAggregator.GetEvent<UserMinEvent>().Subscribe(AddMinUserViewToRegion);
        }

        private void AddMinUserViewToRegion(UserEventArgument e)
        {
            if (this.RegionManager.Regions.ContainsRegionWithName(e.RegionName))
            {
                var view = this.Container.GetInstance<UserMinView>();
                var region = this.RegionManager.Regions[e.RegionName];

                view.ViewModel.Initialize(e.UID);
                region.Add(view);
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            this.EventAggregator.GetEvent<UserMinEvent>().Unsubscribe(AddMinUserViewToRegion);
        }
    }
}
