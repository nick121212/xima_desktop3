using System;
using Microsoft.Practices.Prism.Modularity;

namespace XIMALAYA.PCDesktop.Tools.Untils
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
        public virtual void Dispose()
        {
            this.RegionManager = null;
            this.Container = null;
        }
    }
}
