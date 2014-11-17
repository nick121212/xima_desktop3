
namespace XIMALAYA.PCDesktop.Untils
{
    /// <summary>
    /// 地址引用
    /// </summary>
    public static class WellKnownUrl
    {
        private const string WebPath = "http://mobile.ximalaya.com";

        #region 发现页面接口

        /// <summary>
        /// 获取焦点图列表
        /// </summary>
        public const string FocsImage = WebPath + "/m/focus_image";

        /// <summary>
        /// 发现->分类列表
        /// </summary>
        public const string CategoryList = WebPath + "/mobile/category/sound";

        /// <summary>
        /// 首页推荐专题
        /// </summary>
        public const string SubjectList = WebPath + "/m/index_subjects";

        /// <summary>
        /// 发现页总接口（焦点图，分类，推荐专辑，专题，推荐喜欢的用户）
        /// </summary>
        public const string SuperExploreIndex = WebPath + "/m/super_explore_index2";

        /// <summary>
        /// 分类下的标签接口
        /// </summary>
        public const string CategoryTags = WebPath + "/m/category_tag_list";

        /// <summary>
        /// 发现二级页面--专辑列表
        /// </summary>
        public const string CategoryTagAlbums = WebPath + "/m/explore_album_list";

        /// <summary>
        /// 发现页 -- 热门声音
        /// </summary>
        public const string HotSounds = WebPath + "/m/index_hotsounds";

        #endregion

        #region 详情页

        /// <summary>
        /// 专辑详情页接口
        /// </summary>
        public const string AlbumInfo = WebPath + "/mobile/others/ca/album/track";

        /// <summary>
        /// 声音详情页
        /// </summary>
        public const string SoundInfo = WebPath + "/mobile/track/{0}";

        /// <summary>
        /// 声音详情页
        /// </summary>
        public const string SoundInfoNew = WebPath + "/mobile/track/detail";

        /// <summary>
        /// 单个用户的详细信息
        /// </summary>
        public const string UserDetailInfo = WebPath + "/mobile/others/homePage";

        #endregion

        #region 发现页接口

        /// <summary>
        /// 搜索接口
        /// </summary>
        public const string SearchPath = WebPath + "/s/mobile/search";

        /// <summary>
        /// 单个声音分享界面内容
        /// </summary>
        public const string ShareSoundInfo = WebPath + "/mobile/track/share/content";

        /// <summary>
        /// 单个专辑分享界面内容
        /// </summary>
        public const string ShareAlbumInfo = WebPath + "/mobile/album/share/content";

        /// <summary>
        /// 单个用户分享界面内容
        /// </summary>
        public const string ShareUserInfo = WebPath + "/mobile/user/share/content";
        
        /// <summary>
        /// 喜欢声音的用户列表
        /// </summary>
        public const string LikedSoundUsers = WebPath + "/mobile/track/lovers";

        /// <summary>
        /// 他人专辑列表
        /// </summary>
        public const string UserAlbums = WebPath + "/mobile/others/album";

        /// <summary>
        /// 多用户，多专辑，多声音
        /// </summary>
        public const string MutiData = WebPath + "/m/focus_list";

        #endregion

        #region 登录相关

        /// <summary>
        /// 本站登录
        /// </summary>
        public const string LoginPath = WebPath + "/mobile/login";
        /// <summary>
        /// 第三方登录接口地址
        /// </summary>
        public const string ThirdLoginPath = WebPath + "/passport/auth/{0}/authorize?responseJson=true";
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        public const string LoginUserInfo = WebPath + "/mobile/user_info";

        #endregion
    }
}
