using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.User
{
    /// <summary>
    /// 声音详情页用户信息
    /// </summary>
    public class UserData2 : UserData
    {
        /// <summary>
        /// 构造
        /// </summary>
        public UserData2()
            : base()
        {
            this.doAddMap(() => this.Uid, "uid");
            this.doAddMap(() => this.NickName, "nickname");
            this.doAddMap(() => this.IsVerified, "isVerified");
            this.doAddMap(() => this.MobileSmallLogo, "smallLogo");
            this.doAddMap(() => this.IsFollowed, "isFollowed");
            this.doAddMap(() => this.FollowerCount, "followers");
            this.doAddMap(() => this.FollowingCount, "followings");
            this.doAddMap(() => this.TrackCount, "tracks");
            this.doAddMap(() => this.AlbumCount, "albums");
            this.doAddMap(() => this.PTitle, "ptitle");
            this.doAddMap(() => this.PersonDescribe, "personDescribe");

        }
    }
}
