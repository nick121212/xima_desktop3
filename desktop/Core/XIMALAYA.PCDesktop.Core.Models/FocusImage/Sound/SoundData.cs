using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Sound
{
    /// <summary>
    /// 声音数据
    /// </summary>
    public class SoundData : BaseData
    {
        /// <summary>
        /// ID
        /// </summary>
        public long TrackId { get; set; }
        /// <summary>
        /// 标签，中间用逗号分隔
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 用户uid
        /// </summary>
        public long UID { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 用户是否加V
        /// </summary>
        public bool IsVerified { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string SmallLogo { get; set; }
        /// <summary>
        /// 声音封面图640*640
        /// </summary>
        public string CoverLarge { get; set; }
        /// <summary>
        /// 声音封面图60*60
        /// </summary>
        public string CoverSmall { get; set; }
        /// <summary>
        /// 声音封面图180*180
        /// </summary>
        public string CoverWebSmall { get; set; }
        /// <summary>
        /// 声音所属专辑ID
        /// </summary>
        public long AlbumID { get; set; }
        /// <summary>
        /// 声音所属专辑标题
        /// </summary>
        public string AlbumTitle { get; set; }
        /// <summary>
        /// 声音所属专辑图片
        /// </summary>
        public string AlbumImage { get; set; }
        /// <summary>
        /// 声音所属专辑图片
        /// </summary>
        public string AlbumImage86 { get; set; }
        /// <summary>
        /// 声音所属专辑图片
        /// </summary>
        public string AlbumImage640 { get; set; }
        /// <summary>
        /// 分类的ID
        /// </summary>
        public long CategoryID { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Intro { get; set; }
        /// <summary>
        /// 播放地址
        /// </summary>
        public string PlayUrl32 { get; set; }
        /// <summary>
        /// 播放地址
        /// </summary>
        public string PlayUrl64 { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateAt { get; set; }
        /// <summary>
        /// 转码状态 1：正在处理中；2：转码完成；3：转码失败
        /// </summary>
        public int ProcessState { get; set; }
        /// <summary>
        /// 声音时长，单位秒
        /// </summary>
        public double Duration { get; set; }
        /// <summary>
        /// 是否是自己喜欢过的
        /// </summary>
        public bool IsLike { get; set; }
        /// <summary>
        /// 喜欢次数
        /// </summary>
        public long LikeCount { get; set; }
        /// <summary>
        /// 评论次数
        /// </summary>
        public long CommentCount { get; set; }
        /// <summary>
        /// 播放次数
        /// </summary>
        public long PlayCount { get; set; }
        /// <summary>
        /// 分享次数
        /// </summary>
        public long ShareCount { get; set; }
        /// <summary>
        /// 是否转发过
        /// </summary>
        public bool IsRelay { get; set; }
        /// <summary>
        /// 是否公开，不公开就是私密
        /// </summary>
        public bool IsPlulic { get; set; }
        /// <summary>
        /// 是否关注了声音的创建者
        /// </summary>
        public bool IsFollowed { get; set; }
        /// <summary>
        /// 操作类型1：上传；2：转发
        /// </summary>
        public int OpType { get; set; }
        /// <summary>
        /// 如果是转发，代表转发人的ID
        /// </summary>
        public long RefUid { get; set; }
        /// <summary>
        /// 如果是转发，代表转发人的昵称
        /// </summary>
        public long RefNickName { get; set; }
        /// <summary>
        /// 如果是转发，代表转发人的头像
        /// </summary>
        public long RefSmallLogo { get; set; }
        /// <summary>
        /// 如果是转发，代表转发内容
        /// </summary>
        public string refComment { get; set; }
        /// <summary>
        /// 转发内容对应的ID
        /// </summary>
        public string CommentID { get; set; }
        /// <summary>
        /// 波形
        /// </summary>
        public string WaveForm { get; set; }
        /// <summary>
        /// 上传类型 上传源 1:iphone/2:电脑/3:android
        /// </summary>
        public int UploadSource { get; set; }
        /// <summary>
        /// 活动id
        /// </summary>
        public int ActivityId { get; set; }

        public SoundData()
            : base()
        {
            this.doAddMap(() => this.TrackId, "toTid");
            this.doAddMap(() => this.AlbumID, "albumId");
            this.doAddMap(() => this.AlbumTitle, "albumName");
            this.doAddMap(() => this.CommentCount, "comments");
            this.doAddMap(() => this.CreateAt, "createdAt");
            this.doAddMap(() => this.Duration, "duration");
            this.doAddMap(() => this.IsLike, "isLike");
            this.doAddMap(() => this.IsRelay, "isRelay");
            this.doAddMap(() => this.LikeCount, "likes");
            this.doAddMap(() => this.PlayUrl32, "url");
            this.doAddMap(() => this.PlayCount, "playtimes");
            this.doAddMap(() => this.ShareCount, "shares");
            this.doAddMap(() => this.Title, "title");
            this.doAddMap(() => this.UploadSource, "uploadSource");
            this.doAddMap(() => this.OpType, "userSource");
            this.doAddMap(() => this.WaveForm, "waveform");
            this.doAddMap(() => this.ActivityId, "activity_id");
        }
    }
}
