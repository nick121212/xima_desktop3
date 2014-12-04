using Microsoft.Practices.Prism.MefExtensions.Modularity;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Modules.AboutModule.Views;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Modules.AboutModule
{
    [ModuleExport(WellKnownModuleNames.AboutModule, typeof(AboutModule))]
    public class AboutModule : BaseModule
    {
        public override void Initialize()
        {
            this.EventAggregator.GetEvent<ContentChangeEvent>().Subscribe(s =>
            {
                if (s == WellKnownModuleNames.AboutModule)
                {
                    base.LoadModule(this.Container.GetInstance<AboutView>());
                }
            });
        }
    }
}
