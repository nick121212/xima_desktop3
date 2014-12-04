using Microsoft.Practices.Prism.MefExtensions.Modularity;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Modules.Settings.Views;
using XIMALAYA.PCDesktop.Untils;

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
            this.EventAggregator.GetEvent<ContentChangeEvent>().Subscribe(s =>
            {
                if (s == WellKnownModuleNames.SettingsModule)
                {
                    this.LoadModule(this.Container.GetInstance<SettingsView>());
                }
            });
        }
    }
}
