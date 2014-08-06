using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using WPFSoundVisualizationLib;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Modules.MusicPlayer.Views;
using XIMALAYA.PCDesktop.Tools.Player;
using XIMALAYA.PCDesktop.Tools.Untils;
using Microsoft.Practices.Prism.Modularity;
using XIMALAYA.PCDesktop.Cache;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Tools;

namespace XIMALAYA.PCDesktop.Modules.MusicPlayer
{
    /// <summary>
    /// 播放器视图model
    /// </summary>
    [Export]
    public class MusicPlayerViewModel : BaseViewModel, IModule
    {
        #region command

        public ICommand ShowSpectrumAnalyzerCommand { get; set; }

        #endregion

        #region 属性

        private SoundData _SoundData;
        /// <summary>
        /// 当前播放歌曲信息
        /// </summary>
        public SoundData SoundData
        {
            get
            {
                return _SoundData;
            }
            set
            {
                if (value == _SoundData) return;
                _SoundData = value;
                this.RaisePropertyChanged(() => this.SoundData);
            }
        }
        private bool _ShowSpectrumAnalyzer = true;
        /// <summary>
        /// 是否显示声音波形
        /// </summary>
        public bool ShowSpectrumAnalyzer
        {
            get
            {
                return _ShowSpectrumAnalyzer;
            }
            set
            {
                if (value != _ShowSpectrumAnalyzer)
                {
                    _ShowSpectrumAnalyzer = value;
                    this.RaisePropertyChanged(() => this.ShowSpectrumAnalyzer);
                }
            }
        }
        /// <summary>
        /// 播放器控件
        /// </summary>
        public BassEngine BassEngine
        {
            get
            {
                return PlayerSingleton.Instance;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            this.BassEngine.CurrentSoundUrl = string.Empty;
            this.ShowSpectrumAnalyzerCommand = new DelegateCommand<object>(o =>
            {
                this.ShowSpectrumAnalyzer = !this.ShowSpectrumAnalyzer;

            });
            this.EventAggregator.GetEvent<PlayerEvent>().Subscribe((TrackId) =>
            {
                if (this.SoundData != null && this.SoundData.TrackId == TrackId)
                {
                    return;
                }
                SoundData soundData = SoundCache.Instance[TrackId];
                if (soundData != null)
                {
                    this.SoundData = soundData;
                    this.BassEngine.TrackID = this.SoundData.TrackId;
                    this.BassEngine.OpenUrlAsync(this.SoundData.PlayUrl64);
                }
            });
        }

        #endregion
    }
}