using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Core.Models.Share;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Tools.Extension;
using XIMALAYA.PCDesktop.Tools.Player;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Tools
{
    /// <summary>
    /// 全局命令
    /// </summary>
    public class CommandBaseSingleton : NotificationObject
    {
        #region 字段

        private ItemCollection _SoundCollection;
        private string _TrackTitle;
        private string _TrackImage;
        private long _TrackID;
        private SoundData _SoundData;

        #endregion

        #region 属性

        /// <summary>
        /// 事件管理器
        /// </summary>
        private IEventAggregator EventAggregator { get; set; }
        /// <summary>
        /// 分享服务
        /// </summary>
        private IShareService ShareService { get; set; }
        /// <summary>
        /// 当前播放的声音ID
        /// </summary>
        public long TrackID
        {
            get
            {
                return _TrackID;
            }
            set
            {
                if (value != _TrackID)
                {
                    _TrackID = value;
                    this.RaisePropertyChanged(() => this.TrackID);
                    this.PlaySoundCommand.RaiseCanExecuteChanged();
                }
            }
        }
        /// <summary>
        /// 当前播放的声音Title
        /// </summary>
        public string TrackTitle
        {
            get
            {
                return _TrackTitle;
            }
            set
            {
                if (value != _TrackTitle)
                {
                    _TrackTitle = value;
                    this.RaisePropertyChanged(() => this.TrackTitle);
                }
            }
        }
        /// <summary>
        /// 全局播放
        /// </summary>
        public BassEngine BassEngine
        {
            get
            {
                return PlayerSingleton.Instance;
            }
        }
        /// <summary>
        /// 当前点击播放的声音行
        /// </summary>
        public Control CurrentPlayControl { get; set; }
        /// <summary>
        /// 当前声音播放列表
        /// </summary>
        public ItemCollection SoundCollection
        {
            get
            {
                return _SoundCollection;
            }
            set
            {
                if (value != _SoundCollection)
                {
                    _SoundCollection = value;
                    this.RaisePropertyChanged(() => this.SoundCollection);
                }
            }
        }
        /// <summary>
        /// 声音图片
        /// </summary>
        public string TrackImage
        {
            get
            {
                return _TrackImage;
            }
            set
            {
                if (value != _TrackImage)
                {
                    _TrackImage = value;
                    this.RaisePropertyChanged(() => this.TrackImage);
                }
            }
        }
        /// <summary>
        /// 当前播放的声音
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

        #endregion

        #region 构造

        private CommandBaseSingleton()
        {
            this.EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            this.ShareService = ServiceLocator.Current.GetInstance<IShareService>();
            this.SoundCollection = new ListBox().Items;

            //播放声音命令，获取列表
            this.PlaySound1Command = new DelegateCommand<Control>(con =>
            {
                var soundData = con.DataContext as SoundData;

                if (soundData == null) return;

                this.SoundCollection.Clear();
                if (con.GetType() == typeof(ListBoxItem))
                {
                    var listBox = VisualTreeHelperExtensions.FindAncestor<ListBox>(con);
                    var sounds = new SoundData[listBox.Items.Count];

                    listBox.Items.CopyTo(sounds, 0);
                    sounds.ToList().ForEach(sound => this.SoundCollection.Add(sound));
                }
                else
                {
                    var dataGrid = VisualTreeHelperExtensions.FindAncestor<DataGrid>(con);
                    var sounds = new SoundData[dataGrid.Items.Count];

                    dataGrid.Items.CopyTo(sounds, 0);
                    sounds.ToList().ForEach(sound => this.SoundCollection.Add(sound));
                }

                if (this.SoundCollection.MoveCurrentTo(soundData))
                {
                    this.PlaySoundCommand.Execute(((SoundData)this.SoundCollection.CurrentItem).TrackId);
                }
            });
            //上一首命令
            this.PrevCommand = new DelegateCommand(() =>
            {
                if (this.SoundCollection == null) return;
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    if (this.SoundCollection.MoveCurrentToPrevious())
                    {
                        this.PlaySoundCommand.Execute(((SoundData)this.SoundCollection.CurrentItem).TrackId);
                    }
                }), null);
            }, () =>
            {
                if (this.SoundCollection == null) return false;

                return this.SoundCollection.CurrentPosition > 0;
            });
            //下一首命令
            this.NextCommand = new DelegateCommand(() =>
            {
                if (this.SoundCollection == null) return;
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    if (this.SoundCollection.MoveCurrentToNext())
                    {
                        this.PlaySoundCommand.Execute(((SoundData)this.SoundCollection.CurrentItem).TrackId);
                    }
                }), null);
            }, () =>
            {
                if (this.SoundCollection == null) return false;

                return this.SoundCollection.CurrentPosition < this.SoundCollection.Count - 1;
            });
            //播放声音，无列表
            this.PlaySoundCommand = new DelegateCommand<long?>(trackID =>
            {
                if (trackID == null) return;

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
    /// <summary>
    /// 按钮
    /// </summary>
    public class CommandSingleton : Singleton<CommandBaseSingleton> { }
}
