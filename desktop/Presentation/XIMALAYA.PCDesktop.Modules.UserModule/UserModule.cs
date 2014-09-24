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
            this.EventAggregator.GetEvent<UserEvent<UserEventArgument>>().Subscribe(AddMinUserViewToRegion);
            this.EventAggregator.GetEvent<UserEvent<long>>().Subscribe(AddUserViewToRegion);
        }
        /// <summary>
        /// 普通用户视图
        /// </summary>
        /// <param name="obj"></param>
        private void AddUserViewToRegion(long uid)
        {
            var view = this.Container.GetInstance<UserView>();
            string regionName = this.ContainerView.GetFlyout(string.Empty);

            if (view != null)
            {
                if (this.RegionManager.Regions.ContainsRegionWithName(regionName))
                {
                    var region = this.RegionManager.Regions[regionName];

                    view.ViewModel.Initialize(uid);
                    region.Add(view);
                }
            }
        }
        /// <summary>
        /// 小用户视图
        /// </summary>
        /// <param name="e"></param>
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
            this.EventAggregator.GetEvent<UserEvent<UserEventArgument>>().Unsubscribe(AddMinUserViewToRegion);
        }
    }
}
