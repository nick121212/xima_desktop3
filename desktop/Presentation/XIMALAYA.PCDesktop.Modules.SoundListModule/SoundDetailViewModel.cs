using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Prism.Regions;
using XIMALAYA.PCDesktop.Cache;
using XIMALAYA.PCDesktop.Core.Models.Sound;
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
                        this.SoundData = result;
                        this.EventAggregator.GetEvent<UserMinEvent>().Publish(new UserEventArgument
                        {
                            RegionName = string.Format("UserMinViewRegion_{0}", this.SoundData.TrackId),
                            UID = this.SoundData.UID
                        });
                        return;
                    }
                    DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", "获取声音数据失败！");
                }), DispatcherPriority.Background);
            }, new SoundDetailParam
            {
                TrackId = this.TrackID
            });
            base.GetData(isClear);
        }
        public override void Dispose()
        {
            this.RegionManager.Regions.Remove(string.Format("UserMinViewRegion_{0}", this.SoundData.TrackId));
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
                this.RegionManager.AddToRegion(regionName, soundDetailView);
                this.TrackID = trackID;
                this.GetData(true);
            }
        }

        #endregion
    }
}
