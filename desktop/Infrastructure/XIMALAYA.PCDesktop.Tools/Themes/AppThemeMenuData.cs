using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro;

namespace XIMALAYA.PCDesktop.Tools.Themes
{
    /// <summary>
    /// metro 样式操作
    /// </summary>
    public class AppThemeMenuData : AccentColorMenuData
    {
        /// <summary>
        /// 改变样式
        /// </summary>
        /// <param name="sender"></param>
        protected override void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);
        }
    }
}
