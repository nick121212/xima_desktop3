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
    public class UserData : BaseData
    {
        /// <summary>
        /// id
        /// uid
        /// </summary>
        public long Uid { get; set; }
        /// <summary>
        /// 昵称
        /// nickname
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 用户大头像
        /// </summary>
        public string MobileLargeLogo { get; set; }
        /// <summary>
        /// 用户小头像
        /// </summary>
        public string MobileSmallLogo { get; set; }
        /// <summary>
        /// 关注的人数量
        /// </summary>
        public long FollowingCount { get; set; }
        /// <summary>
        /// 未读新鲜事数
        /// </summary>
        public long NoReadEventCount { get; set; }
        /// <summary>
        /// 未读消息数
        /// </summary>
        public long NoReadMessageCount { get; set; }
        /// <summary>
        /// 新增粉丝数
        /// </summary>
        public long NoReadFollowerCount { get; set; }
        /// <summary>
        /// 未读私信数
        /// </summary>
        public long NoReadLetterCount { get; set; }
        /// <summary>
        /// 关注的领域数
        /// </summary>
        public long FollowingTagCount { get; set; }
        /// <summary>
        /// 声音数量
        /// </summary>
        public long TrackCount { get; set; }
        /// <summary>
        /// 粉丝数
        /// </summary>
        public long FollowerCount { get; set; }
        /// <summary>
        /// 专辑数
        /// </summary>
        public long AlbumCount { get; set; }
        /// <summary>
        /// 第三方未读好友数
        /// </summary>
        public long ThirdNewRegisters { get; set; }
        /// <summary>
        /// 收藏专辑的更新数
        /// </summary>
        public long UpdateNewAlbumCount { get; set; }
        /// <summary>
        /// 是否加V
        /// isVerified
        /// </summary>
        public bool IsVerified { get; set; }
        /// <summary>
        /// 是否关注了他
        /// </summary>
        public bool IsFollowed { get; set; }
        /// <summary>
        /// 是否被他关注
        /// </summary>
        public bool IsBeFollowed { get; set; }
        /// <summary>
        /// 描述
        /// personDescribe
        /// </summary>
        public string PersonDescribe { get; set; }
        /// <summary>
        /// 个人头衔
        /// </summary>
        public string PTitle { get; set; }
        /// <summary>
        /// 个人简介
        /// </summary>
        public string PersonalSignature { get; set; }
        /// <summary>
        /// 背景图片
        /// </summary>
        public string BackGroundLogo { get; set; }
        /// <summary>
        /// 是否设置了密码
        /// </summary>
        public bool IsHanPwd { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateDate { get; set; }
        /// <summary>
        ///性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public long LastUpdateDate { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        public UserData()
            : base()
        {
            this.doAddMap("FXClassName", "UserData");
            this.doAddMap(() => this.NickName, "nickname");
        }
    }
}
