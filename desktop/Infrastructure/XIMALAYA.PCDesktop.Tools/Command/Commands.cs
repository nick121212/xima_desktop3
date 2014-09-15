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
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Tools.Extension;
using XIMALAYA.PCDesktop.Tools.Player;
using XIMALAYA.PCDesktop.Tools.Untils;
using XIMALAYA.PCDesktop.Untils;
using XIMALAYA.PCDesktop.Cache;

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
        /// <summary>
        /// 分享服务
        /// </summary>
        private IShareService ShareService { get; set; }
       
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

        #endregion

        #region 构造

        private CommandBaseSingleton()
        {
            this.EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            this.ShareService = ServiceLocator.Current.GetInstance<IShareService>();
            

            //播放声音命令，获取列表
            this.PlaySound1Command = new DelegateCommand<Control>(con =>
            {
                var soundData = con.DataContext as SoundData;

                if (soundData == null) return;
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
                    else if (GlobalDataSingleton.Instance.SoundCollection.IsCurrentAfterLast)
                    {
                        GlobalDataSingleton.Instance.SoundCollection.MoveCurrentToLast();
                        this.PlaySoundCommand.Execute(((SoundData)GlobalDataSingleton.Instance.SoundCollection.CurrentItem).TrackId);
                    }
                }), null);
            }, () =>
            {
                if (GlobalDataSingleton.Instance.SoundCollection == null) return false;

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
                    else if (GlobalDataSingleton.Instance.SoundCollection.IsCurrentBeforeFirst)
                    {
                        GlobalDataSingleton.Instance.SoundCollection.MoveCurrentToFirst();
                        this.PlaySoundCommand.Execute(((SoundData)GlobalDataSingleton.Instance.SoundCollection.CurrentItem).TrackId);
                    }
                }), null);
            }, () =>
            {
                if (GlobalDataSingleton.Instance.SoundCollection == null) return false;

                return GlobalDataSingleton.Instance.SoundCollection.CurrentPosition < GlobalDataSingleton.Instance.SoundCollection.Count - 1;
            });
            //播放声音，无列表
            this.PlaySoundCommand = new DelegateCommand<long?>(trackID =>
            {
                if (trackID == null || !trackID.HasValue) return;

                SoundData soundData = SoundCache.Instance[(long)trackID];

                if (soundData != null)
                {
                    if (!GlobalDataSingleton.Instance.SoundCollection.Contains(soundData))
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
            //内容切换的命令，现在有搜索和发现切换
            this.ShowContentCommand = new DelegateCommand<string>(s =>
            {
                this.EventAggregator.GetEvent<ContentChangeEvent>().Publish(s);
            });

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

                    switch ((ShareType)arr[1])
                    {
                        case ShareType.Track:
                            this.GetShareSoundLink((Sites)arr[0], (long)arr[2]);
                            break;
                        case ShareType.Album:
                            this.GetShareAlbumLink((Sites)arr[0], (long)arr[2]);
                            break;
                    }
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

            this.CloseCommand = new DelegateCommand(() =>
            {
                var parentWindow = Application.Current.MainWindow;
                if (parentWindow == null)
                    return;

                parentWindow.Close();
            });
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
                    }
                    else
                    {
                        parentWindow.WindowState = WindowState.Normal;
                    }
                }));
            });
            this.MaxisizeCommand = new DelegateCommand(() =>
            {
                var parentWindow = Application.Current.MainWindow;
                if (parentWindow == null)
                    return;

                if (parentWindow.WindowState == WindowState.Normal)
                {
                    SystemCommands.MaximizeWindow(parentWindow);
                }
                else
                {
                    SystemCommands.RestoreWindow(parentWindow);
                }
            });
        }

        #endregion

        #region 方法

        /// <summary>
        /// 分享专辑
        /// </summary>
        /// <param name="Site"></param>
        /// <param name="AlbumID"></param>
        private void GetShareAlbumLink(Sites Site, long AlbumID)
        {
            if (this.ShareService == null) return;
            if (AlbumID <= 0) return;

            this.ShareService.GetData(res =>
            {
                var result = res as ShareResult;

                if (result.Ret == 0)
                {
                    this.DoShare(Site, result.PicUrl, result.Content, result.Url);
                }
            }, new ShareParam
            {
                AlbumId = AlbumID,
                tpName = "qq"
            }, TagType.album);

        }
        /// <summary>
        /// 分享声音
        /// </summary>
        /// <param name="Site"></param>
        /// <param name="SoundID"></param>
        private void GetShareSoundLink(Sites Site, long SoundID)
        {
            if (this.ShareService == null) return;
            if (SoundID <= 0) return;

            this.ShareService.GetData(res =>
            {
                var result = res as ShareResult;

                if (result.Ret == 0)
                {
                    this.DoShare(Site, result.PicUrl, result.Content, result.Url);
                }
            }, new ShareParam
            {
                TrackId = SoundID,
                tpName = "qq"
            }, TagType.sound);
        }
        /// <summary>
        /// 跳转分享链接
        /// </summary>
        /// <param name="Site"></param>
        /// <param name="PicUrl"></param>
        /// <param name="Content"></param>
        /// <param name="url"></param>
        private void DoShare(Sites Site, string PicUrl, string Content, string url)
        {
            Parameters parameters = new Parameters(true);

            switch (Site)
            {
                case Sites.Douban:
                    parameters["name"] = Content;
                    parameters["href"] = url;
                    parameters["image"] = PicUrl;
                    parameters["text"] = string.Empty;
                    parameters["desc"] = string.Empty;
                    parameters["apikey"] = "0c2e1df44f97c4eb248a59dceec74ec1";
                    url = string.Format("http://shuo.douban.com/!service/share?{0}", parameters);
                    break;
                case Sites.Weibo:
                    parameters["appkey"] = "1075899032";
                    parameters["url"] = url;
                    parameters["title"] = Content;
                    parameters["content"] = "utf-8";
                    parameters["pic"] = PicUrl;
                    url = string.Format("http://service.t.sina.com.cn/share/share.php?{0}", parameters);
                    break;
                case Sites.Kaixin:
                    parameters["rurl"] = url;
                    parameters["rcontent"] = "";
                    parameters["rtitle"] = Content;
                    url = string.Format("http://www.kaixin001.com/repaste/bshare.php?{0}", parameters);
                    break;
                case Sites.Renren:
                    parameters["resourceUrl"] = url;
                    parameters["title"] = Content;
                    parameters["pic"] = PicUrl;
                    parameters["description"] = string.Empty;
                    parameters["charset"] = "utf-8";
                    url = string.Format("http://widget.renren.com/dialog/share?{0}", parameters);
                    break;
                case Sites.TencentWeibo:
                    parameters["url"] = url;
                    parameters["title"] = Content;
                    parameters["site"] = "http://www.kfstorm.com/doubanfm";
                    parameters["pic"] = PicUrl;
                    parameters["appkey"] = "801098586";
                    url = string.Format("http://v.t.qq.com/share/share.php?{0}", parameters);
                    break;
                case Sites.Facebook:
                    parameters["u"] = url;
                    parameters["t"] = Content;
                    url = string.Format("http://www.facebook.com/sharer.php?{0}", parameters);
                    break;
                case Sites.Twitter:
                    parameters["status"] = Content + " " + url;
                    url = string.Format("http://twitter.com/home?{0}", parameters);
                    break;
                case Sites.Qzone:
                    parameters["url"] = url;
                    parameters["title"] = Content;
                    url = string.Format("http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_onekey?{0}", parameters);
                    break;
                default:
                    break;
            }

            System.Diagnostics.Process.Start(url);
        }

        #endregion
    }
}
