using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Modularity;
using XIMALAYA.PCDesktop.Cache;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Player;

namespace XIMALAYA.PCDesktop.Modules.MusicPlayer
{
    /// <summary>
    /// 播放器视图model
    /// </summary>
    [Export]
    public class MusicPlayerViewModel : BaseViewModel, IModule
    {
        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            GlobalDataSingleton.Instance.BassEngine.CurrentSoundUrl = string.Empty;
            GlobalDataSingleton.Instance.BassEngine.PlayOverEvent += BassEngine_PlayOverEvent;
            this.EventAggregator.GetEvent<PlayerEvent>().Subscribe(StartPlay);
        }

        public override void Dispose()
        {
            base.Dispose();
            this.EventAggregator.GetEvent<PlayerEvent>().Unsubscribe(StartPlay);
        }

        void StartPlay(long TrackId)
        {
            if (GlobalDataSingleton.Instance.SoundData != null && GlobalDataSingleton.Instance.SoundData.TrackId == TrackId)
            {
                GlobalDataSingleton.Instance.BassEngine.PlayCommand.Execute();
                return;
            }
            SoundData soundData = SoundCache.Instance[TrackId];
            if (soundData == null) return;
            if (soundData.PlayUrl32 == null && soundData.PlayUrl64 == null) return;

            this.EventAggregator.GetEvent<ChangeCoverEvent>().Publish(soundData.CoverLarge);
            GlobalDataSingleton.Instance.SoundData = soundData;
            GlobalDataSingleton.Instance.BassEngine.OpenUrlAsync(soundData.PlayUrl64 == null ? soundData.PlayUrl32 : soundData.PlayUrl64);
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