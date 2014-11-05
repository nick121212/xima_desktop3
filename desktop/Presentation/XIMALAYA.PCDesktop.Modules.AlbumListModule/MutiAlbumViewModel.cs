using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Core.Models.MutiData;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Tools;

namespace XIMALAYA.PCDesktop.Modules.AlbumModule
{
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class MutiAlbumViewModel : BaseViewModel
    {
        #region 属性

        /// <summary>
        /// 他人的专辑列表
        /// </summary>
        public ObservableCollection<AlbumData> Albums { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        private MutiDataParam Params { get; set; }
        /// <summary>
        /// 专辑服务
        /// </summary>
        [Import]
        private IAlbumService AlbumService { get; set; }

        private string _Title;
        /// <summary>
        /// 佔位服务
        /// </summary>
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (value != _Title)
                {
                    _Title = value;
                    this.RaisePropertyChanged(() => this.Title);
                }
            }
        }


        #endregion

        #region 方法

        protected override void GetData(bool isClear)
        {
            if (this.AlbumService == null)
            {
                throw new NullReferenceException();
            }

            this.Params.Page = this.CurrentPage;
            base.GetData(isClear);

            this.AlbumService.GetMutiAlbums(res =>
            {
                var result = res as MutiAlbumResult;

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (result.Ret == 0)
                    {
                        this.IsWaiting = false;
                        this.Total = result.TotalCount;
                        this.Title = result.LongTitle;
                        this.Albums.Clear();
                        foreach (var album in result.Albums)
                        {
                            this.Albums.Add(album);
                        }
                    }
                    else
                    {
                        DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, GlobalDataSingleton.Instance.SystemTitle, result.Message);
                    }
                }), null);

            }, this.Params);
        }

        public void Initialize(int id)
        {
            this.Albums = new ObservableCollection<AlbumData>();
            this.Params = new MutiDataParam
            {
                ID = id,
                Type = 6,
                Page = 1,
                PerPage = 20
            };
            this.PageSize = (int)this.Params.PerPage;
            this.CurrentPage = 1;
        }

        #endregion
    }
}
