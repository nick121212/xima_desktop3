using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Tools
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
        public const string SuperExploreIndex = WebPath + "/m/super_explore_index";

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

        /// <summary>
        /// 专辑详情页接口
        /// </summary>
        public const string AlbumInfo = WebPath + "/mobile/others/ca/album/track";
    }
}
