using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Tools.Attributes
{
    /// <summary>
    /// Mef相关
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    [MetadataAttribute]
    public sealed class ViewExportAttribute : ExportAttribute, IViewRegionRegistration
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ViewExportAttribute()
            : base(typeof(object))
        { }
        /// <summary>
        /// 构造
        /// </summary>
        public ViewExportAttribute(string viewName)
            : base(viewName, typeof(object))
        { }
        /// <summary>
        /// ViewName
        /// </summary>
        public string ViewName { get { return base.ContractName;} }
        /// <summary>
        /// RegionName
        /// </summary>
        public string RegionName { get; set; }
    }
}
