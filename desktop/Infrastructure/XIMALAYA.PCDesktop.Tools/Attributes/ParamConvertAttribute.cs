using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Tools.Attributes
{
    /// <summary>
    /// 参数类转换名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    [MetadataAttribute]
    public class ParamConvertAttribute : ExportAttribute, IParamConvert
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParamName { get; set; }
        /// <summary>
        /// 是否为必填参数
        /// </summary>
        public bool IsRequired { get; set; }
         /// <summary>
        /// 构造
        /// </summary>
        public ParamConvertAttribute()
            : base(typeof(object))
        { }
    }
}
