using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Search
{
    /// <summary>
    /// 搜索返回数据
    /// </summary>
    public class SearchResult : BaseResult
    {
        /// <summary>
        /// 专辑数据
        /// </summary>
        public SearchAlbumResult AlbumData { get; set; }
        /// <summary>
        /// 声音数据
        /// </summary>
        public SearchSoundResult SoundData { get; set; }
        /// <summary>
        /// 用户数据
        /// </summary>
        public SearchUserResult UserData { get; set; }

        public SearchResult()
        {
            this.doAddMap(() => this.AlbumData, "album");
            this.doAddMap(() => this.SoundData, "track");
            this.doAddMap(() => this.UserData, "user");
        }
    }
}
