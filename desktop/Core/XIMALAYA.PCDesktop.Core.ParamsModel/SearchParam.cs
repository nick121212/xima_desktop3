using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.ParamsModel
{
    /// <summary>
    /// 搜索条件类
    /// </summary>
    public class SearchParam : BaseParam
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        [DataMember(Name = "condition", IsRequired = true, Order = 1)]
        public string Condition { get; set; }
        /// <summary>
        /// 搜索范围。搜全部设为all,搜声音设为voice,搜专辑设为album,搜用户设为user
        /// </summary>
        [DataMember(Name = "scope", IsRequired = true, Order = 2)]
        public TagType Scope { get; set; }
        /// <summary>
        /// 排序，不设置则为相关程度搜索。
        /// 声音可选track_play+desc(最多播放)，
        /// track_favor+desc(最多收藏),
        /// created_at+desc(最晚上传)
        /// 专辑可选created_at+desc(最晚上传)
        /// 标签可选tag_follow+desc(最多关注)
        /// 用户可选user_max+desc(最多声音)，
        /// user_fans+desc(最多粉丝),
        /// updated_at+desc(最近更新)
        /// </summary>
        [DataMember(Name = "sort", IsRequired = false, Order = 3)]
        public string Sort { get; set; }
        ///// <summary>
        ///// 额外限制条件
        ///// 示例 category_id:3 AND user_source:1
        ///// </summary>
        //[DataMember(Name = "fq", IsRequired = false, Order = 4)]
        //public string fq { get; set; }
    }
}
