using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.User
{
    public class UserData : BaseData
    {
        /// <summary>
        /// 是否加V
        /// isVerified
        /// </summary>
        public bool IsVerified { get; set; }
        /// <summary>
        /// 大图标
        /// largeLogo
        /// </summary>
        public string LargeLogo { get; set; }
        /// <summary>
        /// 昵称
        /// nickname
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 描述
        /// personDescribe
        /// </summary>
        public string PersonDescribe { get; set; }
        /// <summary>
        /// id
        /// uid
        /// </summary>
        public long Uid { get; set; }
        public UserData()
            : base()
        {
            this.doAddMap(() => this.IsVerified, "isVerified");
            this.doAddMap(() => this.LargeLogo, "largeLogo");
            this.doAddMap(() => this.NickName, "nickname");
            this.doAddMap(() => this.PersonDescribe, "personDescribe");
        }
    }
}
