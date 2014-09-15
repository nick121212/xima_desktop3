using System.ComponentModel.Composition;
using XIMALAYA.PCDesktop.Common;
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
        #region 字段

        private UserData _UserData;

        #endregion

        #region 属性

        /// <summary>
        /// uid
        /// </summary>
        private long UID { get; set; }
        /// <summary>
        /// 用户详情
        /// </summary>
        public UserData UserData
        {
            get
            {
                return _UserData;
            }
            set
            {
                if (value != _UserData)
                {
                    _UserData = value;
                    this.RaisePropertyChanged(() => this.UserData);
                }
            }
        }
        
        /// <summary>
        /// 用户详情服务
        /// </summary>
        [Import]
        private IUserDetailService UserDetailService { get; set; }

        #endregion

        #region 方法

        protected override void GetData(bool isClear)
        {
            if (this.UserDetailService == null) return;
            this.UserDetailService.GetData(res =>
            {
                var result = res as UserData;

                this.UserData = result;
                //base.GetData(isClear);

            }, new UserDetailParam
            {
                ToUID = this.UID
            });
        }

        public void Initialize(long uid)
        {
            this.UID = uid;
            this.GetData(true);
        }

        #endregion
    }
}
