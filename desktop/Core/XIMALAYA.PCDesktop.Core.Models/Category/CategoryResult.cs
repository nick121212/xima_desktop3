using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Category
{
    public class CategoryResult : BaseResult
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 分类列表
        /// </summary>
        public CategoryData[] List { get; set; }

        public CategoryResult()
            : base()
        {
            this.doAddMap(() => this.Count, "count");
            this.doAddMap(() => this.List, "data");
        }
    }

}
