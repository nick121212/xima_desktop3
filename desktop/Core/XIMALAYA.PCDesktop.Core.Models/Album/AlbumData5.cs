using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Album
{
    /// <summary>
    /// 多专辑
    /// </summary>
    public class AlbumData5 : AlbumData
    {
        public AlbumData5()
            : base()
        {
            this.doAddMap(() => this.AlbumCoverUrl86, "album_cover_path_86");
            this.doAddMap(() => this.AlbumCoverUrl290, "album_cover_path_290");
            this.doAddMap(() => this.AlbumCoverUrl640, "album_cover_path_640");
            this.doAddMap(() => this.AlbumID, "album_id");
            this.doAddMap(() => this.Uid, "album_uid");
            this.doAddMap(() => this.Intro, "intro");
            this.doAddMap(() => this.PlayCount, "plays_counts");
            this.doAddMap(() => this.Title, "title");
            this.doAddMap(() => this.LastUptrackDate, "last_uptrack_at");
        }
    }
}
