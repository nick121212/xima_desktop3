using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using XIMALAYA.PCDesktop.Controls.Properties;
using XIMALAYA.PCDesktop.Tools;

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
        public Geometry this[string name, string key = ""]
        {
            get
            {
                return Geometry.Parse(this[name]);
            }
        }

        /// <summary>
        /// 设置按钮
        /// </summary>
        public Geometry ToolbarSettings
        {
            get
            {
                return Geometry.Parse(this["setting"]);
            }
        }

        /// <summary>
        /// 下一首
        /// </summary>
        public Geometry ToolbarNextPath
        {
            get
            {
                return Geometry.Parse(this["next"]);
            }
        }
        /// <summary>
        /// 上一首
        /// </summary>
        public Geometry ToolbarPrevPath
        {
            get
            {
                return Geometry.Parse(this["prev"]);
            }
        }
        /// <summary>
        /// 播放按钮
        /// </summary>
        public Geometry ToolbarPlayPath
        {
            get
            {
                return Geometry.Parse(this["play"]);
            }
        }
        /// <summary>
        /// 暂停按钮
        /// </summary>
        public Geometry ToolbarPausePath
        {
            get
            {
                return Geometry.Parse(this["pause"]);
            }
        }
        /// <summary>
        /// 提交按钮
        /// </summary>
        public Geometry Submit
        {
            get
            {
                return Geometry.Parse(this["submit"]);
            }
        }
        /// <summary>
        /// 音量按钮
        /// </summary>
        public Geometry VolumePath
        {
            get
            {
                return Geometry.Parse(this["volume"]);
            }
        }
        /// <summary>
        /// 静音按钮
        /// </summary>
        public Geometry MutedPath
        {
            get
            {
                return Geometry.Parse(this["muted"]);
            }
        }
        /// <summary>
        /// 访问用户按钮
        /// </summary>
        public Geometry VistorUserPath
        {
            get
            {
                return Geometry.Parse(this["vistor_user"]);
            }
        }
        /// <summary>
        /// 声音数按钮
        /// </summary>
        public Geometry TrackCountPath
        {
            get
            {
                return Geometry.Parse(this["track_count"]);
            }
        }
        /// <summary>
        /// 订阅专辑按钮
        /// </summary>
        public Geometry SubscribeAlbumPath
        {
            get
            {
                return Geometry.Parse(this["subscribe_album"]);
            }
        }
        /// <summary>
        /// 显示声音列表
        /// </summary>
        public Geometry ListViewPath
        {
            get
            {
                return Geometry.Parse(this["list_view"]);
            }
        }
        /// <summary>
        /// 搜索路径
        /// </summary>
        public Geometry SearchPath
        {
            get
            {
                return Geometry.Parse(this["search"]);
            }
        }
        /// <summary>
        /// 发现路径
        /// </summary>
        public Geometry DiscoverPath
        {
            get
            {
                return Geometry.Parse(this["discover"]);
            }
        }
        /// <summary>
        /// 下载
        /// </summary>
        public Geometry DownloadPath
        {
            get
            {
                return Geometry.Parse(this["download"]);
            }
        }
        /// <summary>
        /// 箭头
        /// </summary>
        public Geometry ArrowPath
        {
            get
            {
                return Geometry.Parse(this["arrow"]);
            }
        }
        /// <summary>
        /// 播放模式1
        /// </summary>
        public Geometry PlayModePath1
        {
            get
            {
                return Geometry.Parse(this["play_mode_1"]);
            }
        }
        /// <summary>
        /// 播放模式2
        /// </summary>
        public Geometry PlayModePath2
        {
            get
            {
                return Geometry.Parse(this["play_mode_2"]);
            }
        }
        /// <summary>
        /// 播放模式3
        /// </summary>
        public Geometry PlayModePath3
        {
            get
            {
                return Geometry.Parse(this["play_mode_3"]);
            }
        }
        /// <summary>
        /// 分享按钮
        /// </summary>
        public Geometry SharePath
        {
            get
            {
                return Geometry.Parse(this["share"]);
            }
        }
        /// <summary>
        /// 豆瓣分享按钮
        /// </summary>
        public Geometry ShareDouBanPath
        {
            get
            {
                return Geometry.Parse(this["share_douban"]);
            }
        }
        /// <summary>
        /// 新浪按钮_1
        /// </summary>
        public Geometry ShareSingPath1
        {
            get
            {
                return Geometry.Parse(this["share_sing_1"]);
            }
        }
        /// <summary>
        /// 新浪按钮_2
        /// </summary>
        public Geometry ShareSingPath2
        {
            get
            {
                return Geometry.Parse(this["share_sing_2"]);
            }
        }
        /// <summary>
        /// 新浪按钮_3
        /// </summary>
        public Geometry ShareSingPath3
        {
            get
            {
                return Geometry.Parse(this["share_sing_3"]);
            }
        }
        /// <summary>
        /// 新浪按钮_4
        /// </summary>
        public Geometry ShareSingPath4
        {
            get
            {
                return Geometry.Parse(this["share_sing_4"]);
            }
        }
        /// <summary>
        /// 开心按钮
        /// </summary>
        public Geometry ShareKaixinPath
        {
            get
            {
                return Geometry.Parse(this["share_kaixin"]);
            }
        }

        private PathData() { }
    }
}
