﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Modules.AlbumModule.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Untils;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Events;

namespace XIMALAYA.PCDesktop.Modules.AlbumModule
{
    /// <summary>
    /// 声音列表模块
    /// </summary>
    [ModuleExport(WellKnownModuleNames.AlbumListModule, typeof(AlbumModule), InitializationMode = InitializationMode.WhenAvailable)]
    public class AlbumModule : BaseModule
    {
        #region properties

        /// <summary>
        /// 标签下的声音服务
        /// </summary>
        [Import]
        private ICategoryTagAlbumsService CategoryTagAlbumsService { get; set; }

        #endregion

        #region actions

        private void OnChangeTagEventArgument(TagEventArgument e)
        {
            var albumView = this.Container.GetInstance<AlbumView>();
            string regionName = this.ContainerView.GetFlyout(e.Title);

            if (albumView != null)
            {
                albumView.ViewModel.DoInit(new CategoryTagAlbumParam
                {
                    Category = e.Category,
                    TagName = e.TagName,
                    Condition = ConditionAlbumType.hot,
                    Device = DeviceType.pc,
                    Page = 1,
                    PerPage = 20,
                    Status = 0
                }, regionName, albumView);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="albumID"></param>
        private void OnAlbumDetailEvent(long albumID)
        {
            var albumDetailView = this.Container.GetInstance<AlbumDetailView>();
            string regionName = this.ContainerView.GetFlyout(string.Empty);

            if (albumDetailView != null)
            {
                albumDetailView.ViewModel.DoInit(albumID, regionName, albumDetailView);
            }
        }

        #endregion

        #region method

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            if (this.EventAggregator == null)
            {
                throw new ArgumentNullException("EventAggregator");
            }
            //标签点击事件，获取专辑数据
            this.EventAggregator.GetEvent<AlbumListEvent<TagEventArgument>>().Subscribe(OnChangeTagEventArgument);

            //获取专辑详情数据
            this.EventAggregator.GetEvent<AlbumDetailEvent<long>>().Subscribe(OnAlbumDetailEvent);
        }

        public override void Dispose()
        {
            base.Dispose();
            this.EventAggregator.GetEvent<AlbumDetailEvent<long>>().Unsubscribe(OnAlbumDetailEvent);
            this.EventAggregator.GetEvent<AlbumListEvent<TagEventArgument>>().Unsubscribe(OnChangeTagEventArgument);
        }

        #endregion
    }
}