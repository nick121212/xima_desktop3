using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XIMALAYA.PCDesktop.Tools.Attributes;

namespace XIMALAYA.PCDesktop.Core.ParamsModel
{
    /// <summary>
    /// 分类接口参数
    /// </summary>
    [Export(typeof(CategoryParam))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CategoryParam : BaseParam
    {
        /// <summary>
        /// 图片大小
        /// true
        /// </summary>
        [DataMember(IsRequired = true, Name = "scale")]
        public int? Scale{get;set;}

        /// <summary>
        /// 图片版本	
        /// true
        /// </summary>
        [DataMember(IsRequired = true, Name = "picVersion")]
        public int? PicVersion { get; set; }
    }
}
