using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Subject
{
    /// <summary>
    /// 专题详情接口
    /// </summary>
    public class SubjectListResult : BaseResult
    {
        public SubjectData[] List { get; set; }
        public string ModuleTitle { get; set; }
        public SubjectListResult()
            : base()
        {
            this.doAddMap(() => this.List, "list");
            this.doAddMap(() => this.ModuleTitle, "moduleTitle");
        }
    }
}
