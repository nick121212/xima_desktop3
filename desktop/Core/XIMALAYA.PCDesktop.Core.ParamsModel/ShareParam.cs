using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.ParamsModel
{
    /// <summary>
    /// 获取第三方分享内容
    /// </summary>
    public class ShareParam : BaseParam
    {
        /// <summary>
        /// 声音id
        /// </summary>
        [DataMember(Name = "trackId", IsRequired = false)]
        public long TrackId { get; set; }
        /// <summary>
        /// 专辑id
        /// </summary>
        [DataMember(Name = "albumId", IsRequired = false)]
        public long AlbumId { get; set; }
        /// <summary>
        /// 分享到的平台
        /// </summary>
        [DataMember(Name = "tpName", IsRequired = true)]
        public string tpName { get; set; }
    }
}
