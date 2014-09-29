using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Threading;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Core.Models.User;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;

namespace XIMALAYA.PCDesktop.Modules.UserModule
{
    /// <summary>
    /// 用户详情viewmodel
    /// </summary>
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserViewModel : BaseViewModel
    {
        #region 属性

        /// <summary>
        /// 他人的专辑列表
        /// </summary>
        public ObservableCollection<AlbumData> Albums { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        private UserDetailParam Params { get; set; }
        /// <summary>
        /// 专辑服务
        /// </summary>
        [Import]
        private IAlbumService AlbumService { get; set; }

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
            this.AlbumService.GetUserAlbums(result =>
            {
                AlbumInfoResult1 albumInfo = result as AlbumInfoResult1;

                Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                {
                    this.IsWaiting = false;
                    if (isClear)
                    {
                        this.Albums.Clear();
                    }
                    if (albumInfo.Ret == 0)
                    {
                        this.EventAggregator.GetEvent<UserEvent<UserEventArgument>>().Publish(new UserEventArgument
                        {
                            RegionName = this.RegionName,
                            UID = this.Params.ToUID
                        });
                        //CommandSingleton.Instance.AddToJumpListCommand.Execute(new JumpListEventArgs
                        //{
                        //    Title = this.AlbumData.Title,
                        //    Arguments = "album_" + this.AlbumData.AlbumID,
                        //    Category = "最近浏览过的用户"
                        //});
                        this.Total = albumInfo.TotalCount;
                        foreach (AlbumData album in albumInfo.List)
                        {
                            this.Albums.Add(album);
                        }
                    }

                }), DispatcherPriority.Background);
            }, this.Params);
        }

        public void Initialize(long uid)
        {
            this.Albums = new ObservableCollection<AlbumData>();
            this.Params = new UserDetailParam
            {
                ToUID = uid,
                Page=1,
                PerPage = 20
            };
            this.PageSize = (int)this.Params.PerPage;
            this.CurrentPage = 1;
        }

        #endregion
    }
}
