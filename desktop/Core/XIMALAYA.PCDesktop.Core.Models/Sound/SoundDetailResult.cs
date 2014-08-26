using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIMALAYA.PCDesktop.Core.Models.User;

namespace XIMALAYA.PCDesktop.Core.Models.Sound
{
    /// <summary>
    /// 声音详情结果
    /// </summary>
    public class SoundDetailResult : SoundData
    {
        /// <summary>
        /// 构造
        /// </summary>
        public SoundDetailResult()
            : base()
        {
            this.doAddMap(() => this.TrackId, "trackId");
            this.doAddMap(() => this.Title, "title");
            this.doAddMap(() => this.CategoryID, "categoryId");
            this.doAddMap(() => this.CategoryName, "categoryName");
            this.doAddMap(() => this.PlayUrl64, "playUrl64");
            this.doAddMap(() => this.PlayUrl32, "playUrl32");
            this.doAddMap(() => this.ProcessState, "processState");
            this.doAddMap(() => this.Duration, "duration");
            this.doAddMap(() => this.CoverSmall, "coverSmall");
            this.doAddMap(() => this.CoverLarge, "coverLarge");
            this.doAddMap(() => this.CreateAt, "createdAt");
            this.doAddMap(() => this.UID, "uid");
            this.doAddMap(() => this.UserSource, "userSource");
            this.doAddMap(() => this.AlbumID, "albumId");
            this.doAddMap(() => this.AlbumTitle, "albumTitle");
            this.doAddMap(() => this.AlbumImage, "albumImage");
            this.doAddMap(() => this.OpType, "opType");
            this.doAddMap(() => this.IsPlulic, "isPublic");
            this.doAddMap(() => this.Images, "images");
            this.doAddMap(() => this.Intro, "intro");
            this.doAddMap(() => this.Tags, "tags");
            this.doAddMap(() => this.Lyric, "lyric");
            this.doAddMap(() => this.RichIntro, "richIntro");
            this.doAddMap(() => this.IsRelay, "isRelay");
            this.doAddMap(() => this.IsLike, "isLike");
            this.doAddMap(() => this.LikeCount, "likes");
            this.doAddMap(() => this.PlayCount, "playtimes");
            this.doAddMap(() => this.CommentCount, "comments");
            this.doAddMap(() => this.ShareCount, "shares");

            this.doAddMap(() => this.TrackBlocks, "trackBlocks");

            this.doAddMap(() => this.User, "userInfo");
        }
    }
}
