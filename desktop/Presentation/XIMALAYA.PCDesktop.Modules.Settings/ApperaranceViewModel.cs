using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MahApps.Metro;
using Microsoft.Practices.Prism.ViewModel;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Themes;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Modules.Settings
{
    /// <summary>
    /// 设置中样式的model
    /// </summary>
    [Export(typeof(ApperaranceViewModel))]
    public class ApperaranceViewModel : BaseViewModel
    {
        private AccentColorMenuData _SelectedAccentColor = null;
        /// <summary>
        /// 挡圈选中的颜色
        /// </summary>
        public AccentColorMenuData SelectedAccentColor
        {
            get
            {
                return _SelectedAccentColor;
            }
            set
            {
                if (value != _SelectedAccentColor)
                {
                    _SelectedAccentColor = value;
                    ThemeInfoManager.Instance.AccentColor = value;
                    //_SelectedAccentColor.ChangeAccentCommand.Execute(_SelectedAccentColor);
                    this.RaisePropertyChanged(() => this.SelectedAccentColor);
                }
            }
        }

        private AppThemeMenuData _SelectedTheme = null;
        /// <summary>
        /// 当前选中的样式
        /// </summary>
        public AppThemeMenuData SelectedTheme
        {
            get
            {
                return _SelectedTheme;
            }
            set
            {
                if (value != _SelectedTheme)
                {
                    _SelectedTheme = value;
                    ThemeInfoManager.Instance.Theme = value;
                    //_SelectedTheme.ChangeAccentCommand.Execute(_SelectedTheme);
                    this.RaisePropertyChanged(() => this.SelectedTheme);
                }
            }
        }
        /// <summary>
        /// 构造
        /// </summary>
        public ApperaranceViewModel()
        {
            this.SelectedTheme = ThemeInfoManager.Instance.Theme;
            this.SelectedAccentColor = ThemeInfoManager.Instance.AccentColor;
        }
    }
}
