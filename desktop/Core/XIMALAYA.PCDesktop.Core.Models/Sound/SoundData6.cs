using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Sound
{
    /// <summary>
    /// 多声音
    /// </summary>
    public class SoundData6 : SoundData
    {
        /// <summary>
        /// 构造
        /// </summary>
        public SoundData6()
            : base()
        {
            this.doAddMap(() => this.AlbumID, "album_id");
            this.doAddMap(() => this.AlbumTitle, "album_title");
            this.doAddMap(() => this.CategoryID, "category_id");
            this.doAddMap(() => this.CommentCount, "comments_counts");
            this.doAddMap(() => this.CoverLarge, "coverLarge");
            this.doAddMap(() => this.CoverSmall, "coverSmall");
            this.doAddMap(() => this.CreateAt, "createdAt");
            this.doAddMap(() => this.Duration, "duration");
            this.doAddMap(() => this.LikeCount, "favorites_counts");
            this.doAddMap(() => this.TrackId, "id");
            this.doAddMap(() => this.Intro, "intro");
            this.doAddMap(() => this.IsRelay, "isRelay");
            this.doAddMap(() => this.IsVerified, "isVerified");
            this.doAddMap(() => this.NickName, "nickname");
            this.doAddMap(() => this.PlayUrl32, "play_path_32");
            this.doAddMap(() => this.PlayUrl64, "play_path_64");
            this.doAddMap(() => this.PlayCount, "plays_counts");
            this.doAddMap(() => this.ShareCount, "shares_counts");
            this.doAddMap(() => this.SmallLogo, "smallLogo");
            this.doAddMap(() => this.Title, "title");
            this.doAddMap(() => this.UID, "uid");
            this.doAddMap(() => this.UserSource, "user_source");
        }
    }
}
