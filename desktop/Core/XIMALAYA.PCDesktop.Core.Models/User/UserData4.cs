using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.User
{
    /// <summary>
    /// 喜欢声音的用户列表
    /// </summary>
    public class UserData4 : UserData
    {
        public UserData4()
            : base()
        {
            this.doAddMap(() => this.AlbumCount, "albums");

            this.doAddMap(() => this.FollowerCount, "followers");
            this.doAddMap(() => this.FollowingCount, "followings");
            this.doAddMap(() => this.IsFollowed, "isFollowed");
            this.doAddMap(() => this.IsVerified, "isVerified");
            this.doAddMap(() => this.NickName, "nickname");
            this.doAddMap(() => this.MobileSmallLogo, "smallLogo");
            this.doAddMap(() => this.TrackCount, "tracks");
            this.doAddMap(() => this.Uid, "uid");
        }
    }
}
