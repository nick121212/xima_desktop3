﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Prism.Commands;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Core.Models.Category;
using XIMALAYA.PCDesktop.Core.Models.Discover;
using XIMALAYA.PCDesktop.Core.Models.FocusImage;
using XIMALAYA.PCDesktop.Core.Models.Subject;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;

namespace XIMALAYA.PCDesktop.Modules.DiscoverModule
{
    [Export]
    public sealed class DiscoverViewModel : BaseViewModel
    {
        #region 字段

        private string _SubjectModuleTitle;

        #endregion

        #region 属性

        /// <summary>
        /// 发现也整合接口，没用
        /// </summary>
        [Import]
        private IExploreService SuperExploreIndex { get; set; }
        /// <summary>
        /// 今日焦点的title
        /// </summary>
        public string SubjectModuleTitle
        {
            get { return _SubjectModuleTitle; }
            set
            {
                if (value != _SubjectModuleTitle)
                {
                    _SubjectModuleTitle = value;
                    this.RaisePropertyChanged(() => this.SubjectModuleTitle);
                }
            }
        }
        /// <summary>
        /// 焦点图列表
        /// </summary>
        public ObservableCollection<FocusImageData> FocusImageList { get; set; }
        /// <summary>
        /// 分类列表
        /// </summary>
        public static ObservableCollection<CategoryData> CategoryList { get; set; }
        /// <summary>
        /// 今日热点
        /// </summary>
        public ObservableCollection<SubjectData> SubjectList { get; set; }
        /// <summary>
        /// 热门声音所在的分类
        /// </summary>
        public ObservableCollection<CategoryData> HotSoundsCategories { get; set; }
        /// <summary>
        /// 推荐专辑
        /// </summary>
        public ObservableCollection<AlbumData> AlbumList { get; set; }

        #endregion

        #region 命令

        /// <summary>
        /// 查看分类的详情
        /// </summary>
        public DelegateCommand<string> ShowCategoryDetailCommand { get; private set; }

        #endregion

        #region 方法

        private void GetFocusImageDataAction()
        {
            if (SuperExploreIndex == null) throw new ArgumentNullException();

            this.SuperExploreIndex.GetData(this.GetExporeIndexData, new SuperExploreParam
            {
                Device = DeviceType.iphone,
                PicVersion = 4,
                Scale = 2
            });
        }
        private void GetExporeIndexData(object result)
        {
            var superData = result as SuperExploreIndexResult;

            if (superData == null) throw new ArgumentNullException();

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                if (superData.Ret == 0)
                {
                    int index = 0;
                    this.FocusImageList.Clear();
                    foreach (FocusImageData fi in superData.FocusImages.List)
                    {
                        fi.IsFirst = index == 0;
                        index++;
                        this.FocusImageList.Add(fi);
                    }
                    foreach (var category in superData.Categories.List)
                    {
                        DiscoverViewModel.CategoryList.Add(category);
                    }
                }
                else
                {
                    DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", superData.Message);
                }
            });
        }
        public void Initialize()
        {
            if (this.Initialized) return;

            this.Initialized = true;
            this.GetFocusImageDataAction();
            //SearchService.GetAllData(result =>
            //{
            //    var res = result as SearchResult;
            //}, new SearchParam
            //{
            //    Condition = "N",
            //    Scope = "all"
            //});
        }

        #endregion

        #region 构造函数

        public DiscoverViewModel()
        {
            DiscoverViewModel.CategoryList = new ObservableCollection<CategoryData>();
            this.FocusImageList = new ObservableCollection<FocusImageData>();
            this.SubjectList = new ObservableCollection<SubjectData>();
            this.HotSoundsCategories = new ObservableCollection<CategoryData>();
            this.AlbumList = new ObservableCollection<AlbumData>();

            for (int i = 0; i < 6; i++)
            {
                this.FocusImageList.Add(new FocusImageData { IsFirst = i == 0 });
            }

            //点击分类命令
            this.ShowCategoryDetailCommand = new DelegateCommand<string>(cateName =>
            {
                //发送事件
                this.EventAggregator.GetEvent<TagEvent>().Publish(new TagEventArgument
                {
                    TagKey = cateName
                });
            });
        }

        #endregion
    }
}
