using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Core.Models.Sound;

namespace XIMALAYA.PCDesktop.Core.Models.Search
{
    /// <summary>
    /// 搜索声音数据
    /// </summary>
    public class SearchSoundResult : BaseResult
    {
        /// <summary>
        /// start，起始位置
        /// </summary>
        public long StartAt { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public long TotalCount { get; set; }
        /// <summary>
        /// 声音列表
        /// </summary>
        public SoundData[] Sounds { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        public SearchSoundResult()
        {
            this.doAddMap(() => this.StartAt, "start");
            this.doAddMap(() => this.TotalCount, "numFound");
            this.doAddMap(() => this.Sounds, "docs");
        }
    }
}
