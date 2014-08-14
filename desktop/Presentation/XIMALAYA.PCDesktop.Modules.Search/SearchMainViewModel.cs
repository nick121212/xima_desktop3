using System;
using System.ComponentModel.Composition;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Prism.Commands;
using XIMALAYA.PCDesktop.Core.Models.Search;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Modules.Search
{
    /// <summary>
    /// 搜索页viewmodel
    /// </summary>
    [Export]
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
            switch (this.Param.Scope)
            {
                case TagType.all:
                    this.WidthAlbum = new GridLength(33.3, GridUnitType.Star);
                    this.WidthUser = new GridLength(33.3, GridUnitType.Star);
                    this.WidthSound = new GridLength(33.3, GridUnitType.Star);
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

            //if (isClear)
            //{
            //    if (this.CurrentPage !=1)
            //    {
            //        this.CurrentPage = 1;
            //        return;
            //    }
            //}
            this.IsNextPageVisibled = false;
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
                        DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", "搜索成功！");
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
                    this.IsNextPageVisibled = true;
                    this.IsWaiting = false;
                    if (result == null) return;
                    if (result.Ret == 0)
                    {
                        this.Total = result.TotalCount;
                        DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", "搜索成功！");
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
                    this.IsNextPageVisibled = true;
                    this.IsWaiting = false;
                    if (result == null) return;
                    if (result.Ret == 0)
                    {
                        this.Total = result.TotalCount;
                        DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", "搜索成功！");
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
                    this.IsNextPageVisibled = true;
                    this.IsWaiting = false;
                    if (result == null) return;
                    if (result.Ret == 0)
                    {
                        this.Total = result.TotalCount;
                        DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", "搜索成功！");
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
            this.PageSize = (int)this.Param.PerPage;
            this.IsNextPageVisibled = false;

            this.SearchCommand = new DelegateCommand(() =>
            {
                this.Param.Condition = this.ConditionString;
                this.CurrentPage = 1;
            }, () =>
            {
                return this.ConditionString.Trim() != string.Empty;
            });
        }

        #endregion
    }
}
