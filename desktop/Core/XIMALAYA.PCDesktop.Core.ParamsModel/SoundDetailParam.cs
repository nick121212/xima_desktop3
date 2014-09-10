using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.ParamsModel
{
    public class SoundDetailParam : BaseParam
    {
        /// <summary>
        /// 声音ID
        /// </summary>
        [DataMember(Name = "trackId", IsRequired = true, Order = 1)]
        public long TrackId { get; set; }
        /// <summary>
        /// 分页的页码
        /// </summary>
        [DataMember(IsRequired = false, Name = "pageId", Order = 20)]
        new public int? Page { get; set; }
        /// <summary>
        /// 分页每页的数量
        /// </summary>
        [DataMember(IsRequired = false, Name = "pageSize", Order = 30)]
        new public int? PerPage { get; set; }
    }
}
