using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIMALAYA.PCDesktop.Core.Models.Category;
using XIMALAYA.PCDesktop.Core.Models.FocusImage;
using XIMALAYA.PCDesktop.Core.Models.Recommend;
using XIMALAYA.PCDesktop.Core.Models.Subject;
using XIMALAYA.PCDesktop.Core.Models.User;

namespace XIMALAYA.PCDesktop.Core.Models.Discover
{
    public class SuperExploreIndexResult : BaseResult
    {
        /// <summary>
        /// 焦点图
        /// </summary>
        public FocusImageResult FocusImages { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public CategoryResult Categories { get; set; }
        /// <summary>
        /// 专题
        /// </summary>
        public SubjectListResult Subjects { get; set; }
        /// <summary>
        /// 推荐专辑
        /// </summary>
        public RecommendAlbumResult Albums { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserData[] Users { get; set; }

        public SuperExploreIndexResult()
            : base()
        {
            this.doAddMap(() => this.FocusImages, "focusImages");
            this.doAddMap(() => this.Categories, "categories");
            this.doAddMap(() => this.Subjects, "subjects");
            this.doAddMap(() => this.Albums, "recommendAlbums");
            this.doAddMap(() => this.Users, "maybeLikeUsers");
        }
    }
}
