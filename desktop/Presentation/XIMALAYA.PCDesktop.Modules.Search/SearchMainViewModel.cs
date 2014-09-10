using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Prism.Commands;
using XIMALAYA.PCDesktop.Cache;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Core.Models.Search;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Core.Models.User;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Modules.Search
{
    /// <summary>
    /// 搜索页viewmodel
    /// </summary>
    [Export, PartCreationPolicy(CreationPolicy.Shared)]
    public class SearchMainViewModel : BaseViewModel
    {
        #region 字段

        private string _CurrentUserSort;
        private string _CurrentAlbumSort;
        private string _CurrentSoundSort;
        private MetroTabItem _CurrentTab;
        private string _ConditionString = string.Empty;
        private GridLength _Width1;
        private GridLength _Width;
        private GridLength _WidthUser;

        #endregion

        #region 属性

        /// <summary>
        /// 搜索接口
        /// </summary>
        [Import]
        private ISearchService SearchService { get; set; }
        /// <summary>
        /// 当前的tab
        /// </summary>
        public MetroTabItem CurrentTab
        {
            get
            {
                return _CurrentTab;
            }
            set
            {
                if (value != _CurrentTab)
                {
                    _CurrentTab = value;
                    this.OnChangeCurrentTab();
                }
            }
        }
        /// <summary>
        /// 搜索用户条件
        /// </summary>
        public string CurrentUserSort
        {
            get
            {
                return _CurrentUserSort;
            }
            set
            {
                if (value != _CurrentUserSort)
                {
                    _CurrentUserSort = value;
                    this.RaisePropertyChanged(() => this.CurrentUserSort);
                    this.OnChangeUserSort();
                }
            }
        }
        /// <summary>
        /// 搜索专辑条件
        /// </summary>
        public string CurrentAlbumSort
        {
            get
            {
                return _CurrentAlbumSort;
            }
            set
            {
                if (value != _CurrentAlbumSort)
                {
                    _CurrentAlbumSort = value;
                    this.RaisePropertyChanged(() => this.CurrentAlbumSort);
                    this.OnChangeAlbumSort();
                }
            }
        }
        /// <summary>
        /// 搜索声音条件
        /// </summary>
        public string CurrentSoundSort
        {
            get
            {
                return _CurrentSoundSort;
            }
            set
            {
                if (value != _CurrentSoundSort)
                {
                    _CurrentSoundSort = value;
                    this.RaisePropertyChanged(() => this.CurrentSoundSort);
                    this.OnChangeSoundSort();
                }
            }
        }
        /// <summary>
        /// 参数
        /// </summary>
        private SearchParam Param { get; set; }
        /// <summary>
        /// 搜索字符串
        /// </summary>
        public string ConditionString
        {
            get
            {
                return _ConditionString;
            }
            set
            {
                if (value != _ConditionString)
                {
                    _ConditionString = value;
                    this.CurrentPage = 0;
                    this.CanResearch = true;
                    this.SearchCommand.RaiseCanExecuteChanged();
                    this.RaisePropertyChanged(() => this.ConditionString);
                }
            }
        }
        /// <summary>
        /// 列的宽度
        /// </summary>
        public GridLength WidthSound
        {
            get
            {
                return _Width;
            }
            set
            {
                if (value != _Width)
                {
                    _Width = value;
                    this.RaisePropertyChanged(() => this.WidthSound);
                }
            }
        }
        /// <summary>
        /// 列的宽度
        /// </summary>
        public GridLength WidthAlbum
        {
            get
            {
                return _Width1;
            }
            set
            {
                if (value != _Width1)
                {
                    _Width1 = value;
                    this.RaisePropertyChanged(() => this.WidthAlbum);
                }
            }
        }
        /// <summary>
        /// 列的宽度
        /// </summary>
        public GridLength WidthUser
        {
            get
            {
                return _WidthUser;
            }
            set
            {
                if (value != _WidthUser)
                {
                    _WidthUser = value;
                    this.RaisePropertyChanged(() => this.WidthUser);
                }
            }
        }
        /// <summary>
        /// 专辑数据
        /// </summary>
        public ObservableCollection<AlbumData> AlbumDatas { get; private set; }
        /// <summary>
        /// 声音数据
        /// </summary>
        public ObservableCollection<SoundData> SoundDatas { get; private set; }
        /// <summary>
        /// 用户数据
        /// </summary>
        public ObservableCollection<UserData> UserDatas { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        private bool CanResearch { get; set; }

        #endregion

        #region 命令

        /// <summary>
        /// 搜索命令
        /// </summary>
        public DelegateCommand SearchCommand { get; set; }

        #endregion

        #region 事件切换

        /// <summary>
        /// 切换tab事件
        /// </summary>
        private void OnChangeCurrentTab()
        {
            var tag = this.CurrentTab.Tag;

            this.Param.Scope = (TagType)Enum.Parse(typeof(TagType), tag.ToString());

            this.WidthAlbum = new GridLength(0);
            this.WidthUser = new GridLength(0);
            this.WidthSound = new GridLength(0);
            this.IsNextPageVisibled = true;
            switch (this.Param.Scope)
            {
                case TagType.all:
                    this.WidthAlbum = new GridLength(33.3, GridUnitType.Star);
                    this.WidthUser = new GridLength(33.3, GridUnitType.Star);
                    this.WidthSound = new GridLength(33.3, GridUnitType.Star);
                    this.Total = 0;
                    this.IsNextPageVisibled = false;
                    break;
                case TagType.user:
                    this.WidthUser = new GridLength(100, GridUnitType.Star);
                    break;
                case TagType.album:
                    this.WidthAlbum = new GridLength(100, GridUnitType.Star);
                    break;
                case TagType.voice:
                    this.WidthSound = new GridLength(100, GridUnitType.Star);
                    break;
            }

            this.GetData(true);
        }
        /// <summary>
        /// 切换声音搜索条件事件
        /// </summary>
        private void OnChangeSoundSort()
        {
            this.GetData(true);
        }
        /// <summary>
        /// 切换专辑搜索条件事件
        /// </summary>
        private void OnChangeAlbumSort()
        {
            this.GetData(true);
        }
        /// <summary>
        /// 切换用户搜索条件事件
        /// </summary>
        private void OnChangeUserSort()
        {
            this.GetData(true);
        }

        #endregion

        #region BaseViewModel方法

        protected override void GetData(bool isClear)
        {
            if (this.SearchService == null)
            {
                throw new NullReferenceException();
            }
            if (this.ConditionString.Trim() == string.Empty)
            {
                return;
            }

            this.CanResearch = false;
            this.Param.Page = this.CurrentPage;
            base.GetData(isClear);
            switch (this.Param.Scope)
            {
                case TagType.all:
                    this.SearchAllData();
                    break;
                case TagType.voice:
                    this.SearchSoundData();
                    break;
                case TagType.album:
                    this.SearchAlbumData();
                    break;
                case TagType.user:
                    this.SearchUserData();
                    break;
            }
        }
        private void SearchAllData()
        {
            this.Param.Sort = null;
            this.SearchService.GetAllData((res) =>
            {
                var result = res as SearchResult;

                Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                {
                    this.IsWaiting = false;
                    if (result == null) return;
                    if (result.Ret == 0)
                    {
                        this.UserDatas.Clear();
                        this.AlbumDatas.Clear();
                        this.SoundDatas.Clear();
                        foreach (var sound in result.SoundData.Sounds)
                        {
                            sound.Duration *= 1000;
                            this.SoundDatas.Add(sound);
                        }
                        SoundCache.Instance.SetData(result.SoundData.Sounds);
                        foreach (var album in result.AlbumData.Albums)
                        {
                            this.AlbumDatas.Add(album);
                        }
                        foreach (var user in result.UserData.Users)
                        {
                            this.UserDatas.Add(user);
                        }
                    }
                    else
                    {
                        DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", result.Message);
                    }
                }));

            }, this.Param);
        }
        private void SearchSoundData()
        {
            this.Param.Sort = this.CurrentSoundSort;
            this.SearchService.GetSoundData((res) =>
            {
                var result = res as SearchSoundResult;

                Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                {
                    this.IsWaiting = false;
                    if (result == null) return;
                    if (result.Ret == 0)
                    {
                        this.Total = result.TotalCount;
                        if (this.Total > 0)
                        {
                            this.SoundDatas.Clear();
                            foreach (var sound in result.Sounds)
                            {
                                sound.Duration *= 1000;
                                this.SoundDatas.Add(sound);
                            }
                            SoundCache.Instance.SetData(result.Sounds);
                        }
                        else
                        {
                            DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", "未搜索到相关数据！");
                        }
                    }
                    else
                    {
                        DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", result.Message);
                    }
                }));
            }, this.Param);
        }
        private void SearchAlbumData()
        {
            this.Param.Sort = this.CurrentAlbumSort;
            this.SearchService.GetAlbumData((res) =>
            {
                var result = res as SearchAlbumResult;

                Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                {
                    this.IsWaiting = false;
                    if (result == null) return;
                    if (result.Ret == 0)
                    {
                        this.Total = result.TotalCount;
                        if (this.Total > 0)
                        {
                            this.AlbumDatas.Clear();
                            foreach (var album in result.Albums)
                            {
                                this.AlbumDatas.Add(album);
                            }
                        }
                        else
                        {
                            DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", "未搜索到相关数据！");
                        }

                    }
                    else
                    {
                        DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", result.Message);
                    }
                }));
            }, this.Param);
        }
        private void SearchUserData()
        {
            this.Param.Sort = this.CurrentUserSort;
            this.SearchService.GetUserData((res) =>
            {
                var result = res as SearchUserResult;

                Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                {
                    this.IsWaiting = false;
                    if (result == null) return;
                    if (result.Ret == 0)
                    {
                        this.Total = result.TotalCount;
                        if (this.Total > 0)
                        {
                            this.UserDatas.Clear();
                            foreach (var user in result.Users)
                            {
                                this.UserDatas.Add(user);
                            }
                        }
                        else
                        {
                            DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", "未搜索到相关数据！");
                        }
                    }
                    else
                    {
                        DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", result.Message);
                    }
                }));
            }, this.Param);
        }

        #endregion

        #region 构造

        /// <summary>
        /// 构造
        /// </summary>
        public SearchMainViewModel()
            : base()
        {
            this.Param = new SearchParam
            {
                PerPage = 30
            };
            this.CanResearch = true;
            this.PageSize = (int)this.Param.PerPage;
            this.IsNextPageVisibled = false;

            this.SearchCommand = new DelegateCommand(() =>
            {
                this.Param.Condition = this.ConditionString;
                this.CurrentPage = 1;
            }, () =>
            {
                return this.ConditionString.Trim() != string.Empty && this.CanResearch;
            });
            this.SoundDatas = new ObservableCollection<SoundData>();
            this.AlbumDatas = new ObservableCollection<AlbumData>();
            this.UserDatas = new ObservableCollection<UserData>();
        }

        #endregion
    }
}
