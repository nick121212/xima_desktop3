using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIMALAYA.PCDesktop.Core.Models.Sound;

namespace XIMALAYA.PCDesktop.Core.Models.MutiData
{
    public class MutiSoundResult : BaseResult
    {
        public long TotalCount { get; set; }
        public string LongTitle { get; set; }
        public string ShortTitle { get; set; }
        public SoundData[] Sounds { get; set; }

        public MutiSoundResult()
        {
            this.doAddMap(() => this.TotalCount, "count");
            this.doAddMap(() => this.LongTitle, "long_title");
            this.doAddMap(() => this.ShortTitle, "short_title");
            this.doAddMap(() => this.Sounds, "list");
        }
    }
}
