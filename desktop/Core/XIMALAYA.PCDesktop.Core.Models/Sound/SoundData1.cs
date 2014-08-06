using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Sound
{
    /// <summary>
    /// 声音数据
    /// </summary>
    public class SoundData1 : SoundData
    {
        public SoundData1()
            : base()
        {
            this.doAddMap("FXClassName", "SoundData");
            this.doAddMap(() => this.AlbumImage, "album_cover_path");
            this.doAddMap(() => this.AlbumImage, "album_cover_path");
            this.doAddMap(() => this.AlbumImage86, "album_cover_path_86");
            this.doAddMap(() => this.AlbumImage640, "album_cover_path_640");
            this.doAddMap(() => this.AlbumID, "album_id");
            this.doAddMap(() => this.AlbumTitle, "album_title");
            this.doAddMap(() => this.CategoryID, "category_id");
            this.doAddMap(() => this.CommentCount, "comments_counts");
            this.doAddMap(() => this.CommentID, "comment_id");
            this.doAddMap(() => this.CoverLarge, "coverLarge");
            this.doAddMap(() => this.CoverSmall, "coverSmall");
            this.doAddMap(() => this.CreateAt, "createdAt");
            this.doAddMap(() => this.Duration, "duration");
            this.doAddMap(() => this.Intro, "intro");
            this.doAddMap(() => this.IsLike, "is_favorited");
            this.doAddMap(() => this.IsPlulic, "is_public");
            this.doAddMap(() => this.IsRelay, "isRelay");
            this.doAddMap(() => this.LikeCount, "favorites_counts");
            this.doAddMap(() => this.NickName, "nickname");
            this.doAddMap(() => this.OpType, "op_type");
            this.doAddMap(() => this.PlayCount, "plays_counts");
            this.doAddMap(() => this.PlayUrl32, "play_path_32");
            this.doAddMap(() => this.PlayUrl64, "play_path_64");
            this.doAddMap(() => this.ProcessState, "transcode_state");
            this.doAddMap(() => this.refComment, "comment_content");
            this.doAddMap(() => this.ShareCount, "shares_counts");
            this.doAddMap(() => this.SmallLogo, "smallLogo");
            this.doAddMap(() => this.Tags, "tags");
            this.doAddMap(() => this.Title, "title");
            this.doAddMap(() => this.TrackId, "track_id");
            this.doAddMap(() => this.UID, "track_uid");
            this.doAddMap(() => this.UploadSource, "upload_source");
        }
    }
}
