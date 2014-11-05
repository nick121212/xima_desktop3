using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Modules.SoundModule.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Untils;

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

        private void OnArgument(string argument)
        {
            string[] args = argument.Split('_');
            long id = 0;

            if (args.Length == 2)
            {
                if (args[0] == "sound" && long.TryParse(args[1], out id))
                {
                    this.OnSoundDetailEvent(id);
                }
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
            this.EventAggregator.GetEvent<JumplistEvent<string>>().Subscribe(OnArgument);
            this.EventAggregator.GetEvent<SoundDetailEvent<int>>().Subscribe(AddMutiSoundViewToRegion);
            //this.EventAggregator.GetEvent<SoundDetailEvent<long>>().Publish(352374);
        }
        /// <summary>
        /// 多声音视图
        /// </summary>
        /// <param name="id"></param>
        private void AddMutiSoundViewToRegion(int id)
        {
            var view = this.Container.GetInstance<MutiSoundView>();
            string regionName = this.ContainerView.GetFlyout(" ", null, null, view.ViewModel, () => view.ViewModel.Title);

            if (view != null)
            {
                if (this.RegionManager.Regions.ContainsRegionWithName(regionName))
                {
                    var region = this.RegionManager.Regions[regionName];

                    view.ViewModel.Initialize(id);
                    region.Add(view);
                }
            }
        }
        public override void Dispose()
        {
            base.Dispose();
            this.EventAggregator.GetEvent<SoundDetailEvent<long>>().Unsubscribe(OnSoundDetailEvent);
            this.EventAggregator.GetEvent<JumplistEvent<string>>().Unsubscribe(OnArgument);
            this.EventAggregator.GetEvent<SoundDetailEvent<int>>().Unsubscribe(AddMutiSoundViewToRegion);
        }

        #endregion
    }
}
