using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Practices.Prism.ViewModel;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Tools.Player;

namespace XIMALAYA.PCDesktop.Tools
{
    public class GlobalData : NotificationObject
    {
        #region 字段

        private ItemCollection _SoundCollection;
        private SoundData _SoundData;
        private Color _CurrentSoundCoverColor;
        private bool _IsWindowShow = true;
        private bool _IsSettingFlyoutShow = false;
        private bool _IsShowListView;

        #endregion

        #region 属性

        /// <summary>
        /// 全局播放
        /// </summary>
        public BassEngine BassEngine
        {
            get
            {
                return PlayerSingleton.Instance;
            }
        }
        /// <summary>
        /// 当前点击播放的声音行
        /// </summary>
        public Control CurrentPlayControl { get; set; }
        /// <summary>
        /// 当前声音播放列表
        /// </summary>
        public ItemCollection SoundCollection
        {
            get
            {
                return _SoundCollection;
            }
            set
            {
                if (value != _SoundCollection)
                {
                    _SoundCollection = value;
                    this.RaisePropertyChanged(() => this.SoundCollection);
                }
            }
        }
        /// <summary>
        /// 当前播放的声音
        /// </summary>
        public SoundData SoundData
        {
            get
            {
                return _SoundData;
            }
            set
            {
                if (value != _SoundData)
                {
                    _SoundData = value;
                    this.RaisePropertyChanged(() => this.SoundData);
                }
            }
        }
        /// <summary>
        /// 当前声音的封面图颜色
        /// </summary>
        public Color CurrentSoundCoverColor
        {
            get
            {
                return _CurrentSoundCoverColor;
            }
            set
            {
                if (value != _CurrentSoundCoverColor)
                {
                    _CurrentSoundCoverColor = value;
                    this.RaisePropertyChanged(() => this.CurrentSoundCoverColor);
                }
            }
        }
        /// <summary>
        /// 窗体是否为显示状态
        /// </summary>
        public bool IsWindowShow
        {
            get
            {
                return _IsWindowShow;
            }
            set
            {
                if (value != _IsWindowShow)
                {
                    _IsWindowShow = value;
                    this.RaisePropertyChanged(() => this.IsWindowShow);
                }
            }
        }
        /// <summary>
        /// 显示设置flyout
        /// </summary>
        public bool IsSettingFlyoutShow
        {
            get
            {
                return _IsSettingFlyoutShow;
            }
            set
            {
                if (value != _IsSettingFlyoutShow)
                {
                    _IsSettingFlyoutShow = value;
                    this.RaisePropertyChanged(() => this.IsSettingFlyoutShow);
                }
            }
        }
        /// <summary>
        /// 显示当前播放列表
        /// </summary>
        public bool IsShowListView
        {
            get
            {
                return _IsShowListView;
            }
            set
            {
                if (value != _IsShowListView)
                {
                    _IsShowListView = value;
                    this.RaisePropertyChanged(() => this.IsShowListView);
                }
            }
        }

        #endregion

        #region 构造

        private GlobalData()
        {
            this.SoundData = new SoundData
            {
                Title = "喜马拉雅，听我想听"
            };
            this.SoundCollection = new ListBox().Items;
            this.CurrentSoundCoverColor = Colors.Black;
        }

        #endregion
    }
}
