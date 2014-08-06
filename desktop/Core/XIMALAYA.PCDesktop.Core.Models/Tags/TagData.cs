using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XIMALAYA.PCDesktop.Core.Models.Tags
{
    /// <summary>
    /// 标签列表
    /// </summary>
    public class TagData : BaseData
    {
        /// <summary>
        /// 标签的id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 标签图片
        /// </summary>
        public string CoverPath { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        public TagData()
            : base()
        {
            this.doAddMap(() => this.CategoryId, "category_id");
            this.doAddMap(() => this.CoverPath, "cover_path");
            this.doAddMap(() => this.TagName, "tname");
        }
    }
}
