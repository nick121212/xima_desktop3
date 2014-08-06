using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Tags
{
    /// <summary>
    /// 分类下的标签
    /// </summary>
    public class CategoryTagResult : BaseResult
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 最大页码
        /// </summary>
        public int MaxPageId { get; set; }
        /// <summary>
        /// 数据列表
        /// </summary>
        public TagData[] List { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        public CategoryTagResult()
            : base()
        {
            this.doAddMap(() => this.List, "list");
            this.doAddMap(() => this.MaxPageId, "maxPageId");
            this.doAddMap(() => this.Count, "count");
        }
    }
}
