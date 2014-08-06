using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Un4seen.Bass;
using WPFSoundVisualizationLib;

namespace XIMALAYA.PCDesktop.Tools.Player
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BassEngine : NotificationObject, ISpectrumPlayer, IDisposable
    {
        #region 字段

        private int _ActiveStreamHandle = 0;
        private bool _disposed;
        private string _CurrentSoundUrl = string.Empty;
        private TimeSpan _TotalTime = TimeSpan.Zero;
        private TimeSpan _CurrentTime = TimeSpan.Zero;
        private int _SampleFrequency = 44100;
        private bool _CanPlay = false;
        private bool _CanPause = false;
        private bool _IsPlaying = false;
        private float _Volume = 0.5F;
        private bool _IsMuted = false;
        private readonly int maxFFT = (int)(Un4seen.Bass.BASSData.BASS_DATA_AVAILABLE | Un4seen.Bass.BASSData.BASS_DATA_FFT4096);
        private float _Process = 0F;

        #endregion

        #region 属性

        /// <summary>
        /// 当前流的句柄，声音播放句柄
        /// </summary>
        private int ActiveStreamHandle
        {
            get
            {
                return _ActiveStreamHandle;
            }
            set
            {
                if (value != _ActiveStreamHandle)
                {
                    _ActiveStreamHandle = value;
                    this.RaisePropertyChanged(() => this.ActiveStreamHandle);
                }
            }
        }
        /// <summary>
        /// 播放完毕回调
        /// </summary>
        private SYNCPROC EndTrackSyncProc { get; set; }
        /// <summary>
        /// 下载回调
        /// </summary>
        private DOWNLOADPROC DownloadProc { get; set; }
        /// <summary>
        /// 下载网络地址声音 线程
        /// </summary>
        private Thread OnlineFileWorker { get; set; }
        /// <summary>
        /// 代理
        /// </summary>
        private IntPtr ProxyHandle { get; set; }
        /// <summary>
        /// 属性描述
        /// </summary>
        private int SampleFrequency
        {
            get
            {
                return _SampleFrequency;
            }
            set
            {
                if (value != _SampleFrequency)
                {
                    _SampleFrequency = value;
                    this.RaisePropertyChanged(() => this.SampleFrequency);
                }
            }
        }
        /// <summary>
        /// 当前播放位置获取
        /// </summary>
        private DispatcherTimer CurrentPositionTimer { get; set; }
        /// <summary>
        /// 当前播放时长是否更新
        /// </summary>
        private bool IsCurrnetPositionUpdate { get; set; }
        /// <summary>
        /// 文件总长度
        /// </summary>
        private float TotalSize { get; set; }
        /// <summary>
        /// 当前播放时长是否更新完毕
        /// </summary>
        private bool IsCurrentPositionUpdated { get; set; }
        /// <summary>
        /// 当前播放音乐的地址
        /// </summary>
        public string CurrentSoundUrl
        {
            get
            {
                return _CurrentSoundUrl;
            }
            set
            {
                if (value != _CurrentSoundUrl)
                {
                    _CurrentSoundUrl = value;
                    this.RaisePropertyChanged(() => this.CurrentSoundUrl);
                }
            }
        }
        /// <summary>
        /// 声音总时长
        /// </summary>
        public TimeSpan TotalTime
        {
            get
            {
                return _TotalTime;
            }
            set
            {
                if (value != _TotalTime)
                {
                    _TotalTime = value;
                    this.RaisePropertyChanged(() => this.TotalTime);
                }
            }
        }
        /// <summary>
        /// 当前播放位置
        /// </summary>
        public TimeSpan CurrentTime
        {
            get
            {
                return _CurrentTime;
            }
            set
            {
                if (value != _CurrentTime)
                {
                    //_CurrentTime = value;
                    this.ChangeCurrentTime(value);
                    this.RaisePropertyChanged(() => this.CurrentTime);
                }
            }
        }
        /// <summary>
        /// 是否可以播放声音
        /// </summary>
        public bool CanPlay
        {
            get
            {
                return _CanPlay;
            }
            set
            {
                if (value != _CanPlay)
                {
                    _CanPlay = value;
                    this.RaisePropertyChanged(() => this.CanPlay);
                    this.PlayCommand.RaiseCanExecuteChanged();
                }
            }
        }
        /// <summary>
        /// 是否可以暂停
        /// </summary>
        public bool CanPause
        {
            get
            {
                return _CanPause;
            }
            set
            {
                if (value != _CanPause)
                {
                    _CanPause = value;
                    this.RaisePropertyChanged(() => this.CanPause);
                    this.PlayCommand.RaiseCanExecuteChanged();
                }
            }
        }
        /// <summary>
        /// 是否正在播放
        /// </summary>
        public bool IsPlaying
        {
            get
            {
                return _IsPlaying;
            }
            private set
            {
                if (value != _IsPlaying)
                {
                    _IsPlaying = value;
                    this.CurrentPositionTimer.IsEnabled = value;
                    this.RaisePropertyChanged(() => this.IsPlaying);
                    this.PlayCommand.RaiseCanExecuteChanged();
                }
            }
        }
        /// <summary>
        /// 音量
        /// </summary>
        public float Volume
        {
            get
            {
                return _Volume;
            }
            set
            {
                if (value != _Volume)
                {
                    _Volume = value;
                    this.SetVolume();
                    this.RaisePropertyChanged(() => this.Volume);
                }
            }
        }
        /// <summary>
        /// 是否静音
        /// </summary>
        public bool IsMuted
        {
            get
            {
                return _IsMuted;
            }
            set
            {
                if (value != _IsMuted)
                {
                    _IsMuted = value;
                    this.SetVolume();
                    this.RaisePropertyChanged(() => this.IsMuted);
                }
            }
        }
        /// <summary>
        /// 下载进度
        /// </summary>
        public float Process
        {
            get
            {
                return _Process;
            }
            set
            {
                if (value != _Process)
                {
                    _Process = value;
                    this.RaisePropertyChanged(() => this.Process);
                }
            }
        }
        /// <summary>
        /// 第一次加载完数据后，自动播放
        /// </summary>
        public bool IsAutoPlayed { get; set; }
        private long _trackID;
        /// <summary>
        /// 当前播放的声音ID
        /// </summary>
        public long TrackID
        {
            get { return _trackID; }
            set
            {
                if (_trackID != value)
                {
                    _trackID = value;
                    this.RaisePropertyChanged(() => this.TrackID);
                }
            }
        }


        #endregion

        #region command

        /// <summary>
        /// 播放命令
        /// </summary>
        public DelegateCommand PlayCommand { get; set; }
        private void PlayCommandAction()
        {
            if (this.CanPlay)
            {
                this.Play();
            }
            else if (this.CanPause)
            {
                this.Pause();
            }
        }
        private bool CanPlayCommand()
        {
            if (this.IsPlaying)
            {
                return this.CanPause;
            }
            else
            {
                return this.CanPlay;
            }
        }

        #endregion

        #region 播放逻辑

        /// <summary>
        /// 
        /// </summary>
        public void Play()
        {
            if (this.CanPlay)
            {
                this.PlayCurrentStream();
                this.IsPlaying = this.CanPause = true;
                this.CanPlay = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Pause()
        {
            if (this.IsPlaying && this.CanPause)
            {
                Bass.BASS_ChannelPause(this.ActiveStreamHandle);
                this.IsPlaying = false;
                this.CanPlay = true;
                this.CanPause = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            if (this.CanPause)
            {
                this.CurrentTime = TimeSpan.Zero;
                this.CanPause = this.IsPlaying = false;
                this.CanPlay = true;
                if (this.ActiveStreamHandle != 0)
                {
                    Bass.BASS_ChannelStop(this.ActiveStreamHandle);
                    Bass.BASS_ChannelSetPosition(this.ActiveStreamHandle, this.CurrentTime.TotalSeconds);
                }
            }
            this.FreeCurrentStream();
        }
        /// <summary>
        /// 打开网络播放地址
        /// </summary>
        /// <param name="url"></param>
        public void OpenUrlAsync(string url)
        {
            int handle = 0, syncHandle;
            BASS_CHANNELINFO info;

            this.SetSoundPath(url);
            this.OnlineFileWorker = new Thread(() =>
            {
                handle = Bass.BASS_StreamCreateURL(this.CurrentSoundUrl, 0, BASSFlag.BASS_DEFAULT, this.DownloadProc, IntPtr.Zero);
                this.ActiveStreamHandle = handle;
                if (handle == 0)
                {
                    Debug.WriteLine("打开声音错误！", DateTime.Now);
                    return;
                }
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (this.CurrentSoundUrl == url)
                    {
                        this.IsAutoPlayed = false;
                        this.ActiveStreamHandle = handle;
                        this.TotalTime = TimeSpan.FromSeconds(Bass.BASS_ChannelBytes2Seconds(this.ActiveStreamHandle, Bass.BASS_ChannelGetLength(ActiveStreamHandle, 0)));
                        info = Bass.BASS_ChannelGetInfo(this.ActiveStreamHandle);
                        this.SampleFrequency = info.freq;
                        syncHandle = Bass.BASS_ChannelSetSync(this.ActiveStreamHandle,
                             BASSSync.BASS_SYNC_END,
                             0,
                             this.EndTrackSyncProc,
                             IntPtr.Zero);
                        if (syncHandle == 0)
                        {
                            throw new ArgumentException("Error establishing End Sync on file stream.", "path");
                        }
                        this.CanPlay = true;
                    }
                    else
                    {
                        Debug.WriteLine("播放地址已更改", DateTime.Now);
                    }
                }));
                this.OnlineFileWorker.Abort();
                this.OnlineFileWorker = null;
            });
            this.OnlineFileWorker.IsBackground = true;
            this.OnlineFileWorker.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public void OpenFile(string filename)
        {
            this.CurrentSoundUrl = filename;
        }
        /// <summary>
        /// 设置播放路径
        /// </summary>
        /// <param name="path"></param>
        private void SetSoundPath(string path)
        {
            this.Stop();
            this.CurrentSoundUrl = path;
            this.Process = 0;
            this.CurrentTime = TimeSpan.Zero;
        }
        private void ChangeCurrentTime(TimeSpan value)
        {
            if (!this.IsCurrentPositionUpdated)
            {
                this.IsCurrentPositionUpdated = true;
                if (value > this.TotalTime)
                {
                    value = this.TotalTime;
                }
                else if (value < TimeSpan.Zero)
                {
                    value = TimeSpan.Zero;
                }
                //定时器更新
                if (!this.IsCurrnetPositionUpdate)
                {
                    Bass.BASS_ChannelSetPosition(this.ActiveStreamHandle, Bass.BASS_ChannelSeconds2Bytes(ActiveStreamHandle, value.TotalSeconds));
                }
                this._CurrentTime = value;
                this.IsCurrentPositionUpdated = false;
            }
        }
        private void SetVolume()
        {
            if (ActiveStreamHandle != 0)
            {
                if (IsMuted)
                {
                    Bass.BASS_ChannelSetAttribute(ActiveStreamHandle, BASSAttribute.BASS_ATTRIB_VOL, 0);
                }
                else
                {
                    Bass.BASS_ChannelSetAttribute(ActiveStreamHandle, BASSAttribute.BASS_ATTRIB_VOL, this.Volume);
                }
            }
        }

        #endregion

        #region 流操作

        /// <summary>
        /// 播放当前流
        /// </summary>
        private void PlayCurrentStream()
        {
            if (this.ActiveStreamHandle != 0 && Bass.BASS_ChannelPlay(this.ActiveStreamHandle, false))
            {
                BASS_CHANNELINFO info = new Un4seen.Bass.BASS_CHANNELINFO();
                Bass.BASS_ChannelGetInfo(this.ActiveStreamHandle, info);
            }
#if DEBUG
            else
            {

                Debug.WriteLine("Error={0}", Un4seen.Bass.Bass.BASS_ErrorGetCode());

            }
#endif
        }
        /// <summary>
        /// 释放当前流
        /// </summary>
        private void FreeCurrentStream()
        {
            if (this.OnlineFileWorker != null)
            {
                this.OnlineFileWorker.Abort();
                this.OnlineFileWorker = null;
            }

            if (this.ActiveStreamHandle != 0)
            {
                if (!Bass.BASS_StreamFree(this.ActiveStreamHandle))
                {
                    Debug.WriteLine("BASS_StreamFree失败：" + Un4seen.Bass.Bass.BASS_ErrorGetCode());
                }
                this.ActiveStreamHandle = 0;
            }
        }

        #endregion

        #region Constructor

        private BassEngine()
        {
            Un4seen.Bass.BassNet.Registration("yk000123@sina.com", "2X34201017282922");

            //判断当前系统是32位系统还是64位系统，并加载对应版本的bass.dll
            string targetPath;
            if (Un4seen.Bass.Utils.Is64Bit)
                targetPath = Path.Combine(Path.GetDirectoryName(typeof(BassEngine).Assembly.GetModules()[0].FullyQualifiedName), "x64");
            else
                targetPath = Path.Combine(Path.GetDirectoryName(typeof(BassEngine).Assembly.GetModules()[0].FullyQualifiedName), "x86");

            // now load all libs manually
            Un4seen.Bass.Bass.LoadMe(targetPath);

            float down;
            Window mainWindow = Application.Current.MainWindow;
            WindowInteropHelper interopHelper = new WindowInteropHelper(mainWindow);

            if (!Un4seen.Bass.Bass.BASS_Init(-1, 44100, Un4seen.Bass.BASSInit.BASS_DEVICE_DEFAULT, interopHelper.Handle))
            {
                throw new Exception("Bass initialization error : " + Un4seen.Bass.Bass.BASS_ErrorGetCode());
            }

            Un4seen.Bass.Bass.BASS_SetConfig(Un4seen.Bass.BASSConfig.BASS_CONFIG_NET_TIMEOUT, 15000);

            this.EndTrackSyncProc = this.EndTrack;
            this.DownloadProc = new DOWNLOADPROC((a, b, c) =>
            {
                if (this.TotalSize <= 0)
                {
                    this.TotalSize = Bass.BASS_StreamGetFilePosition(this.ActiveStreamHandle, BASSStreamFilePosition.BASS_FILEPOS_END);
                }
                if (this.TotalSize > 0)
                {
                    down = Bass.BASS_StreamGetFilePosition(this.ActiveStreamHandle, BASSStreamFilePosition.BASS_FILEPOS_DOWNLOAD);
                    this.Process = down / this.TotalSize;
                    Debug.WriteLine(down, this.TotalSize.ToString());
                    if (this.Process > 0 && !this.IsAutoPlayed)
                    {
                        this.IsAutoPlayed = true;
                        this.Play();
                    }
                }
            });
            this.CurrentPositionTimer = new DispatcherTimer();
            this.CurrentPositionTimer.Interval = TimeSpan.FromMilliseconds(50);
            this.CurrentPositionTimer.Tick += CurrentPositionTimer_Tick;

            this.PlayCommand = new DelegateCommand(this.PlayCommandAction, this.CanPlayCommand);
        }

        void CurrentPositionTimer_Tick(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (this.ActiveStreamHandle == 0)
                {
                    this.CurrentTime = TimeSpan.Zero;
                }
                else
                {
                    this.IsCurrnetPositionUpdate = true;
                    this.CurrentTime = TimeSpan.FromSeconds(Bass.BASS_ChannelBytes2Seconds(ActiveStreamHandle, Bass.BASS_ChannelGetPosition(ActiveStreamHandle, 0)));
                    this.IsCurrnetPositionUpdate = false;
                }
            }));
        }

        #endregion

        #region Destructor
        /// <summary>
        /// 
        /// </summary>
        ~BassEngine()
        {
            Dispose();
        }
        #endregion

        #region Bass Callbacks
        /// <summary>
        /// 播放完毕
        /// </summary>
        private void EndTrack(int handle, int channel, int data, IntPtr user)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                //this.Pause();
                this.Stop();
            }));
        }
        #endregion

        #region ISpectrumPlayer方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fftDataBuffer"></param>
        /// <returns></returns>
        public bool GetFFTData(float[] fftDataBuffer)
        {
            return (Un4seen.Bass.Bass.BASS_ChannelGetData(ActiveStreamHandle, fftDataBuffer, maxFFT)) > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public int GetFFTFrequencyIndex(int frequency)
        {
            return Un4seen.Bass.Utils.FFTFrequency2Index(frequency, 4096, this.SampleFrequency);
        }

        #endregion

        #region IDisposable方法
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (this.OnlineFileWorker != null)
                    {
                        this.OnlineFileWorker.Abort();
                        this.OnlineFileWorker = null;
                    }
                }
                Bass.BASS_Free();
                Bass.FreeMe();

                if (this.ProxyHandle != IntPtr.Zero)
                    Marshal.FreeHGlobal(this.ProxyHandle);
                this.ProxyHandle = IntPtr.Zero;

                _disposed = true;
            }
        }
        #endregion
    }
}
