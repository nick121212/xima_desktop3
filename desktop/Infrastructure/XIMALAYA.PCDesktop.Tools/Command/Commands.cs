using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Core.Models.Share;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Tools.Extension;
using XIMALAYA.PCDesktop.Tools.Player;
using XIMALAYA.PCDesktop.Tools.Untils;
using XIMALAYA.PCDesktop.Untils;
using XIMALAYA.PCDesktop.Cache;
using System.Collections;

namespace XIMALAYA.PCDesktop.Tools
{
    /// <summary>
    /// 全局命令
    /// </summary>
    public class CommandBaseSingleton : NotificationObject
    {
        #region 属性

        /// <summary>
        /// 事件管理器
        /// </summary>
        private IEventAggregator EventAggregator { get; set; }

        #endregion

        #region 命令

        /// <summary>
        /// 播放命令
        /// </summary>
        public DelegateCommand<long?> PlaySoundCommand { get; set; }
        /// <summary>
        /// 播放命令
        /// </summary>
        public DelegateCommand<Control> PlaySound1Command { get; set; }
        /// <summary>
        /// 专辑详情页命令
        /// </summary>
        public DelegateCommand<long?> AlbumDetailCommand { get; set; }
        /// <summary>
        /// 用户详情页命令
        /// </summary>
        public DelegateCommand<long?> UserDetailCommand { get; set; }
        /// <summary>
        /// 上一首命令
        /// </summary>
        public DelegateCommand PrevCommand { get; set; }
        /// <summary>
        /// 下一首命令
        /// </summary>
        public DelegateCommand NextCommand { get; set; }
        /// <summary>
        /// 热门专辑命令
        /// </summary>
        public DelegateCommand HotAlbumListCommand { get; set; }
        /// <summary>
        /// 声音详情页命令
        /// </summary>
        public DelegateCommand<long?> ShowSoundDetailCommand { get; set; }
        /// <summary>
        /// 内容切换的命令
        /// </summary>
        public DelegateCommand<string> ShowContentCommand { get; set; }
        /// <summary>
        /// 跳转网页
        /// </summary>
        public DelegateCommand<string> RedirectCommand { get; set; }
        /// <summary>
        /// 分享命令
        /// </summary>
        public DelegateCommand<object> ShareCommand { get; set; }
        /// <summary>
        /// 提高音量
        /// </summary>
        public DelegateCommand VolumeUpCommand { get; set; }
        /// <summary>
        /// 减小音量
        /// </summary>
        public DelegateCommand VolumeDownCommand { get; set; }
        /// <summary>
        /// 退出程序命令
        /// </summary>
        public DelegateCommand CloseCommand { get; set; }
        /// <summary>
        /// 最小化命令
        /// </summary>
        public DelegateCommand MinisizeCommand { get; set; }
        /// <summary>
        /// 最大化
        /// </summary>
        public DelegateCommand MaxisizeCommand { get; set; }
        /// <summary>
        /// 添加到跳转列表
        /// </summary>
        public DelegateCommand<JumpListEventArgs> AddToJumpListCommand { get; set; }

        public DelegateCommand<int?> MutiUserCommand { get; set; }
        public DelegateCommand<int?> MutiAlbumCommand { get; set; }
        public DelegateCommand<int?> MutiSoundCommand { get; set; }

        #endregion

        #region 构造

