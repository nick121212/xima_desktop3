using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XIMALAYA.PCDesktop.Tools.Attributes;

namespace XIMALAYA.PCDesktop.Core.ParamsModel
{
    /// <summary>
    /// 分类，标签下的声音
    /// </summary>
    public class CategoryTagSounds : BaseParam
    {
        /// <summary>
        /// 分类，传分类名称，like：music
        /// </summary>
        [DataMember(Name = "category", IsRequired = false)]
        public string Category { get; set; }
        /// <summary>
        /// 标签，传标签名. 例如 “小清新”
        /// </summary>
        [DataMember(Name = "tag", IsRequired = false)]
        public string TagName { get; set; }
        /// <summary>
        /// 默认(不传或传””) : 24小时最热声音,
        /// “zuixinshangchuan” : 最新上传的声音，
        /// “zuiduoshoucang” : 最多收藏的声音，
        /// “jincaituijian” : 精彩推荐的声音
        /// </summary>
        [DataMember(Name = "condition", IsRequired = false)]
        public ConditionSoundType Condition { get; set; }
    }
}
