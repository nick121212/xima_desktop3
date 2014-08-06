using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIMALAYA.PCDesktop.Core.Models.Sound;

namespace XIMALAYA.PCDesktop.Core.Models.Album
{
    /// <summary>
    /// 专辑下的声音
    /// </summary>
    public class AlbumInfoResult : BaseResult
    {
        /// <summary>
        /// 专辑数据
        /// </summary>
        public AlbumData Album { get; set; }
        /// <summary>
        /// 声音数据
        /// </summary>
        public SoundsResult SoundsResult { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        public AlbumInfoResult()
        {
            this.doAddMap(() => this.Album, "album");
            this.doAddMap(() => this.SoundsResult, "tracks");
        }
    }
}
