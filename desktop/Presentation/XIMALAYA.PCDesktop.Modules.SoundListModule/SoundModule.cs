using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using System;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Untils;
using XIMALAYA.PCDesktop.Modules.SoundModule.Views;

namespace XIMALAYA.PCDesktop.Modules.SoundModule
{
    /// <summary>
    /// 声音列表模块
    /// </summary>
    [ModuleExport(WellKnownModuleNames.SoundListModule, typeof(SoundModule), InitializationMode = InitializationMode.WhenAvailable)]
    public class SoundModule : BaseModule
    {
        #region 事件

        /// <summary>
        /// 声音详情页
        /// </summary>
        /// <param name="e"></param>
        private void OnSoundDetailEvent(long e)
        {
            var soundDetailView = this.Container.GetInstance<SoundDetailView>();
            string regionName = this.ContainerView.GetFlyout(string.Empty, null, null);

            if (soundDetailView != null)
            {
                soundDetailView.ViewModel.DoInit(e, regionName, soundDetailView);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            //标签点击事件，获取专辑详情数据
            this.EventAggregator.GetEvent<SoundDetailEvent<long>>().Subscribe(OnSoundDetailEvent);

            //this.EventAggregator.GetEvent<SoundDetailEvent<long>>().Publish(352374);
        }

        public override void Dispose()
        {
            base.Dispose();
            this.EventAggregator.GetEvent<SoundDetailEvent<long>>().Unsubscribe(OnSoundDetailEvent);
        }

        #endregion
    }
}
