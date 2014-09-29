using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Core.Models.MutiData;
using XIMALAYA.PCDesktop.Core.Models.User;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Tools;

namespace XIMALAYA.PCDesktop.Modules.UserModule
{
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class MutiUserViewModel : BaseViewModel
    {
        #region 属性

        /// <summary>
        /// 他人的专辑列表
        /// </summary>
        public ObservableCollection<UserData> Users { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        private MutiDataParam Params { get; set; }
        /// <summary>
        /// 专辑服务
        /// </summary>
        [Import]
        private IUserService UserService { get; set; }

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
            if (this.UserService == null)
            {
                throw new NullReferenceException();
            }

            this.Params.Page = this.CurrentPage;
            base.GetData(isClear);

            this.UserService.GetMutiUsers(res =>
            {
                var result = res as MutiUserResult;

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (result.Ret == 0)
                    {
                        this.IsWaiting = false;
                        this.Total = result.TotalCount;
                        this.Title = result.LongTitle;
                        this.Users.Clear();
                        foreach (var user in result.Users)
                        {
                            this.Users.Add(user);
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
            this.Users = new ObservableCollection<UserData>();
            this.Params = new MutiDataParam
            {
                ID = id,
                Type = 5,
                Page = 1,
                PerPage = 20
            };
            this.PageSize = (int)this.Params.PerPage;
            this.CurrentPage = 1;
        }

        #endregion
    }
}
