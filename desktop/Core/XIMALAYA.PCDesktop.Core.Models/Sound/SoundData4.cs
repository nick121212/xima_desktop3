using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Sound
{
    /// <summary>
    /// 搜索的声音
    /// </summary>
    public class SoundData4 : SoundData
    {
        public SoundData4()
            : base()
        {
            this.doAddMap(() => this.AlbumImage86, "album_cover_path");
            this.doAddMap(() => this.AlbumID, "album_id");
            this.doAddMap(() => this.AlbumTitle, "album_title");
            this.doAddMap(() => this.CategoryID, "category_id");
            this.doAddMap(() => this.CommentCount, "count_comment");
            this.doAddMap(() => this.CoverSmall, "cover_path");
            this.doAddMap(() => this.CreateAt, "created_at");
            this.doAddMap(() => this.Duration, "duration");
            this.doAddMap(() => this.TrackId, "id");
            this.doAddMap(() => this.IsRelay, "iRelay");
            this.doAddMap(() => this.IsLike, "is_like");
            this.doAddMap(() => this.IsVerified, "is_v");
            this.doAddMap(() => this.NickName, "nickname");
            this.doAddMap(() => this.IsRelay, "iRelay");
            this.doAddMap(() => this.PlayUrl32, "play_path_32");
            this.doAddMap(() => this.PlayUrl64, "play_path_64");
            this.doAddMap(() => this.Title, "title");
            this.doAddMap(() => this.UID, "uid");
            this.doAddMap(() => this.UpdateAt, "updated_at");
            this.doAddMap(() => this.LikeCount, "count_like");
            this.doAddMap(() => this.PlayCount, "count_play");
            this.doAddMap(() => this.ShareCount, "count_share");
            this.doAddMap(() => this.CommentCount, "count_comment");
        }
    }
}
