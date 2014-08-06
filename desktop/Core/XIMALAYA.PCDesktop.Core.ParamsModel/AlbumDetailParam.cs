using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.ParamsModel
{
    /// <summary>
    /// 请求专辑下的声音
    /// </summary>
    public class AlbumDetailParam : BaseParam
    {
        /// <summary>
        /// 专辑ID
        /// </summary>
        [DataMember(IsRequired = true, Name = "albumId", Order = 1)]
        public long AlbumID { get; set; }
        /// <summary>
        /// 排序
        /// 专辑中的声音是否正序显示，默认为正序
        /// </summary>
        [DataMember(IsRequired = true, Name = "isAsc", Order = 2)]
        public bool IsAsc { get; set; }
        /// <summary>
        /// 分页的页码
        /// </summary>
        [DataMember(IsRequired = false, Name = "pageId", Order = 20)]
        public new int? Page { get; set; }
        /// <summary>
        /// 分页每页的数量
        /// </summary>
        [DataMember(IsRequired = false, Name = "pageSize", Order = 30)]
        public new int? PerPage { get; set; }
    }
}
