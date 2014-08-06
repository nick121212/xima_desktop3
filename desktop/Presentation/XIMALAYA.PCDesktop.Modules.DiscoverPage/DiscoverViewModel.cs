using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using XIMALAYA.PCDesktop.Core.Models.Category;
using XIMALAYA.PCDesktop.Core.Models.Discover;
using XIMALAYA.PCDesktop.Core.Models.FocusImage;
using XIMALAYA.PCDesktop.Core.Models.Subject;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Tools.Untils;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Cache;

namespace XIMALAYA.PCDesktop.Modules.DiscoverPage
{
    [Export]
    public sealed class DiscoverViewModel : BaseViewModel
    {
        #region fields

        private string _SubjectModuleTitle;

        #endregion

        #region Properties

        /// <summary>
        /// 焦点图服务
        /// </summary>
        [Import]
        private IFocusImageService FocusImageService { get; set; }
        /// <summary>
        /// 发现也整合接口，没用
        /// </summary>
        [Import]
        private ISuperExploreIndex SuperExploreIndex { get; set; }
        /// <summary>
        /// 分类接口服务
        /// </summary>
        [Import]
        private ICategoryService CategoryService { get; set; }
        /// <summary>
        /// 热门声音服务
        /// </summary>
        [Import]
        private IHotSoundsService HotSoundsService { get; set; }
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

        #region commands

        /// <summary>
        /// 查看分类的详情
        /// </summary>
        public DelegateCommand<string> ShowCategoryDetailCommand { get; private set; }

        #endregion

        #region actions

        private void GetFocusImageDataAction()
        {
            if (SuperExploreIndex == null) throw new ArgumentNullException();

            this.SuperExploreIndex.GetData(this.GetExporeIndexData, new SuperExploreParam
            {
                Device = DeviceType.pc,
                PicVersion = 7
            });
        }
        private void GetCategoryListAction()
        {
            this.CategoryService.GetData(categories =>
            {
                CategoryResult categoryResult = categories as CategoryResult;
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    foreach (CategoryData cd in categoryResult.List)
                    {
                        DiscoverViewModel.CategoryList.Add(cd);
                    }
                });
            }, new CategoryParam
            {
                Device = DeviceType.pc,
                PicVersion = 5,
                Scale = 2
            });
        }
        private void GetExporeIndexData(object result)
        {
            var superData = result as SuperExploreIndexResult;

            if (superData == null) throw new ArgumentNullException();
            if (superData.Ret == 0)
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    int index = 0;
                    this.FocusImageList.Clear();
                    foreach (FocusImageData fi in superData.FocusImages.List)
                    {
                        fi.IsFirst = index == 0;
                        index++;
                        this.FocusImageList.Add(fi);
                    }
                    this.SubjectModuleTitle = superData.Subjects.ModuleTitle;
                    foreach (var sd in superData.Subjects.List)
                    {
                        this.SubjectList.Add(sd);
                    }
                    foreach (var album in superData.Albums.List)
                    {
                        this.AlbumList.Add(album);
                    }
                }, System.Windows.Threading.DispatcherPriority.Background);
            }
            else
            {
                DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", superData.Message);
            }
        }
        private void GetHotSoundsAction()
        {
            if (this.HotSoundsService != null)
            {
                this.HotSoundsService.GetData(result =>
                {
                    var res = result as HotSoundsResult;

                    if (res.Ret == 0)
                    {
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            foreach (var cate in res.Categories)
                            {
                                SoundCache.Instance.SetData(cate.Sounds);
                                this.HotSoundsCategories.Add(cate);
                            }
                        });

                    }
                }, new BaseParam
                {
                    Device = DeviceType.pc
                });
            }
        }
        public void Initialize()
        {
            this.GetFocusImageDataAction();

            this.GetHotSoundsAction();

            this.GetCategoryListAction();
        }

        #endregion

        #region construction

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
