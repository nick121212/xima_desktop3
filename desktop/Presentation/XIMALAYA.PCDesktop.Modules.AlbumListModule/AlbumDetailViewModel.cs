using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Regions;
using XIMALAYA.PCDesktop.Cache;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Modules.AlbumModule.Views;
using XIMALAYA.PCDesktop.Tools;

namespace XIMALAYA.PCDesktop.Modules.AlbumModule
{
    /// <summary>
    /// 专辑详情页
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AlbumDetailViewModel : BaseViewModel
    {
        #region 字段

        private AlbumData _AlbumData;
        private AlbumDetailParam _Params;

        #endregion

        #region 属性

        /// <summary>
        /// 专辑详情服务
        /// </summary>
        [Import]
        private IAlbumService AlbumDetailService { get; set; }
        /// <summary>
        /// 专辑数据
        /// </summary>
        public AlbumData AlbumData
        {
            get
            {
                return _AlbumData;
            }
            set
            {
                if (value != _AlbumData)
                {
                    _AlbumData = value;
                    this.RaisePropertyChanged(() => this.AlbumData);
                }
            }
        }
        /// <summary>
        /// 参数类
        /// </summary>
        public AlbumDetailParam Params
        {
            get
            {
                return _Params;
            }
            set
            {
                if (value != _Params)
                {
                    _Params = value;
                    this.RaisePropertyChanged(() => this.Params);
                }
            }
        }
        /// <summary>
        /// 专辑下的声音数据
        /// </summary>
        public ObservableCollection<SoundData> Sounds { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="isClear"></param>
        protected override void GetData(bool isClear)
        {
            if (this.AlbumDetailService == null)
            {
                throw new NullReferenceException();
            }

            this.Params.Page = this.CurrentPage;
            base.GetData(isClear);
            this.AlbumDetailService.GetDetailData(result =>
            {
                AlbumInfoResult albumInfo = result as AlbumInfoResult;

                Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                {
                    this.IsWaiting = false;
                    if (isClear)
                    {
                        this.Sounds.Clear();
                    }
                    if (albumInfo.Ret == 0)
                    {
                        this.AlbumData = albumInfo.Album;
                        this.EventAggregator.GetEvent<UserEvent<UserEventArgument>>().Publish(new UserEventArgument
                        {
                            RegionName = this.RegionName,
                            UID = this.AlbumData.Uid
                        });
                        CommandSingleton.Instance.AddToJumpListCommand.Execute(new JumpListEventArgs
                        {
                            Title = this.AlbumData.Title,
                            Arguments = "album_" + this.AlbumData.AlbumID,
                            Category = "最近听过的专辑"
                        });
                        this.Total = albumInfo.SoundsResult.TotalCount;
                        foreach (SoundData sound in albumInfo.SoundsResult.Sounds)
                        {
                            sound.Duration *= 1000;
                            this.Sounds.Add(sound);
                        }
                        SoundCache.Instance.SetData(albumInfo.SoundsResult.Sounds);
                    }

                }), DispatcherPriority.Background);
            }, this.Params);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="albumID">专辑ID</param>
        /// <param name="regionName"></param>
        /// <param name="view"></param>
        public void DoInit(long albumID, string regionName, AlbumDetailView view)
        {
            if (this.RegionManager != null && this.RegionManager.Regions.ContainsRegionWithName(regionName))
            {
                this.Sounds = new ObservableCollection<SoundData>();
                this.RegionManager.AddToRegion(regionName, view);
                this.Params = new AlbumDetailParam
                {
                    AlbumID = albumID,
                    Device = DeviceType.pc,
                    IsAsc = true,
                    Page = 1,
                    PerPage = 20
                };
                this.PageSize = (int)this.Params.PerPage;
                this.CurrentPage = 1;
            }
        }
        public override void Dispose()
        {
            this.RegionManager.Regions.Remove(this.RegionName);
            base.Dispose();
        }
        #endregion
    }
}
