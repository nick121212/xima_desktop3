using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIMALAYA.PCDesktop.Core.Models.Album;

namespace XIMALAYA.PCDesktop.Core.Models.MutiData
{
    public class MutiAlbumResult : BaseResult
    {
        public long TotalCount { get; set; }
        public string LongTitle { get; set; }
        public string ShortTitle { get; set; }
        public AlbumData[] Albums { get; set; }

        public MutiAlbumResult()
        {
            this.doAddMap(() => this.TotalCount, "count");
            this.doAddMap(() => this.LongTitle, "long_title");
            this.doAddMap(() => this.ShortTitle, "short_title");
            this.doAddMap(() => this.Albums, "list");
        }
    }
}
