using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Discover
{
    /// <summary>
    /// 发现页 热门声音接口数据
    /// </summary>
    public class HotSoundsResult : BaseResult
    {
        public Category.CategoryData[] Categories { get; set; }

        public HotSoundsResult()
        {
            this.doAddMap(() => this.Categories, "categories");
        }
    }
}
