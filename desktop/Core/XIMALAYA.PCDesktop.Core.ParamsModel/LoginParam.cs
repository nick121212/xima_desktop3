using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.ParamsModel
{
    public class LoginParam : BaseParam
    {
        /// <summary>
        /// 账号
        /// </summary>
        [DataMember(Name = "account", IsRequired = false)]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [DataMember(Name = "password", IsRequired = false)]
        public string Password { get; set; }
        /// <summary>
        /// 是否记住登录状态
        /// </summary>
        [DataMember(Name = "rememberMe", IsRequired = false)]
        public bool Remember { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        [DataMember(Name = "deviceToken", IsRequired = false)]
        public string DeviceToken { get; set; }
    }
}
