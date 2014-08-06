using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Category
{
    public class CategoryDiscoverData : CategoryData
    {

        public CategoryDiscoverData()
            : base()
        {
            this.doAddMap(() => this.CoverPath, "cover_path");
        }
    }
}
