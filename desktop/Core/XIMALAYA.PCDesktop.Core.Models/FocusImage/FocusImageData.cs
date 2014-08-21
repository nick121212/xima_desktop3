using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.FocusImage
{
    /// <summary>
    /// 焦点图数据
    /// </summary>
    public class FocusImageData : BaseData
    {
        public int ID { get; set; }
        public string ShortTitle { get; set; }
        public string LongTitle { get; set; }
        public string Pic { get; set; }
        public long TrackId { get; set; }
        public int Type { get; set; }
        public long Uid { get; set; }
        public long AlbumID { get; set; }
        public string Url { get; set; }

        public FocusImageData()
            : base()
        {
            this.doAddMap(() => this.ID, "id");
            this.doAddMap(() => this.ShortTitle, "shortTitle");
            this.doAddMap(() => this.TrackId, "trackId");
            this.doAddMap(() => this.LongTitle, "longTitle");
            this.doAddMap(() => this.Type, "type");
            this.doAddMap(() => this.Uid, "uid");
            this.doAddMap(() => this.Pic, "pic");
            this.doAddMap(() => this.AlbumID, "albumId");
            this.doAddMap(() => this.Url, "url");
        }
    }
}