using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.User
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserData1 : UserData
    {
        /// <summary>
        /// 构造
        /// </summary>
        public UserData1()
            : base()
        {
            this.doAddMap(() => this.IsBeFollowed, "be_followed");
            this.doAddMap(() => this.CreateDate, "create_time");
            this.doAddMap(() => this.FollowerCount, "followers_counts");
            this.doAddMap(() => this.FollowingCount, "followings_counts");
            this.doAddMap(() => this.Gender, "gender");
            this.doAddMap(() => this.IsVerified, "isVerified");
            this.doAddMap(() => this.IsFollowed, "is_follow");
            this.doAddMap(() => this.MobileLargeLogo, "logoPic");
            this.doAddMap(() => this.NickName, "nickname");
            this.doAddMap(() => this.MobileSmallLogo, "smallPic");
            this.doAddMap(() => this.TrackCount, "tracks_counts");
            this.doAddMap(() => this.Uid, "uid");
            this.doAddMap(() => this.LastUpdateDate, "last_update");
        }
    }
}
