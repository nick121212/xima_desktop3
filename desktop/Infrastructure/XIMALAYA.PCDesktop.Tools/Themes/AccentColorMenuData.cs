using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro;
using Microsoft.Practices.Prism.Commands;

namespace XIMALAYA.PCDesktop.Tools.Themes
{
    /// <summary>
    /// metro 色彩集合操作
    /// </summary>
    public class AccentColorMenuData
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// BorderBrush
        /// </summary>
        public Brush BorderColorBrush { get; set; }
        /// <summary>
        /// ColorBrush
        /// </summary>
        public Brush ColorBrush { get; set; }

        private ICommand changeAccentCommand;
        /// <summary>
        /// 命令
        /// </summary>
        public ICommand ChangeAccentCommand
        {
            get { return this.changeAccentCommand ?? (changeAccentCommand = new DelegateCommand<object>(x => this.DoChangeTheme(x), x => true)); }
        }
        /// <summary>
        /// 改变色彩
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var accent = ThemeManager.GetAccent(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
        }
    }
}
