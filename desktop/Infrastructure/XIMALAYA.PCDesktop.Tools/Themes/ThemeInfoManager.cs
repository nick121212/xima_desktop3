﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro;
using Microsoft.Practices.Prism.ViewModel;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Tools.Themes
{
    /// <summary>
    /// Metro 样式管理器
    /// </summary>
    public class ThemeInfoManagerBase : NotificationObject
    {
        #region 字段

        private AppThemeMenuData _Theme = null;
        private AccentColorMenuData _AccentColor = null;

        #endregion

        #region 属性

        /// <summary>
        /// 所有色彩
        /// </summary>
        public IEnumerable<string> BrushResources { get; private set; }
        /// <summary>
        /// 色彩集合
        /// </summary>
        public List<AccentColorMenuData> AccentColors { get; set; }
        /// <summary>
        /// 样式集合
        /// </summary>
        public List<AppThemeMenuData> AppThemes { get; set; }
        /// <summary>
        /// 当前样式
        /// </summary>
        public AppThemeMenuData Theme
        {
            get
            {
                return _Theme;
            }
            set
            {
                if (value != _Theme)
                {
                    _Theme = value;
                    _Theme.ChangeAccentCommand.Execute(_Theme);
                    this.RaisePropertyChanged(() => this.Theme);
                }
            }
        }
        /// <summary>
        /// 当前色彩
        /// </summary>
        public AccentColorMenuData AccentColor
        {
            get
            {
                return _AccentColor;
            }
            set
            {
                if (value != _AccentColor)
                {
                    _AccentColor = value;
                    _AccentColor.ChangeAccentCommand.Execute(_AccentColor);
                    this.RaisePropertyChanged(() => this.AccentColor);
                }
            }
        }

        public string AccentColorName
        {
            get
            {
                if (this.AccentColor != null)
                {
                    return this.AccentColor.Name;
                }

                return string.Empty;
            }
            set
            {
                this.AccentColor = this.AccentColors.FirstOrDefault(x => x.Name.Equals(value));
                this.RaisePropertyChanged(() => this.ThemeName);
            }
        }

        /// <summary>
        /// 佔位服务
        /// </summary>
        public string ThemeName
        {
            get
            {
                if (this.Theme != null)
                {
                    return this.Theme.Name;
                }

                return string.Empty;
            }
            set
            {
                this.Theme = this.AppThemes.FirstOrDefault(x => x.Name.Equals(value));
                this.RaisePropertyChanged(() => this.ThemeName);
            }
        }


        #endregion

        #region 构造

        private ThemeInfoManagerBase()
        {
            this.AccentColors = ThemeManager.Accents
                                           .Select(a => new AccentColorMenuData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush })
                                           .ToList();
            this.AppThemes = ThemeManager.AppThemes
                                           .Select(a => new AppThemeMenuData() { Name = a.Name, BorderColorBrush = a.Resources["BlackColorBrush"] as Brush, ColorBrush = a.Resources["WhiteColorBrush"] as Brush })
                                           .ToList();
            this.BrushResources = FindBrushResources();
            this.Theme = this.AppThemes.Find(x => x.Name.Equals("BaseDark"));
            this.AccentColor = this.AccentColors.Find(x => x.Name.Equals("Blue"));
        }

        #endregion

        #region  方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ResourceDictionary FindResourceDictionary(string path)
        {
            var rd = new ResourceDictionary
            {
                Source = new Uri(path, UriKind.RelativeOrAbsolute)
            };

            return rd;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> FindBrushResources()
        {
            var rd = new ResourceDictionary
            {
                Source = new Uri(@"/MahApps.Metro;component/Styles/Colors.xaml", UriKind.RelativeOrAbsolute)
            };

            var resources = rd.Keys.Cast<object>()
                    .Where(key => rd[key] is Brush)
                    .Select(key => key.ToString())
                    .OrderBy(s => s)
                    .ToList();

            return resources;
        }

        #endregion
    }
}
