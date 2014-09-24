using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIMALAYA.PCDesktop.Core.Models.Album;

namespace XIMALAYA.PCDesktop.Core.Models.Album
{
    public class AlbumInfoResult1 : BaseResult
    {
        /// <summary>
        /// 最大页数
        /// maxPageId
        /// </summary>
        public int MaxPageId { get; set; }
        /// <summary>
        /// 总数
        /// count
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 专辑数据
        /// list
        /// </summary>
        public AlbumData[] List { get; set; }

        public AlbumInfoResult1()
            : base()
        {
            this.doAddMap(() => this.MaxPageId, "maxPageId");
            this.doAddMap(() => this.TotalCount, "count");
            this.doAddMap(() => this.TotalCount, "totalCount");
            this.doAddMap(() => this.List, "list");
        }

    }
}
