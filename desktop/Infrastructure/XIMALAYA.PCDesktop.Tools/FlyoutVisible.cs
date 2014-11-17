using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Tools
{
    /// <summary>
    /// 全局flytou显示属性
    /// </summary>
    public class FlyoutVisibleBase : NotificationObject
    {
        #region fields

        private bool _IsSettingFlyoutShow = false;
        private bool _IsShowListView;

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
                        return;
                    }
                    this.EventAggregator.GetEvent<ModulesManagerEvent>().Publish(new ModuleInfoArgument()
                    {
                        ModuleName = WellKnownModuleNames.SettingsModule
                    });
                }
            }
        }
        /// <summary>
        /// 显示当前播放列表
        /// </summary>
        public bool IsShowListView
        {
            get
            {
                return _IsShowListView;
            }
            set
            {
                if (value != _IsShowListView)
                {
                    _IsShowListView = value;
                    this.RaisePropertyChanged(() => this.IsShowListView);
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

}
