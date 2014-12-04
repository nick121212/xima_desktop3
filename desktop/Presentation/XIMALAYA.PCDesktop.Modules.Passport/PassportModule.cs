using System;
using System.Windows.Controls;
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
    class PassportModule : BaseModule
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
                    this.LoadModule(this.Container.GetInstance<LoginView>());
                }
            });

            //this.LoadModule();
        }
    }
}
