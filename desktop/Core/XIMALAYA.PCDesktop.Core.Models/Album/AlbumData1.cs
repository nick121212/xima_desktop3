using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Album
{
    public class AlbumData1 : AlbumData
    {
        public AlbumData1()
            : base()
        {
            this.doAddMap("FXClassName", "AlbumData");
            this.doAddMap(() => this.Title, "title");
            this.doAddMap(() => this.AlbumCoverUrl290, "albumCoverUrl290");
            this.doAddMap(() => this.AlbumID, "id");
            this.doAddMap(() => this.PlayCount, "playsCounts");
            this.doAddMap(() => this.LastUptrackDate, "lastUptrackAt");
            this.doAddMap(() => this.SerialState, "serialState");
        }
    }
}
