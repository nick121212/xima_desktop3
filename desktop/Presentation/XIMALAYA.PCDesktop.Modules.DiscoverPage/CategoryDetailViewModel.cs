using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using XIMALAYA.PCDesktop.Core.Models.Category;
using XIMALAYA.PCDesktop.Core.Models.Tags;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Modules.DiscoverPage.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Modules.DiscoverPage
{
    /// <summary>
    /// 分类详情页面
    /// </summary>
    [Export]
    public class CategoryDetailViewModel : BaseViewModel
    {
        #region fields

        private ObservableCollection<TagData> _TagDataList;
        private CategoryData _CurrentCategory = null;

        #endregion

        #region properties

        /// <summary>
        /// 焦点图列表
        /// </summary>
        public ObservableCollection<TagData> TagDataList
        {
            get { return _TagDataList; }
            set
            {
                if (value != _TagDataList)
                {
                    _TagDataList = value;
                }
            }
        }
        /// <summary>
        /// 分类服务
        /// </summary>
        [Import]
        public ICategoryTagService CategoryTagService { get; set; }
        /// <summary>
        /// 当前分类
        /// </summary>
        public CategoryData CurrentCategory
        {
            get
            {
                return _CurrentCategory;
            }
            set
            {
                if (value != _CurrentCategory)
                {
                    _CurrentCategory = value;
                    this.RaisePropertyChanged(() => this.CurrentCategory);
                }
            }
        }
        /// <summary>
        /// 分类，标签 下的专辑
        /// </summary>
        public DelegateCommand<string> ShowAlbumListForTagCommand { get; set; }
        /// <summary>
        /// 视图类
        /// </summary>
        [Import]
        private CategoryDetailView CategoryDetailView { get; set; }

        #endregion

        #region actions

        /// <summary>
        /// 分类事件函数
        /// </summary>
        /// <param name="e"></param>
        private void OnTagEventPublish(TagEventArgument e)
        {
            if (DiscoverViewModel.CategoryList == null)
            {
                throw new ArgumentException("分类集合为空！");
            }

            this.CurrentCategory = DiscoverViewModel.CategoryList.FirstOrDefault(c => c.Name == e.TagKey);
            if (this.CurrentCategory == null || this.CurrentCategory.Name != e.TagKey)
            {
                throw new ArgumentException(string.Format("找不到分类：分类key为{0}", e.TagKey));
            }

            var regionName = this.ContainerView.GetFlyout(this.CurrentCategory.Title);
            if (this.RegionManager != null)
            {
                this.RegionManager.AddToRegion(regionName, this.CategoryDetailView);
            }

            if (this.CategoryTagService != null)
            {
                this.IsWaiting = true;
                this.CategoryTagService.GetData(result =>
                {
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        CategoryTagResult tagResult = result as CategoryTagResult;

                        this.IsWaiting = false;
                        if (tagResult != null && tagResult.Ret == 0)
                        {
                            this.TagDataList.Clear();
                            this.TagDataList.Add(new TagData
                            {
                                CategoryId = 0,
                                CoverPath = "pack://application:,,,/XIMALAYA.PCDesktop.Tools;component/Resources/Images/tagall.jpg",
                                TagName = "全部"
                            });
                            foreach (var tag in tagResult.List)
                            {
                                this.TagDataList.Add(tag);
                            }
                        }
                    }));
                }, new CategoryTagParam
                {
                    Category = this.CurrentCategory.Name,
                    Type = TagType.album
                });
            }
        }

        #endregion

        #region construction

        public void Initialize()
        {
            //点击分类事件
            this.EventAggregator.GetEvent<TagEvent>().Subscribe(OnTagEventPublish, ThreadOption.UIThread);
        }

        /// <summary>
        /// 构造
        /// </summary>
        public CategoryDetailViewModel()
        {
            this.TagDataList = new ObservableCollection<TagData>();

            //点击标签事件
            this.ShowAlbumListForTagCommand = new DelegateCommand<string>(tagName =>
            {
                if (this.CurrentCategory != null)
                {
                    this.EventAggregator.GetEvent<AlbumListEvent<TagEventArgument>>().Publish(new TagEventArgument()
                    {
                        Category = this.CurrentCategory.Name,
                        Title = tagName == "全部" ? "热门" : tagName,
                        TagName = tagName == "全部" ? " " : tagName,
                    });
                }
            });
        }

        #endregion
    }
}
