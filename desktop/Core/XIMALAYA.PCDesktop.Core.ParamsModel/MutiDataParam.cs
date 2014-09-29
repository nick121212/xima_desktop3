using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.ParamsModel
{
    /// <summary>
    /// 多用户，多专辑，多声音参数类
    /// </summary>
    public class MutiDataParam : BaseParam
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        [DataMember(Name = "type", IsRequired = true, Order = 1)]
        public int Type { get; set; }
        /// <summary>
        /// 搜索关键字
        /// </summary>
        [DataMember(Name = "id", IsRequired = true, Order = 10)]
        public int ID { get; set; }
    }
}
