﻿using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Practices.Prism.ViewModel;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Tools.Player;

namespace XIMALAYA.PCDesktop.Tools
{
    public class GlobalData : NotificationObject, IDisposable
    {
        #region 字段

        private ItemCollection _SoundCollection;
        private SoundData _SoundData;
        private Color _CurrentSoundCoverColor;
        private bool _IsWindowShow = true;
        private bool _IsSettingFlyoutShow = false;
        private bool _IsShowListView;
        private bool _IsAutoStart;
        private bool _IsActualExit;
        private bool _IsExitConfirm;
        private bool? _PlayMode;

        #endregion

        #region 属性

        /// <summary>
        /// 指令队列（用于win7中jumplist）
        /// </summary>
        public Queue<string> ArgumentList { get; set; }
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
        private Thread thread { get; set; }
        /// <summary>
        /// 内存映射文件的文件名
        /// </summary>
        public string MappingFileID { get; set; }
        /// <summary>
        /// 内存映射文件的文件
        /// </summary>
        public MemoryMappedFile MemoryMappedFile { get; set; }
        /// <summary>
        /// 启动路径
        /// </summary>
        public string ExeFileLocation { get; set; }
        /// <summary>
        /// 开机自动启动
        /// </summary>
        public bool IsAutoStart
        {
            get
            {
                return _IsAutoStart;
            }
            set
            {
                if (value != _IsAutoStart)
                {
                    _IsAutoStart = value;
                    //Functions.SetAutoRun(this.ExeFileLocation, _IsAutoStart);
                    this.RaisePropertyChanged(() => this.IsAutoStart);
                }
            }
        }
        /// <summary>
        /// 关闭是否最小化
        /// </summary>
        public bool IsActualExit
        {
            get
            {
                return _IsActualExit;
            }
            set
            {
                if (value != _IsActualExit)
                {
                    _IsActualExit = value;
                    this.RaisePropertyChanged(() => this.IsActualExit);
                }
            }
        }
        /// <summary>
        /// 退出时是否确认
        /// </summary>
        public bool IsExitConfirm
        {
            get
            {
                return _IsExitConfirm;
            }
            set
            {
                if (value != _IsExitConfirm)
                {
                    _IsExitConfirm = value;
                    this.RaisePropertyChanged(() => this.IsExitConfirm);
                }
            }
        }
        /// <summary>
        /// 是否执行退出命令
        /// </summary>
        public bool IsExit { get; set; }
        /// <summary>
        /// 系统的标题
        /// </summary>
        public string SystemTitle
        {
            get
            {
                return "喜马拉雅-听上瘾！听过瘾！";
            }
        }
        /// <summary>
        /// 播放模式
        /// </summary>
        public bool? PlayMode
        {
            get
            {
                return _PlayMode;
            }
            set
            {
                if (value != _PlayMode)
                {
                    _PlayMode = value;
                    this.RaisePropertyChanged(() => this.PlayMode);
                    CommandSingleton.Instance.NextCommand.RaiseCanExecuteChanged();
                    CommandSingleton.Instance.PrevCommand.RaiseCanExecuteChanged();
                }
            }
        }
        /// <summary>
        /// 当前版本
        /// </summary>
        public string Version
        {
            get
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                }
                return string.Empty + "1.0.0.1";
            }
        }
        /// <summary>
        /// 是否已经提示过托盘
        /// </summary>
        public bool IsTrip { get; set; }

        #endregion

        #region 构造

        private GlobalData()
        {
            this.SoundData = new SoundData
            {
                Title = this.SystemTitle
            };
            this.SoundCollection = new ListBox().Items;
            this.CurrentSoundCoverColor = Colors.Black;
            this.ArgumentList = new Queue<string>();
            this.MappingFileID = "{04EFCEB4-F10A-403D-9824-1E685C4B7962}";
            this.MemoryMappedFile = MemoryMappedFile.CreateOrOpen(this.MappingFileID, 1024 * 1024, MemoryMappedFileAccess.ReadWrite);
        }

        #endregion

        #region IDisposable 成员

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            this.SoundCollection.Clear();
            this.SoundCollection = null;
            this.ArgumentList.Clear();
            this.ArgumentList = null;
        }

        #endregion
    }
}
