using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using XIMALAYA.PCDesktop.Controls.Properties;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Controls
{
    /// <summary>
    /// 图片路径
    /// </summary>
    public class PathData : Singleton<PathData>
    {
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string name]
        {
            get
            {
                return Resources.ResourceManager.GetString(name);
            }
        }
        /// <summary>
        /// 索引器 获取Geimetry对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public Geometry this[string name, string key = ""]
        {
            get
            {
                return Geometry.Parse(this[name]);
            }
        }
       
        private PathData() { }
    }
}
