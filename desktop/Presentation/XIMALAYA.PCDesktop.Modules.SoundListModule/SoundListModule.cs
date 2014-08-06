using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using System;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Modules.SoundListModule.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Modules.SoundListModule
{
    /// <summary>
    /// 声音列表模块
    /// </summary>
    [ModuleExport(WellKnownModuleNames.SoundListModule, typeof(SoundListModule), InitializationMode = InitializationMode.WhenAvailable)]
    public class SoundListModule : BaseModule
    {
        #region fields

        private AlbumData _Album;

        #endregion

        #region properties

        /// <summary>
        /// 当前专辑数据
        /// </summary>
        public AlbumData Album
        {
            get
            {
                return _Album;
            }
            set
            {
                if (value == _Album) return;
                _Album = value;
                this.RaisePropertyChanged(() => this.Album);
                this.OnAlbumChanged();
            }
        }

        #endregion

        #region actions

        /// <summary>
        /// 当前专辑数据更改
        /// </summary>
        private void OnAlbumChanged()
        {
            AlbumSoundsView view = this.Container.GetInstance<AlbumSoundsView>();
            string regionName = this.ContainerView.GetFlyout(this.Album.Title);
            if (view != null)
            {
                view.ViewModel.DoInit(this.Album, regionName, view);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            if (this.EventAggregator == null)
            {
                throw new ArgumentNullException("EventAggregator");
            }
            //标签点击事件，获取声音数据
            this.EventAggregator.GetEvent<SoundListEvent<AlbumData>>().Subscribe(e =>
            {
                this.Album = e;
            });
        }

        #endregion
    }
}
