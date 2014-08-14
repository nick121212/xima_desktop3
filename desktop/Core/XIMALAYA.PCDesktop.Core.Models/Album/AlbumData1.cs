using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Album
{
    /// <summary>
    /// 分类下的标签下的专辑
    /// </summary>
    public class AlbumData1 : AlbumData
    {
        public AlbumData1()
            : base()
        {
           
            this.doAddMap(() => this.Title, "title");
            this.doAddMap(() => this.AlbumCoverUrl290, "albumCoverUrl290");
            //this.doAddMap(() => this.AlbumCoverUrl290, "coverSmall");
            this.doAddMap(() => this.AlbumID, "id");
            this.doAddMap(() => this.PlayCount, "playsCounts");
            this.doAddMap(() => this.LastUptrackDate, "lastUptrackAt");
            this.doAddMap(() => this.SerialState, "serialState");
        }
    }
}
