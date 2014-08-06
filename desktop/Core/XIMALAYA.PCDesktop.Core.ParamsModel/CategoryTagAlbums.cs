using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.Composition;
using XIMALAYA.PCDesktop.Tools.Attributes;
using System.Runtime.Serialization;

namespace XIMALAYA.PCDesktop.Core.ParamsModel
{
    /// <summary>
    /// 分类，标签下的专辑
    /// </summary>
    public class CategoryTagAlbums : BaseParam
    {
        /// <summary>
        /// 默认(不传或传””) : 24小时最热声音,
        /// “zuixinshangchuan” : 最新上传的声音，
        /// “zuiduoshoucang” : 最多收藏的声音，
        /// “jincaituijian” : 精彩推荐的声音
        /// </summary>
        [DataMember(Name = "condition", IsRequired = false, Order = 2)]
        public ConditionAlbumType Condition { get; set; }
        /// <summary>
        /// 分类，传分类名称，like：music
        /// </summary>
        [DataMember(Name = "category_name", IsRequired = true, Order = 1)]
        public string Category { get; set; }
        /// <summary>
        /// 标签，传标签名. 例如 “小清新”
        /// </summary>
        [DataMember(Name = "tag_name", IsRequired = true, Order = 50)]
        public string TagName { get; set; }
        /// <summary>
        /// 连载完结状态（只限小说）
        /// </summary>
        [DataMember(Name = "status", IsRequired = false, Order = 40)]
        public int Status { get; set; }
    }
}
