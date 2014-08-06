using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Core.Models.Tags;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Modules.AlbumListModule.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Untils;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace XIMALAYA.PCDesktop.Modules.AlbumListModule
{
    /// <summary>
    /// mvvm model
    /// </summary>
    //[ModuleExport(WellKnownModuleNames.AlbumListModule, typeof(AlbumListModule), InitializationMode = InitializationMode.WhenAvailable)]
    public class AlbumListViewModel : BaseViewModel, IModule
    {
        #region fields

        private TagEventArgument _TagEventArgument;

        #endregion

        #region properties

        /// <summary>
        /// 标签下的声音服务
        /// </summary>
        [Import]
        private ICategoryTagAlbumsService CategoryTagAlbumsService { get; set; }
        /// <summary>
        /// 属性描述
        /// </summary>
        public TagEventArgument TagEventArgument
        {
            get
            {
                return _TagEventArgument;
            }
            set
            {
                if (value != _TagEventArgument)
                {
                    _TagEventArgument = value;
                    this.OnChangeTagEventArgument();
                    this.RaisePropertyChanged(() => this.TagEventArgument);
                }
            }
        }

        #endregion

        #region actions

        private void OnChangeTagEventArgument()
        {
            AlbumView albumView = this.Container.GetInstance<AlbumView>();
            string regionName = this.ContainerView.GetFlyout(this.TagEventArgument.TagName);
            if (albumView != null)
            {
                albumView.AlbumViewModel.DoInit(new CategoryTagAlbums
                {
                    Category = this.TagEventArgument.Category,
                    TagName = this.TagEventArgument.TagName,
                    Condition = ConditionAlbumType.hot,
                    Device = DeviceType.pc,
                    Page = 1,
                    PerPage = 20,
                    Status = 0
                }, regionName, albumView);
            }
        }

        #endregion

        #region construction

        /// <summary>
        /// 构造
        /// </summary>
        public AlbumListViewModel()
        {
            if (this.EventAggregator == null)
            {
                throw new ArgumentNullException("EventAggregator");
            }
            //标签点击事件，获取专辑数据
            this.EventAggregator.GetEvent<AlbumListEvent<TagEventArgument>>().Subscribe(e =>
            {
                this.TagEventArgument = e;
            });
        }

        #endregion

        public void Initialize()
        {
            
        }
    }
}
