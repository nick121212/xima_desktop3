using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.User
{
    /// <summary>
    /// 用户详情页数据
    /// </summary>
    public class UserData3 : UserData
    {
        /// <summary>
        /// 构造
        /// </summary>
        public UserData3()
            : base()
        {
            this.doAddMap(() => this.AlbumCount, "albums");
            this.doAddMap(() => this.BackGroundLogo, "backgroundLogo");
            this.doAddMap(() => this.FavoriteAlbumCount, "favoriteAlbums");
            this.doAddMap(() => this.FavoriteAlbumIsUpdate, "favoriteAlbumIsUpdate");
            this.doAddMap(() => this.FollowerCount, "followers");
            this.doAddMap(() => this.FollowingTagCount, "followingTags");
            this.doAddMap(() => this.FollowingCount, "followings");
            this.doAddMap(() => this.IsHanPwd, "hasPwd");
            this.doAddMap(() => this.IsFollowed, "isFollowed");
            this.doAddMap(() => this.IsMutualFollowed, "isMutualFollowed");
            this.doAddMap(() => this.IsVerified, "isVerified");
            this.doAddMap(() => this.NoReadLetterCount, "leters");
            this.doAddMap(() => this.NoReadMessageCount, "messages");
            this.doAddMap(() => this.MobileLargeLogo, "mobileLargeLogo");
            this.doAddMap(() => this.MobileSmallLogo, "mobileSmallLogo");
            this.doAddMap(() => this.MobileMiddleLogo, "mobileMiddleLogo");
            this.doAddMap(() => this.NickName, "nickname");
            this.doAddMap(() => this.TotalSpace, "totalSpace");
            this.doAddMap(() => this.TrackCount, "tracks");
            this.doAddMap(() => this.Uid, "uid");
            this.doAddMap(() => this.UsedSpace, "usedSpace");
        }
    }
}
