using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.User
{
    /// <summary>
    /// 多用户
    /// </summary>
    public class UserData5 : UserData
    {
        public UserData5()
            : base()
        {
            this.doAddMap(() => this.FollowerCount, "followers_counts");
            this.doAddMap(() => this.FollowingCount, "followings_counts");
            this.doAddMap(() => this.IsVerified, "isVerified");
            this.doAddMap(() => this.IsFollowed, "is_follow");
            this.doAddMap(() => this.NickName, "nickname");
            this.doAddMap(() => this.Uid, "uid");
            this.doAddMap(() => this.PersonDescribe, "personDescribe");
            this.doAddMap(() => this.MobileSmallLogo, "smallLogo");
            this.doAddMap(() => this.TrackCount, "tracks_counts");
            this.doAddMap(() => this.MobileMiddleLogo, "user_cover_path_290");
        }
    }
}
