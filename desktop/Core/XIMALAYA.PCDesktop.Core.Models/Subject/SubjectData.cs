using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Subject
{
    public class SubjectData : BaseData
    {
        /// <summary>
        /// 专题ID
        /// specialId
        /// </summary>
        public int SpecialId { get; set; }
        /// <summary>
        /// 专题类型，1为专辑；2个声音；
        /// contentType
        /// </summary>
        public int ContentType { get; set; }

        /// <summary>
        /// 专题封面图大
        /// coverPathBig
        /// </summary>
        public string CoverPathBig { get; set; }

        /// <summary>
        /// 专题封面图小
        /// coverPathSmall
        /// </summary>
        public string CoverPathSmall { get; set; }
        /// <summary>
        /// 专题标题
        /// title
        /// </summary>
        public string Title { get; set; }

        public SubjectData()
            : base()
        {
            this.doAddMap(() => this.SpecialId, "specialId");
            this.doAddMap(() => this.ContentType, "contentType");
            this.doAddMap(() => this.CoverPathBig, "coverPathBig");
            this.doAddMap(() => this.CoverPathSmall, "coverPathSmall");
            this.doAddMap(() => this.Title, "title");
        }
    }
}
