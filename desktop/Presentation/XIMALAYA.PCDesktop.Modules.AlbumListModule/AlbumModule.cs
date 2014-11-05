using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Modules.AlbumModule.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Modules.AlbumModule
{
    /// <summary>
    /// 声音列表模块
    /// </summary>
    [ModuleExport(WellKnownModuleNames.AlbumListModule, typeof(AlbumModule), InitializationMode = InitializationMode.WhenAvailable)]
    public class AlbumModule : BaseModule
    {
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
        private void OnArgument(string argument)
        {

            string[] args = argument.Split('_');
            long id = 0;

            if (args.Length == 2)
            {
                if (args[0] == "album" && long.TryParse(args[1], out id))
                {
                    this.OnAlbumDetailEvent(id);
                }
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
            //跳转指令
            this.EventAggregator.GetEvent<JumplistEvent<string>>().Subscribe(OnArgument);
            //多专辑
            this.EventAggregator.GetEvent<AlbumDetailEvent<int>>().Subscribe(AddMutiAlbumViewToRegion);


            this.EventAggregator.GetEvent<AlbumDetailEvent<int>>().Publish((int)911);
        }

        /// <summary>
        /// 多用户视图
        /// </summary>
        /// <param name="id"></param>
        private void AddMutiAlbumViewToRegion(int id)
        {
            var view = this.Container.GetInstance<MutiAlbumView>();
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
            this.EventAggregator.GetEvent<AlbumDetailEvent<long>>().Unsubscribe(OnAlbumDetailEvent);
            this.EventAggregator.GetEvent<AlbumListEvent<TagEventArgument>>().Unsubscribe(OnChangeTagEventArgument);
            this.EventAggregator.GetEvent<JumplistEvent<string>>().Unsubscribe(OnArgument);
            this.EventAggregator.GetEvent<AlbumDetailEvent<int>>().Unsubscribe(AddMutiAlbumViewToRegion);
        }

        #endregion
    }
}
