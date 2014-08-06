using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.FocusImage
{

    public class FocusImageResult : BaseResult
    {
        public FocusImageData[] List { get; set; }
        public FocusImageResult()
            : base()
        {
            this.doAddMap(() => this.List, "list");
        }
    }
}
