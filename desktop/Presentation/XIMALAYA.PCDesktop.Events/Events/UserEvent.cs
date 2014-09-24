using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;

namespace XIMALAYA.PCDesktop.Common.Events
{
    public class UserEventArgument
    {
        public string RegionName { get; set; }
        public long UID { get; set; }
    }

    public class UserEvent<T> : CompositePresentationEvent<T> { }
}
