using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.ParamsModel
{
    /// <summary>
    /// 用户详情
    /// </summary>
    public class UserDetailParam : BaseParam
    {
        /// <summary>
        /// 查看用户的UID
        /// </summary>
        [DataMember(Name = "toUid", IsRequired = true, Order = 1)]
        public long ToUID { get; set; }
    }
}
