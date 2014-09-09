using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Prism.Regions;
using XIMALAYA.PCDesktop.Cache;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Core.Models.User;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Modules.SoundModule.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Modules.SoundModule
{
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class SoundDetailViewModel : BaseViewModel
    {
        #region 字段

        private SoundData _SoundData;

        #endregion

        #region 属性

        /// <summary>
        /// 声音数据
        /// </summary>
        public SoundData SoundData
        {
            get
            {
                return _SoundData;
            }
            set
            {
                if (value != _SoundData)
                {
                    _SoundData = value;
                    this.RaisePropertyChanged(() => this.SoundData);
                }
            }
        }
        /// <summary>
        /// 当前声音ID
        /// </summary>
        private long TrackID { get; set; }
        /// <summary>
        /// 声音详情页接口
        /// </summary>
        [Import]
        private ISoundDetailService SoundDetailService { get; set; }
        /// <summary>
        /// 喜欢改声音的用户列表
        /// </summary>
        public ObservableCollection<UserData> LikedUsers { get; set; }

        #endregion

        #region BaseViewModel方法

        /// <summary>
        /// 获取声音详情数据
        /// </summary>
        /// <param name="isClear"></param>
        protected override void GetData(bool isClear)
        {
            if (this.SoundDetailService == null) return;

            this.SoundDetailService.GetData(res =>
            {
                var result = res as SoundData;
                Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                {
                    if (result.TrackId > 0)
                    {
                        SoundCache.Instance[result.TrackId] = result;
                        result.Duration *= 1000;
                        this.SoundData = result;
                        this.EventAggregator.GetEvent<UserMinEvent>().Publish(new UserEventArgument
                        {
                            RegionName = this.RegionName,
                            UID = this.SoundData.UID
                        });
                        if (CommandSingleton.Instance.SoundData.TrackId != this.SoundData.TrackId)
                        {
                            this.EventAggregator.GetEvent<PlayerEvent>().Publish(this.SoundData.TrackId);
                        }
                        return;
                    }
                    DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", "获取声音数据失败！");
                }), DispatcherPriority.Background);
            }, new SoundDetailParam
            {
                TrackId = this.TrackID
            });
            this.SoundDetailService.GetLikedUsers(res =>
            {
                var result = res as LikedSoundUserResult;
                Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                {
                    this.Total = result.TotalCount;

                    foreach (UserData user in result.Users)
                    {
                        this.LikedUsers.Add(user);
                    }
                }), DispatcherPriority.Background);
            }, new SoundDetailParam
            {
                TrackId = this.TrackID
            });
            base.GetData(isClear);
        }
        public override void Dispose()
        {
            this.RegionManager.Regions.Remove(this.RegionName);
            base.Dispose();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化入口
        /// </summary>
        /// <param name="trackID"></param>
        /// <param name="regionName"></param>
        /// <param name="soundDetailView"></param>
        public void DoInit(long trackID, string regionName, SoundDetailView soundDetailView)
        {
            if (this.RegionManager != null && this.RegionManager.Regions.ContainsRegionWithName(regionName))
            {
                this.LikedUsers = new ObservableCollection<UserData>();
                this.RegionManager.AddToRegion(regionName, soundDetailView);
                this.TrackID = trackID;
                this.GetData(true);
            }
        }

        #endregion
    }
}
