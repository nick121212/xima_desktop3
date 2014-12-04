using System;
using System.Windows.Forms;
using Microsoft.Practices.Prism.Modularity;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Common
{
    /// <summary>
    /// 基础Module
    /// </summary>
    public class BaseModule : BaseViewModel, IModule, IDisposable
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Initialize()
        {

        }
        /// <summary>
        /// 销毁
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            this.RegionManager = null;
            this.Container = null;
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void LoadModule(object view)
        {
            if (this.RegionManager.Regions.ContainsRegionWithName(WellKnownRegionNames.DiscoverModuleRegion))
            {
                //var view = this.Container.GetInstance<LoginView>();
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
