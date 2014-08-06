using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Sound
{
    /// <summary>
    /// 专辑下的声音
    /// </summary>
    public class SoundData2 : SoundData
    {
        public SoundData2()
            : base()
        {
            this.doAddMap("FXClassName", "SoundData");
            this.doAddMap(() => this.AlbumImage, "album_cover_path");
            this.doAddMap(() => this.AlbumImage86, "albumImage");
            this.doAddMap(() => this.AlbumID, "albumId");
            this.doAddMap(() => this.AlbumTitle, "albumTitle");
            this.doAddMap(() => this.CommentCount, "comments");
            this.doAddMap(() => this.CoverLarge, "coverLarge");
            this.doAddMap(() => this.CoverSmall, "coverSmall");
            this.doAddMap(() => this.CreateAt, "createdAt");
            this.doAddMap(() => this.Duration, "duration");
            this.doAddMap(() => this.IsPlulic, "is_public");
            this.doAddMap(() => this.LikeCount, "likes");
            this.doAddMap(() => this.NickName, "nickname");
            this.doAddMap(() => this.OpType, "opType");
            this.doAddMap(() => this.PlayCount, "playtimes");
            this.doAddMap(() => this.PlayUrl32, "playUrl32");
            this.doAddMap(() => this.PlayUrl64, "playUrl64");
            this.doAddMap(() => this.ProcessState, "processState");
            this.doAddMap(() => this.ShareCount, "shares");
            this.doAddMap(() => this.SmallLogo, "smallLogo");
            this.doAddMap(() => this.Title, "title");
            this.doAddMap(() => this.TrackId, "trackId");
            this.doAddMap(() => this.UID, "uid");
            this.doAddMap(() => this.UploadSource, "userSource");
        }
    }
}
