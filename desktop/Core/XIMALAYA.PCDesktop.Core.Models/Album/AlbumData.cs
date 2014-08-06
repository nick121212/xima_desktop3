using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Album
{
    public class AlbumData : BaseData
    {
        /// <summary>
        /// 专辑ID
        /// id
        /// </summary>
        public long AlbumID { get; set; }
        /// <summary>
        /// 专辑标题
        /// title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 专辑图片 290*290
        /// albumCoverUrl290
        /// </summary>
        public string AlbumCoverUrl290 { get; set; }
        /// <summary>
        /// 专辑图片86*86
        /// </summary>
        public string AlbumCoverUrl86 { get; set; }
        /// <summary>
        /// 专辑图片140*140
        /// </summary>
        public string AlbumCoverUrl140 { get; set; }
        /// <summary>
        /// 专辑图片640*640
        /// </summary>
        public string AlbumCoverUrl640 { get; set; }
        /// <summary>
        /// 专辑所属人昵称
        /// </summary>
        public string AlbumNickName { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string AvatarPath { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string AvatarPath86 { get; set; }
        /// <summary>
        /// 分类id
        /// </summary>
        public long CategoryID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CoverPath { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DigStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HumanCategoryID { get; set; }
        /// <summary>
        /// 专辑简介
        /// </summary>
        public string Intro { get; set; }
        /// <summary>
        /// 是否加V
        /// </summary>
        public bool IsVerified { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsCrawler { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsPublic { get; set; }
        /// <summary>
        /// 是否连载
        /// </summary>
        public bool IsPublish { get; set; }
        /// <summary>
        /// 是否喜欢
        /// </summary>
        public bool IsFavorite { get; set; }
        /// <summary>
        /// 是否倒叙排列
        /// </summary>
        public bool IsRecordsDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsV { get; set; }
        /// <summary>
        /// 最新声音更新时间
        /// </summary>
        public long LastUptrackDate { get; set; }
        /// <summary>
        /// 最新声音图片
        /// </summary>
        public string LastUptrackCoverPath { get; set; }
        /// <summary>
        /// 最新声音ID
        /// </summary>
        public long LastUptrackID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MusicCategory { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OpType { get; set; }
        /// <summary>
        /// 播放次数
        /// </summary>
        public long PlayCount { get; set; }
        /// <summary>
        /// 富文本简介
        /// </summary>
        public string RichIntro { get; set; }
        /// <summary>
        /// 短简介
        /// </summary>
        public string ShortIntro { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SourceUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 所属标签
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 声音数量
        /// </summary>
        public int TrackCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TrackOrder { get; set; }
        /// <summary>
        /// UID
        /// </summary>
        public long Uid { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public long UpdateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UserSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SerialState { get; set; }


        public AlbumData()
            : base()
        {
            this.doAddMap(() => this.AlbumID, "id");
            this.doAddMap(() => this.Title, "title");
            this.doAddMap(() => this.AlbumCoverUrl290, "albumCoverUrl290");
        }
    }
}
