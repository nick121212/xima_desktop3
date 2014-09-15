using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Core.Models.Tags;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Modules.AlbumModule.Views;

namespace XIMALAYA.PCDesktop.Modules.AlbumModule
{
    /// <summary>
    /// 专辑model
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AlbumViewModel : BaseViewModel
    {
        #region 字段

        private bool _IsShowStatus;
        private ConditionAlbumType _Condition;
        private int _Status;

        #endregion

        #region 属性

        /// <summary>
        /// 专辑数据
        /// </summary>
        public ObservableCollection<AlbumData> Albums { private get; set; }
        /// <summary>
        /// 标签下的专辑服务
        /// </summary>
        [Import]
        private ICategoryTagAlbumsService CategoryTagAlbumsService { get; set; }
        /// <summary>
        /// 当前参数
        /// </summary>
        private CategoryTagAlbumParam Params { get; set; }
        /// <summary>
        /// 状态列表
        /// </summary>
        public List<String> StatusList { get; set; }
        /// <summary>
        /// 显示条件搜索
        /// </summary>
        public bool IsShowStatus
        {
            get
            {
                return _IsShowStatus;
            }
            set
            {
                if (value == _IsShowStatus) return;
                _IsShowStatus = value;
                this.RaisePropertyChanged(() => this.IsShowStatus);

            }
        }
        /// <summary>
        /// 当前选中的条件
        /// </summary>
        public ConditionAlbumType Condition
        {
            get
            {
                return _Condition;
            }
            set
            {
                if (value == _Condition) return;
                _Condition = value;
                this.RaisePropertyChanged(() => this.Condition);
            }
        }
        /// <summary>
        /// 属性描述
        /// </summary>
        public int Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if (value == _Status) return;
                _Status = value;
                this.RaisePropertyChanged(() => this.Status);
                this.OnStatusChanged();
            }
        }

        #endregion

        #region 命令

        /// <summary>
        /// 排序条件
        /// </summary>
        public DelegateCommand<string> ConditionCommand { get; set; }

        #endregion

        #region 事件

        private void OnStatusChanged()
        {
            this.Params.Status = Status;
            this.GetData(true);
        }

        #endregion

        #region 构造

        /// <summary>
        /// 构造
        /// </summary>
        public AlbumViewModel()
        {
            this.Albums = new ObservableCollection<AlbumData>();
            this.ConditionCommand = new DelegateCommand<string>(s =>
            {
                int type = int.Parse(s);
                this.Condition = (ConditionAlbumType)type;
                if (this.Condition != this.Params.Condition)
                {
                    this.Params.Condition = this.Condition;
                    this.GetData(true);
                }
            });
            this.StatusList = new List<string>() { "全部", "完结", "连载" };
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="isClear"></param>
        protected override void GetData(bool isClear)
        {
            if (this.CategoryTagAlbumsService != null)
            {
                this.Params.Page = this.CurrentPage;
                base.GetData(isClear);
                this.CategoryTagAlbumsService.GetData(result =>
                {
                    TagAlbumsResult tagAlbumsResult = result as TagAlbumsResult;
                    Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                    {
                        this.IsWaiting = false;

                        if (tagAlbumsResult.Ret == 0)
                        {
                            this.Total = tagAlbumsResult.Count;
                            if (isClear)
                            {
                                this.Albums.Clear();
                            }
                            foreach (var album in tagAlbumsResult.List)
                            {
                                this.Albums.Add(album);
                            }
                            //base.IsNextPageVisibled = tagAlbumsResult.MaxPageId > this.Params.Page;
                        }
                    }), DispatcherPriority.Background);
                }, this.Params);
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="param"></param>
        /// <param name="regionName"></param>
        /// <param name="view"></param>
        public void DoInit(CategoryTagAlbumParam param, string regionName, AlbumView view)
        {
            if (this.RegionManager != null)
            {
                this.RegionManager.AddToRegion(regionName, view);
                this.Params = param;
                this.Condition = this.Params.Condition;
                this.IsShowStatus = this.Params.Category == "book";
                //this.GetData(true);
                this.PageSize = (int)this.Params.PerPage;
                this.CurrentPage = 1;
            }
        }

        #endregion
    }
}
