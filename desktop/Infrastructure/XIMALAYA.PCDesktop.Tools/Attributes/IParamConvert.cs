using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Tools.Attributes
{
    /// <summary>
    /// 参数类转换名称接口
    /// </summary>
    public interface IParamConvert
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        string ParamName { get; }
        /// <summary>
        /// 是否为必须
        /// </summary>
        bool IsRequired { get; }
    }
}
