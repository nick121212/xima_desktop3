using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Sound
{
    /// <summary>
    /// 专辑下的声音
    /// </summary>
    public class SoundData3 : SoundData
    {
        public SoundData3()
            : base()
        {
            this.doAddMap("FXClassName", "SoundData");

            this.doAddMap(() => this.CoverSmall, "coverSmall");
            this.doAddMap(() => this.CoverWebSmall, "coverWebSmall");
            this.doAddMap(() => this.TrackId, "id");
            this.doAddMap(() => this.PlayUrl32, "playPath32");
            this.doAddMap(() => this.PlayUrl64, "playPath64");
            this.doAddMap(() => this.PlayCount, "playsCounts");
            this.doAddMap(() => this.Title, "title");
        }
    }
}
