using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using XIMALAYA.PCDesktop.Core.Models.Tags;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Modules.SoundListModule.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Core.Models.Album;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Modules.SoundListModule
{
    /// <summary>
    /// mvvm model
    /// </summary>
    [Export]
    public class SoundListViewModel : BaseViewModel
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

        #region construction

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="eventAggregator">事件管理器</param>
        /// <param name="container"></param>
        [ImportingConstructor]
        public SoundListViewModel()
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
