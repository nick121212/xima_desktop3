using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Sound
{
    /// <summary>
    /// 声音结果
    /// </summary>
    public class SoundsResult : BaseResult
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageID { get; set; }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 最大页数
        /// </summary>
        public int MaxPageID { get; set; }
        /// <summary>
        /// 声音数据集合
        /// </summary>
        public SoundData[] Sounds { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        public SoundsResult()
        {
            this.doAddMap(() => this.PageID, "pageId");
            this.doAddMap(() => this.PageSize, "pageSize");
            this.doAddMap(() => this.TotalCount, "totalCount");
            this.doAddMap(() => this.MaxPageID, "maxPageId");
            this.doAddMap(() => this.Sounds, "list");
        }
    }
}
