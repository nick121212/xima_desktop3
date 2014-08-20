using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Tools.Extension;
using XIMALAYA.PCDesktop.Tools.Player;

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
        /// 佔位服务
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
        /// 佔位服务
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

        #endregion

        #region 构造

        private CommandBaseSingleton()
        {
            this.EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            this.SoundCollection = new ListBox().Items;
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

            this.AlbumDetailCommand = new DelegateCommand<long?>(albumID =>
            {
                if (albumID == null) return;

                this.EventAggregator.GetEvent<AlbumDetailEvent<long>>().Publish((long)albumID);
            });

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

        }

        #endregion
    }
    /// <summary>
    /// 按钮
    /// </summary>
    public class CommandSingleton : Singleton<CommandBaseSingleton> { }
}
