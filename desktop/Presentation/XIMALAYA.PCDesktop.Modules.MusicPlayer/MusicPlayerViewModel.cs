using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Modularity;
using XIMALAYA.PCDesktop.Cache;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Player;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Modules.MusicPlayer
{
    /// <summary>
    /// 播放器视图model
    /// </summary>
    [Export]
    public class MusicPlayerViewModel : BaseViewModel, IModule
    {
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
            this.BassEngine.PlayOverEvent += BassEngine_PlayOverEvent;
            this.EventAggregator.GetEvent<PlayerEvent>().Subscribe(StartPlay);
            this.SoundData = new SoundData
            {
                Title = "喜马拉雅，听我想听"
            };
        }

        public override void Dispose()
        {
            base.Dispose();
            this.EventAggregator.GetEvent<PlayerEvent>().Unsubscribe(StartPlay);
        }

        void StartPlay(long TrackId)
        {
            if (this.SoundData != null && this.SoundData.TrackId == TrackId)
            {
                this.BassEngine.PlayCommand.Execute();
                return;
            }
            SoundData soundData = SoundCache.Instance[TrackId];
            if (soundData == null) return;
            if (soundData.PlayUrl32 == null && soundData.PlayUrl64 == null) return;

            this.SoundData = soundData;
            this.EventAggregator.GetEvent<ChangeCoverEvent>().Publish(this.SoundData.CoverLarge);
            CommandSingleton.Instance.SoundData = this.SoundData;
            this.BassEngine.OpenUrlAsync(this.SoundData.PlayUrl64 == null ? this.SoundData.PlayUrl32 : this.SoundData.PlayUrl64);
        }
        /// <summary>
        /// 当前声音播放完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BassEngine_PlayOverEvent(object sender, EventArgs e)
        {
            CommandSingleton.Instance.NextCommand.Execute();
        }

        #endregion
    }
}