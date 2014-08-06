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
    /// 分类下的标签借口参数
    /// </summary>
    public class CategoryTagParam : BaseParam
    {
        /// <summary>
        /// 分类，传分类名称，like：music
        /// </summary>
        [DataMember(Name = "category", IsRequired = false)]
        public string Category { get; set; }
        /// <summary>
        /// 标签的种类，可取专辑和声音
        /// </summary>
        [DataMember(Name = "type", IsRequired = false)]
        public TagType? Type { get; set; }
    }
}
