using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Events;

namespace XIMALAYA.PCDesktop.Tools
{
    /// <summary>
    /// 全局flytou显示属性
    /// </summary>
    public sealed class FlyoutVisibleBase : NotificationObject
    {
        #region fields

        private bool _IsSettingFlyoutShow = false;

        #endregion

        #region propertites

        /// <summary>
        /// 显示设置flyout
        /// </summary>
        public bool IsSettingFlyoutShow
        {
            get
            {
                return _IsSettingFlyoutShow;
            }
            set
            {
                if (value != _IsSettingFlyoutShow)
                {
                    _IsSettingFlyoutShow = value;
                    this.RaisePropertyChanged(() => this.IsSettingFlyoutShow);
                    if (this.EventAggregator == null)
                    {
                        throw new Exception("EventAggregator null");
                    }
                    this.EventAggregator.GetEvent<ModulesManagerEvent>().Publish(new ModuleInfoArgument()
                    {
                        ModuleName = WellKnownModuleNames.SettingsModule
                    });
                }
            }
        }
        /// <summary>
        /// 事件管理器
        /// </summary>
        public IEventAggregator EventAggregator { get; set; }

        #endregion

        #region constructor

        private FlyoutVisibleBase()
        {
            this.EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        #endregion
    }
    /// <summary>
    /// 全局单例
    /// </summary>
    public class FlyoutVisible : Singleton<FlyoutVisibleBase>
    {

    }
}
