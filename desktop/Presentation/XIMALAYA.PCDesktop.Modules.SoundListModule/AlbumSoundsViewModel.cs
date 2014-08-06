using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Modules.SoundListModule.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Untils;
using XIMALAYA.PCDesktop.Cache;

namespace XIMALAYA.PCDesktop.Modules.SoundListModule
{
    /// <summary>
    /// mvvm model
    /// </summary>
    [Export]
    [PartCreationPolicy((CreationPolicy.NonShared))]
    public sealed class AlbumSoundsViewModel : BaseViewModel
    {
        #region 属性

        /// <summary>
        /// 专辑详情服务
        /// </summary>
        [Import]
        private IAlbumInfoService AlbumInfoService { get; set; }
        /// <summary>
        /// 当前参数
        /// </summary>
        private AlbumInfoParam Params { get; set; }
        /// <summary>
        /// 专辑下的声音数据
        /// </summary>
        public ObservableCollection<SoundData> Sounds { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="album"></param>
        /// <param name="regionName"></param>
        /// <param name="view"></param>
        public void DoInit(AlbumData album, string regionName, AlbumSoundsView view)
        {
            if (this.RegionManager != null)
            {
                this.RegionManager.AddToRegion(regionName, view);
                this.Params = new AlbumInfoParam
                {
                    AlbumID = album.AlbumID
                };
                this.GetData(true);
            }
        }
        /// <summary>
        /// 构造
        /// </summary>
        public AlbumSoundsViewModel()
        {
            this.Sounds = new ObservableCollection<SoundData>();
        }
        /// <summary>
        /// 获取声音数据
        /// </summary>
        /// <param name="isClear"></param>
        protected override void GetData(bool isClear)
        {
            if (this.AlbumInfoService == null) return;

            if (isClear)
            {
                this.Params.Page = 1;
                this.Sounds.Clear();
            }

            if (this.AlbumInfoService != null)
            {
                this.IsWaiting = true;
                this.AlbumInfoService.GetData(result =>
                {
                    var albumInfoResult = result as AlbumInfoResult;
                    Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                    {
                        this.IsWaiting = false;
                        if (albumInfoResult.Ret == 0)
                        {
                            foreach (var sound in albumInfoResult.SoundsResult.Sounds)
                            {
                                SoundCache.Instance[sound.TrackId] = sound;
                                this.Sounds.Add(sound);
                            }
                        }
                        else
                        {
                            DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", albumInfoResult.Message);
                        }
                    }));
                }, this.Params);
            }
        }

        #endregion
    }
}