        private CommandBaseSingleton()
        {
            this.EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            //播放声音命令，获取列表
            this.PlaySound1Command = new DelegateCommand<Control>(con =>
            {
                var soundData = con.DataContext as SoundData;
                SoundData soundCol = null;

                if (soundData == null) return;

                for (int i = 0; i < GlobalDataSingleton.Instance.SoundCollection.Count; i++)
                {
                    soundCol = GlobalDataSingleton.Instance.SoundCollection[i] as SoundData;
                    if (soundCol.TrackId == soundData.TrackId)
                    {
                        break;
                    }
                    soundCol = null;
                }
                if (soundCol == null)
                {
                    GlobalDataSingleton.Instance.SoundCollection.Clear();
                    if (con.GetType() == typeof(ListBoxItem))
                    {
                        var listBox = VisualTreeHelperExtensions.FindAncestor<ListBox>(con);
                        var sounds = new SoundData[listBox.Items.Count];

                        listBox.Items.CopyTo(sounds, 0);
                        sounds.ToList().ForEach(sound => GlobalDataSingleton.Instance.SoundCollection.Add(sound));
                    }
                    else
                    {
                        var dataGrid = VisualTreeHelperExtensions.FindAncestor<DataGrid>(con);
                        var sounds = new SoundData[dataGrid.Items.Count];

                        dataGrid.Items.CopyTo(sounds, 0);
                        sounds.ToList().ForEach(sound => GlobalDataSingleton.Instance.SoundCollection.Add(sound));
                    }
                }

                if (GlobalDataSingleton.Instance.SoundCollection.MoveCurrentTo(soundData))
                {
                    this.PlaySoundCommand.Execute(((SoundData)GlobalDataSingleton.Instance.SoundCollection.CurrentItem).TrackId);
                }
            });
            //上一首命令
            this.PrevCommand = new DelegateCommand(() =>
            {
                if (GlobalDataSingleton.Instance.SoundCollection == null) return;
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    if (GlobalDataSingleton.Instance.SoundCollection.MoveCurrentToPrevious())
                    {
                        this.PlaySoundCommand.Execute(((SoundData)GlobalDataSingleton.Instance.SoundCollection.CurrentItem).TrackId);
                    }
                    else if (GlobalDataSingleton.Instance.SoundCollection.IsCurrentBeforeFirst)
                    {
                        GlobalDataSingleton.Instance.SoundCollection.MoveCurrentToLast();
                        this.PlaySoundCommand.Execute(((SoundData)GlobalDataSingleton.Instance.SoundCollection.CurrentItem).TrackId);
                    }
                }), null);
            }, () =>
            {
                if (GlobalDataSingleton.Instance.SoundCollection == null || GlobalDataSingleton.Instance.PlayMode == true || GlobalDataSingleton.Instance.SoundCollection.Count <= 1) return false;
                //列表循环
                if (GlobalDataSingleton.Instance.PlayMode == null)
                {
                    return true;
                }
                return GlobalDataSingleton.Instance.SoundCollection.CurrentPosition > 0;
            });
            //下一首命令
            this.NextCommand = new DelegateCommand(() =>
            {
                if (GlobalDataSingleton.Instance.SoundCollection == null) return;
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    if (GlobalDataSingleton.Instance.SoundCollection.MoveCurrentToNext())
                    {
                        this.PlaySoundCommand.Execute(((SoundData)GlobalDataSingleton.Instance.SoundCollection.CurrentItem).TrackId);
                    }
                    else if (GlobalDataSingleton.Instance.SoundCollection.IsCurrentAfterLast)
                    {
                        GlobalDataSingleton.Instance.SoundCollection.MoveCurrentToFirst();
                        this.PlaySoundCommand.Execute(((SoundData)GlobalDataSingleton.Instance.SoundCollection.CurrentItem).TrackId);
                    }
                }), null);
            }, () =>
            {
                if (GlobalDataSingleton.Instance.SoundCollection == null || GlobalDataSingleton.Instance.SoundCollection.CurrentPosition == -1 || GlobalDataSingleton.Instance.PlayMode == true || GlobalDataSingleton.Instance.SoundCollection.Count <= 1) return false;
                //列表循环
                if (GlobalDataSingleton.Instance.PlayMode == null)
                {
                    return true;
                }
                return GlobalDataSingleton.Instance.SoundCollection.CurrentPosition < GlobalDataSingleton.Instance.SoundCollection.Count - 1;
            });
            //播放声音，无列表
            this.PlaySoundCommand = new DelegateCommand<long?>(trackID =>
            {
                if (trackID == null || !trackID.HasValue) return;

                SoundData soundData = SoundCache.Instance[(long)trackID];
                SoundData soundCol = null;

                if (soundData != null)
                {
                    for (int i = 0; i < GlobalDataSingleton.Instance.SoundCollection.Count; i++)
                    {
                        soundCol = GlobalDataSingleton.Instance.SoundCollection[i] as SoundData;
                        if (soundCol.TrackId == soundData.TrackId)
                        {
                            break;
                        }
                        soundCol = null;
                    }

                    if (soundCol == null)
                    {
                        GlobalDataSingleton.Instance.SoundCollection.Clear();
                        GlobalDataSingleton.Instance.SoundCollection.Add(soundData);
                        this.RaisePropertyChanged(() => GlobalDataSingleton.Instance.SoundCollection);
                    }
                }

                this.PrevCommand.RaiseCanExecuteChanged();
                this.NextCommand.RaiseCanExecuteChanged();
                this.EventAggregator.GetEvent<ModulesManagerEvent>().Publish(new ModuleInfoArgument()
                {
                    ModuleName = WellKnownModuleNames.MusicPlayerModule,
                    Action = new Action(() =>
                    {
                        this.EventAggregator.GetEvent<PlayerEvent>().Publish((long)trackID);
                    })
                });
            }, (trackID) =>
            {
                return true;
            });
            //专辑详情命令
            this.AlbumDetailCommand = new DelegateCommand<long?>(albumID =>
            {
                if (albumID == null) return;

                this.EventAggregator.GetEvent<AlbumDetailEvent<long>>().Publish((long)albumID);
            });
            //热门专辑
            this.HotAlbumListCommand = new DelegateCommand(() =>
            {
                this.EventAggregator.GetEvent<AlbumListEvent<TagEventArgument>>().Publish(new TagEventArgument()
                {
                    Category = "all",
                    Title = "热门",
                    TagName = " ",
                });
            });
            //声音详情页
            this.ShowSoundDetailCommand = new DelegateCommand<long?>(trackId =>
            {
                if (trackId.HasValue)
                {
                    this.EventAggregator.GetEvent<SoundDetailEvent<long>>().Publish((long)trackId);
                }
            });
            //用户详情页
            this.UserDetailCommand = new DelegateCommand<long?>(userId =>
            {
                if (userId.HasValue)
                {
                    this.EventAggregator.GetEvent<UserEvent<long>>().Publish((long)userId);
                }
            });
            //内容切换的命令，现在有搜索和发现切换
            this.ShowContentCommand = new DelegateCommand<string>(s =>
            {
                this.EventAggregator.GetEvent<ContentChangeEvent>().Publish(s);
            });
            //直接跳转浏览器
            this.RedirectCommand = new DelegateCommand<string>(s =>
            {
                try
                {
                    System.Diagnostics.Process.Start(s);
                }
                catch
                {

                }
            });
            //分享命令
            this.ShareCommand = new DelegateCommand<object>((param) =>
            {
                if (param != null)
                {
                    if (param.GetType() != typeof(object[]))
                    {
                        return;
                    }
                    object[] arr = param as object[];

                    if (arr.Length != 3) return;
                    if (arr[0].GetType() != typeof(Sites)) return;
                    if (arr[1].GetType() != typeof(ShareType)) return;
                    if (arr[2].GetType() != typeof(long)) return;

                    this.EventAggregator.GetEvent<ShareEvent<ShareEventArgument>>().Publish(new ShareEventArgument
                    {
                        ID = (long)arr[2],
                        Site = (Sites)arr[0],
                        ShareType = (ShareType)arr[1]
                    });
                }
            });
            //提高音量命令
            this.VolumeUpCommand = new DelegateCommand(() =>
            {
                if (GlobalDataSingleton.Instance.BassEngine.Volume + 0.1F < 1)
                {
                    GlobalDataSingleton.Instance.BassEngine.Volume += 0.1F;
                }
                else
                {
                    GlobalDataSingleton.Instance.BassEngine.Volume = 1;
                }
            }, () =>
            {
                return GlobalDataSingleton.Instance.BassEngine.Volume < 1;
            });
            //提高音量命令
            this.VolumeDownCommand = new DelegateCommand(() =>
            {
                if (GlobalDataSingleton.Instance.BassEngine.Volume - 0.1F > 0)
                {
                    GlobalDataSingleton.Instance.BassEngine.Volume -= 0.1F;
                }
                else
                {
                    GlobalDataSingleton.Instance.BassEngine.Volume = 0;
                }
            }, () =>
            {
                return GlobalDataSingleton.Instance.BassEngine.Volume > 0;
            });
            //关闭窗体命令
            this.CloseCommand = new DelegateCommand(() =>
            {
                var parentWindow = Application.Current.MainWindow;
                if (parentWindow == null)
                    return;
                GlobalDataSingleton.Instance.IsExit = true;
                parentWindow.Show();
                parentWindow.Activate();
                if (parentWindow.WindowState == WindowState.Minimized)
                {
                    parentWindow.WindowState = WindowState.Normal;
                    parentWindow.Activate();
                }
                parentWindow.Close();
            });
            //最小化命令
            this.MinisizeCommand = new DelegateCommand(() =>
            {
                var parentWindow = Application.Current.MainWindow;
                if (parentWindow == null)
                    return;

                parentWindow.Dispatcher.Invoke(new Action(() =>
                {
                    if (parentWindow.WindowState == WindowState.Normal)
                    {
                        parentWindow.WindowState = WindowState.Minimized;
                        //parentWindow.Hide();
                    }
                    else
                    {
                        parentWindow.Show();
                        parentWindow.Activate();
                        parentWindow.WindowState = WindowState.Normal;
                    }
                }));
            });
            //最大化命令
            this.MaxisizeCommand = new DelegateCommand(() =>
            {
                var parentWindow = Application.Current.MainWindow;
                if (parentWindow == null)
                    return;

                if (parentWindow.WindowState == WindowState.Normal)
                {
                    parentWindow.Show();
                    parentWindow.Activate();
                    SystemCommands.MaximizeWindow(parentWindow);
                }
                else
                {
                    parentWindow.Show();
                    SystemCommands.RestoreWindow(parentWindow);
                }
            });
            //添加到跳转列表命令
            this.AddToJumpListCommand = new DelegateCommand<JumpListEventArgs>((e) =>
            {
                this.EventAggregator.GetEvent<JumplistEvent<JumpListEventArgs>>().Publish(e);
            });
            this.MutiUserCommand = new DelegateCommand<int?>((id) =>
            {
                if (id.HasValue)
                {
                    this.EventAggregator.GetEvent<UserEvent<int>>().Publish((int)id);
                }
            });
            this.MutiAlbumCommand = new DelegateCommand<int?>((id) =>
            {
                if (id.HasValue)
                {
                    this.EventAggregator.GetEvent<AlbumDetailEvent<int>>().Publish((int)id);
                }
            });
            this.MutiSoundCommand = new DelegateCommand<int?>((id) =>
            {
                if (id.HasValue)
                {
                    this.EventAggregator.GetEvent<SoundDetailEvent<int>>().Publish((int)id);
                }
            });
        }

        #endregion
    }
}
