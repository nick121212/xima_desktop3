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
    /// 发现页接口参数
    /// </summary>
    [Export(typeof(SuperExploreParam))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SuperExploreParam : BaseParam
    {
        /// <summary>
        /// 图片大小
        /// true
        /// </summary>
        [DataMember(IsRequired = false, Name = "scale")]
        public int? Scale { get; set; }
       
        /// <summary>
        /// 图片版本	
        /// true
        /// </summary>
        [DataMember(IsRequired = true, Name = "picVersion")]
        public int? PicVersion { get; set; }
    }
}
