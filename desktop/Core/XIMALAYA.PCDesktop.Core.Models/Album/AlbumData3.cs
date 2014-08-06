using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Album
{
    /// <summary>
    /// 用于专辑详情接口
    /// </summary>
    public class AlbumData3 : AlbumData
    {
        public AlbumData3()
            : base()
        {
            this.doAddMap("FXClassName", "AlbumData");

            this.doAddMap(() => this.AlbumCoverUrl140, "coverLarge");
            this.doAddMap(() => this.AlbumCoverUrl290, "coverWebLarge");
            this.doAddMap(() => this.AlbumCoverUrl640, "coverOrigin");
            this.doAddMap(() => this.AlbumCoverUrl86, "coverSmall");
            this.doAddMap(() => this.AlbumID, "albumId");
            this.doAddMap(() => this.AvatarPath, "avatarPath");
            this.doAddMap(() => this.CategoryID, "categoryId");
            this.doAddMap(() => this.CreateDate, "createdAt");
            this.doAddMap(() => this.Intro, "intro");
            this.doAddMap(() => this.IsVerified, "isVerified");
            this.doAddMap(() => this.NickName, "nickname");
            this.doAddMap(() => this.PlayCount, "playTimes");
            this.doAddMap(() => this.RichIntro, "rich_intro");
            this.doAddMap(() => this.Status, "status");
            this.doAddMap(() => this.Tags, "tags");
            this.doAddMap(() => this.Title, "title");
            this.doAddMap(() => this.TrackCount, "tracks");
            this.doAddMap(() => this.IsFavorite,"isFavorite");
            this.doAddMap(() => this.Uid, "uid");
            this.doAddMap(() => this.UpdateDate, "updatedAt");
            this.doAddMap(() => this.SerialState, "serializeStatus");
            this.doAddMap(() => this.LastUptrackDate, "lastUptrackAt");
        }
    }
}
