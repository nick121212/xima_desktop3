using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Modules.Upload.Views;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Modules.Upload
{
    [ModuleExport(WellKnownModuleNames.UploadModule, typeof(UploadModule))]
    class UploadModule : BaseModule
    {
        public override void Initialize()
        {
            this.EventAggregator.GetEvent<ContentChangeEvent>().Subscribe(s =>
            {
                if (s == WellKnownModuleNames.UploadModule)
                {
                    this.LoadModule(this.Container.GetInstance<UploadIndex>());
                }
            });
        }
    }
}
