using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace XIMALAYA.PCDesktop.Tools.Untils
{
    /// <summary>
    /// Tileset
    /// </summary>
    public class TileSet
    {
        public Geometry ContentPath { get; set; }
        public Geometry ContentCheckPath { get; set; }
        public Geometry ContentThreeStatePath { get; set; }
        public string Title { get; set; }
        public bool IsCircle { get; set; }
        public bool IsThreeState { get; set; }

        public TileSet()
        {
            this.IsThreeState = false;
            this.IsCircle = true;
        }
    }
}
