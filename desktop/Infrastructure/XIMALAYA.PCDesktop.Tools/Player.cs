using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using XIMALAYA.PCDesktop.Tools.Player;

namespace XIMALAYA.PCDesktop.Tools
{
    /// <summary>
    /// 全局播放器类
    /// </summary>
    public class PlayerSingleton : Singleton<BassEngine>
    {
        
    }
}
