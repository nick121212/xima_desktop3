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
        /// <summary>
        /// 需要排除的专辑列表（用户他人的专辑列表）
        /// </summary>
        [DataMember(Name = "excludeId", IsRequired = false, Order = 10)]
        public long ExcludeId { get; set; }
        /// <summary>
        /// 是否正序显示声音，正序true，倒序false。默认为true（用户他人的声音列表）
        /// </summary>
        [DataMember(Name = "isAsc", IsRequired = false, Order = 10)]
        public bool IsAsc { get; set; }
    }
}
