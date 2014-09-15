using System.ComponentModel.Composition;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Themes;

namespace XIMALAYA.PCDesktop.Modules.Settings
{
    /// <summary>
    /// 设置中样式的model
    /// </summary>
    [Export(typeof(SettingsViewModel))]
    public class SettingsViewModel : BaseViewModel
    {
        #region 字段

        private AccentColorMenuData _SelectedAccentColor = null;
        private AppThemeMenuData _SelectedTheme = null;

        #endregion

        #region 属性

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
                    ThemeInfoManagerSingleton.Instance.AccentColor = value;
                }
            }
        }
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
                    ThemeInfoManagerSingleton.Instance.Theme = value;
                }
            }
        }

        #endregion

        #region 构造

        /// <summary>
        /// 构造
        /// </summary>
        public SettingsViewModel()
        {
            this.SelectedTheme = ThemeInfoManagerSingleton.Instance.Theme;
            this.SelectedAccentColor = ThemeInfoManagerSingleton.Instance.AccentColor;
        }

        #endregion
    }
}
