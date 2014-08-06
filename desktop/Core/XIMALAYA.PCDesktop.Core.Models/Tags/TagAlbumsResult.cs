using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Core.Models.Sound;

namespace XIMALAYA.PCDesktop.Core.Models.Tags
{
    /// <summary>
    /// 标签下的专辑
    /// </summary>
    public class TagAlbumsResult : BaseResult
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
        public AlbumData[] List { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        public TagAlbumsResult()
            : base()
        {
            this.doAddMap(() => this.List, "list");
            this.doAddMap(() => this.MaxPageId, "maxPageId");
            this.doAddMap(() => this.Count, "count");
        }
    }
}
