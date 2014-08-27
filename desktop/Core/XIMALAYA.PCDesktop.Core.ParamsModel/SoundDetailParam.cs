using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.ParamsModel
{
    public class SoundDetailParam : BaseParam
    {
        /// <summary>
        /// 声音ID
        /// </summary>
        [DataMember(Name = "trackId", IsRequired = true, Order = 1)]
        public long TrackId { get; set; }
    }
}
