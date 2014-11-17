using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIMALAYA.PCDesktop.Tools.HotKey;
using XIMALAYA.PCDesktop.Tools.Player;
using XIMALAYA.PCDesktop.Tools.Themes;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Tools
{
    ///// <summary>
    ///// 全局flyout显示隐藏控制类
    ///// </summary>
    //public sealed class FlyoutVisibleSingleton : Singleton<FlyoutVisibleBase> { }
    /// <summary>
    /// 全局热键管理
    /// </summary>
    public sealed class HotKeysManagerSingleton : Singleton<HotKeysManager> { }
    /// <summary>
    /// 全局样式控制类
    /// </summary>
    public sealed class ThemeInfoManagerSingleton : Singleton<ThemeInfoManagerBase> { }
    /// <summary>
    /// 全局命令
    /// </summary>
    public sealed class CommandSingleton : Singleton<CommandBaseSingleton> { }
    /// <summary>
    /// 全局播放器
    /// </summary>
    public sealed class PlayerSingleton : Singleton<BassEngine> { }
    /// <summary>
    /// 全局数据
    /// </summary>
    public sealed class GlobalDataSingleton : Singleton<GlobalData> { }
    /// <summary>
    /// 
    /// </summary>
    public sealed class HttpWebRequestSingleton : Singleton<HttpWebRequestOpt> { }
}
