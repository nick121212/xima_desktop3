using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Album
{
    /// <summary>
    /// 用于标签下的专辑列表
    /// </summary>
    public class AlbumData2 : AlbumData
    {
        public AlbumData2()
            : base()
        {
            this.doAddMap("FXClassName", "AlbumData");
            this.doAddMap(() => this.AlbumCoverUrl290, "album_cover_path_290");
            this.doAddMap(() => this.AlbumCoverUrl640, "album_cover_path_640");
            this.doAddMap(() => this.AlbumCoverUrl86, "album_cover_path_86");
            this.doAddMap(() => this.AlbumID, "album_id");
            this.doAddMap(() => this.AlbumNickName, "album_nickname");
            this.doAddMap(() => this.AvatarPath, "avatar_path");
            this.doAddMap(() => this.AvatarPath86, "avatar_path_86");
            this.doAddMap(() => this.CategoryID, "category_id");
            this.doAddMap(() => this.CoverPath, "cover_path");
            this.doAddMap(() => this.CreateDate, "createdAt");
            this.doAddMap(() => this.DigStatus, "dig_status");
            this.doAddMap(() => this.HumanCategoryID, "human_category_id");
            this.doAddMap(() => this.Intro, "intro");
            this.doAddMap(() => this.IsCrawler, "is_crawler");
            this.doAddMap(() => this.IsDelete, "is_deleted");
            this.doAddMap(() => this.IsPublic, "is_public");
            this.doAddMap(() => this.IsPublish, "is_publish");
            this.doAddMap(() => this.IsRecordsDesc, "is_records_desc");
            this.doAddMap(() => this.IsV, "is_v");
            this.doAddMap(() => this.IsVerified, "isVerified");
            this.doAddMap(() => this.LastUptrackCoverPath, "last_uptrack_cover_path");
            this.doAddMap(() => this.LastUptrackDate, "last_uptrack_at");
            this.doAddMap(() => this.LastUptrackID, "last_uptrack_id");
            this.doAddMap(() => this.MusicCategory, "music_category");
            this.doAddMap(() => this.NickName, "nickname");
            this.doAddMap(() => this.OpType, "op_type");
            this.doAddMap(() => this.PlayCount, "plays_counts");
            this.doAddMap(() => this.RichIntro, "rich_intro");
            this.doAddMap(() => this.ShortIntro, "short_intro");
            this.doAddMap(() => this.SourceUrl, "source_url");
            this.doAddMap(() => this.Status, "status");
            this.doAddMap(() => this.Tags, "tags");
            this.doAddMap(() => this.Title, "title");
            this.doAddMap(() => this.TrackCount, "tracks_counts");
            this.doAddMap(() => this.TrackOrder, "tracks_order");
            this.doAddMap(() => this.Uid, "uid");
            this.doAddMap(() => this.UpdateDate, "updatedAt");
            this.doAddMap(() => this.UserSource, "user_source");
        }
    }
}
