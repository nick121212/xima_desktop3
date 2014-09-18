using System;
using Microsoft.Practices.Prism.Modularity;

namespace XIMALAYA.PCDesktop.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseModule : BaseViewModel, IModule, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual void Initialize()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            this.RegionManager = null;
            this.Container = null;
        }
    }
}
